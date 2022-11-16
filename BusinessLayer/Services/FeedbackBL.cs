using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
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
            catch (Exception)
            {

                throw;
            }
        }
    }
}
