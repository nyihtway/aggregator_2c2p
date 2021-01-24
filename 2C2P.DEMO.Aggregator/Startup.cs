using _2C2P.DEMO.AGGREGATOR.AutofacModules;
using _2C2P.DEMO.AGGREGATOR.BackgroundServices;
using _2C2P.DEMO.AGGREGATOR.Services.Kafka;
using _2C2P.DEMO.Infrastructure.AutoMapper;
using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2C2P.DEMO.AGGREGATOR
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

            var mappingConfig = new MapperConfiguration(cfg =>
            {
                cfg.AllowNullCollections = true;
                cfg.AddProfile<MappingProfile>();
            });

            services.AddSingleton(mappingConfig.CreateMapper());

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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
