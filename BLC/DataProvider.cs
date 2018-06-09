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

        public DataProvider(string whichMock)
        {
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
            }catch(ReflectionTypeLoadException ex)
            {
                foreach(var e in ex.LoaderExceptions)
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
