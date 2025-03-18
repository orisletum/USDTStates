using Kucoin.Net.Clients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace USDTStates.Services
{
    public class KucoinService : IExchangeService
    {
        private KucoinSocketClient socketClient;
        public async Task<decimal?> GetPriceBtcUsdtAsync()
        {
            var restClient = new KucoinRestClient();
            var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTC-USDT");
            var lastPrice = tickerResult.Data.LastPrice;
            return tickerResult.Success ? lastPrice : (decimal?)null;
        }

        public async Task SubscribeToPricesBtcUsdtAsync(Action<decimal> onUpdate)
        {
            try
            {
                socketClient = new KucoinSocketClient();
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20));

                var tickerSubscriptionResult = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("BTC-USDT", update =>
                {
                    if (update.Data != null)
                        onUpdate((decimal)update.Data.LastPrice);
                }, cts.Token);
                Console.WriteLine("Subscribe на Kucoin");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Таймаут Subscribe к Kucoin");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Subscribe к Kucoin: {ex.Message}");
            }
          
        }
        public void Unsubscribe()
        {
            try
            {
                if (socketClient != null)
                {
                    socketClient.UnsubscribeAllAsync();
                    Console.WriteLine("Unsubscribe от Binance");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Unsubscribe от Binance: {ex.Message}");
            }
            finally
            {
                socketClient?.Dispose();
                socketClient = null;
            }
        }
    }
}
