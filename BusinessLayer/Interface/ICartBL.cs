using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        string AddBookToCart(AddToCart cartBook, int userId);
        public string DeleteCart(int CartId);
        bool UpdateCart(int CartId, int BooksQty);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
