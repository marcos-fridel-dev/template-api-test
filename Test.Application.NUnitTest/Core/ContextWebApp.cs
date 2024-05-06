using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Services.Api.Models.Test;

namespace Test.Application.NUnitTest.Core
{
    public class ContextWebApp : WebApplicationFactory<ApiTest>
    {
        private IHost _host;
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            base.ConfigureWebHost(builder);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Configura cualquier Mock o similares aquí.
            });

            _host = base.CreateHost(builder);

            return _host;
        }

        public IHost GetHost()
        {
            return _host;
        }
    }    
}
