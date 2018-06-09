using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.CORE;

namespace Obst.ølCatalog.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IPiwo> GetAllPiwa();
        IEnumerable<IProducent> GetAllProducenci();
        IPiwo AddNewPiwo();
        IProducent AddNewProducent();
        void SavePiwo(IPiwo piwo);
        void SavePiwo(IPiwo piwo, int indeks);
        void SaveProducent(IProducent producent);
        void SaveProducent(IProducent producent, int indeks);
    }
}
