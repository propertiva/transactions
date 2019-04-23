using DataContracts.Order;
using System;
using System.Threading.Tasks;

namespace OrderIntakeLib
{
    internal class OrderStore
    {
        public OrderStore()
        {
        }

        internal async Task<Order> Get()
        {
            throw new NotImplementedException();
        }

        internal Task Delete(Order order)
        {
            throw new NotImplementedException();
        }

        internal Task<bool> Put(Order order)
        {
            throw new NotImplementedException();
        }
    }
}