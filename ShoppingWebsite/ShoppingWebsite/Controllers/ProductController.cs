using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShoppingWebsiteBL;
using ShoppingWebsiteBL.IProductBL;
using ShoppingWebsiteBL.ViewModel;
using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingWebsite.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductsBL _product;
        private readonly IWebHostEnvironment _hosting;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public ProductController(IProductsBL product, IWebHostEnvironment hosting, IMapper mapper, IConfiguration configuration)
        {
            this._product = product;
            this._hosting = hosting;
            this._mapper = mapper;
            this._config = configuration;
        }

        //Get products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var listOfProducts = await _product.GetAllProducts();
                return Ok(listOfProducts);
            }
            catch (Exception e)
            {
                return StatusCode(statusCode: 500, e.Message);
            }
        }

        //Get products for user
        [HttpGet]
        public async Task<IActionResult> GetAllProductForUser()
        {
            try
            {
                var listOfProducts = await _product.GetAllProductForUser();
                return Ok(listOfProducts);
            }

            catch (Exception e)
            {
                return StatusCode(statusCode: 500, e.Message);
            }
        }

        //get all categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var listOfCategories = await _product.GetAllCategory();
                return Ok(listOfCategories);
            }

            catch (Exception e)
            {
                return StatusCode(statusCode: 500, e.Message);
            }
        }

        //get all brands
        [HttpGet]
        public async Task<IActionResult> GetAllBrands()
        {
            try
            {
                var listOfBrands = await _product.GetAllBrands();
                return Ok(listOfBrands);
            }

            catch (Exception e)
            {
                return StatusCode(statusCode: 500, e.Message);
            }
        }

        //Create product
        [HttpPost]
        public void CreateProduct([FromBody] ProductVM product)
        {
            try
            {
                _product.AddProduct(product);
                
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //get product based on category
        [HttpGet]
        public void GetProductByCategory(int id)
        {
            try
            {
                _product.GetProductByCategory(id);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //[HttpPost]
        ////Create image
        //public void CreateImage([FromBody] ImageVM image)
        //{
        //    try
        //    {
                  
        //    }
        //    catch
        //    {

        //    }
        //}


        //for uploading the image
        [HttpPost]
        public string UploadFile([FromBody] ImageVM image)
        {
            //String timeStamp = GetTimestamp(DateTime.Now);
            string fileName = null;
            if (image.ImageName != null)
            {
                string uploadPhoto = Path.Combine(_hosting.WebRootPath, "images");
                fileName = image.ImageName.FileName;

                string filePath = Path.Combine(uploadPhoto, fileName);
                image.ImageName.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return fileName;
        }

        //public static String GetTimestamp(DateTime value)
        //{
        //    return value.ToString("yyyyMMddHHmmssffff");
        //}

        
    }
}
