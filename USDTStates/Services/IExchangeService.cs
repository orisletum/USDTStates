using System;
using System.Threading.Tasks;

namespace USDTStates.Services
{
    public interface IExchangeService
    {
        Task<decimal?> GetPriceBtcUsdtAsync();
        Task SubscribeToPricesBtcUsdtAsync(Action<decimal> onUpdate);
        void Unsubscribe();
    }
}
