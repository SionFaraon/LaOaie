using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LaOaie
{
    public class User
    {
        public String Name { get; }
        public String Password {  get; }
        public List<Produs> Cart { get; set; }

        public User(String Username, String password) 
        {
            try
            {
                if (Username == "" || password == "")
                {
                    this.Name = "";
                    this.Password = "";
                    throw new ArgumentException("Trebuie sa adaugi un nume si o parola");
                }
                else
                {
                    bool find = false;
                    String[] files = Directory.GetFiles("C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Users", "*.txt");
                    foreach (String file in files)
                    {
                        if(file.Split('\\')[13].Split('.')[0] == Username)
                        {   
                            find = true;
                            FileStream fs = new FileStream(file, FileMode.Open, FileAccess.Read);
                            StreamReader sr = new StreamReader(fs);
                            String var = sr.ReadLine().Split(':')[1];
                            if(var == password)
                            {
                                Name = Username;
                                Password = password;
                                Cart = new List<Produs>();
                                var = sr.ReadLine();
                                while (var != null)
                                {
                                    List<Produs> allProdus = Magazin.Read();

                                    String cod = var.Split('/')[0];
                                    int cart = Convert.ToInt32(var.Split('/')[1]); 

                                    foreach(Produs produs in allProdus)
                                    {
                                        if (produs.codIdentificare.Equals(cod))
                                        {
                                            produs.cartStoc = cart;
                                            this.Cart.Add(produs);
                                        }
                                    }
                                    var = sr.ReadLine();
                                }
                            }
                            else
                            {
                                this.Name = "";
                                this.Password = "";
                                throw new ArgumentException("Parola invalida!");
                            }
                            sr.Close();
                            fs.Close();
                        }
                        
                    }
                    if(find == false)
                    {
                        this.Name = "";
                        this.Password = "";
                        throw new ArgumentException("Usernameul " + Username + " nu exista!");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public User(String name, String password, String Cpassword)
        {
            try
            {
                if (name == "" || password == "")
                {
                    throw new ArgumentException("Trebuie sa adaugi un nume si o parola");
                }
                else
                {
                    String[] files = Directory.GetFiles("C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Users", "*.txt");
                    foreach (String file in files)
                    {
                        if (file.Split('\\')[13].Split('.')[0] == name)
                        {
                            throw new ArgumentException("Usernameul " + name + " exista deja!");
                        }
                    }

                    if (password != Cpassword)
                    {
                        throw new ArgumentException("Confirmare Esuata");
                    }
                    else
                    {
                        Name = name;
                        Password = password;

                        FileStream fs = new FileStream($"C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Users\\{name}.txt", FileMode.Create, FileAccess.Write);
                        StreamWriter writer = new StreamWriter(fs);
                        writer.WriteLine($"{name}:{password}");
                        writer.Close();
                        fs.Close();
                    }
                }
            }catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        public void setCart()
        {
            String path = $"C:\\Users\\alins\\OneDrive\\Desktop\\Programe\\Programe cpp\\OOP\\Proiecte\\LaOaie\\bin\\Debug\\Users\\{this.Name}.txt";
            String userID = File.ReadLines(path).First();

            File.WriteAllText(path, string.Empty);

            FileStream fs = null;
            StreamWriter wr = null;
            try
            {
                fs = new FileStream(path, FileMode.Open);
                wr = new StreamWriter(fs);

                wr.WriteLine(userID);

                foreach(Produs produs in this.Cart)
                {
                    wr.WriteLine($"{produs.codIdentificare}/{produs.cartStoc}");
                }

                if(wr != null) wr.Close();
                if(fs != null) fs.Close();

            }catch (Exception ex)
            {
                if (wr != null) wr.Close();
                if (fs != null) fs.Close();

                MessageBox.Show("Error: " + ex.Message);
            }
        }

    }
}
