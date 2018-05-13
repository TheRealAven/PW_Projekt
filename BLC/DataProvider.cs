using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;
using System.Reflection;
using BLC.Properties;

namespace Obst.ølCatalog.BLC
{
    public class DataProvider
    {
        private Settings _settings = new Settings();
        public IDAO DAO { get; set; }
        public IEnumerable<IPiwo> Piwa
        {
            get { return DAO.GetAllPiwa(); }
        }
        public IEnumerable<IProducent> Producenci
        {
            get { return DAO.GetAllProducenci(); }
        }

        public DataProvider(string whichMock)
        {
            Assembly assemblyDAO = Assembly.UnsafeLoadFrom(whichMock + ".dll");
            Type typeDAO = null;

            foreach (Type tempType in assemblyDAO.GetTypes())
            {
                if (tempType.GetInterfaces().Contains<Type>(typeof(IDAO)))
                {
                    typeDAO = tempType;
                    break;
                }
            }

            ConstructorInfo constructorInfo = typeDAO.GetConstructor(new Type[] { });
            var tempDAOObject = constructorInfo.Invoke(new object[] { });
            DAO = (IDAO)tempDAOObject;
        }
    }
}
