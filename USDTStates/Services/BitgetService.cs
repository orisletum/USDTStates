using Bitget.Net.Clients;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace USDTStates.Services
{
    public class BitgetService : IExchangeService
    {
        private BitgetSocketClient socketClient;
        public async Task<decimal?> GetPriceBtcUsdtAsync()
        {
            var restClient = new BitgetRestClient();
            var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("BTCUSDT_SPBL");
            var lastPrice = tickerResult.Data.ClosePrice;
            return tickerResult.Success ? lastPrice : (decimal?)null;
        }

        public async Task SubscribeToPricesBtcUsdtAsync(Action<decimal> onUpdate)
        {
            try
            {
                socketClient = new BitgetSocketClient();
                var cts = new CancellationTokenSource(TimeSpan.FromSeconds(20)); // Таймаут 10 секунд

                var tickerSubscriptionResult = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync("BTCUSDT", update =>
                {
                    if (update.Data != null)
                        onUpdate((decimal)update.Data.LastPrice);
                }, cts.Token);
                Console.WriteLine("Subscribe на Bitget");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Таймаут Subscribe к Bitget");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка Subscribe к Bitget: {ex.Message}");
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
