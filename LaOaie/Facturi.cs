using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaOaie
{
    public class Facturi
    {
        static public void GenFactura(User user, PictureBox pictureBox)
        {
            Bitmap bitmap = new Bitmap(pictureBox.Width, pictureBox.Height);
            Graphics g = Graphics.FromImage(bitmap);
            g.Clear(Color.White);
            g.DrawImage(Image.FromFile("C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Logo\\LogoMenu.png"),15,15,120,120);

            //Nume Furnizor si client
            Pen pen = new Pen(Color.LightYellow);
            g.DrawString("Furnizor : LaOaie \t Nume Client: " + user.Name, new Font("Arial", 15, FontStyle.Regular),Brushes.Black, new Point(15,140));

            double pretFaraTVA = 0, pretTVA = 0, pretTotal = 0;
            DateTime dateTime = DateTime.Now;

            //Numarul facturii
            int nr = 1;
            string[] files = Directory.GetFiles($"C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Facturi\\", "Factura*.jpeg");
            foreach (string file in files)
            {
                string filename = Path.GetFileNameWithoutExtension(file);
                string number = filename.Substring(7);

                if (int.TryParse(number, out int filenr))
                {
                    nr = Math.Max(nr, filenr + 1);
                }
            }

            //nr, data, cota
            g.FillRectangle(Brushes.LightYellow, new Rectangle(new Point(15, 170), new Size(530, 25)));
            g.DrawString("Factura nr.: " + nr + "  data: " + dateTime.Day + "/" + dateTime.Month + "/" + dateTime.Year + "\t TVA: 19%", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(15, 170));

            //Tabel
            g.FillRectangle(Brushes.LightYellow, new Rectangle(new Point(15, 220), new Size(530, 40)));
            g.DrawString("Total    TVA    Pret    Cant.     Denumire", new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(15, 225));
            for(int i = 1; i<=user.Cart.Count; i++)
            {
                if (i % 2 == 0)
                    g.FillRectangle(Brushes.WhiteSmoke, new Rectangle(new Point(15, 220 + (30 * i)), new Size(530, 25)));
                else
                    g.FillRectangle(Brushes.White, new Rectangle(new Point(15, 220 + (30 * i)), new Size(530, 25)));
                double pret = user.Cart[i-1].pret;
                pretFaraTVA += pret;
                double tva = (19.0 / 100) * pret;
                pretTVA += tva;
                int cantitate = user.Cart[i-1].cartStoc;
                pretTotal += pret + tva;
                double total = tva + (cantitate * pret);
                g.DrawString(total + "\t" + tva + "\t" + pret + "\t" + cantitate + "\t" + user.Cart[i - 1].denumire, new Font("Arial", 13, FontStyle.Regular), Brushes.Black, new Point(15, 220 + (30 * i)));
            }

            //All Totals
            g.FillRectangle(Brushes.LightYellow, new Rectangle(new Point(15, 270+(30*user.Cart.Count)), new Size(250, 90)));
            g.DrawString("Pret fara TVA: " + pretFaraTVA, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(15, 270 + (30 * user.Cart.Count)));
            g.DrawString("Valoare TVA: " + pretTVA, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(15, 300 + (30 * user.Cart.Count)));
            g.DrawString("Total de plata: " + pretTotal, new Font("Arial", 15, FontStyle.Regular), Brushes.Black, new Point(15, 330 + (30 * user.Cart.Count)));

            pictureBox.Image = bitmap;
            g.Dispose();
        }

        static public void Save(PictureBox pictureBox)
        {
            try
            {
                if (pictureBox == null)
                {
                    throw new Exception("Picture Box null");
                }

                int nr = 1;
                string[] files = Directory.GetFiles($"C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Facturi\\", "Factura*.jpeg");
                foreach(string file in files)
                {
                    string filename = Path.GetFileNameWithoutExtension(file);
                    string number = filename.Substring(7);
                    
                    if(int.TryParse(number, out int filenr))
                    {
                        nr = Math.Max(nr, filenr + 1);
                    }
                }

                Bitmap bitmap = new Bitmap(pictureBox.Image);
                bitmap.Save($"C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Facturi\\Factura{nr}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);

            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
