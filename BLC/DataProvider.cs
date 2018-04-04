using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;
using Obst.ølCatalog.DAOMock;

namespace Obst.ølCatalog.BLC
{
    public class DataProvider
    {
        public IDAO DAO { get; set; }
        public IEnumerable<IPiwo> Piwa
        {
            get { return DAO.GetAllPiwa(); }
        }
        public IEnumerable<IProducent> Producenci
        {
            get { return DAO.GetAllProducenci(); }
        }

        public DataProvider()
        {
            DAO = (IDAO) new DAOMock.DAO();
        }
    }
}
