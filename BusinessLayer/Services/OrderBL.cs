using BusinessLayer.Interface;
using CommonLayer.model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {

        private readonly IOrderRL orderRL;
        public OrderBL(IOrderRL orderRL)
        {
            this.orderRL = orderRL;
        }
        public OrderModel AddOrder(OrderModel orderModel,int userId)
        {
            try
            {
                return this.orderRL.AddOrder(orderModel, userId);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public string CancelOrder(int OrdersId)
        {
            try
            {
                return this.orderRL.CancelOrder(OrdersId);
            }
            catch (Exception)
            {

                throw;
            }
        }

       
    }
}
