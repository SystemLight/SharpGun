using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SharpGun
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config) {
            _config = config;
        }

        public void ConfigureServices(IServiceCollection services) {
            // 尝试注册单例服务，如果已经注册将不注册，永不销毁
            // services.TryAddSingleton<IElvesRepositoryService, ElvesRepositoryService>();

            // 尝试注册服务，如果实现类相同将不注册
            // services.TryAddEnumerable(ServiceDescriptor.Singleton<IElvesRepositoryService, ElvesRepositoryService>());

            // 注册单例服务，永不销毁
            // services.AddSingleton<IElvesRepositoryService, ElvesRepositoryService>();
            // services.AddSingleton<IElvesRepositoryService>(new ElvesRepositoryService());
            // services.AddSingleton<IElvesRepositoryService>(provider => new ElvesRepositoryService());
            // services.AddSingleton(typeof(IElvesRepositoryService<>), typeof(ElvesRepositoryService<>));

            // 注册作用域服务，作用域内实例不销毁
            // services.AddScoped<IElvesRepositoryService, ElvesRepositoryService>();

            // 注册瞬时服务，每次请求都实例新的对象
            // services.AddTransient<IElvesRepositoryService, ElvesRepositoryService>();

            // 移除所有注册的指定服务
            // services.RemoveAll<IElvesRepositoryService>();
            // 替换注册的指定服务
            // services.Replace(ServiceDescriptor.Singleton<IElvesRepositoryService, ElvesRepositoryService>());

            #region 配置Kestrel服务器

            /*
                services.Configure<KestrelServerOptions>(options =>
                {
                    options.Listen(IPAddress.Any, 2222);
                });
            */

            #endregion

            #region 配置控制台生命周期

            /*
                services.Configure<ConsoleLifetimeOptions>(options =>
                {
                    // 抑制控制台消息输出
                    options.SuppressStatusMessages = false;
                });
            */

            #endregion

            #region 配置重定向规则选项

            /*

                services.Configure<RewriteOptions>(options =>
                {
                    // 添加一条正则匹配重写规则并跳过其余规则
                    // options.AddRewrite("Home/Index", "Api/Index", true);
                    options.AddRewrite(@"^Gun/(.+)", "Api/$1", true);
                });

            */

            #endregion

            #region 服务配置之后再进行配置注入

            /*
                services.PostConfigure<KestrelServerOptions>(options =>
                {
                    options.Listen(IPAddress.Any, 2222);
                });
            */

            #endregion

            #region 服务选项验证

            /*
                services.AddOptions<ElvesRepositoryServiceOptions>().Configure(options =>
                {
                    options.MaxAge = 120;
                }).Validate(options =>
                {
                    Console.WriteLine(options.MaxAge);
                    return options.MaxAge < 100;
                }, "MaxAge不能大于100");
            */

            // services.AddOptions<object>().Configure(options => { }).ValidateDataAnnotations();
            // services.AddOptions<object>().Configure(options => { }).Services.AddSingleton<IValidateOptions<ElvesRepositoryServiceValidateOptions>>();

            #endregion

            #region 注册Swagger生成文档服务，!!-需要开启XML文档注释生成并将路径指向生成位置-!!

            /*
                services.AddSwaggerGen(options =>
                {
                    options.SwaggerDoc("v1", new OpenApiInfo {Title = "SharpGun", Version = "v1"});
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, "SharpGun.xml"), true);
                });
            */

            #endregion

            #region VaryByQueryKeys高级缓存配置

            /*
                services.AddResponseCaching(options =>
                {
                    options.SizeLimit = 200;
                    options.MaximumBodySize = 200;
                    options.UseCaseSensitivePaths = true;
                });
            */

            #endregion

            // 注册日志所需服务
            // services.AddLogging();

            // 注册目录浏览服务
            // services.AddDirectoryBrowser();

            // 为IHttpContextAccessor服务添加默认实现，控制器中模拟HttpContext.Current接口
            // services.AddHttpContextAccessor();

            // 将Controller所需的service添加注册到IOC容器，搭配endpoint.MapControllers()方法
            services.AddControllers();

            // 将Controller所需的service添加注册到IOC容器，并包含Views视图的服务
            // services.AddControllersWithViews();
            services.AddRazorPages();

            #region 添加Razor引擎服务处理视图，代替AddMvc方法

            /*
                services.AddRazorPages(options =>
                {
                    // 添加友好路由，第一个参数时页面实体文件，可以不加拓展，第二个参数是实际映射路由
                    options.Conventions.AddPageRoute("/DetailA", "/Pages");

                    // 所有请求映射到一个文件
                    options.Conventions.AddPageRoute("/index", "{*url}");
                }).AddViewOptions(options =>
                {
                    // 禁用客户端验证
                    options.HtmlHelperOptions.ClientValidationEnabled = false;
                });
            */

            #endregion

            // 将MVC所需的service添加注册到IOC容器，搭配endpoint.MapControllerRoute()方法
            // services.AddMvc();

            #region MVC生命周期服务高级配置功能

            /*
                services.AddMvc(options =>
                {
                    // Authorization-Resource-Exception-Action-Result
                    options.Filters.Add<MyExceptionFilter>();
                    options.CacheProfiles.Add("default", new CacheProfile
                    {
                        Duration = 200,
                        Location = ResponseCacheLocation.Client,
                        NoStore = true
                    });
                }).AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
                    options.JsonSerializerOptions.Converters.Add(new DateTimeConvert());
                });
            */

            #endregion

            #region 禁用Endpoint路由以支持UseMvc()中间件

            /*
                services.AddMvc(option =>
                {
                    option.EnableEndpointRouting = false;
                });
            */

            #endregion

            // 自定义服务添加拓展举例
            // services.AddElvesRepository(120);
            // services.AddReadJson("wwwroot/trial.json");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            // 在Application级别获取依赖注入的服务，返回服务实例
            // app.ApplicationServices.GetService<IElvesRepositoryService>();

            // 关闭应用程序，关闭后程序进程退出
            // app.ApplicationServices.GetService<IHostApplicationLifetime>()?.StopApplication();

            if (env.IsDevelopment()) {
                // 使用异常页面显示错误信息，便于开发调试
                app.UseDeveloperExceptionPage();

                #region 引入Swagger中间件

                /*
                    app.UseSwagger();
                    app.UseSwaggerUI(options =>
                    {
                        // 访问地址：GET /swagger/index.html
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "SharpGun v1");
                    });
                */

                #endregion
            }
            else {
                // 注册开发环境异常处理错误路由重定向，支持lambda函数参数
                app.UseExceptionHandler("/error");

                // 将 HTTP 严格传输安全协议 (HSTS) 标头发送到客户端
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                // app.UseHsts();
            }

            #region 注册访问请求处理函数，Run方法注册会短路中间件通道，之后的中间件不会执行

            /*
                app.Run(async context =>
                {
                    await context.Response.WriteAsync("Hello Run!");
                });
            */

            #endregion

            #region 注册中间件并执行接下来的中间件

            /*
                app.Use(async (context, next) =>
                {
                    Console.WriteLine("Hello use");
                    await next();
                });
            */

            #endregion

            #region 当访问指定路由时，执行中间件注册

            /*
                app.Map("/map", appBuilder =>
                    {
                        appBuilder.Run(async context =>
                        {
                            await context.Response.WriteAsync("Hello Run!");
                        });
                    }
                );
            */

            #endregion

            #region 当函数返回True时，执行中间件注册

            /*
                app.MapWhen(context =>
                    {
                        return context.Request.Path.Value.StartsWith("/mapwhen");
                    }, appBuilder =>
                    {
                        appBuilder.Run(async context =>
                        {
                            Console.WriteLine("am");
                            //
                            await context.Response.WriteAsync("I am mapWhen !");
                        });
                    }
                );
            */

            #endregion

            // 自定义重定向规则中间件
            // app.UseRouteRewrite();

            // VaryByQueryKeys高级缓存配置
            // app.UseResponseCaching();

            // 注册http访问重定向到https访问
            // app.UseHttpsRedirection();

            // 重定向中间件
            // app.UseRewriter();

            // 启用跨域访问
            // app.UseCors();

            // 使用静态文件默认页配置，默认访问index.html，在UseStaticFiles()中间件之前启用
            // app.UseDefaultFiles();

            // 启用静态文件访问，静态文件目录默认规定为wwwroot目录下
            // app.UseStaticFiles();

            #region 静态文件个性化配置

            /*
                var provider = new FileExtensionContentTypeProvider();
                provider.Mappings[".myapp"] = "application/x-msdownload";
                provider.Mappings[".htm3"] = "text/html";
                provider.Mappings[".image"] = "image/png";
                provider.Mappings[".rtf"] = "application/x-msdownload";
                provider.Mappings.Remove(".mp4");

                app.UseStaticFiles(
                    new StaticFileOptions
                    {
                        FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, "MyStaticFiles")),
                        RequestPath = "/StaticFiles",
                        ServeUnknownFileTypes = true,
                        DefaultContentType = "image/png",
                        ContentTypeProvider = provider,
                        OnPrepareResponse = ctx =>
                        {
                            ctx.Context.Response.Headers.Append("Cache-Control", $"public, max-age={604800}");
                        }
                    }
                );
            */

            #endregion

            // UseStaticFiles和UseDefaultFiles合并的中间件
            // app.UseFileServer(enableDirectoryBrowsing: false);

            // 路由匹配，找到匹配的终结点路由Endpoint
            app.UseRouting();

            // 启用验证功能
            // app.UseAuthentication();
            // app.UseAuthorization();

            // 【弃用】使用MVC中间件，使用endpoint中MapControllerRoute()方法代替
            // app.UseMvc();

            // 【弃用】使用MVC默认配置中间件，使用endpoint中MapDefaultControllerRoute()方法代替
            // app.UseMvcWithDefaultRoute();

            // 针对UseRouting 中间件匹配到的路由进行 委托方法的执行
            app.UseEndpoints(endpoints =>
            {
                #region 注册访问指定路由时处理的委托函数

                /*
                    endpoints.Map("/test", async context =>
                    {
                        await context.Response.WriteAsync("Hello Test!");
                    });
                */

                #endregion

                #region 注册使用GET方法访问指定路由时处理的委托函数

                /*
                    endpoints.MapGet("/", async context =>
                    {
                        await context.Response.WriteAsync("Hello World!");
                    });
                */

                #endregion

                // Configure the Health Check endpoint and require an authorized user.
                // endpoints.MapHealthChecks("/healthz").RequireAuthorization();

                // 只映射添加[Route("")]装饰的Controller类
                endpoints.MapControllers();

                // 映射Pages目录下RazorPages视图文件
                endpoints.MapRazorPages();

                #region 映射MVC控制器路由，该方法代替了UseMvc()中间件

                /*
                    endpoints.MapControllerRoute(
                        name: "default",
                        pattern: "{controller=Home}/{action=Index}{id?}"
                    );
                */

                #endregion

                // 【语法糖】等效于上述的default配置
                // endpoints.MapDefaultControllerRoute();

                #region 映射MVC区域路由，控制器中通过[Area("名称")]装饰区分区域

                /*
                    endpoints.MapAreaControllerRoute(
                        name: "areas", "areas",
                        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                */

                #endregion
            });

            // 注册HTTP状态管理中间件，使用内置Page
            app.UseStatusCodePages();

            // 注册HTTP状态管理中间件，使用客户端重定向
            // app.UseStatusCodePagesWithRedirects("/StatusCode/{0}");

            // 注册HTTP状态管理中间件，使用服务端重定向
            // app.UseStatusCodePagesWithReExecute("/StatusCode/{0}");
        }
    }
}
