using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaOaie
{
    public class Produs
    {
        public int stoc {  get; set; }
        public String codIdentificare { get; set; }
        public String denumire { get; set; }
        public String descriere { get; set; }
        public double pret { get; set; }
        public int cartStoc {  get; set; }

        public Produs(String codIdentificare, string denumire, string descriere, double pret, int stoc, int cartStoc)
        {
            this.codIdentificare = codIdentificare;
            this.denumire = denumire;
            this.descriere = descriere;
            this.pret = pret;
            this.stoc = stoc;
            this.cartStoc = cartStoc;
        }

        public void CodDeBare(PictureBox EAN)
        {
            try
            {
                int Si = 0;
                for(int i = 0; i < this.codIdentificare.Length - 1; i+=2)
                {
                    Si += (int)char.GetNumericValue(this.codIdentificare[i]);
                }
                Si *= 3;
                int Sp = 0;
                for (int p = 1; p < this.codIdentificare.Length - 1; p += 2)
                {
                    Sp += (int)char.GetNumericValue(this.codIdentificare[p]);
                }
                if ((10 - (Si + Sp) % 10) % 10 != (int)char.GetNumericValue(this.codIdentificare[this.codIdentificare.Length - 1])) { 
                    throw new Exception("Codul de bare nu este valid");
                }
                else
                {
                    String[,] tabel = new String[10, 3]
                    {
                            {"0001101","0100111","1110010" },
                            {"0011001","0110011","1100110" },
                            {"0010011","0011011","1101100" },
                            {"0111101","0100001","1000010" },
                            {"0100011","0011101","1011100" },
                            {"0110001","0111001","1001110" },
                            {"0101111","0000101","1010000" },
                            {"0111011","0010001","1000100" },
                            {"0110111","0001001","1001000" },
                            {"0001011","0010111","1110100" },
                    };

                    if (this.codIdentificare.Length == 8)
                    {
                        StringBuilder sb = new StringBuilder("101");
                        for (int i = 0; i < 4; i++)
                            sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                        sb.Append("01010");
                        for (int i = 4; i < 8; i++)
                            sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 2]);
                        sb.Append("101");
                        String cod = sb.ToString();
                        Bitmap bitmap = new Bitmap(EAN.Width,EAN.Height);
                        Graphics graphics = Graphics.FromImage(bitmap);
                        graphics.Clear(Color.White);
                        float grosimeLinie = EAN.Width/(float)cod.Length;
                        Pen white = new Pen(Color.White, grosimeLinie);
                        Pen black = new Pen(Color.Black, grosimeLinie);

                        for(int i = 0; i < cod.Length; i++)
                        {
                            if ((int)char.GetNumericValue(cod[i]) == 0)
                            {
                                graphics.DrawLine(white, grosimeLinie * i, 0, grosimeLinie * i, EAN.Height);
                            }
                            else
                            {
                                graphics.DrawLine(black, grosimeLinie * i, 0, grosimeLinie * i, EAN.Height);
                            }
                        }
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point(20, 65), new Size(60, 30)));
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point(100, 65), new Size(60, 30)));
                        graphics.DrawString(this.codIdentificare.Substring(0, 4) + "    " + this.codIdentificare.Substring(4, 4), new Font("Arial", 18, FontStyle.Regular), Brushes.Black, new Point(20, 60));
                        EAN.Image = bitmap;
                        graphics.Dispose();

                    }else if(this.codIdentificare.Length == 13)
                    {
                        StringBuilder sb = new StringBuilder("101");
                        switch ((int)char.GetNumericValue(this.codIdentificare[0])){
                            case '0':
                                //LLLLLL
                                for (int i = 1; i < 7; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                break;
                            case '1':
                                //LLGLGG
                                for (int i = 1; i < 3; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[3]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[4]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[5]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 1]);
                                break;
                            case '2':
                                //LLGGLG
                                for (int i = 1; i < 3; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                for (int i = 3; i < 5; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[5]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 1]);
                                break;
                            case '3':
                                //LLGGGL
                                for (int i = 1; i < 3; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                for (int i = 3; i < 6; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 0]);
                                break;
                            case '4':
                                //LGLLGG
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[2]), 1]);
                                for (int i = 3; i < 5; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                for (int i = 5; i < 7; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                break;
                            case '5':
                                //LGGLLG
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                for (int i = 2; i < 4; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                for (int i = 4; i < 6; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 0]);
                                break;
                            case '6':
                                //LGGGLL
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                for (int i = 2; i < 5; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                for (int i = 5; i < 7; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 0]);
                                break;
                            case '7':
                                //LGLGLG
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[2]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[3]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[4]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[5]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 1]);
                                break;
                            case '8':
                                //LGLGGL
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[2]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[3]), 0]);
                                for (int i = 4; i < 6; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 0]);
                                break;
                            case '9':
                                //LGGLGL
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[1]), 0]);
                                for (int i = 2; i < 4; i++)
                                    sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[4]), 0]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[5]), 1]);
                                sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[6]), 0]);
                                break;
                        }
                        sb.Append("01010");
                        //RRRRRR
                        for (int i = 1; i < 7; i++)
                            sb.Append(tabel[(int)char.GetNumericValue(this.codIdentificare[i]), 2]);
                        sb.Append("101");
                        String cod = sb.ToString();
                        Bitmap bitmap = new Bitmap(EAN.Width, EAN.Height);
                        Graphics graphics = Graphics.FromImage(bitmap);
                        graphics.Clear(Color.White);
                        float grosimeLinie = EAN.Width / (float)cod.Length;
                        Pen white = new Pen(Color.White, grosimeLinie);
                        Pen black = new Pen(Color.Black, grosimeLinie);

                        for (int i = 0; i < cod.Length; i++)
                        {
                            if ((int)char.GetNumericValue(cod[i]) == 0)
                            {
                                graphics.DrawLine(white, grosimeLinie * i, 0, grosimeLinie * i, EAN.Height);
                            }
                            else
                            {
                                graphics.DrawLine(black, grosimeLinie * i, 0, grosimeLinie * i, EAN.Height);
                            }
                        }
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point(0, 65), new Size(15, 30)));
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point(20, 65), new Size(70, 30)));
                        graphics.FillRectangle(Brushes.White, new Rectangle(new Point(100, 65), new Size(70, 30)));
                        graphics.DrawString(this.codIdentificare[0] + "  " + this.codIdentificare.Substring(1, 6) + "   " + this.codIdentificare.Substring(6, 6), new Font("Arial", 14, FontStyle.Regular), Brushes.Black, new Point(0, 62));
                        EAN.Image = bitmap;
                        graphics.Dispose();
                    }
                    else
                    {
                        throw new Exception("Codul de bare nu se poate genera");
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public virtual String Afisare()
        {
            String inCart;
            inCart = this.pret + "ron (" + this.cartStoc + ") \t" + this.denumire;
            return inCart;
        }
    }

}
