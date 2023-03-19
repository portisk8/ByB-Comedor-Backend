using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Auth.Config
{
    public class AuthConfig : Core.Config.ConfigBase
    {
        public string SecretKey { get; set; }
        public string AuthUrl { get; set; }

        public static AuthConfig Build(IConfiguration configuration)
        {
            var prefix = "AuthConfig";
            var config = new AuthConfig();
            config.ConnectionString = configuration[$"{prefix}:ConnectionString"];
            config.AuthUrl = configuration[$"{prefix}:AuthUrl"];
            config.SecretKey = configuration[$"{prefix}:SecretKey"];
            return config;
        }

    }
}
