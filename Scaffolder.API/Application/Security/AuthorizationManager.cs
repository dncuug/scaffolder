using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Scaffolder.Core.Meta;

namespace Scaffolder.API.Application.Security
{
    public class AuthorizationManager
    {
        private readonly Dictionary<Configuration, string> _configurations;

        public AuthorizationManager(String workingDirectory)
        {
            var configurationDirectories = Directory.GetDirectories(workingDirectory);

            _configurations = new Dictionary<Configuration, string>();

            foreach (var configurationDirectory in configurationDirectories)
            {
                var touple = LoadConfiguration(configurationDirectory);
                _configurations.Add(touple.Item1, touple.Item2);
            }

            if (!Valid())
            {

            }
        }

        private Tuple<Configuration, String> LoadConfiguration(String configurationDirectory)
        {
            var path = configurationDirectory + "configuration.json";

            if (!System.IO.File.Exists(path))
            {
                Configuration.Create().Save(path);
            }
            
            var configuration = Configuration.Load(path);

            return new Tuple<Configuration, String>(configuration, configurationDirectory);
        }

        private bool Valid()
        {
            var users = _configurations.SelectMany(c => c.Key.Users).ToList();

            foreach (var u in users)
            {
                if (users.Count(o => o.Login == u.Login && o.Password == u.Password) > 1)
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns>USer and path to configuration</returns>
        public Tuple<User, string> Auth(string login, string password)
        {
            foreach (var configuration in _configurations)
            {
                var user = configuration.Key.Users.SingleOrDefault(o => o.Login == login && o.Password == password);

                if (user != null)
                {
                    return new Tuple<User, String>(user, configuration.Value);
                }
            }

            return null;
        }

    }
}
