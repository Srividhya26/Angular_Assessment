using CustomerFeedbackBL.Interfaces;
using CustomerFeedbackBL.ViewModels;
using CustomerFeedbackDA.Interfaces;
using CustomerFeedbackDA.ViewModel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerFeedbackBL.Logic
{
    public class FeedbackBLRepo : IFeedbackBLRepo
    {
        private readonly IFeedbackRepository _feedback;

        public FeedbackBLRepo(IFeedbackRepository feedback)
        {
            this._feedback = feedback;
        }

        public Task<FeedbackResutViewModel> addFeedback(FeedbackVM feedback)
        {
            FeedbackViewModel feedbacks = new();

            feedbacks.Title = feedback.Title;
            feedbacks.FirstName = feedback.FirstName;
            feedbacks.Initial = feedback.Initial;
            feedbacks.Email = feedback.Email;
            feedbacks.StreetAddress = feedback.StreetAddress;
            feedbacks.StreetAddress2 = feedback.StreetAddress2;
            feedbacks.City = feedback.City;
            feedbacks.Region = feedback.Region;
            feedbacks.PostalCode = feedback.PostalCode;
            feedbacks.Country = feedback.Country;
            feedbacks.ProductId = feedback.ProductId;
            feedbacks.ReviewId = feedback.ReviewId;
            feedbacks.LastPurchasedItem = feedback.LastPurchasedItem;
            feedbacks.ProductFeedback = feedback.ProductFeedback;
            feedbacks.ReasonForUnSatisfaction = feedback.ReasonForUnSatisfaction;
            feedbacks.FileUpload = feedback.FileUpload;

            var res = _feedback.addFeedback(feedbacks);
            return res;
        }

        public Task<IEnumerable<ProductViewModel>> GetAllroducts()
        {
            var products = _feedback.GetAllProducts();
            return products;
        }

        public Task<IEnumerable<CategoryViewModel>> GetCategory()
        {
            var categories = _feedback.GetCategory();
            return categories;
        }

        public Task<IEnumerable<ProductViewModel>> GetProductsByCategory(int id)
        {
            var products = _feedback.GetProductsByCategory(id);
            return products;
        }

        public Task<IEnumerable<ReviewViewModel>> GetReviews()
        {
            var reviews = _feedback.GetReviews();
            return reviews;
        }

        public Task<GetFeedbackViewModel> GetFeedback(string encryptedText)
        {
            var feedback = _feedback.GetFeedback(encryptedText);
            return feedback;
        }

    }
}
