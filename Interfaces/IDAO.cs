using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obst.ølCatalog.Interfaces
{
    public interface IDAO
    {
        IEnumerable<IPiwo> GetAllPiwa();
        IEnumerable<IProducent> GetAllProducenci();
    }
}
