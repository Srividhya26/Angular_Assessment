using AutoMapper;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using ShoppingWebsiteDA.Data;
using ShoppingWebsiteDA.Interface;
using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace ShoppingWebsiteDA.Access
{
    public class ProductDAL : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly ShoppingWebsiteContext _db;
        private readonly IHostingEnvironment _hosting;


        public ProductDAL(IConfiguration configuration,ShoppingWebsiteContext db,IHostingEnvironment hosting)
        {
            this._configuration = configuration;
            this._db = db;
            this._hosting = hosting;
        }

        public void AddImage(ImageVM image)
        {
            AddImage images = this.GetProductIdForImage(image);
            FileInfo info = new(image.FileName.FileName);

            Image image1 = new();

            image1.ProductId = images.ProductId;
            image1.Version = images.Version;
            image1.ImageName = images.ImageUrl;
            image1.FileName = string.Concat(DateTime.UtcNow.ToString("yyyyMMddTHHss"), info.Extension);

            _db.Add(image1);

            _db.SaveChanges();           
        }

        public AddImage GetProductIdForImage(ImageVM image)
        {
            AddImage images = new();
            
            int version = 0;
            string fileName;
            string imageName;

            if(image.FileName != null)
            {
                version = _db.Images.Where(x => x.ProductId == image.ProductId).Count();
                version += 1;
               
                fileName = image.FileName.FileName;

                imageName = Path.Combine(_hosting.ContentRootPath, "UploadedImage", fileName);
                
                images.Version = version;
                images.ImageUrl = imageName;
                images.ProductId = image.ProductId;

                return images;
            }

            return images;
        }

        public void AddProduct(CreateProduct product)
        {
            Product products = new();

            products.Name = product.ProductName;
            products.CategoryId = product.CategoryId;
            products.BrandId = product.BrandId;
            products.Price = product.Price;
            products.Stock = product.Stock;
            products.Description = product.Description;
            products.Discount = product.Discount;

            _db.Add(products);
            _db.SaveChanges();
        }

        public async Task<IEnumerable<BrandVM>> GetAllBrands()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllBrands";

            var listOfCandidates = await Task.FromResult(dbConnection.Query<BrandVM>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfCandidates;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategories()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllCategories";

            var listOfCandidates = await Task.FromResult(dbConnection.Query<CategoryVM>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfCandidates;
        }

        public async Task<IEnumerable<ProductAdmin>> GetAllProducts()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllProducts";

            var listOfCandidates = await Task.FromResult(dbConnection.Query<ProductAdmin>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfCandidates;
        }

        public async Task<IEnumerable<ProductUser>> GetAllProductsForUser()
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "Sp_GetAllProductForUser";

            var listOfCandidates = await Task.FromResult(dbConnection.Query<ProductUser>(sp, commandType: CommandType.StoredProcedure).ToList());

            return listOfCandidates;
        }

        public async Task<IEnumerable<ProductAdmin>> GetProductByCategory(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "[dbo].[Sp_GetProductByCategory]";

            DynamicParameters parameters = new();

            parameters.Add("CategoryId", id);

            return await Task.FromResult(dbConnection.Query<ProductAdmin>(sp, parameters, commandType: CommandType.StoredProcedure).ToList());
        }

        public async Task<GetImageVM> GetProductImage(int ProductId)
        {
            using IDbConnection dbConnection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

            string sp = "[dbo].[Sp_GetProductImage]";

            DynamicParameters parameters = new();

            parameters.Add("ProductId", ProductId);

            return await Task.FromResult(dbConnection.Query<GetImageVM>(sp,parameters, commandType: CommandType.StoredProcedure).FirstOrDefault());
        }
    }
}
