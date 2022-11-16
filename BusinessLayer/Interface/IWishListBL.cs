using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IWishListBL
    {
        string AddBookinWishList(AddToWishList wishListModel, int userId);
        List<WishListModel> GetAllBooksinWishList(int UserId);
        bool DeleteBookinWishList(int WishListId, int userId);
    }
}
