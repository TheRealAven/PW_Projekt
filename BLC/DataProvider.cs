using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;
using System.Reflection;
using BLC.Properties;
using Obst.ølCatalog.CORE;

namespace Obst.ølCatalog.BLC
{
    public sealed class DataProvider
    {
        private Settings _settings = new Settings();
        private static readonly DataProvider _instance = new DataProvider();
        public static DataProvider Instance
        {
            get
            {
                return _instance;
            }
        }

        static DataProvider()
        {

        }

        public IDAO DAO { get; set; }
        public IEnumerable<IPiwo> Piwa
        {
            get { return DAO.GetAllPiwa(); }
        }
        public IEnumerable<IProducent> Producenci
        {
            get { return DAO.GetAllProducenci(); }
        }

        public IProducent AddProducent()
        {
            return DAO.AddNewProducent();
        }

        public IPiwo AddPiwo()
        {
            return DAO.AddNewPiwo();
        }

        public void SavePiwo(IPiwo piwo)
        {
            DAO.SavePiwo(piwo);
        }

        public void SavePiwo(IPiwo piwo, int indeks)
        {
            DAO.SavePiwo(piwo, indeks);
        }

        public void SaveProducent(IProducent producent)
        {
            DAO.SaveProducent(producent);
        }

        public void SaveProducent(IProducent producent, int indeks)
        {
            DAO.SaveProducent(producent, indeks);
        }

        private DataProvider()
        {
            string whichMock = _settings.mockName;
            Assembly assemblyDAO = Assembly.UnsafeLoadFrom(whichMock + ".dll");
            Type typeDAO = null;

            try
            {
                foreach (Type tempType in assemblyDAO.GetTypes())
                {
                    if (tempType.GetInterfaces().Contains<Type>(typeof(IDAO)))
                    {
                        typeDAO = tempType;
                        break;
                    }
                }
            }
            catch (ReflectionTypeLoadException ex)
            {
                foreach (var e in ex.LoaderExceptions)
                {
                    Console.WriteLine(e.Message);
                }
            }

            ConstructorInfo constructorInfo = typeDAO.GetConstructor(new Type[] { });
            var tempDAOObject = constructorInfo.Invoke(new object[] { });
            DAO = (IDAO)tempDAOObject;
        }
    }
}
