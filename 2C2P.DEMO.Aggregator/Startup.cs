using _2C2P.DEMO.Aggregator.AutofacModules;
using _2C2P.DEMO.Aggregator.BackgroundServices;
using _2C2P.DEMO.Aggregator.MiddleWares.Jaeger;
using _2C2P.DEMO.Aggregator.Services.Jaeger;
using _2C2P.DEMO.Aggregator.Services.Kafka;
using Autofac;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using Serilog;

namespace _2C2P.DEMO.Aggregator
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.IgnoreNullValues = true);
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddHttpClient()
                .AddHttpContextAccessor();

            services.AddKafka();
            services.AddJaeger();

            services.AddHostedService<TransactionSubscriberService>();

            services.AddMediatR(typeof(Startup).Assembly);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            var clientSettings = MongoClientSettings.FromUrl(new MongoUrl(Configuration.GetSection("MongoDBConnection:ConnectionString").Value));

            builder.RegisterModule(new InfrastructureModule(new MongoClient(clientSettings),
                       Configuration.GetSection("MongoDBConnection:Database").Value));

            builder.RegisterModule(new BsonModule());
            builder.RegisterModule(new MediatorModule());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseJaeger();

            app.UseSerilogRequestLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
