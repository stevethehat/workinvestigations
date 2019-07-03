using Net.Ibcos.GoldAPIServer.DataLayer;
using Net.Ibcos.GoldAPIServer.DataLayer.Models;
using Net.Ibcos.GoldAPIServer.DataLayer.Isam.Core;
using Net.Ibcos.GoldAPIServer.Util;
using Net.Ibcos.GoldAPIServer.Util.Config;
using System;
using Microsoft.Extensions.Options;
using Net.Ibcos.GoldAPIServer.DataLayer.Isam.Repositories;
using Moq;

namespace Gold
{
    public class Gold
    {
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
}
