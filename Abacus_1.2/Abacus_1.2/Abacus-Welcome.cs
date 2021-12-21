using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Abacus_1._2
{
    public partial class Abacus_Welcome : Form
    {
        public Abacus_Welcome()
        {
            InitializeComponent();

            // Initializing Welcome
            InitializeWelcome();
        }

        private void Skip_intro_label_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(); // creating an instance of form1

            this.Visible = false;

            newForm.Visible = true;
        }

        private void Home_next_btn_Click(object sender, EventArgs e)
        {

            Default_panel.Visible = true; // display the next panel 

            AddMenuStrip(Default_panel);

        }

        private void Default_prev_btn_Click(object sender, EventArgs e)
        {
            Default_panel.Visible = false;

            Home_panel.Visible = true;

        }

        private void Default_next_btn_Click(object sender, EventArgs e)
        {
            Add_one_panel.Visible = true;
        }

        private void Add_one_prev_btn_Click(object sender, EventArgs e)
        {
            Add_one_panel.Visible = false;

            Default_panel.Visible = true;
        }

        private void Add_one_next_btn_Click(object sender, EventArgs e)
        {

            Add_5_panel.Visible = true;
        }

        private void Add5_prev_btn_Click(object sender, EventArgs e)
        {
            Add_5_panel.Visible = false;

            Add_one_panel.Visible = true;
        }

        private void Add5_next_btn_Click(object sender, EventArgs e)
        {
            Add20_panel.Visible = true;
        }

        private void Add20_prev_btn_Click(object sender, EventArgs e)
        {
            Add20_panel.Visible = false;

            Add_5_panel.Visible = true;
        }

        private void Add20_next_btn_Click(object sender, EventArgs e)
        {
            Add50_panel.Visible = true;
        }

        private void Add50_prev_btn_Click(object sender, EventArgs e)
        {
            Add50_panel.Visible = false;

            Add20_panel.Visible = true;
        }

        private void Add50_next_btn_Click(object sender, EventArgs e)
        {
            Rules_panel.Visible = true;
        }

        private void Rules_prev_btn_Click(object sender, EventArgs e)
        {
            Rules_panel.Visible = false;

            Add50_panel.Visible = true;
        }

        private void Rules_next_btn_Click(object sender, EventArgs e)
        {
            Substraction_panel.Visible = true;
        }

        private void Sub1_prev_btn_Click(object sender, EventArgs e)
        {
            Substraction_panel.Visible = false;

            Rules_panel.Visible = true;
        }

        private void Sub1_next_btn_Click(object sender, EventArgs e)
        {
            subtraction_2_panel.Visible = true;
        }

        private void Sub2_prev_btn_Click(object sender, EventArgs e)
        {
            subtraction_2_panel.Visible = false;

            Substraction_panel.Visible = true;
        }

        private void Finish_intro_btn_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(); // creating an instance of form1

            this.Visible = false;

            newForm.Visible = true;

            // if they finished intro
            WriteFile();
        }

        private void WriteFile()
        {
            using (StreamWriter finishedIntroFile = new StreamWriter("finishedIntro.txt"))
            {
                // write a success message 
                finishedIntroFile.WriteLine("Hello there, you have successfully completed " +
                    "the Ubacus introduction now you will be able to skip the intro next you start the application.");
            }
        }

        private void InitializeWelcome()
        {
            string myFile = "finishedIntro.txt";

            // check if the file exists 
            if (File.Exists(myFile))
            {
                // make the skip label visiable
                Skip_intro_label.Visible = true;
            }
            else
            {
                Skip_intro_label.Visible = false;
            }
        }

        private void AddMenuStrip(Panel myPanel)
        {
            myPanel.Controls.Add(menuStrip1);

            menuStrip1.Text = "what";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1(); // creating an instance of form1

            newForm.Visible = true;

            this.Visible = false;
        }
    }
}
