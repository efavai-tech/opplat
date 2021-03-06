using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Account.WebApi.Data;
using Account.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using opplatApplication.Data;
using opplatApplication.Utils;
using Swashbuckle.AspNetCore.Swagger;
using FinanzasWebApi.Helper;
using notificationsWebApi.Data;
using opplatApplication.Hubs;
using ContabilidadWebApi.Services;
using FinanzasWebApi.Data;
using InventarioWebApi.Data;
using ImportadorDatos.Models.Versat;
using ContabilidadWebApi.Data;
using ImportadorDatos.Models.EnlaceVersat;
using ImportadorDatos.Jobs;
using ImportadorDatos.HostedServices;

[assembly: HostingStartup(typeof(opplatApplication.Startup))]
namespace opplatApplication
{
    public class Startup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {

            builder.ConfigureServices(ConfigureServices);
            builder.Configure(Configure);
        }

        public void ConfigureServices(WebHostBuilderContext context, IServiceCollection services)
        {
            //contabilidad context
            services.AddDbContext<ContabilidadWebApi.Data.ContabilidadDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("ContabilidadApiDbContext"), b => b.MigrationsAssembly("ContabilidadWebApi")));

            //finanzas db context
            services.AddDbContext<FinanzasWebApi.Data.FinanzasDbContext>(options =>
                    options.UseNpgsql(context.Configuration.GetConnectionString("FinanzasApiDbContext"), b => b.MigrationsAssembly("FinanzasWebApi")));
            //

            // NotificationsDbContext
            services.AddDbContext<notificationsDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("notificationsApi"), b => b.MigrationsAssembly("notificationsWebApi")));
            //

            //inventario DbContext
            services.AddDbContext<InventarioDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("InventarioConnection"), b => b.MigrationsAssembly("InventarioWebApi")));
            //
            services.AddDbContext<AccountDbContext>(options =>
            options.UseNpgsql(context.Configuration.GetConnectionString("AccountConnection"), b => b.MigrationsAssembly("Account.WebApi")));

            services.AddDbContext<OpplatAppDbContext>(options =>
                options.UseNpgsql(context.Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("opplatApplication")));

            services.AddDbContext<VersatDbContext>(options =>
               options.UseSqlServer(context.Configuration.GetConnectionString("VersatConnection")));

            services.AddDbContext<EnlaceVersatDbContext>(options =>
                 options.UseNpgsql(context.Configuration.GetConnectionString("EnlaceVersatDbContext")));

            services.AddSignalR();

            services.AddIdentity<Usuario, IdentityRole>()
                .AddEntityFrameworkStores<AccountDbContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<AccountDbContext>();
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                ).AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "https://microapp.cu",
                        ValidAudience = "https://microapp.cu",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("Admin123*1234567890"))
                    };
                });

            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "Account API",
                        Description = "Gestiona la autenticacion y autorizacion, asi como la gestion de usuarios.",
                        TermsOfService = "APACHE 2.0",
                        Contact = new Contact
                        {
                            Name = "EFAVAI Tech",
                            Email = "efavai.tech@gmail.com",
                            Url = "https://efavai.com/"
                        }
                    });

                    // Set the comments path for the Swagger JSON and UI.
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                });

            services.AddTransient<OpplatAppDbContext>();
            services.AddSingleton<MenuLoader>();
            services.AddSingleton<LicenciaService>();

            services.AddScoped<CuentasServices>();

            //finanzas services
            services.AddScoped<FinanzasDbContext>();
            services.AddScoped<ObtenerPlanGI>();
            // services.AddSingleton<ObtenerPlanGI_Context>();
            // services.AddSingleton<GetTotalIngresosEnMes>();
            // services.AddSingleton<GetTotalEgresosEnMes>();
            //fin

            //importador
            services.AddScoped<VersatDbContext>();
            services.AddScoped<ContabilidadDbContext>();
            services.AddScoped<EnlaceVersatDbContext>();
            services.AddTransient<ImportarVersat>();

            services.AddHostedService<UpdateCuentasHostedService>();
            services.AddHostedService<UpdatePeriodosHostedService>();
            services.AddHostedService<UpdateAsientosHostedService>();
            //fin
            services.AddSpaStaticFiles(config =>
            {
                config.RootPath = context.Configuration.GetValue<string>("ClientApp");
            });
            services.AddCors();
            var mvcBuilder = services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddJsonOptions(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });
            var lista = new string[]{
                "Account.WebApi"
            };
            var asseblies = AppDomain.CurrentDomain.GetAssemblies().Where(c => lista.Contains(c.FullName));
            foreach (var assembly in asseblies)
            {
                mvcBuilder.AddApplicationPart(assembly);
            }
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
            var config = app.ApplicationServices.GetRequiredService<IConfiguration>();

            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSwagger(c => c.RouteTemplate = "docs/{documentName}/docs.json");
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/docs/v1/docs.json", "Account API v1");
                c.RoutePrefix = "docs";
            });
            app.UseSignalR(routes =>
        {
            routes.MapHub<NotificationsHub>("/notihub");
        });
            app.UseCors(build => build.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseMvc(builder =>
            {
                builder.MapRoute(
                    name: "default",
                    template: "admin/{controller=Home}/{action=Index}/{id?}");

            });
            app.UseSpaStaticFiles();
            app.Map("/home", conf =>
            {
                conf.UseSpa(builder =>
                {
                    builder.Options.SourcePath = config.GetValue<string>("ClientApp");

                });
            });
            // app.UseSpa(builder =>
            // {
            //     builder.Options.SourcePath = "../opplat-vue/dist";

            // });
        }
    }
}