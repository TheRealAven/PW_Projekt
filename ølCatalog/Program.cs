using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Obst.ølCatalog.BLC;

namespace Obst.ølCatalog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BLC.DataProvider dataProvider = new DataProvider("DAOMock");
            foreach(var p in dataProvider.Producenci)
            {
                Console.WriteLine($"{p.Nazwa}");
            }
            foreach(var p in dataProvider.Piwa)
            {
                Console.WriteLine($"{p.Nazwa}, {p.Cena}, {p.ProducentPiwa.Nazwa}");
            }
            Console.ReadLine();
        }
    }
}
