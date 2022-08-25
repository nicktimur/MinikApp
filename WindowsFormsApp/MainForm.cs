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
    public partial class MainForm : Form
    {
        public void bilgi_getir()
        {
            SQLiteConnection baglan = new SQLiteConnection();
            baglan.ConnectionString = ("Data Source = db/data.db");
            baglan.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM hesaplar", baglan);
            cmd.ExecuteNonQuery();
            SQLiteDataReader oku;
            oku = cmd.ExecuteReader();

            while (oku.Read())
            {
                isim.Text = oku["İsim"].ToString();
                soyisim.Text = oku["Soyisim"].ToString();
                yas.Text = oku["Yaş"].ToString();
            }
            baglan.Close();
        }
        public MainForm()
        {
            InitializeComponent();
            bilgi_getir();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label9.Visible = true;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            label9.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox3_MouseEnter(object sender, EventArgs e)
        {
            label8.Visible = true;
        }

        private void pictureBox3_MouseLeave(object sender, EventArgs e)
        {
            label8.Visible = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void isim_Click(object sender, EventArgs e)
        {

        }
        bool move;
        int mouse_x;
        int mouse_y;

        private void MainForm_MouseDown(object sender, MouseEventArgs e)
        {
            move = true;
            mouse_x = e.X;
            mouse_y = e.Y;

        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == true)
            {
                this.SetDesktopLocation(MousePosition.X - mouse_x, MousePosition.Y - mouse_y);
            }
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            move = false;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.Opacity = 96;
        }

    }
}
