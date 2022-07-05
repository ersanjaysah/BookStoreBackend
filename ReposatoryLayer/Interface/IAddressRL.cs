using DatabaseLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReposatoryLayer.Interface
{
    public interface IAddressRL
    {
        public string AddAddress(int UserId, AddressModel addressModel);
        public bool UpdateAddress(int AddressId, AddressModel addressModel);
        public bool DeleteAddress(int AddressId);
    }
}
