using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;

namespace REHAN_TYPING
{
    public partial class REHAN_Project : Form
    {
        private Button t;
        private int next = -1;
        private int se;
        private int correct;
        private int incorrect;
        private float speed=-1;
        private float accuracy;


        public REHAN_Project()
        {
            InitializeComponent();
        }//end REHAN_Project

        private void REHAN_Project_Load(object sender, EventArgs e)
        {
            // initialize all controls and variables

            txtbox.BackColor = Color.DarkKhaki;
            textBox1.TabIndex = 0;
            se = 0;
            correct = 0;
            incorrect = 0;
            txtbox.Text = loadtext("LESSON1.txt");
            timer1.Interval = 1000;
            textBox1.Focus();
            timer1.Start();
        }//End REHAN Project Load

        private string loadtext(string filename) 
        {
            FileStream fs;
            StreamReader fr;
            fs = new FileStream(filename, FileMode.Open, FileAccess.Read);
            fr = new StreamReader(fs);
            string content = fr.ReadToEnd();
            fs.Close();
            return content;
        }// End LoadText

        private void reload(string filename)
        {
            try
            {
                t.BackColor = Color.DarkGray;
            }
            catch (Exception ex) { }

            next = -1;
            se = 0;
            correct = 0;
            incorrect = 0;
            txtcorrect.Text = "";
            txtincorrect.Text = "";
            timer1.Start();
            textBox1.Focus();
            txtbox.Clear();
            txtaccuracy.Clear();
            txtspeed.Clear();
            txttime.Clear();
            txtbox.Text = loadtext(filename);
        }//End reload

        private void btnrefresh_Click(object sender, EventArgs e)
        {
            reload("LESSON1.txt");
        }//End button refresh

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            char key = e.KeyChar;
            char[] c = txtbox.Text.ToCharArray();
            next++;
            //check number keys and letters keys
            //highligh character and button
            for (int i = 0; i < this.panel1.Controls.Count; i++)
            {
                if (this.panel1.Controls[i].Name.Substring(0, 4).ToLower().CompareTo("btn" + key.ToString().ToLower()) == 0 && this.panel1.Controls[i].Name.Length <= 4)
                    highligh((Button)this.panel1.Controls[i], c, next, key);

            }

            //check symbols: ',~!, @ #, $ %,...

            if ((int)key == 33) highligh(btn1, c, next, key);
            if ((int)key == 64) highligh(btn2, c, next, key);
            if ((int)key == 35) highligh(btn3, c, next, key);
            if ((int)key == 36) highligh(btn4, c, next, key);
            if ((int)key == 37) highligh(btn5, c, next, key);
            if ((int)key == 94) highligh(btn6, c, next, key);
            if ((int)key == 38) highligh(btn7, c, next, key);
            if ((int)key == 42) highligh(btn8, c, next, key);
            if ((int)key == 40) highligh(btn9, c, next, key);
            if ((int)key == 41) highligh(btn0, c, next, key);


            if ((int)key == 126 || (int)key == 96) highligh(btnaccent, c, next, key);

            if ((int)key == 123 || (int)key == 91) highligh(btnopenbrace, c, next, key);
            if ((int)key == 125 || (int)key == 93) highligh(btnclosebrace, c, next, key);
            if ((int)key == 58 || (int)key == 59) highligh(btnsemi, c, next, key);
            if ((int)key == 34 || (int)key == 39) highligh(btnquote, c, next, key);
            if ((int)key == 60 || (int)key == 44) highligh(btncomma, c, next, key);
            if ((int)key == 62 || (int)key == 46) highligh(btnpoint, c, next, key);
            if ((int)key == 63 || (int)key == 47) highligh(btnforwardslash, c, next, key);
            if ((int)key == 124 || (int)key == 92) highligh(btnbackslash, c, next, key);
            if ((int)key == 95 || (int)key == 45) highligh(btnminus, c, next, key);
            if ((int)key == 43 || (int)key == 61) highligh(btnequal, c, next, key);

            //check spacebar and enter keys
            if ((int)key == 32) highligh(btnspace, c, next, key);
            if ((int)key == 13) highligh(btnenter, c, next, key);

            //stop timer when the number of keypesses equal to the number of letters displayed in the box
            if (c.Length == next + 1)
            {
                timer1.Stop();
                txtcorrect.Text = correct.ToString();
                txtincorrect.Text = incorrect.ToString();
                accuracy = (  100* correct / (float)(correct + incorrect));
                txtaccuracy.Text = accuracy.ToString() + " %";
                speed = (float)(incorrect + correct)*60 / (se*5);
                txtspeed.Text = speed.ToString() + " WPM";
            }
        }//end TextBox1.KeyPress

        private void highligh(Button con, char[] c, int next, char key)
        {

            try
            {
                t.BackColor = Color.Black;
            }
            catch (Exception e) { }

            try
            {
                if ((int)key == (int)c[next])
                {

                    //check(key, Color.Green);
                    con.BackColor = Color.Green;
                    t = con;
                    txtbox.SelectionStart = next;
                    txtbox.SelectionLength = 1;
                    txtbox.SelectionColor = Color.Green;
                    correct++;
                    SystemSounds.Asterisk.Play();
                }
                else if ((int)key == 13 && (int)c[next] == 10)
                {
                    btnenter.BackColor = Color.Green;
                    t = btnenter;
                    correct++;
                    SystemSounds.Hand.Play();
                }
                else
                {
                    //check(key, Color.Red);
                    con.BackColor = Color.Red;
                    t = con;
                    txtbox.SelectionStart = next;
                    txtbox.SelectionLength = 1;
                    txtbox.SelectionColor = Color.Red;
                    incorrect++;
                    SystemSounds.Exclamation.Play();
                }

            }
            catch (Exception e) { }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            se += 1;
            txttime.Text = se.ToString();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void droplesson_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(e.Equals("LESSON2"));
                reload("LESSON2.txt");
            if(e.Equals("LESSON2"));
               reload("LESSON2.txt");  
            if(e.Equals("LESSON3"));
                reload("LESSON3.txt");
            if(e.Equals("LESSON4"));
                reload("LESSON4.txt");
            if(e.Equals("LESSON5"));
                reload("LESSON5.txt");
        }

        private void txtbox_TextChanged(object sender, EventArgs e)
        {

        }



    }
}

