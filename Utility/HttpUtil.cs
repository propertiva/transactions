using System;
using System.Threading;
using System.Threading.Tasks;

namespace Utility
{
    public class HttpUtil
    {
        public static async Task<T> MakeHttpCallAsync<T>(string uri)
        {
            var resultObj = default(T);

            try
            {
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
            }

            return resultObj;
        }
    }
}
