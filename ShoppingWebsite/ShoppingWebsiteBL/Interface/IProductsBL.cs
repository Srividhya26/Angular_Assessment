using Microsoft.AspNetCore.Http;
using ShoppingWebsiteBL.ViewModel;
using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteBL.IProductBL
{
    public interface IProductsBL
    {
        Task<IEnumerable<ProductAdmin>> GetAllProducts();

        void AddProduct(ProductVM add);

        Task<IEnumerable<ProductUser>> GetAllProductForUser();

        Task<IEnumerable<CategoryVM>> GetAllCategory();
        Task<IEnumerable<BrandVM>> GetAllBrands();

        Task<IEnumerable<ProductAdmin>> GetProductByCategory(int id);

        void AddImage(ImageVM image);

        Task<GetImageVM> GetProductImage(int ProductId);
    }
}
