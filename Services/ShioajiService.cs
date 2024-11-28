using Microsoft.Extensions.Configuration;
using Sinopac.Shioaji;

namespace FinanceWeb.Services
{
    public class ShioajiService
    {
        private readonly IConfiguration _configuration;
        private Shioaji _api;

        // 使用構造函數注入 IConfiguration
        public ShioajiService(IConfiguration configuration)
        {
            _configuration = configuration;

            // 讀取配置
            var apiKey = _configuration["Shioaji:API_KEY"];
            var secretKey = _configuration["Shioaji:SECRET_KEY"];

            _api = new Shioaji();
            _api.Login(apiKey, secretKey); // 使用配置登錄
        }

        public Shioaji GetApi()
        {
            return _api;
        }
       
    }
}
