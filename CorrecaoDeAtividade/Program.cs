using CorrecaoDeAtividade.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace CorrecaoDeAtividade
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Parallel.Invoke(() =>
            {
                var keepRunning = true;
                while (keepRunning)
                {
                    CorrecaoService.ConsumirAtvidadesConcluidas();
                    Thread.Sleep(1000);
                }
            });

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
