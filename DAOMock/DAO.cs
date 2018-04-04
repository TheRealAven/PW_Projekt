using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.Interfaces;

namespace Obst.ølCatalog.DAOMock
{
    public class DAO: IDAO
    {
        private List<IProducent> producenci;
        private List<IPiwo> piwa;

        public DAO()
        {
            producenci = new List<IProducent>
            {
                new Producent(){Nazwa="Żywiec", Miasto="Żywiec" },
                new Producent(){Nazwa="Browar Fortuna", Miasto="Miłosław"}
            };
            piwa = new List<IPiwo>
            {
                new Piwo(){Cena=5, Nazwa="Żywiec",KolorPiwa=CORE.Kolor.Jasne,ProcentAlkoholu=4,ProducentPiwa=producenci[0]}
            };
        }

        public IEnumerable<IPiwo> GetAllPiwa()
        {
            return piwa;
        }

        public IEnumerable<IProducent> GetAllProducenci()
        {
            return producenci;
        }
    }
}
