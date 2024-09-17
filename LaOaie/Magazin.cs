using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Drawing;

namespace LaOaie
{
    
    class Magazin
    {   
        public static List<Produs> Read() 
        { 
            //detalii magazin 
            string denumireMagazin = "";
            string adresaMagazin = "";

            //citirea listei propriuzise
            Boolean citesc = false;
            Produs produs = null;
            List<Produs> ShopList = new List<Produs>();
            string url = "file:///C:/Users/alins/OneDrive/Desktop/Programe/Programe%20cpp/OOP/Proiecte/LaOaie/bin/Debug/produse.xml";
            XmlTextReader reader = new XmlTextReader(url);
            while (reader.Read())
            {
                if (reader.NodeType == XmlNodeType.Element)
                {
                    if (reader.Name == "depozit")
                    {
                        while (reader.MoveToNextAttribute())
                        {
                            if (reader.Name == "nume")
                                denumireMagazin = reader.Value;
                            else if (reader.Name == "adresa")
                                adresaMagazin = reader.Value;
                        }
                    }
                    else if (reader.Name == "produs")
                    {
                        if (citesc && produs != null)
                        {
                            ShopList.Add(produs);
                            citesc = false;

                        }
                        citesc = true;
                        produs = new Produs("", "", "",0,0,0);
                    }
                    else if (reader.Name == "cod")
                    {
                        reader.Read();
                        if(produs != null)
                            produs.codIdentificare = reader.Value.ToString();
                    }
                    else if (reader.Name == "denumire")
                    {
                        reader.Read();
                        if (produs != null)
                            produs.denumire = reader.Value;
                    }
                    else if (reader.Name == "descriere")
                    {
                        reader.Read();
                        if (produs != null)
                            produs.descriere = reader.Value;
                    }
                    else if (reader.Name == "pret")
                    {
                        reader.Read();
                        if (produs != null)
                        {
                            try
                            {
                                produs.pret = Convert.ToDouble(reader.Value);
                            }
                            catch { produs.pret = 0; }
                        }
                    }
                    else if (reader.Name == "stoc")
                    {
                        reader.Read();
                        try
                        {
                            produs.stoc = Convert.ToInt32(reader.Value);
                        }
                        catch { produs.stoc = 0; }
                    }
                    else if (reader.Name == "pValabilitate")
                    {
                        reader.Read();
                        if (produs != null)
                        {
                            produs = new Alimentar(produs.codIdentificare, produs.denumire, produs.descriere, produs.pret, produs.stoc, produs.cartStoc, reader.Value);
                        }
                    }
                    else if (reader.Name == "garantie")
                    {
                        reader.Read();
                        if (produs != null)
                        {
                            produs = new Nealimentar(produs.codIdentificare, produs.denumire, produs.descriere, produs.pret, produs.stoc, produs.cartStoc, reader.Value);
                        }
                    }
                }
            }
            if (citesc == true && produs != null)
            {
                ShopList.Add(produs);
            }
            return ShopList;
        }

        public static void DrawShop(List<Produs> shopStock, ListBox listShop, String Search)
        {
            List<Produs> aux = new List<Produs>();
            if (Search == "")
            {
                listShop.Items.Clear();
                foreach (Produs produs in shopStock)
                {
                    listShop.Items.Add(produs.pret + "ron \t" + produs.denumire + " (" + produs.denumire + " )");
                    aux.Add(produs);
                }
            }
            else
            {
                listShop.Items.Clear();
                foreach(Produs produs in shopStock)
                {
                    if (produs.denumire.ToLower().IndexOf(Search.ToLower().Trim()) >= 0)
                    {
                        listShop.Items.Add(produs.pret + "ron \t" + produs.denumire + " (" + produs.denumire + " )");
                        aux.Add(produs);
                    }
                }
                if (listShop.Items.Count == 0) { MessageBox.Show("Nu s-au gasit produse!"); }
            }
            shopStock = aux;
        }

        public static void AddCart(List<Produs> Cart,String Quantity, List<Produs> shopStock, int Selected)
        {
            try
            {
                if (Quantity == "" || Quantity == "0")
                {
                    throw new Exception("Trebuie sa selectezi o cantitate");
                }
                else if (Convert.ToInt32(Quantity) > shopStock[Selected].stoc)
                {
                    throw new Exception("Cantitate indisponibila");
                }
                else
                {
                    bool ok = true;
                    foreach(Produs produs in Cart)
                    {
                        if(produs.codIdentificare == shopStock[Selected].codIdentificare)
                        {
                            ok = false;
                        }
                    }
                    if(ok) 
                    {
                        Cart.Add(shopStock[Selected]);
                    }
                    shopStock[Selected].stoc -= Convert.ToInt32(Quantity);
                    shopStock[Selected].cartStoc += Convert.ToInt32(Quantity);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public static void DrawCart(List<Produs> Cart, ListBox listCart, Label Total)
        {
            listCart.Items.Clear();
            double total = 0;
            try
            {
                if(Cart == null)
                {
                    throw new Exception("Cosul este gol!");
                }

                foreach(Produs produs in Cart)
                {
                    listCart.Items.Add(produs.Afisare());
                    total += produs.pret*produs.cartStoc;
                }
                Total.Text = "Total: " + total.ToString() + "ron";

            }
            catch (Exception ex)
            { 
                listCart.Items.Add(ex.Message);
                Total.Text = "Total: " + total.ToString() + "ron";
            }
        }

        public static void RmCart(List<Produs> Cart,List<Produs> shopStock , String Quantity, int Selected)
        {
            try
            {
                if (Quantity == "")
                {
                    throw new Exception("Trebuie sa selectezi o cantitate");
                }
                else if (Convert.ToInt32(Quantity) > Cart[Selected].cartStoc)
                {
                    throw new Exception("Cantitate indisponibila");
                }
                else
                {
                    if (Cart[Selected].cartStoc > Convert.ToInt32(Quantity))
                    {
                        Cart[Selected].cartStoc -= Convert.ToInt32(Quantity);
                        foreach(Produs produs in shopStock)
                        {
                            if(produs.codIdentificare == Cart[Selected].codIdentificare)
                            {
                                produs.stoc += Convert.ToInt32(Quantity);
                            }
                        }
                    }
                    else if(Cart[Selected].cartStoc == Convert.ToInt32(Quantity))
                    {
                        Cart.Remove(Cart[Selected]);
                        foreach (Produs produs in shopStock)
                        {
                            if (produs.codIdentificare == Cart[Selected].codIdentificare)
                            {
                                produs.stoc += Cart[Selected].cartStoc;
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public static void SaveBarcodes(PictureBox pictureBox, Produs produs)
        {
            try
            {
                if(pictureBox == null)
                {
                    throw new Exception("Codul de bare nu exista");
                }

                DateTime dateTime = DateTime.Now;

                Bitmap bitmap = new Bitmap(pictureBox.Image);
                bitmap.Save("C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\CoduriDeBare\\" + produs.denumire + "-" + produs.codIdentificare + "-" + dateTime.Ticks + ".jpeg");

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Error: " + ex);
            }
        }
    }
}

