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


        public ProductDAL(IConfiguration configuration,ShoppingWebsiteContext db)
        {
            this._configuration = configuration;
            this._db = db;
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

            string sp = "Sp_GetProductByCategory";

            DynamicParameters parameters = new();

            parameters.Add("@CategoryId", id);

            return await Task.FromResult(dbConnection.Query<ProductAdmin>(sp, parameters, commandType: CommandType.StoredProcedure).ToList());

        }
    }
}
