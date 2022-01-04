using CustomerFeedbackDA.Interfaces;
using CustomerFeedbackDA.ViewModel;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using NLog;

namespace CustomerFeedbackDA.Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hosting;
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public FeedbackRepository(IConfiguration configuration, IWebHostEnvironment hosting)
        {
            this._configuration = configuration;
            this._hosting = hosting;
        }
        public async Task<FeedbackResutViewModel> addFeedback(FeedbackViewModel feedback)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Add_Feedback";

            var ext = Path.GetExtension(feedback.FileUpload.FileName);
            var fileName = string.Concat(DateTime.UtcNow.ToString("yyyyMMddTHHss"), ext);
            var storeFilePath = Path.Combine(_configuration.GetSection("FileUpload:Path").Value.ToString(), fileName.ToString());

            if (feedback.FileUpload.Length > 0 && feedback.FileUpload.Length < 5e+6 && ext.ToLower() == ".pdf")
            {
                feedback.FileUpload.CopyTo(new FileStream(storeFilePath, FileMode.Create));
            }

            else
            {
                logger.Warn("This file type is not supported");
                throw new Exception("File type or file size is invalid");
            }

            DynamicParameters parameters = new();

            parameters.Add("Title", feedback.Title);
            parameters.Add("FirstName", feedback.FirstName);
            parameters.Add("Initial", feedback.Initial);
            parameters.Add("Email", feedback.Email);
            parameters.Add("StreetAddress", feedback.StreetAddress);
            parameters.Add("StreetAddress2", feedback.StreetAddress2);
            parameters.Add("City", feedback.City);
            parameters.Add("Region", feedback.Region);
            parameters.Add("PostalCode", feedback.PostalCode);
            parameters.Add("Country", feedback.Country);
            parameters.Add("ProductId", feedback.ProductId);
            parameters.Add("ReviewId", feedback.ReviewId);
            parameters.Add("LastPurchasedItem", feedback.LastPurchasedItem);
            parameters.Add("ProductFeedback", feedback.ProductFeedback);
            parameters.Add("ReasonForUnsatisfaction", feedback.ReasonForUnSatisfaction);
            parameters.Add("FileUpload", fileName);

            var listOfProducts = Task.FromResult(dbConnection.Query<FeedbackResutViewModel>(sp, parameters, commandType: CommandType.StoredProcedure).FirstOrDefault()).GetAwaiter().GetResult();

            string PlainText = listOfProducts.UserId + ";" + listOfProducts.FeedbackId;
            string SecretKey = _configuration.GetSection("Encryption:Key").Value.ToString();
            byte[] Key = Encoding.UTF8.GetBytes(SecretKey);
            byte[] IV = Encoding.UTF8.GetBytes(SecretKey).Take(16).ToArray();

            var encrypt = EncryptLink(PlainText,Key,IV);

            SentMail(feedback.Email, encrypt);

            return listOfProducts;
        }

        public string EncryptLink(string plainText, byte[] key, byte[] iV)
        {

            byte[] encrypted;
            string encryptedValue;

            using(Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = key;
                aes.IV = iV;

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using(MemoryStream ms = new MemoryStream())
                {
                    using(CryptoStream cs = new CryptoStream(ms,encryptor,CryptoStreamMode.Write))
                    {
                        using(StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.Write(plainText);
                        }

                        encrypted = ms.ToArray();
                    }
                }
            }

            encryptedValue = System.Web.HttpUtility.UrlEncode(Convert.ToBase64String(encrypted).ToString());
            return encryptedValue;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetProducts";

            var listOfProducts = await Task.FromResult(dbConnection.Query<ProductViewModel>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfProducts;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategory()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllCategory";

            var listOfCategory = await Task.FromResult(dbConnection.Query<CategoryViewModel>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfCategory;
        }

        public async Task<IEnumerable<ProductViewModel>> GetProductsByCategory(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllProducts";

            DynamicParameters parameters = new();

            parameters.Add("CategoryId", id);

            var listOfProducts = await Task.FromResult(dbConnection.Query<ProductViewModel>(sp, parameters, commandType: CommandType.StoredProcedure).ToList());

            return listOfProducts;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetReviews()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllReviews";

            var listOfReview = await Task.FromResult(dbConnection.Query<ReviewViewModel>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfReview;
        }

        public Task<GetFeedbackViewModel> GetFeedback(string encryptedText)
        {
            string SecretKey = _configuration.GetSection("Encryption:Key").Value.ToString();
            byte[] Key = Encoding.UTF8.GetBytes(SecretKey);
            byte[] IV = Encoding.UTF8.GetBytes(SecretKey).Take(16).ToArray();

            string decryptedValue = DecryptTheText(encryptedText,Key,IV);
            string[] separatedValues = decryptedValue.Split(";");

            var feedbackIds = separatedValues[1];
            var usersId = separatedValues[0];

            int feedbackId = int.Parse(feedbackIds);
            int userId = int.Parse(usersId);

            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetFeedbackDetails";

            DynamicParameters paramters = new();

            paramters.Add("feedbackId", feedbackId);
            paramters.Add("userId", userId);

            var feedback = Task.FromResult(dbConnection.Query<GetFeedbackViewModel>(sp, paramters, commandType: CommandType.StoredProcedure).FirstOrDefault());

            return feedback;
        }

        private static string DecryptTheText(string encryptedText,byte[] Key,byte[] IV)
        {
            byte[] decrypted;
            string decryptedValue;

            string decode = System.Web.HttpUtility.UrlDecode(encryptedText);
            decrypted = Convert.FromBase64String(decode);

            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = Key;
                aes.IV = IV;

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream(decrypted))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader reader = new StreamReader(cs))
                        {
                            decryptedValue = reader.ReadToEnd();
                        }
                    }
                }
            }

            return decryptedValue;
        }

        public void SentMail(string toValue,string urlLink)
        {
            MailAddress to = new MailAddress(toValue);
            MailAddress from = new MailAddress("srividhyatesting@gmail.com");
            //string urlValue = "https://localhost:44301/api/Feedback/GetFeedback?encryptedText=" + urlLink.ToString();
            string urlValue = "http://localhost:4200/Feedback/" + urlLink.ToString();

            MailMessage message = new MailMessage(from, to);
            message.Subject = "Feedback Copy";
            string body = urlValue;
            message.Body = body;
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("srividhyatesting@gmail.com", "Sri@2000"),
                EnableSsl = true,
                UseDefaultCredentials = false
            };

            client.Send(message);
        }
    }
}
