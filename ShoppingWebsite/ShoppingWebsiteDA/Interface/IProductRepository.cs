using ShoppingWebsiteDA.Model;
using ShoppingWebsiteDA.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingWebsiteDA.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductAdmin>> GetAllProducts();

        void AddProduct(CreateProduct add);

        Task<IEnumerable<ProductUser>> GetAllProductsForUser();

        Task<IEnumerable<CategoryVM>> GetAllCategories();

        Task<IEnumerable<BrandVM>> GetAllBrands();

        Task<IEnumerable<ProductAdmin>> GetProductByCategory(int id);

        void AddImage(ImageVM image);

        Task<GetImageVM> GetProductImage(int ProductId);

    }
}
