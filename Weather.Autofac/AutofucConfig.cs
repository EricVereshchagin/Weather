using Autofac;
using System;
using Autofac.Core;
using Weather.Core.Data;
using Weather.Core.Parser;
using Weather.Core.API;
using Weather.Infrastructure.Database;
using Weather.Parser.Gismeteo;
using Microsoft.Extensions.Options;


namespace Weather.Autofac
{
    public class AutofucConfig
    {
        public static IContainer Container { get; set; }

        public static void ConfigureContainer()
        {
            WeatherDbConfig weatherDbConfig = new WeatherDbConfig 
            {
                City_Collection_Name = "cities",
                Connection_String = "mongodb://127.0.0.1:27017",
                Weather_Collection_Name = "temperature",
                Database_Name = "weather_db" 
            };
            var builder = new ContainerBuilder();
            
            builder.RegisterType<WeatherLoadServices>().As<ILoaderData<WeatherForDayShort>>();
            builder.RegisterType<DbClient>().AsSelf()
                                            .AsImplementedInterfaces()
                                            .SingleInstance()
                                            .WithParameter("DbConfig", Options.Create(weatherDbConfig));
            builder.RegisterType<ParserGismeteo>().As<IParser>();
   
            Container = builder.Build();
        }
    }
}
