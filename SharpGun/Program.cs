using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace SharpGun
{
    public static class Program
    {
        private static IHostBuilder CreateHostBuilder(string[] args) {
            return Host
                /*
                    执行内容：
                        1. 加载主机和应用程序配置信息
                        2. 配置日志记录
                        3. 设置web服务器
                        4. 设置dotnet core的托管形式
                 */
                .CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // 将所有配置项通过该泛型类Startup映射
                    webBuilder.UseStartup<Startup>();
                });
        }

        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }
    }
}
