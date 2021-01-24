﻿using _2C2P.DEMO.AGGREGATOR.Services;
using _2C2P.DEMO.Infrastructure;
using Autofac;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace _2C2P.DEMO.AGGREGATOR.AutofacModules
{
    public class InfrastructureModule : Autofac.Module
    {
        IMongoClient _mongoClient;
        string _dbName;

        public InfrastructureModule(IMongoClient client, string dbName)
        {
            _mongoClient = client;
            _dbName = dbName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.Load("2C2P.DEMO.Infrastructure"))
            .Where(t => t.Name.EndsWith("Repository"))
            .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.Register(ctx =>
            {
                return new MongoContext(_mongoClient, _dbName);
            }).As<IMongoContext>().SingleInstance();

            ConventionRegistry.Register(
                "Ignore null values",
                new ConventionPack
                {
                    new IgnoreIfNullConvention(true)
                },
                t => true);

            builder.RegisterType<CrudService>().As<ICrudService>().InstancePerLifetimeScope();
        }
    }
}
