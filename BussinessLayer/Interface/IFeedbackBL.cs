using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId);
        public List<DisplayFeedback> GetAllFeedback(int bookId);
    }
}
