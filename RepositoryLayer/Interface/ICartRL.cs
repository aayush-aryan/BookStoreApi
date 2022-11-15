using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface ICartRL
    {
        string AddBookToCart(AddToCart cartBook, int userId);
        string DeleteCart(int CartId);
        bool UpdateCart(int CartId, int BooksQty);
        List<CartModel> GetAllBooksinCart(int UserId);
    }
}
