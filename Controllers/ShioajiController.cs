using Microsoft.AspNetCore.Mvc;
using FinanceWeb.Services;
using Sinopac.Shioaji;

namespace FinanceWeb.Controllers
{
    public class ShioajiController : Controller
    {
        private readonly ShioajiService _shioajiService;

        // 透過依賴注入獲取 ShioajiManager
        public ShioajiController(ShioajiService shioajiService)
        {
            _shioajiService = shioajiService;
        }

        public IActionResult Index()
        {
            var _api = _shioajiService.GetApi();

            var contract = _api.Contracts.Stocks["TSE"]["2330"];
            Ticks ticks = _api.Ticks(
                Contract: contract,
                date: "2021-02-24",
                query_type: TicksQueryType.RangeTime,
                time_start: "09:00:00",
                time_end: "09:20:01"
            );           
            ViewBag.Ticks = ticks;
            ViewBag.Usage = _api.Usage();

            return View();
        }
    }
}
