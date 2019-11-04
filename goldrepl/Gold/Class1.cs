using Net.Ibcos.GoldAPIServer.DataLayer;
using Net.Ibcos.GoldAPIServer.DataLayer.Models;
using Net.Ibcos.GoldAPIServer.DataLayer.Isam.Core;
using Net.Ibcos.GoldAPIServer.Util;
using Net.Ibcos.GoldAPIServer.Util.Config;
using System;
using Microsoft.Extensions.Options;
using Net.Ibcos.GoldAPIServer.DataLayer.Isam.Repositories;
using Moq;
using System.Reflection;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using IronPython.Runtime;

namespace Gold
{
    public class Gold
    {
        private const string PythonDictionary = "IronPython.Runtime.PythonDictionary";
        public readonly IsamConnection Connection;
        public readonly IOptions<IsamConfig> IsamConfig ;
        public readonly GlobalUserContext UserContext;

        public Gold(string isamPath)
        {
            ILogger<IsamConnection> logger = new Mock<ILogger<IsamConnection>>().Object;
            Connection = new IsamConnection(logger);
            UserContext = new GlobalUserContext()
            {
                CompanyId = 1
            };

            IsamConfig = Options.Create<IsamConfig>(
                new IsamConfig()
                {
                    Isam = new IsamProperties()
                    {
                        IsamPath = isamPath
                    }
                }
            );
            Console.WriteLine("Gold 5");
        }

        public IsamRepository<T> GetRepo<T>() where T: GoldModel, new()
        {
            ILogger<IsamRepository<T>> logger = new Mock<ILogger<IsamRepository<T>>>().Object;
            
            IsamRepository<T> result = new IsamRepository<T>(
                Connection, IsamConfig, UserContext, logger                
            );

            return result;
        }
    }

    public class Extensions
    {
        public GoldModel GetFromDict<T>(IsamRepository<T> repo, object obj) where T : GoldModel, new()
        {
            var result = new Pdfrec();
            result.PartNo = "PartNo";
            return result;
        }
    }
}
