using Microsoft.Extensions.Configuration;
using ShoppingWebsiteBL.IProductBL;
using ShoppingWebsiteBL.ViewModel;
using ShoppingWebsiteDA.Access;
using ShoppingWebsiteDA.Interface;
using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteBL
{
    public class ProductBL : IProductsBL
    {
        private readonly IProductRepository _product;
        private readonly IConfiguration _config;

        public ProductBL(IProductRepository product, IConfiguration configuration)
        {
            this._product = product;
            this._config = configuration;
        }

        public async Task<IEnumerable<ProductAdmin>> GetAllProducts()
        {
            var res = await _product.GetAllProducts();
            return res;
        }

        public void AddProduct(ProductVM product)
        {
            CreateProduct products = new();

            products.ProductName = product.ProductName;
            products.CategoryId = product.CategoryId;
            products.BrandId = product.BrandId;
            products.Price = product.Price;
            products.Discount = product.Discount;
            products.Description = product.Description;
            products.Stock = product.Stock;

            _product.AddProduct(products);
        }

        public async Task<IEnumerable<ProductUser>> GetAllProductForUser()
        {
            var res = await _product.GetAllProductsForUser();
            return res;
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategory()
        {
            var res = await _product.GetAllCategories();
            return res;
        }

        public async Task<IEnumerable<BrandVM>> GetAllBrands()
        {
            var res = await _product.GetAllBrands();
            return res;
        }

        public async Task<IEnumerable<ProductAdmin>> GetProductByCategory(int id)
        {
            var res = await _product.GetProductByCategory(id);
            return res;
        }

        public void AddImage(ImageVM image)
        {
            _product.AddImage(image);
        }

        public async Task<GetImageVM> GetProductImage(int id)
        {
            var res = await _product.GetProductImage(id);
            return res;
        }
    }
}
