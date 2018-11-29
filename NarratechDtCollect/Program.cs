using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Web.Administration;
using ConfigurationSection = Microsoft.Web.Administration.ConfigurationSection;

namespace NarratechDtCollect
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
            using (ServerManager serverManager = new ServerManager())
            {
                var config = serverManager.GetApplicationHostConfiguration();
                ConfigurationSection windowsAuthenticationSection =
                    config.GetSection("system.webServer/security/authentication/windowsAuthentication", "Narratech");
                windowsAuthenticationSection["enabled"] = true;

                ConfigurationElementCollection providersCollection = windowsAuthenticationSection.GetCollection("providers");
                ConfigurationElement addElement = FindElement(providersCollection, "add", "value", @"Negotiate");

                if (addElement == null) throw new InvalidOperationException("Element not found!");
                providersCollection.Remove(addElement);

                serverManager.CommitChanges();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseHttpSys(options =>
                {
                    options.Authentication.Schemes =
                        AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                    options.Authentication.AllowAnonymous = false;
                });
        
        private static ConfigurationElement FindElement(ConfigurationElementCollection collection, string elementTagName, params string[] keyValues)
        {
            foreach (ConfigurationElement element in collection)
            {
                if (String.Equals(element.ElementTagName, elementTagName, StringComparison.OrdinalIgnoreCase))
                {
                    bool matches = true;
                    for (int i = 0; i < keyValues.Length; i += 2)
                    {
                        object o = element.GetAttributeValue(keyValues[i]);
                        string value = null;
                        if (o != null)
                        {
                            value = o.ToString();
                        }
                        if (!String.Equals(value, keyValues[i + 1], StringComparison.OrdinalIgnoreCase))
                        {
                            matches = false;
                            break;
                        }
                    }
                    if (matches)
                    {
                        return element;
                    }
                }
            }
            return null;
        }
    }
}