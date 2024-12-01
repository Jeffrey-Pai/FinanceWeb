using Microsoft.AspNetCore.Mvc;
using FinanceWeb.Services;
using Sinopac.Shioaji;
using FinanceWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanceWeb.Controllers
{
    public class ShioajiController : Controller
    {
        private readonly ShioajiService _shioajiService;
        private readonly StockDataContext _context;
        
        // 透過依賴注入獲取 ShioajiManager
        public ShioajiController(ShioajiService shioajiService, StockDataContext context)
        {
            _shioajiService = shioajiService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var _api = _shioajiService.GetApi();
            var contract = _api.Contracts.Stocks["TSE"]["2330"];
            Kbars kbars = _api.Kbars(contract, "2021-09-13", "2021-09-13");

            // 儲存股市資料至資料庫
            foreach (var timestamp in kbars.ts)
            {
                long unixTimestampInMilliseconds = timestamp / 1_000_000; //timestamp為奈秒
                // 將毫秒轉換為 DateTime
                DateTime dateTime = DateTimeOffset.FromUnixTimeMilliseconds(unixTimestampInMilliseconds).UtcDateTime;
                var aaa = _context.Stocks.Where(a => a.Id == 1).ToList();
                var stock = new Models.Stock
                {
                    Symbol = "2330",
                    Date = dateTime,
                    Open = kbars.Open[kbars.ts.ToList().IndexOf(timestamp)],
                    High = kbars.High[kbars.ts.ToList().IndexOf(timestamp)],
                    Low = kbars.Low[kbars.ts.ToList().IndexOf(timestamp)],
                    Close = kbars.Close[kbars.ts.ToList().IndexOf(timestamp)],
                    Volume = kbars.Volume[kbars.ts.ToList().IndexOf(timestamp)],
                };
                _context.Stocks.Add(stock);
            }

            await _context.SaveChangesAsync();

            // 取得資料庫中的股票資料
            var stocks = await _context.Stocks.ToListAsync();
            return View(stocks);
        }
        public IActionResult Usage()
        {
            var _api = _shioajiService.GetApi();
            var usageStatus = _api.Usage();  // 假設這是返回數據的結果

            return View(usageStatus);  // 傳遞到視圖
        }
    }
}
