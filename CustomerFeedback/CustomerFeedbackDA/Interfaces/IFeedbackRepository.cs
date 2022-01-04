using CustomerFeedbackDA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFeedbackDA.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<FeedbackResutViewModel> addFeedback(FeedbackViewModel feedback);
        Task<IEnumerable<CategoryViewModel>> GetCategory();
        Task<IEnumerable<ProductViewModel>> GetProductsByCategory(int id);
        Task<IEnumerable<ReviewViewModel>> GetReviews();
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<GetFeedbackViewModel> GetFeedback(string encryptedText);
    }
}
