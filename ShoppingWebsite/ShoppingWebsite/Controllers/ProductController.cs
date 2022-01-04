using AutoMapper;
using CustomIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using System.Security.Claims;
using System.Text;
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
        private readonly UserManager<ApplicationUser> _user;

        public ProductController(UserManager<ApplicationUser> user,IProductsBL product, IWebHostEnvironment hosting, IMapper mapper, IConfiguration configuration)
        {
            this._product = product;
            this._hosting = hosting;
            this._mapper = mapper;
            this._config = configuration;
            this._user = user;
        }

        //Get products
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> GetAllProducts()
        {

            try
            {
                var currentUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
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
        [Authorize(Roles = "USER")]
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
        public async Task<IActionResult> GetProductByCategory(int id)
        {
            if (id == default)
            {
                var listOfProducts = await _product.GetAllProductForUser();
                return Ok(listOfProducts);
            }

            else
            {
                var result = await _product.GetProductByCategory(id);
                return Ok(result);
            }
        }

        [HttpPost]
        //Create image
        public void CreateImage([FromForm] ImageVM image)
        {
            try
            {
                var productIds = HttpContext.Request.Form["ProductId"];
                int id = int.Parse(productIds);
                var fileName = HttpContext.Request.Form["FileName"];

                string uploadPath = Path.Combine(_hosting.ContentRootPath, "Images").ToString();
                string storeImage = Path.Combine(_hosting.ContentRootPath, "UploadedImage").ToString();

                var filePath = Path.Combine(uploadPath, fileName);
                var storeImagePath = Path.Combine(storeImage, fileName);

                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    ImageVM images = new ImageVM
                    {
                        ProductId = id,
                        FileName = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name)),
                    };

                    images.FileName.CopyTo(new FileStream(storeImagePath, FileMode.Create));
                    _product.AddImage(images);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        [Route("{ProductId}")]
        [HttpGet]
        //get image for products
        public async Task<IActionResult> GetProductImage(int ProductId)
        {
            try
            {
                var productImage = await _product.GetProductImage(ProductId);
                
                string filePath = Path.Combine(_config.GetSection("Image").GetSection("Path").Value.ToString(), productImage.ImageName);

                if (filePath != null)
                {
                    if (System.IO.File.Exists(filePath))
                    {
                        byte[] b = System.IO.File.ReadAllBytes(filePath);
                        return File(b, "image/png");
                    }
                }
                return null;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
