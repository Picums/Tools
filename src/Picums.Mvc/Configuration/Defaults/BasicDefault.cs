﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Mvc.Configuration.Defaults
{
    public abstract class BasicDefault : IDefault
    {
        public void Apply(StartupConfigurations host, params object[] arguments)
        {
            host.ASP.Add((app, env) => this.ConfigureApp(app, env));
            host.Services.Add(services => this.ConfigureServices(services));
        }

        protected virtual void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
        {
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }
    }
}