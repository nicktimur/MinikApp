using System;
using System.Collections.Generic;
using System.ComponentModel;using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Net.Mail;
using System.Net;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace WindowsFormsApp
{   
   
    public partial class Form1 : Form
    {
        public int verify = 0;
        public string Post;
        public void IsVerify()
        {
            if (verify == 1)
            {
                dogrulama_form();
                Send_mail();
                PostBox.Text = Post;
            }
        }
        public Form1()
        {
            InitializeComponent();
            InitializeMyControl();
            pictureBox1.Left = 492;
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
        int code;
        public void Send_mail()
        {   
            Random rastgele = new Random();
            int kod = rastgele.Next(999,10000);
            SmtpClient sc = new SmtpClient();
            sc.Port = 587;
            sc.Host = "smtp.gmail.com";
            sc.EnableSsl = true;
            sc.UseDefaultCredentials = false;
            sc.Credentials = new NetworkCredential("minikappcheck@gmail.com", "vveiadmsahoqmwhp");
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("minikappcheck@gmail.com", "MinikApp Mail Checker");
            if (verify == 0)
            {
                mail.To.Add(PostBox.Text.ToLower());
                mail.Subject = "Doğrulama";
                mail.IsBodyHtml = true;
                mail.Body = "Merhaba Minik Kuş'un muhteşem uygulamasına gelişini kutluyoruz," + NameBox.Text + "." + "\nBu uygulamada SqLite veritananı kullanıldı ve içerisine FlappyBird oyunu var." + "\nBu oyunun skoru da bizzat senin hesabında görünecek." + "\nAma maalesef girmeden önce mail adresini kontrol etmeliyiz." + "\nMail adresini doğrulamak için bu kodu gerekli alana girin: " + kod;
            }
            else
            {
                mail.To.Add(Post);
                mail.Subject = "Doğrulama";
                mail.IsBodyHtml = true;
                mail.Body = "Merhaba Minik Kuş'un muhteşem uygulamasına gelişini kutluyoruz" + "." + "\nBu uygulamada SqLite veritananı kullanıldı ve içerisine FlappyBird oyunu var." + "\nBu oyunun skoru da bizzat senin hesabında görünecek." + "\nAma maalesef girmeden önce mail adresini kontrol etmeliyiz." + "\nMail adresini doğrulamak için bu kodu gerekli alana girin: " + kod;
            }
            sc.Send(mail);
            code = kod;
        }
        public void dogrulama_form()
        {
            label2.Visible = false;
            label9.Visible = false;
            label1.Visible = false;
            NameBox.Visible = false;
            label3.Visible = false;
            SurnameBox.Visible = false;
            AgeBox.Visible = false;
            PostBox.Visible = false;
            PassBox.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            button1.Visible = false;
            label11.Visible = true;
            pictureBox1.Left = 60;
            pictureBox2.Left = 414;
            label11.Left = 470;
            label10.Left = 561;
            kodBox.Left = 564;
            button2.Left = 596;
            label12.Left = 576;
            label11.Visible = true;
            label10.Visible = true;
            kodBox.Visible = true;
            button2.Visible = true;
            label12.Visible = true;
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
                        string sql = "insert into hesaplar(İsim,Soyisim,Yaş,Posta,Şifre,Puan,Doğrulanmış) VALUES(@isim,@soyad,@yas,@mail,@pass,@point,@verify)";
                        SQLiteCommand cmd = new SQLiteCommand(sql, baglan);
                        cmd.Parameters.Add(new SQLiteParameter("@isim", NameBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@soyad", SurnameBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@yas", AgeBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@mail", PostBox.Text.ToLower()));
                        cmd.Parameters.Add(new SQLiteParameter("@pass", PassBox.Text));
                        cmd.Parameters.Add(new SQLiteParameter("@point", "0"));
                        cmd.Parameters.Add(new SQLiteParameter("@verify", "0"));
                        cmd.ExecuteNonQuery();
                        baglan.Close();
                        MessageBox.Show("Kayıt Başarılı. Minik kuş Dünyasına Hoşgeldin, " + NameBox.Text + "");
                        Send_mail();
                        dogrulama_form();

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
        public class FormProvider
        {
            public static Form2 Form2
            {
                get
                {
                    if (_form2 == null)
                    {
                        _form2 = new Form2();
                    }
                    return _form2;
                }
            }
            private static Form2 _form2;
        }
        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            FormProvider.Form2.Show();
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
            IsVerify();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            Send_mail();
        }

        private void kodBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (kodBox.Text == code.ToString())
            {
                MessageBox.Show("Hesabınız başarıyla doğrulandı.", "İşlem Başarılı", MessageBoxButtons.OK);
                SQLiteConnection baglan = new SQLiteConnection();
                baglan.ConnectionString = ("Data Source = db/data.db");
                baglan.Open();
                string sql = "UPDATE hesaplar SET Doğrulanmış = '"+1+"' WHERE Posta = '"+ PostBox.Text.ToLower() + "' ";
                SQLiteCommand cmd = new SQLiteCommand(sql, baglan);
                cmd.ExecuteNonQuery();
                baglan.Close();
                this.Hide();
                a.Show();
            }
            else
            {
                MessageBox.Show("Kod doğrulanamadı.", "Başarısız işlem", MessageBoxButtons.OK);
                kodBox.Clear();
            }
        }
    }
}
