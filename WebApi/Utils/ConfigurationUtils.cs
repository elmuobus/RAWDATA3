using System;
using Microsoft.Extensions.Configuration;

namespace WebApi.Utils
{
    public class ConfigurationUtils
    {
        private readonly IConfiguration _configuration;
        
        public ConfigurationUtils(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetString(string key, string errorMessage)
        {
            var result = _configuration.GetSection(key).Value;
            
            if (string.IsNullOrEmpty(result))
                throw new ArgumentException(errorMessage);
            return result;
        }
        
        public int GetInt(string key, string errorMessage)
        {
            var result = int.TryParse(_configuration.GetSection(key).Value, out int pwdSize);
            
            if (result == false)
                throw new ArgumentException(errorMessage);
            return pwdSize;
        }
    }
}