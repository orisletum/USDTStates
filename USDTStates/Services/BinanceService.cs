using Binance.Net.Clients;
using CryptoExchange.Net.Interfaces;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace USDTStates.Services
{
    public class BinanceService : IExchangeService
    {
        private BinanceSocketClient socketClient;
        public async Task<decimal?> GetPriceBtcUsdtAsync()
        {
            var restClient = new BinanceRestClient();
            var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT");
            var lastPrice = tickerResult.Data.LastPrice;
            return tickerResult.Success ? lastPrice : (decimal?)null;
        }
        public async Task SubscribeToPricesBtcUsdtAsync(Action<decimal> onUpdate)
        {
            try
            {
                socketClient = new BinanceSocketClient();
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20)); // Таймаут 10 секунд

                var tickerSubscriptionResult = await socketClient.SpotApi.ExchangeData.SubscribeToTickerUpdatesAsync("BTCUSDT", update =>
                {
                    if (update.Data != null)
                        onUpdate((decimal)update.Data.LastPrice);
                }, cts.Token);
                Console.WriteLine("Subscribe на Binance");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Таймаут Subscribe к Binance.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Subscribe к Binance: {ex.Message}");
            }


          
        }
        public void Unsubscribe()
        {
            try
            {
                if (socketClient != null)
                {
                    socketClient.UnsubscribeAllAsync();
                    Console.WriteLine("Unsubscribe от Binance.");
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


