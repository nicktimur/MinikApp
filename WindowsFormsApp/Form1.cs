using System;
using System.Collections.Generic;
using System.ComponentModel;using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{   
   
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            InitializeMyControl();
        }
        public bool MailControl(string mail)
        {
            bool b = false;
            try
            {
                SQLiteConnection baglan = new SQLiteConnection();
                baglan.ConnectionString = ("Data Source = db/data.db");
                baglan.Open();
                string sql = "select count(*)   from hesaplar where Posta = @mail";
                SQLiteCommand cmd = new SQLiteCommand(sql, baglan); 
                cmd.Parameters.Add(new SQLiteParameter("@mail", PostBox.Text));
                var t = cmd.ExecuteScalar();
               b= Convert.ToInt32(t.ToString()) ==0 ? true : false;  
                baglan.Close();
            }
                catch (Exception)
            {

                throw;
            }
            return b;

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }
        public bool IsNumeric(string value)
        {
            return value.All(char.IsNumber);
        }
        Form2 a = new Form2();
        private void button1_Click(object sender, EventArgs e)
        {
            if (NameBox.Text == "" | SurnameBox.Text == "" | AgeBox.Text == "" | PostBox.Text == "" | PassBox.Text == "")
            {
                MessageBox.Show("Alanları doldurmak zorunludur.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (!IsNumeric(AgeBox.Text))
            {
                MessageBox.Show("Yaş Sayısal Değerler Almalıdır", "Yaş Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if (MailControl(PostBox.Text))
                    {
                        string sql = "insert into hesaplar(İsim,Soyisim,Yaş,Posta,Şifre,Puan) VALUES(@isim,@soyad,@yas,@mail,@pass,@point)";
                        SQLiteCommand cmd = new SQLiteCommand(sql, baglan);
                        cmd.Parameters.Add(new SQLiteParameter("@isim", NameBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@soyad", SurnameBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@yas", AgeBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@mail", PostBox.Text.ToLower()));
                        cmd.Parameters.Add(new SQLiteParameter("@pass", PassBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@point", "0"));
                        cmd.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı. Minikkuş Dünyasına Hoşgeldin, " + NameBox.Text + "");
                        NameBox.Clear();
                        SurnameBox.Clear();
                        AgeBox.Clear();
                        PostBox.Clear();
                        PassBox.Clear();
                        this.Hide();
                        a.Show();
                    }
                    else
                    {
                        MessageBox.Show("Bu mail kullanılmış", "Hata", MessageBoxButtons.OK,MessageBoxIcon.Error);
                    }
                    
                }
                catch(Exception ec )
                {
                    MessageBox.Show($"Hata : {ec.Message.ToString()}");
                }
            }
        }

        private void AgeBox_TextChanged(object sender, EventArgs e)
        {

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
        private void PassBox_TextChanged(object sender, EventArgs e)
        {
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            a.Show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_MouseMove(object sender, MouseEventArgs e)
        {
            label8.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void label2_MouseMove(object sender, MouseEventArgs e)
        {
            label9.Visible = true;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        bool move;
        int mouse_x;
        int mouse_y;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == true)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Opacity = 96;
        }
    }
}
