using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId);
    }
}
