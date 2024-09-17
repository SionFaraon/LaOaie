using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaOaie
{
    public partial class Factura : Form
    {
        private User _user;
        private PrintDocument printDocument = new PrintDocument();

        public Factura(User user)
        {
            InitializeComponent();
            _user = user;
            printDocument.PrintPage += PrintImage;
        }

        private void PrintImage(object sender, PrintPageEventArgs e)
        {
            if(pictureBoxFactura != null)
            {
                e.Graphics.DrawImage(pictureBoxFactura.Image, pictureBoxFactura.Width, pictureBoxFactura.Height);
            }
        }

        private void Factura_Load(object sender, EventArgs e)
        {
            Facturi.GenFactura(_user, pictureBoxFactura);
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            Facturi.Save(pictureBoxFactura);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;
            if(printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }
    }
}
