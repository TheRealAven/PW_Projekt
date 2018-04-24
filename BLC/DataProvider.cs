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

        private void _loadChosenDatabase()
        {
            Assembly assemblyDAO = Assembly.UnsafeLoadFrom(System.IO.Directory.GetParent(System.IO.Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).ToString()).ToString() + "/" + _settings.mockName + ".dll");
            Type typeDAO = assemblyDAO.GetType("Obst.ølCatalog." + _settings.mockName + ".DAO");
            ConstructorInfo constructorInfo = typeDAO.GetConstructor(new Type[] { });
            var tempDAOObject = constructorInfo.Invoke(new object[] { });
            DAO = (IDAO)tempDAOObject;
        }

        public DataProvider()
        {
            _loadChosenDatabase();
        }
    }
}
