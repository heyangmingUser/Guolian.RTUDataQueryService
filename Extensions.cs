
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Guolian.RTUDataQueryService
{
    public static class Extensions
    {
        public static IServiceCollection AddRTUClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectConfigs>(configuration.GetSection("ConnectionEquipment"));
            services.AddTransient<QueryService>();
            return services;
        }

    }
}
