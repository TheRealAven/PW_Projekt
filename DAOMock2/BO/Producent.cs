using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;

namespace Obst.ølCatalog.DAOMock2
{
    public class Producent: IProducent
    {
        public String Nazwa { set; get; }
        public String Miasto { set; get; }
    }
}
