using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaOaie
{
    public class Nealimentar:Produs
    {
        public String garantie { get; set; }
        public Nealimentar(String codIdentificare, string denumire, string descriere, double pret, int stoc, int cartStoc, String garantie):base(codIdentificare, denumire, descriere, pret, stoc, cartStoc)
        {
            this.garantie = garantie;
        }

        public override String Afisare()
        {
            String inCart;
            inCart = this.pret + "ron (" + this.cartStoc + ") \t" + this.denumire + "\t  Garantie: " + this.garantie ;
            return inCart;
        }
    }
}
