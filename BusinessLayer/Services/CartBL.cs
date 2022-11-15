using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }
        
        public string AddBookToCart(AddToCart cartBook, int userId)
        {
            try
            {
                return this.cartRL.AddBookToCart(cartBook, userId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateCart(int CartId, int BooksQty)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, BooksQty);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<CartModel> GetAllBooksinCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllBooksinCart(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
