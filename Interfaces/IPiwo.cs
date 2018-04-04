using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.CORE;

namespace Obst.ølCatalog.Interfaces
{
    public interface IPiwo
    {
        IProducent ProducentPiwa { set; get; }
        double Cena { set; get; }
        double ProcentAlkoholu { set; get; }
        string Nazwa { set; get; }
        Kolor KolorPiwa { set; get; }

    }
}
