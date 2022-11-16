using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.model
{
    public class DisplayFeedback
    {
        public int FeedbackId { get; set; }
        public string Comment { get; set; }
        public int Rating { get; set; }
        public int BookId { get; set; }
        public UserModel User { get; set; }
    }
}
