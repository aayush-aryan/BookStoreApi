using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
   public interface IAddressBL
    {
        string AddAddress(int UserId, AddressModel addressModel);
        bool UpdateAddress(int AddressId, AddressModel addressModel);
        bool DeleteAddress(int AddressId);
        public List<AddressModel> GetAllAddresses(int userId);
    }
}
