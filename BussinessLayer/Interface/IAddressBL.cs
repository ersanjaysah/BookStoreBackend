using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Interface
{
    public interface IAddressBL
    {
        public string AddAddress(int UserId, AddressModel addressModel);
        public bool UpdateAddress(int AddressId, AddressModel addressModel);
        public bool DeleteAddress(int AddressId);
    }
}
