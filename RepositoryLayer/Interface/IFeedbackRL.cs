using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IFeedbackRL
    {
        public FeedBackModel AddFeedback(FeedBackModel feedbackModel, int userId);
    }
}
