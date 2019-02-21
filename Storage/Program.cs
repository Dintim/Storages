using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage
{
    class Program
    {
        static void Main(string[] args)
        {            

            Menu m = new Menu();
            Flash f = new Flash("flash", "ff", 5000);
            DVD d = new DVD("DVD", "dd", DVDType.oneSide);
            HDD h = new HDD("HDD", "hh", 4, 2000);
            m.storages.Add(f);
            m.storages.Add(d);
            m.storages.Add(h);
            

            m.StartMenu();
        }
    }
}
