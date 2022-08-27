using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IdentityModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class Form3 : Form
    {

        // coded for MOO ICT Flappy Bird Tutorial

        // Variables start here
        public string Postbox = "";
        int pipeSpeed = 8; // default pipe speed defined with an integer
        int gravity = 15; // default gravity speed defined with an integer
        private Timer gameTimer;
        private IContainer components;
        private PictureBox flappyBird;
        private PictureBox pipeTop;
        private PictureBox pipeBottom;
        private Label scoreText;
        private PictureBox ground;
        private PictureBox pictureBox1;
        private Label intscore;
        int score = 0; // default score integer set to 0
        // variable ends

        public Form3()
        {
            InitializeComponent();
        }

        private void gamekeyisdown(object sender, KeyEventArgs e)
        {
            // this is the game key is down event thats linked to the main form
            if (e.KeyCode == Keys.Space)
            {
                // if the space key is pressed then the gravity will be set to -15
                gravity = -15;
            }


        }

        private void gamekeyisup(object sender, KeyEventArgs e)
        {
            // this is the game key is up event thats linked to the main form

            if (e.KeyCode == Keys.Space)
            {
                // if the space key is released then gravity is set back to 15
                gravity = 15;
            }

        }

        private void endGame()
        {
            // this is the game end function, this function will when the bird touches the ground or the pipes
            SQLiteConnection baglan = new SQLiteConnection();
            baglan.ConnectionString = ("Data Source = db/data.db");
            baglan.Open();
            SQLiteCommand cmd = new SQLiteCommand("SELECT * FROM hesaplar Where Posta = '" + Postbox + "' ", baglan);
            cmd.ExecuteNonQuery();
            SQLiteDataReader oku;
            oku = cmd.ExecuteReader();
            int maxpuan = 0;
            while (oku.Read())
            {
                Int32.TryParse(oku["Puan"].ToString(), out maxpuan);
            }
            baglan.Close();
            if (maxpuan<score)
            {
                SQLiteConnection baglan2 = new SQLiteConnection();
                baglan2.ConnectionString = ("Data Source = db/data.db");
                baglan2.Open();
                string sql2 = "UPDATE hesaplar SET Puan = '" + score + "' WHERE Posta = '" + Postbox + "' ";
                SQLiteCommand cmdd = new SQLiteCommand(sql2, baglan2);
                cmdd.ExecuteNonQuery();
            }
          
            gameTimer.Stop(); // stop the main timer
            intscore.Text += " Oyun Bitti!!!"; // show the game over text on the score text, += is used to add the new string of text next to the score instead of overriding it
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            flappyBird.Top += gravity; // link the flappy bird picture box to the gravity, += means it will add the speed of gravity to the picture boxes top location so it will move down
            pipeBottom.Left -= pipeSpeed; // link the bottom pipes left position to the pipe speed integer, it will reduce the pipe speed value from the left position of the pipe picture box so it will move left with each tick
            pipeTop.Left -= pipeSpeed; // the same is happening with the top pipe, reduce the value of pipe speed integer from the left position of the pipe using the -= sign
            intscore.Text =  score.ToString(); // show the current score on the score text label

            // below we are checking if any of the pipes have left the screen

            if (pipeBottom.Left < -150)
            {
                // if the bottom pipes location is -150 then we will reset it back to 800 and add 1 to the score
                pipeBottom.Left = 800;
                score++;
            }
            if (pipeTop.Left < -180)
            {
                // if the top pipe location is -180 then we will reset the pipe back to the 950 and add 1 to the score
                pipeTop.Left = 950;
                score++;
            }

            // the if statement below is checking if the pipe hit the ground, pipes or if the player has left the screen from the top
            // the two pipe symbols stand for OR inside of an if statement so we can have multiple conditions inside of this if statement because its all going to do the same thing

            if (flappyBird.Bounds.IntersectsWith(pipeBottom.Bounds) ||
                flappyBird.Bounds.IntersectsWith(pipeTop.Bounds) ||
                flappyBird.Bounds.IntersectsWith(ground.Bounds) || flappyBird.Top < -25
                )
            {
                // if any of the conditions are met from above then we will run the end game function
                endGame();
            }


            // if score is greater then 5 then we will increase the pipe speed to 15
            if (score > 5)
            {
                pipeSpeed = 15;
            }

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gameTimer = new System.Windows.Forms.Timer(this.components);
            this.scoreText = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ground = new System.Windows.Forms.PictureBox();
            this.pipeBottom = new System.Windows.Forms.PictureBox();
            this.pipeTop = new System.Windows.Forms.PictureBox();
            this.flappyBird = new System.Windows.Forms.PictureBox();
            this.intscore = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipeBottom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipeTop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flappyBird)).BeginInit();
            this.SuspendLayout();
            // 
            // gameTimer
            // 
            this.gameTimer.Enabled = true;
            this.gameTimer.Interval = 20;
            this.gameTimer.Tick += new System.EventHandler(this.gameTimerEvent);
            // 
            // scoreText
            // 
            this.scoreText.AutoSize = true;
            this.scoreText.BackColor = System.Drawing.Color.Moccasin;
            this.scoreText.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.scoreText.Location = new System.Drawing.Point(181, 661);
            this.scoreText.Name = "scoreText";
            this.scoreText.Size = new System.Drawing.Size(91, 37);
            this.scoreText.TabIndex = 7;
            this.scoreText.Text = "Puan:";
            this.scoreText.Click += new System.EventHandler(this.scoreText_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Moccasin;
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::WindowsFormsApp.Properties.Resources.return_button;
            this.pictureBox1.Location = new System.Drawing.Point(546, 656);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(49, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ground
            // 
            this.ground.Image = global::WindowsFormsApp.Properties.Resources.ground;
            this.ground.Location = new System.Drawing.Point(-16, 633);
            this.ground.Name = "ground";
            this.ground.Size = new System.Drawing.Size(655, 126);
            this.ground.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.ground.TabIndex = 6;
            this.ground.TabStop = false;
            this.ground.Click += new System.EventHandler(this.ground_Click);
            // 
            // pipeBottom
            // 
            this.pipeBottom.Image = global::WindowsFormsApp.Properties.Resources.pipe;
            this.pipeBottom.Location = new System.Drawing.Point(362, 418);
            this.pipeBottom.Name = "pipeBottom";
            this.pipeBottom.Size = new System.Drawing.Size(109, 286);
            this.pipeBottom.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pipeBottom.TabIndex = 5;
            this.pipeBottom.TabStop = false;
            // 
            // pipeTop
            // 
            this.pipeTop.Image = global::WindowsFormsApp.Properties.Resources.pipedown;
            this.pipeTop.Location = new System.Drawing.Point(495, -59);
            this.pipeTop.Name = "pipeTop";
            this.pipeTop.Size = new System.Drawing.Size(100, 266);
            this.pipeTop.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pipeTop.TabIndex = 4;
            this.pipeTop.TabStop = false;
            // 
            // flappyBird
            // 
            this.flappyBird.Image = global::WindowsFormsApp.Properties.Resources.bird;
            this.flappyBird.Location = new System.Drawing.Point(69, 228);
            this.flappyBird.Name = "flappyBird";
            this.flappyBird.Size = new System.Drawing.Size(82, 69);
            this.flappyBird.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.flappyBird.TabIndex = 3;
            this.flappyBird.TabStop = false;
            // 
            // intscore
            // 
            this.intscore.AutoSize = true;
            this.intscore.BackColor = System.Drawing.Color.Moccasin;
            this.intscore.Font = new System.Drawing.Font("Arial Narrow", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.intscore.Location = new System.Drawing.Point(263, 661);
            this.intscore.Name = "intscore";
            this.intscore.Size = new System.Drawing.Size(32, 37);
            this.intscore.TabIndex = 9;
            this.intscore.Text = "0";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Aqua;
            this.ClientSize = new System.Drawing.Size(622, 707);
            this.Controls.Add(this.intscore);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.scoreText);
            this.Controls.Add(this.ground);
            this.Controls.Add(this.pipeBottom);
            this.Controls.Add(this.pipeTop);
            this.Controls.Add(this.flappyBird);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form3";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Flappy Bird Game";
            this.Load += new System.EventHandler(this.Form3_Load_1);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gamekeyisdown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gamekeyisup);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ground)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipeBottom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pipeTop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flappyBird)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void scoreText_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void ground_Click(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm MMenu = new MainForm();
            MMenu.Show();
            MMenu.Postbox = Postbox.ToString();
        }

        private void Form3_Load_1(object sender, EventArgs e)
        {

        }
    }
}
