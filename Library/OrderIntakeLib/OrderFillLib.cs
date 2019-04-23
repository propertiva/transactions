using DataContracts.Order;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderIntakeLib
{
    public static class OrderFillLib
    {
        public static async Task TryFillOrder()
        {
            try
            {
                var entityStore = new OrderStore();
                var order = await entityStore.Get();

                // fill order by invoking order matching library/service
                await entityStore.Delete(order);
            }
            catch (Exception e)
            {
            }

            return;
        }
    }
}
