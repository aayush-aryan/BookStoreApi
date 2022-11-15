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
    }
}
