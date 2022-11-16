using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishlistRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishlistRL = wishListRL;
        }
        
        public string AddBookinWishList(AddToWishList wishListModel, int userId)
        {

            try
            {
                return wishlistRL.AddBookinWishList(wishListModel, userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteBookinWishList(int WishListId, int userId)
        {
            try
            {
                return wishlistRL.DeleteBookinWishList(WishListId, userId);

            }
            catch (Exception ex)
            {
                throw ex;
            }  
        }

        public List<WishListModel> GetAllBooksinWishList(int UserId)
        {
            try
            {
                return wishlistRL.GetAllBooksinWishList(UserId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
