using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LaOaie
{
    public partial class LaOaie : Form
    {
        public User user;
        List<Produs> shopStock;

        public LaOaie()
        {
            InitializeComponent();
            shopStock = Magazin.Read();
            Magazin.DrawShop(shopStock, listShop, "");
        }

        //LogIn&&Register Page
        private void LogInR_Click(object sender, EventArgs e)
        {
            panelLog.Visible = true;
            panelRegister.Visible = false;
        }

        private void RegisterR_Click(object sender, EventArgs e)
        {
            new User(textBoxUsernameR.Text,textBoxPasswordR.Text,textBoxCPasswordR.Text);
            textBoxUsernameR.Text = "";
            textBoxPasswordR.Text = "";
            textBoxCPasswordR.Text = "";
            panelRegister.Visible = false;
            panelLog.Visible = true;
        }

        private void RegisterL_Click(object sender, EventArgs e)
        {
            panelRegister.Visible = true;
            panelLog.Visible = false;
        }

        private void LogInL_Click(object sender, EventArgs e)
        {
            user = new User(textBoxUsernameL.Text, textBoxPasswordL.Text);
            panelLog.Visible = false;
            if(user.Name == "" || user.Password == "")
            {
                panelLog.Visible = true;
            }
            else
            {   
                UsernameMenu.Text = user.Name;
                panelMainMenu.Visible = true;
                Logo.Visible = false;
            }
        }

        //MainMenu
        private void LogOut_Click(object sender, EventArgs e)
        {
            user.setCart();
            user.Cart = new List<Produs>();
            panelMainMenu.Visible = false;
            Logo.Visible = true;
            panelLog.Visible = true;
            panelCart.Visible = false;
            panelShop.Visible = true;
        }

        //Shop
        private void Shop_Click(object sender, EventArgs e)
        {
            panelShop.Visible = true;
            panelCart.Visible = false;
        }

        private void buttonAddCart_Click(object sender, EventArgs e)
        {
            Magazin.AddCart(user.Cart,comboBoxQuantity.Text, shopStock, listShop.SelectedIndex);
            
        }

        private void listShop_Click(object sender, EventArgs e)
        {
            comboBoxQuantity.Items.Clear();
            for (int i = 0; i < shopStock[listShop.SelectedIndex].stoc; i++)
            {
                comboBoxQuantity.Items.Add(i.ToString());
            }

            shopStock[listShop.SelectedIndex].CodDeBare(pictureBoxEAN);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Magazin.DrawShop(shopStock, listShop, textSearch.Text);
        }

        private void buttonSaveBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (pictureBoxEAN.Image == null)
                {
                    throw new Exception("Selectati un produs!");
                }

                Magazin.SaveBarcodes(pictureBoxEAN, shopStock[listShop.SelectedIndex]);

            }catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        //Cart
        private void Cart_Click(object sender, EventArgs e)
        {
            Magazin.DrawCart(user.Cart, listCart, labelTotal);
            user.setCart();
            panelShop.Visible = false;
            panelCart.Visible = true;
            labelUsername.Text = user.Name + "'s Cart";
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            Magazin.RmCart(user.Cart, shopStock, comboQuantity.Text, listCart.SelectedIndex);
            Magazin.DrawCart(user.Cart,listCart, labelTotal);
        }

        private void listCart_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                comboQuantity.Items.Clear();
                for (int i = 1; i <= user.Cart[listCart.SelectedIndex].cartStoc; i++)
                {
                    comboQuantity.Items.Add(i.ToString());
                }
            }catch
            {
                MessageBox.Show("Produsul nu se poate selecta!");
            }
        }

        private void buttonBuy_Click(object sender, EventArgs e)
        {
            try
            {
                if(listCart.Items.Count == 0)
                {
                    throw new Exception("Cosul este gol!");
                }

                Factura factura = new Factura(user);
                factura.Show();
                listCart.Items.Clear();
                user.Cart.Clear();
                Magazin.DrawCart(user.Cart, listCart, labelTotal);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        
    }
}
