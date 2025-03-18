using Binance.Net.Clients;
using Bybit.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace USDTStates.Services
{
    public class BybitService : IExchangeService
    {
        private BybitSocketClient socketClient;
        public async Task<decimal?> GetPriceBtcUsdtAsync()
        {
            var restClient = new BybitRestClient();
            var tickerResult = await restClient.V5Api.ExchangeData.GetSpotTickersAsync("BTCUSDT");
            var lastPrice = tickerResult.Data.List.First().LastPrice;
            return tickerResult.Success ? lastPrice : (decimal?)null;
        }

        public async Task SubscribeToPricesBtcUsdtAsync(Action<decimal> onUpdate)
        {
            try
            {
                socketClient = new BybitSocketClient();
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20)); // Таймаут 10 секунд

                var tickerSubscriptionResult = await socketClient.V5SpotApi.SubscribeToTickerUpdatesAsync("BTCUSDT", update =>
                {
                    if (update.Data != null)
                        onUpdate((decimal)update.Data.LastPrice);
                }, cts.Token);
                Console.WriteLine("Subscribe на Bybit");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Таймаут Subscribe к Bybit");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Subscribe к Bybit: {ex.Message}");
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
