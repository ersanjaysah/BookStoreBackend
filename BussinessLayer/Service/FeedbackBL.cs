using BussinessLayer.Interface;
using DatabaseLayer.Model;
using ReposatoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
       

        private readonly IFeedbackRL feedbackRL;

        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId)
        {
            try
            {
                return this.feedbackRL.AddFeedback(feedbackModel, userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DisplayFeedback> GetAllFeedback(int bookId)
        {
            try
            {
                return this.feedbackRL.GetAllFeedback(bookId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
