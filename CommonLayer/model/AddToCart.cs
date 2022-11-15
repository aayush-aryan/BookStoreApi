using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.model
{
   public class AddToCart
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
        public int BooksQty { get; set; }
    }
}
