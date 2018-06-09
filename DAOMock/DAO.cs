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
        private List<IProducent> _producenci;
        private List<IPiwo> _piwa;

        public DAO()
        {
            _producenci = new List<IProducent>
            {
                new Producent(){Nazwa="Żywiec", Miasto="Żywiec" },
                new Producent(){Nazwa="Browar Fortuna", Miasto="Miłosław"}
            };
            _piwa = new List<IPiwo>
            {
                new Piwo(){Cena=5, Nazwa="Żywiec",KolorPiwa=CORE.Kolor.Jasne,ProcentAlkoholu=4,ProducentPiwa=_producenci[0]},
                new Piwo(){Cena=7.59, Nazwa="Komes Belgian Ipa",KolorPiwa=CORE.Kolor.Jasne,ProcentAlkoholu=8,ProducentPiwa=_producenci[1]},
                new Piwo(){Cena=4.59, Nazwa="Miłosław Witbier",KolorPiwa=CORE.Kolor.Jasne,ProcentAlkoholu=4.5,ProducentPiwa=_producenci[1]},
                new Piwo(){Cena=4.99, Nazwa="Fortuna Czarna",KolorPiwa=CORE.Kolor.Ciemne,ProcentAlkoholu=5,ProducentPiwa=_producenci[1]}
            };
        }

        public IPiwo AddNewPiwo()
        {
            IPiwo tPiwo = new Piwo();
            tPiwo.Nazwa = "";
            tPiwo.Cena = 0.0;
            tPiwo.ProcentAlkoholu = 0.0;
            return tPiwo;
        }

        public IProducent AddNewProducent()
        {
            IProducent tProd = new Producent();
            tProd.Nazwa = "";
            tProd.Miasto = "";
            return tProd;
        }

        public IEnumerable<IPiwo> GetAllPiwa()
        {
            return _piwa;
        }

        public IEnumerable<IProducent> GetAllProducenci()
        {
            return _producenci;
        }

        public void SavePiwo(IPiwo piwo)
        {
            _piwa.Add(piwo);
        }

        public void SavePiwo(IPiwo piwo, int indeks)
        {
            _piwa[indeks] = piwo;
        }

        public void SaveProducent(IProducent producent)
        {
            _producenci.Add(producent);
        }

        public void SaveProducent(IProducent producent, int indeks)
        {
            _producenci[indeks] = producent;
        }
    }
}
