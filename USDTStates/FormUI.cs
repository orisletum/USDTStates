using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using USDTStates.Services;

namespace USDTStates
{
    public partial class FormUI : Form
    {
        private readonly Timer _timer;
        private readonly IExchangeService[] _exchanges;
        public FormUI()
        {
            InitializeComponent();
            _exchanges = new IExchangeService[]
            {
                new BinanceService(),
                new BybitService(),
                new KucoinService(),
                new BitgetService()
            };
            //без сокетов по таймеру
            //_timer = new Timer { Interval = 5000 };
            //_timer.Tick += async (s, e) => await UpdatePrices();
            //_timer.Start();
            _ = UpdateExchangePrices();
            SubscribeToPrices();
        }
        private async void SubscribeToPrices()
        {
            foreach (var exchange in _exchanges)
            {
                try
                {
                    await exchange.SubscribeToPricesBtcUsdtAsync(price =>
                    {
                        BeginInvoke((Action)(() =>
                        {
                            UpdateLabel(exchange.GetType().Name, price);
                        }));
                    });
                }
                catch (Exception ex)
                {
                    UpdateLabel(exchange.GetType().Name, -1);
                    MessageBox.Show($"Не удалось подключиться к {exchange.GetType().Name}: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateLabel(string exchangeName, decimal price)
        {
            
            switch (exchangeName)
            {
                case nameof(BinanceService):
                    binanceLabel.Text = $"Binance BTC/USDT: {price}";
                    break;
                case nameof(BybitService):
                    bybitLabel.Text = $"Bybit BTC/USDT: {price}";
                    break;
                case nameof(KucoinService):
                    kucoinLabel.Text = $"Kucoin BTC/USDT: {price}";
                    break;
                case nameof(BitgetService):
                    bitgetLabel.Text = $"Bitget BTC/USDT: {price}";
                    break;
            }
        }
        private async Task UpdateExchangePrices()
        {
            var tasks = _exchanges.Select(ex => ex.GetPriceBtcUsdtAsync());
            var prices = await Task.WhenAll(tasks);

            BeginInvoke((Action)(() =>
            {
                binanceLabel.Text = $"Binance BTC/USDT: {prices[0]}";
                bybitLabel.Text = $"Bybit BTC/USDT: {prices[1]}";
                kucoinLabel.Text = $"Kucoin BTC/USDT: {prices[2]}";
                bitgetLabel.Text = $"Bitget BTC/USDT: {prices[3]}";
            }));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            foreach (var exchange in _exchanges)
                exchange.Unsubscribe();
            base.OnFormClosing(e);
        }
    }
}
