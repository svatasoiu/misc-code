using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

//made by Sorin Vatasoiu Jr.

namespace PsychExperimentProgram
{
    public partial class Form1 : Form
    {
        PsychStringData psychData = new PsychStringData(); //class PsychStringData is defined in PsychStringData.cs

        //default initaliazer, it essentially loads the GUI, the visual interface
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {}

        private void button1_Click(object sender, EventArgs e) //Calculate and Save Button
        {
            string standardTxt = textBox1.Text;
            int lenStandTxt = standardTxt.Length;

            if (pictureBox2.Visible == true) //if the green box is visible, the input string is the right length, so add the entry to the database
            {
                //textBox1.Text = standard string; textBox2.Text = input string;  Convert.ToInt32(label6.Text) = number correct
                //(listBox1.SelectedItem != null) ? listBox1.SelectedItem.ToString() : ""; logic used to determine the which experimental group is selected
                // the ?: statement is an obscure construction, look it up. I think it's called a conditional operator
                psychData.AddEntry(textBox1.Text, textBox2.Text, Convert.ToInt32(label6.Text), (listBox1.SelectedItem != null) ? listBox1.SelectedItem.ToString() : "");
                label7.Text = "Entry Added!";
                textBox2.Text = "";
            }
            else
            {
                label7.Text = "Incorrect Length!";
            }
        }

        //this function runs everytime something is typed into the return string textbox
        private void textBox2_TextChanged(object sender, EventArgs e) 
        {
            int lenTxt = textBox2.Text.Length;
            label3.Text = lenTxt.ToString();

            //if the input is the same length as the standard, change the box to green; otherwise it is red
            if (lenTxt == textBox1.Text.Length)
            {
                pictureBox1.Visible = false;
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox2.Visible = false;
                pictureBox1.Visible = true;
            }

            //the following calculates how many digits are in the correct position
            int correctPos = 0;
            for (int i = 0; i < Math.Min(lenTxt,textBox1.Text.Length); i++)
            {
                if (textBox1.Text[i] == textBox2.Text[i])
                {
                    correctPos++;
                }
            }
            label6.Text = correctPos.ToString();
            correctPos = 0;
        }

        private void button2_Click(object sender, EventArgs e) //update the grid
        {
            dataGridView1.DataSource = psychData.displayTable();
        }

        Random r = new Random();

        private void button3_Click(object sender, EventArgs e) //generate random sequence button
        {
            string s = "";
            //textBox4.Text is the number of digits in the sequence
            for (int i = 0; i < Convert.ToInt32(textBox4.Text); i++)
            {
                s += r.Next(1, 3).ToString();
            }
            textBox3.Text = s;
            label10.Text = "1's: " + countDigit(1, s).ToString(); //display the number of 1's in the sequence
            label11.Text = "2's: " + countDigit(2, s).ToString(); //display the number of 2's in the sequence
        }

        private int countDigit(int digit, string s) //counts how many occurances of 'digit' there are in 's'
        {
            int count = 0;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToString() == digit.ToString())
                {
                    count++;
                }
            }
            return count;
        }
    }
}
