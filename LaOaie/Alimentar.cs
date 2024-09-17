using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaOaie
{
    public class Alimentar : Produs
    {
        public String perioadaValabilitate { get; set; }
        public Alimentar(String codIdentificare, string denumire, string descriere, double pret, int stoc, int cartStoc ,String perioadaValabilitate): base(codIdentificare, denumire, descriere, pret, stoc, cartStoc)
        {
            this.perioadaValabilitate = perioadaValabilitate;
        }

        public override String Afisare()
        {
            String inCart;
            inCart = this.pret + "ron (" + this.cartStoc + ") \t" + this.denumire + "\t  Valabilitate: " + this.perioadaValabilitate;
            return inCart;
        }
    }
}
