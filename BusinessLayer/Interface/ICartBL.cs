using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        string AddBookToCart(AddToCart cartBook, int userId);
    }
}
