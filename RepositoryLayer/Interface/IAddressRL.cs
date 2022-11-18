using CommonLayer.model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
   public interface IAddressRL
    {
        string AddAddress(int UserId, AddressModel addressModel);
        bool UpdateAddress(int AddressId, AddressModel addressModel);
        bool DeleteAddress(int AddressId);

    }
}
