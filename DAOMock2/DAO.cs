using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;

namespace Obst.ølCatalog.DAOMock2
{
    public class DAO : IDAO
    {
        private List<IProducent> _producenci;
        private List<IPiwo> _piwa;

        public DAO()
        {
            _producenci = new List<IProducent>
            { 
                new Producent(){Nazwa="Lech Browar Wielkopolski", Miasto="Poznań" }
            };
            _piwa = new List<IPiwo>
            {
                new Piwo(){Cena=3.50, Nazwa="Lech Premium",KolorPiwa=CORE.Kolor.Jasne,ProcentAlkoholu=5,ProducentPiwa=_producenci[0]}
            };
        }

        public IEnumerable<IPiwo> GetAllPiwa()
        {
            return _piwa;
        }

        public IEnumerable<IProducent> GetAllProducenci()
        {
            return _producenci;
        }
    }
}
