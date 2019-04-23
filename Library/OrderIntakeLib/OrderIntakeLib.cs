using System;
using System.Threading.Tasks;
using Utility;
using DataContracts.Order;
using DataContracts.User;

namespace OrderIntakeLib
{
    public static class OrderIntakeLib
    {
        public static async Task<bool> TryQueueOrder(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.PropertyID))
            {
                throw new ArgumentNullException();
            }

            order.OrderID = KeyGenerator.GetInvertedKey();
            var stored = false;

            try
            {
                
                var entityStore = new OrderStore();
                stored = await entityStore.Put(order);

                if (stored)
                {
                    var userOrderStore = new UserOrderStore();
                    stored = await userOrderStore.Put(
                        new UserOrder
                        {
                            UserID = order.UserID,
                            PropertyID = order.PropertyID,
                            OrderID = order.OrderID
                        });
                }
            }
            catch (Exception e)
            {
                return false;
            }

            return stored;
        }
    }
}
