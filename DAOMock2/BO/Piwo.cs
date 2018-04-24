using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;
using Obst.ølCatalog.CORE;

namespace Obst.ølCatalog.DAOMock2
{
    public class Piwo : IPiwo
    {
        public IProducent ProducentPiwa { get; set; }
        public double Cena { get; set; }
        public double ProcentAlkoholu { get; set; }
        public string Nazwa { get; set; }
        public Kolor KolorPiwa { get; set; }
    }
}
