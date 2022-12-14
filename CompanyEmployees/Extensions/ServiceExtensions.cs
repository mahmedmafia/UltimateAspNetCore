
using Contracts;
using LoggerService;
using Microsoft.EntityFrameworkCore;
using Presentation.ActionFilters;
using Repository;
using Service.Contracts;
using Services;
using Shared.DTO;

namespace CompanyEmployees.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            });
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options =>
            {

            });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
        public static void ConfigureServiceManager(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(opts => opts.UseSqlServer(configuration.GetConnectionString("sqlConnection")));
        }

        public static IMvcBuilder AddCustomCsvFromatter(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutPutFormatter()));
        }
        public static void ConfigureActionFilters(this IServiceCollection services)
        {
            services.AddScoped<ValidationFilterAttribute>();
        }
        public static void ConfigureDataShaperServices(this IServiceCollection services)
        {
            services.AddScoped<IDataShaper<EmployeeDto>, DataShaper<EmployeeDto>>();
        }
        public static void ConfigureController(this IServiceCollection services)
        {
            services.AddControllers(
                 config =>
                 {
                     config.RespectBrowserAcceptHeader = true;
                     config.ReturnHttpNotAcceptable = true;
                     config.InputFormatters.Insert(0, JSONPatchFormatters.GetJsonPatchInputFormatter());
                 }).AddXmlDataContractSerializerFormatters()
             .AddCustomCsvFromatter()
             .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
        }

    }
}
