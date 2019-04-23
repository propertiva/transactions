using System;
using System.Threading.Tasks;
using DataContracts.User;

namespace OrderIntakeLib
{
    internal class UserOrderStore
    {
        public UserOrderStore()
        {
        }

        internal Task<bool> Put(UserOrder userOrder)
        {
            throw new NotImplementedException();
        }
    }
}