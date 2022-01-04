using CustomerFeedbackBL.Interfaces;
using CustomerFeedbackBL.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;
using System.Net.Http;
using System.Web.Http;

namespace CustomerFeedback.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackBLRepo _feedback;
        private readonly IWebHostEnvironment _hosting;
        private readonly IConfiguration _config;

        public FeedbackController(IFeedbackBLRepo feedback, IWebHostEnvironment hosting, IConfiguration config)
        {
            this._feedback = feedback;
            this._hosting = hosting;
            this._config = config;
        }

        [HttpPost]
        public void CreateFeedback([FromForm] FeedbackVM feedbacks)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var fileUpload = HttpContext.Request.Form.Files["fileUpload"];

                    FeedbackVM feedback = new FeedbackVM
                    {
                        FileUpload = fileUpload,
                        LastPurchasedItem = feedbacks.LastPurchasedItem,
                        Title = feedbacks.Title,
                        FirstName = feedbacks.FirstName,
                        Initial = feedbacks.Initial,
                        Email = feedbacks.Email,
                        StreetAddress = feedbacks.StreetAddress,
                        StreetAddress2 = feedbacks.StreetAddress2,
                        City = feedbacks.City,
                        Region = feedbacks.Region,
                        PostalCode = feedbacks.PostalCode,
                        Country = feedbacks.Country,
                        ProductId = feedbacks.ProductId,
                        ReviewId = feedbacks.ReviewId,
                        ProductFeedback = feedbacks.ProductFeedback,
                        ReasonForUnSatisfaction = feedbacks.ReasonForUnSatisfaction
                    };

                 _feedback.addFeedback(feedback);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetFeedback(string encryptedText)
        {
            try
            {
                var feedback = await _feedback.GetFeedback(encryptedText);
                return Ok(feedback);
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: 400, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            try
            {
                var category = await _feedback.GetCategory();
                return Ok(category);
            }
            catch (Exception e)
            {
                HttpResponseMessage responseMessage = new HttpResponseMessage();
                responseMessage.StatusCode = HttpStatusCode.BadRequest;
                responseMessage.ReasonPhrase = “Student not found”;
                responseMessage.Content = new StringContent(“No student exists against provided student id”);
                throw new HttpResponseException(responseMessage);

                return StatusCode(statusCode: 400, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            try
            {
                if (id == default)
                {
                    var res = await _feedback.GetAllroducts();
                    return Ok(res);
                }

                else
                {
                    var products = await _feedback.GetProductsByCategory(id);
                    return Ok(products);
                }
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: 400, e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            try
            {
                var reviews = await _feedback.GetReviews();
                return Ok(reviews);
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: 400, e.Message);
            }
        }

    }
}
