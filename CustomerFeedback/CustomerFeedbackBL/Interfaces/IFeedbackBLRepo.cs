using CustomerFeedbackBL.ViewModels;
using CustomerFeedbackDA.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFeedbackBL.Interfaces
{
    public interface IFeedbackBLRepo
    {
        Task<FeedbackResutViewModel> addFeedback(FeedbackVM feedback);
        Task<IEnumerable<CategoryViewModel>> GetCategory();

        Task<IEnumerable<ProductViewModel>> GetProductsByCategory(int id);
        Task<IEnumerable<ReviewViewModel>> GetReviews();
        Task<IEnumerable<ProductViewModel>> GetAllroducts();
        Task<GetFeedbackViewModel> GetFeedback(string encryptedText);

    }
}
