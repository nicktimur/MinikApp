using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            InitializeMyControl();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }
        public class FormProvider
        {
            public static Form1 Form1
            {
                get
                {
                    if (_form1 == null)
                    {
                        _form1 = new Form1();
                    }
                    return _form1;
                }
            }
            private static Form1 _form1;
        }
        private void label7_Click(object sender, EventArgs e)
        {

            this.Hide();
            FormProvider.Form1.Show();
        }
        public bool VerifyControl()
        {
            bool b = false;
            try
            {
                SQLiteConnection baglan = new SQLiteConnection();
                baglan.ConnectionString = ("Data Source = db/data.db");
                baglan.Open();
                SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM hesaplar Where Posta = '" + PostBox.Text.ToLower() + "' ", baglan);
                cmd.ExecuteNonQuery();
                SQLiteDataReader oku;
                oku = cmd.ExecuteReader();

                while (oku.Read())
                {
                    if (oku["Doğrulanmış"].ToString() == "1")
                    {
                        b = true;
                    }
                }
                baglan.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return b;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (PostBox.Text == "" | PassBox.Text == "")
            {
                MessageBox.Show("Alanları doldurmak zorunludur.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!this.PostBox.Text.Contains('@') || !this.PostBox.Text.Contains('.'))
            {
                MessageBox.Show("Lütfen Geçerli Bir E-Posta Adresi Giriniz", "Geçersiz E-Posta", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                try
                {
                    SQLiteConnection baglan = new SQLiteConnection();
                    baglan.ConnectionString = ("Data Source = db/data.db");
                    baglan.Open();
                    string sql = "SELECT * FROM hesaplar WHERE Posta=@posta AND Şifre=@şifre";
                    SQLiteParameter prm4 = new SQLiteParameter("@posta", PostBox.Text.ToLower());
                    SQLiteParameter prm5 = new SQLiteParameter("@şifre", PassBox.Text);
                    SQLiteCommand cmd = new SQLiteCommand(sql, baglan);
                    cmd.Parameters.Add(prm4);
                    cmd.Parameters.Add(prm5);
                    DataTable dt = new DataTable();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(cmd);
                    da.Fill(dt);
                    
                    if (dt.Rows.Count > 0)
                    {
                        baglan.Close();
                        if (VerifyControl() == true)
                        {
                            MainForm main = new MainForm();
                            main.Postbox = PostBox.Text.ToLower().ToString();
                            MessageBox.Show("Giriş Başarıyla Yapıldı." + "  Yönlendiriliyor. ", "Giriş Başarılı", MessageBoxButtons.OK);
                            PostBox.Clear();
                            PassBox.Clear();
                            this.Hide();
                            main.Show();
                        }
                        else
                        {
                            MessageBox.Show("Mail adresi doğrulanmamış.", "Hata", MessageBoxButtons.OK);
                            Form1 form1 = new Form1();
                            form1.verify = 1;
                            form1.Post = PostBox.Text;
                            form1.Show();
                            this.Hide();
                        }
                    }

                    else
                    {
                        MessageBox.Show("E-Posta Hesabınız Veya Şifreniz Hatalı.");
                        PassBox.Clear();
                    }
                    baglan.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            
        }
        private void InitializeMyControl()
        {
            // Set to no text.
            PassBox.Text = "";
            // The password character is an asterisk.
            PassBox.PasswordChar = '*';
            // The control will allow no more than 14 characters.
            PassBox.MaxLength = 14;
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Form2_Load(object sender, EventArgs e)
        {
            this.Opacity = 96;
        }


        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            label8.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label9.Visible = true;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            label8.Visible = true;
        }
        bool move;
        int mouse_x;
        int mouse_y;
        private void Form2_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;
        }

        private void Form2_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == true)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void Form2_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }
    }
}
