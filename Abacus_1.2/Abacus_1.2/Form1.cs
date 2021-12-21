using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Abacus_1._2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        const int COLUMNS = 10;
        const int HEAVEN_ROWS = 3;
        const int EARTH_ROWS = 6;

        // heaven and earth column values
        long[] heavenValues = new long[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        long[] earthValues = new long[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        long[] columnValues = new long[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        // total counts
        long heavenTotal = 0;
        long earthTotal = 0;
        long total = 0;

        // flags for determining mode
        bool additionSubration = true;
        bool multiplication = false;
        bool division = false;

        // flag for multiplication error
        bool invalidExpression = false;

        // division guides
        string[] division0 = new string[] { "Ds", "Qu", "Dd", "", "", "", "", "", "", "" };
        string[] division1 = new string[] { "Ds", "Qu", "Qu", "Dd", "Dd", "", "", "", "", "" };
        string[] division2 = new string[] { "Ds", "Qu", "Qu", "Qu", "Dd", "Dd", "Dd", "", "", "" };
        string[] division3 = new string[] { "Ds", "Qu", "Qu", "Qu", "Qu", "Dd", "Dd", "Dd", "Dd", "" };
        string[] division4 = new string[] { "Ds", "Ds", "Qu", "Dd", "Dd", "", "", "", "", "" };
        string[] division5 = new string[] { "Ds", "Ds", "Qu", "Qu", "Dd", "Dd", "Dd", "", "", "" };
        string[] division6 = new string[] { "Ds", "Ds", "Qu", "Qu", "Qu", "Dd", "Dd", "Dd", "Dd", "" }; // new
        string[] division7 = new string[] { "Ds", "Ds", "Ds", "Qu", "Dd", "Dd", "Dd", "", "", "" };
        string[] division8 = new string[] { "Ds", "Ds", "Ds", "Qu", "Qu", "Dd", "Dd", "Dd", "Dd", "" }; // new
        string[] division9 = new string[] { "Ds", "Ds", "Ds", "Ds", "Qu", "Dd", "Dd", "Dd", "Dd", "" }; // new


        // default abacus values
        long[] HEAVEN_VALUES = new long[] { 5, 50, 500, 5000, 50000, 500000, 5000000, 50000000, 500000000, 5000000000 };
        long[,] EARTH_VALUES = new long[10, 5] {{1, 2, 3, 4, 5}, {10, 20, 30, 40, 50}, {100, 200, 300, 400, 500}, {1000, 2000, 3000, 4000, 5000}, {10000, 20000, 30000, 40000, 50000}, {100000, 200000, 300000, 400000, 500000}, {1000000, 2000000, 3000000, 4000000, 5000000}, {10000000, 20000000, 30000000, 40000000, 50000000}, {100000000, 200000000, 300000000, 400000000, 500000000}, {1000000000, 2000000000, 3000000000, 4000000000, 5000000000}};

        // multiplication and division abacus values
        long HEAVEN_VALUES_MULT = 5;
        long[] EARTH_VALUES_MULT = new long[] { 1, 2, 3, 4, };

        PictureBox[,] heavenBeads;
        PictureBox[,] earthBeads;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.heavenBeads = new PictureBox[,] { {heavenButton00, heavenButton01, heavenButton02 },
                                                   {heavenButton10, heavenButton11, heavenButton12 },
                                                   {heavenButton20, heavenButton21, heavenButton22 },
                                                   {heavenButton30, heavenButton31, heavenButton32 },
                                                   {heavenButton40, heavenButton41, heavenButton42 },
                                                   {heavenButton50, heavenButton51, heavenButton52 },
                                                   {heavenButton60, heavenButton61, heavenButton62 },
                                                   {heavenButton70, heavenButton71, heavenButton72 },
                                                   {heavenButton80, heavenButton81, heavenButton82 },
                                                   {heavenButton90, heavenButton91, heavenButton92 } };
            this.earthBeads = new PictureBox[,] { {earthButton00, earthButton01, earthButton02, earthButton03, earthButton04, earthButton05 },
                                                  {earthButton10, earthButton11, earthButton12, earthButton13, earthButton14, earthButton15 },
                                                  {earthButton20, earthButton21, earthButton22, earthButton23, earthButton24, earthButton25 },
                                                  {earthButton30, earthButton31, earthButton32, earthButton33, earthButton34, earthButton35 },
                                                  {earthButton40, earthButton41, earthButton42, earthButton43, earthButton44, earthButton45 },
                                                  {earthButton50, earthButton51, earthButton52, earthButton53, earthButton54, earthButton55 },
                                                  {earthButton60, earthButton61, earthButton62, earthButton63, earthButton64, earthButton65 },
                                                  {earthButton70, earthButton71, earthButton72, earthButton73, earthButton74, earthButton75 },
                                                  {earthButton80, earthButton81, earthButton82, earthButton83, earthButton84, earthButton85 },
                                                  {earthButton90, earthButton91, earthButton92, earthButton93, earthButton94, earthButton95 } };

            for (int i = 0; i < COLUMNS; i++)
            {
                int index1 = i;
                for (int j = 0; j < EARTH_ROWS; j++)
                {
                    int index2 = j;
                    if (j == 0)
                    {
                        this.heavenBeads[i, j].Visible = false;
                        this.earthBeads[i, j].Visible = false;
                    }
                    if (j < HEAVEN_ROWS)
                    {
                        this.heavenBeads[i, j].Click += (object sender1, EventArgs ex) => this.HeavenButtonClicked(index1, index2);
                    }
                    this.earthBeads[i, j].Click += (object sender1, EventArgs ex) => this.EarthButtonClicked(index1, index2);
                }
            }
        }

        public void HeavenButtonClicked(int col, int row)
        {
            if (additionSubration)
            {
                // determine heaven bead value
                long result = HEAVEN_VALUES[col] * row;
                heavenValues[col] = result;

                // calculate heavenTotal and and overall total
                heavenTotal = heavenValues.Sum();
                total = earthTotal + heavenTotal;

                // output totals (dubugging)
                Console.WriteLine("heavenTotal: " + heavenTotal + "\tearthTotal: " + earthTotal + "\ttotal: " + total);
            }
            else if (multiplication || division)
            {
                // determine heaven bead and column value
                long result = (row != 0) ? HEAVEN_VALUES_MULT : 0;
                heavenValues[col] = result;
                columnValues[col] = result + earthValues[col];

                // display column value
                displayColumnValue(col);

                // output totals (dubugging)
                Console.WriteLine("heavenColumn[" + col + "]: " + heavenValues[col] + "\tearthColumn[" + col + "]: " + earthValues[col] + "\tcolumnTotal: " + columnValues[col]);
            }

            // clear result textbox
            resultTextBox.Clear();

            // update bead visability
            for (int x = 0; x < HEAVEN_ROWS; x++)
            {
                this.heavenBeads[col, x].Visible = (x != row) ? true : false;
            }
        }


        public void EarthButtonClicked(int col, int row)
        {
            if (additionSubration)
            {
                // determine earth bead value
                long result = (row > 0) ? EARTH_VALUES[col, (row - 1)] : 0;
                earthValues[col] = result;

                // calculate earthTotal and overall total
                earthTotal = earthValues.Sum();
                total = earthTotal + heavenTotal;

                // output totals (dubugging)
                Console.WriteLine("heavenTotal: " + heavenTotal + "\tearthTotal: " + earthTotal + "\ttotal: " + total);
            }
            else if (multiplication || division)
            {
                // determine earth bead and colomn value
                long result = (row > 0) ? EARTH_VALUES_MULT[row - 1] : 0;
                earthValues[col] = result;
                columnValues[col] = result + heavenValues[col];

                // display column value
                displayColumnValue(col);

                // output totals (dubugging)
                Console.WriteLine("heavenColumn[" + col + "]: " + heavenValues[col] + "\tearthColumn[" + col + "]: " + earthValues[col] + "\tcolumnTotal: " + columnValues[col]);
            }

            // clear result textbox
            resultTextBox.Clear();

            // update bead visability
            for (int x = 0; x < EARTH_ROWS; x++)
            {
                this.earthBeads[col, x].Visible = (x != row) ? true : false;
            }
        }

        private void displayColumnValue(int column)
        {
            // display column value
            switch (column)
            {
                case 9:
                    txtbxCol9.Text = columnValues[column].ToString();
                    break;
                case 8:
                    txtbxCol8.Text = columnValues[column].ToString();
                    break;
                case 7:
                    txtbxCol7.Text = columnValues[column].ToString();
                    break;
                case 6:
                    txtbxCol6.Text = columnValues[column].ToString();
                    break;
                case 5:
                    txtbxCol5.Text = columnValues[column].ToString();
                    break;
                case 4:
                    txtbxCol4.Text = columnValues[column].ToString();
                    break;
                case 3:
                    txtbxCol3.Text = columnValues[column].ToString();
                    break;
                case 2:
                    txtbxCol2.Text = columnValues[column].ToString();
                    break;
                case 1:
                    txtbxCol1.Text = columnValues[column].ToString();
                    break;
                case 0:
                    txtbxCol0.Text = columnValues[column].ToString();
                    break;
            }
        }

        // this event will triger when diplay button is cliked on
        public void btnResults_Click(object sender, EventArgs e)
        {
            string formatted = "";
            if (additionSubration)
            {
                formatted = string.Format("{0:N0}", total);
                resultTextBox.Text = formatted;
            }
            else if (multiplication)
            {
                formatted = string.Format("{0:N0}", getProduct());
                resultTextBox.Text = invalidExpression ? "Invalid Expression" : formatted;
                invalidExpression = false;
            }
            else if (division)
            {
                resultTextBox.Text = getDivisionResult();
            }
        }

        private int getProduct()
        {
            string multiplyDivideNumbers = "";
            multiplyDivideNumbers += txtbxCol9.Text;
            multiplyDivideNumbers += txtbxCol8.Text;
            multiplyDivideNumbers += txtbxCol7.Text;
            multiplyDivideNumbers += txtbxCol6.Text;
            multiplyDivideNumbers += txtbxCol5.Text;

            string[] numbers = multiplyDivideNumbers.Split('x');
            if (numbers.Length <= 1 || numbers.Length >= 3)
            {
                invalidExpression = true;
                return 0; // !!! MAYBE TELL THE USER THERE EXPRESSION IS INVALID !!!
            }
            return (Int32.Parse(numbers[0]) * Int32.Parse(numbers[1]));
        }

        private string getDivisionResult()
        {
            int dividend;
            int divisor;

            try
            {
                dividend = Int32.Parse(txtbxDividend.Text);
            }
            catch (Exception e)
            {
                if (txtbxDividend.Text == "")
                {
                    return "Enter a dividend in Problem Tracker";
                }
                return "Invalid dividend";
            }

            try
            {
                divisor = Int32.Parse(txtbxDivisor.Text);
            }
            catch (Exception e)
            {
                if (txtbxDivisor.Text == "")
                {
                    return "Enter a divisor in Problem Tracker";
                }
                return "Invalid divisor";
            }

            if (divisor == 0)
            {
                return "Unable to divide by 0";
            }
            else if (divisor > dividend)
            {
                return "Divisor must be less than dividend";
            }

            if (dividend < 0)
            {
                return "Dividend must be postive";
            }
            if (divisor < 0)
            {
                return "Divisor must be postive";
            }

            int quotient = dividend / divisor;
            int remainder = dividend % divisor;

            return quotient + " r " + remainder;
        }

        // resets all buttons, values, and textbox to default
        private void btnReset_Click(object sender, EventArgs e)
        {
            resetForm();
        }

        // resets form to defaults
        private void resetForm()
        {    
            // set totals to 0
            earthTotal = 0;
            heavenTotal = 0;
            total = 0;

            // clear heaven and earth totals
            for (int i = 0; i < COLUMNS; i++)
            {
                heavenValues[i] = 0;
                earthValues[i] = 0;
                columnValues[i] = 0;
            }

            // reset first row beads
            for (int i = 0; i < COLUMNS; i++)
            {
                this.heavenBeads[i, 0].Visible = false;
                this.earthBeads[i, 0].Visible = false;
            }

            // reset heaven beads
            for (int i = 0; i < COLUMNS; i++)
            {
                for (int j = 1; j < HEAVEN_ROWS; j++)
                {
                    this.heavenBeads[i, j].Visible = true;
                }
            }

            // reset earth beads
            for (int i = 0; i < COLUMNS; i++)
            {
                for (int j = 1; j < EARTH_ROWS; j++)
                {
                    this.earthBeads[i, j].Visible = true;
                }
            }

            // clear text boxes
            resultTextBox.Clear();
            txtbxCol9.Text = "0";
            txtbxCol8.Text = "0";
            txtbxCol7.Text = "0";
            txtbxCol6.Text = "0";
            txtbxCol5.Text = "0";
            txtbxCol4.Text = "0";
            txtbxCol3.Text = "0";
            txtbxCol2.Text = "0";
            txtbxCol1.Text = "0";
            txtbxCol0.Text = "0";

            txtbxDividend.Text = "";
            txtbxDivisor.Text = "";

            cmbbxDivision.SelectedIndex = -1;
            populateDivisionGuide();
        }

        // resets a column's beads and values to default
        private void resetColumn(int col)
        {
            // set column values to 0
            heavenValues[col] = 0;
            earthValues[col] = 0;
            columnValues[col] = 0;

            this.heavenBeads[col, 0].Visible = false;
            this.earthBeads[col, 0].Visible = false;

            // reset heaven beads
            for (int i = 1; i < HEAVEN_ROWS - 1; i++)
            {
                this.heavenBeads[col, i].Visible = true;
            }

            // reset earth beads
            for (int i = 1; i < EARTH_ROWS - 1; i++)
            {
                this.earthBeads[col, i].Visible = true;
            }

            // output totals (dubugging)
            Console.WriteLine("heavenColumn[" + col + "]: " + heavenValues[col] + "\tearthColumn[" + col + "]: " + earthValues[col] + "\tcolumnTotal: " + columnValues[col]);
        }

        private void btnAdditionSubtraction_Click(object sender, EventArgs e)
        {
            additionSubration = true;
            multiplication = false;
            division = false;

            showMultiplyDivideButtons(false);

            resetForm();
            enableBeads(true);
        }

        private void btnMultiplication_Click(object sender, EventArgs e)
        {
            multiplication = true;
            additionSubration = false;
            division = false;

            showMultiplyDivideButtons(true);
            resetForm();
            enableBeads(false);
        }

        private void btnDivision_Click(object sender, EventArgs e)
        {
            division = true;
            additionSubration = false;
            multiplication = false;

            showMultiplyDivideButtons(true);
            resetForm();
            enableBeads(false);
        }

        // shows or hides multiply and divide items based on arithmetic mode
        public void showMultiplyDivideButtons(bool flag)
        {
            txtbxCol9.Visible = flag;
            txtbxCol8.Visible = flag;
            txtbxCol7.Visible = flag;
            txtbxCol6.Visible = flag;
            txtbxCol5.Visible = flag;
            txtbxCol4.Visible = multiplication ? false : flag;
            txtbxCol3.Visible = flag;
            txtbxCol2.Visible = flag;
            txtbxCol1.Visible = flag;
            txtbxCol0.Visible = flag;

            btnMultiplierCol8.Visible = multiplication ? true : false;
            btnMultiplierCol7.Visible = multiplication ? true : false;
            btnMultiplierCol6.Visible = multiplication ? true : false;

            lblEqual.Visible = division ? false : flag;

            txtbxCol9_R2.Visible = division ? true : false;
            txtbxCol8_R2.Visible = division ? true : false; 
            txtbxCol7_R2.Visible = division ? true : false;
            txtbxCol6_R2.Visible = division ? true : false;
            txtbxCol5_R2.Visible = division ? true : false;
            txtbxCol4_R2.Visible = division ? true : false;
            txtbxCol3_R2.Visible = division ? true : false;
            txtbxCol2_R2.Visible = division ? true : false;
            txtbxCol1_R2.Visible = division ? true : false;
            txtbxCol0_R2.Visible = division ? true : false;

            txtbxDividend.Visible = division ? true : false;
            txtbxDivisor.Visible = division ? true : false;

            cmbbxDivision.Visible = division ? true : false;

            lblDivisionType.Visible = division ? true : false;
            lblDivideSign.Visible = division ? true : false;
            lblProblem.Visible = division ? true : false;
            lblDS.Visible = division ? true : false;
            lblDD.Visible = division ? true : false;

            txtbxLegend.Visible = division ? true : false;
        }

        // inputs
        public void populateDivisionGuide()
        {
            List<TextBox> boxes = new List<TextBox>();
            boxes.Add(txtbxCol9_R2);
            boxes.Add(txtbxCol8_R2);
            boxes.Add(txtbxCol7_R2);
            boxes.Add(txtbxCol6_R2);
            boxes.Add(txtbxCol5_R2);
            boxes.Add(txtbxCol4_R2);
            boxes.Add(txtbxCol3_R2);
            boxes.Add(txtbxCol2_R2);
            boxes.Add(txtbxCol1_R2);
            boxes.Add(txtbxCol0_R2);

            int index = cmbbxDivision.SelectedIndex;
            switch (index)
            {
                case 0:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division0[i];
                    }
                    break;
                case 1:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division1[i];
                    }
                    break;
                case 2:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division2[i];
                    }
                    break;
                case 3:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division3[i];
                    }
                    break;
                case 4:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division4[i];
                    }
                    break;
                case 5:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division5[i];
                    }
                    break;
                case 6:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division6[i];
                    }
                    break;
                case 7:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division7[i];
                    }
                    break;
                case 8:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division8[i];
                    }
                    break;
                case 9:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = division9[i];
                    }
                    break;
                default:
                    for (int i = 0; i < COLUMNS; i++)
                    {
                        boxes[i].Text = "";
                    }
                    break;
            }
        }

        // enables or disables beads based on arithmetic mode
        public void enableBeads(bool flag)
        {
            // enable or disable top row of heaven beads
            for (int i = 0; i < COLUMNS; i++)
            {
                this.heavenBeads[i, 2].Enabled = flag;
            }

            // enable or disable bottom row of earth beads
            for (int i = 0; i < COLUMNS; i++)
            {
                this.earthBeads[i, 5].Enabled = flag;
            }
        }

        private void btnMultiplierCol8_Click(object sender, EventArgs e)
        {
            txtbxCol8.Text = txtbxCol8.Text == "x" ? "0" : "x";
            resetColumn(8);
        }

        private void btnMultiplierCol7_Click(object sender, EventArgs e)
        {
            txtbxCol7.Text = txtbxCol7.Text == "x" ? "0" : "x";
            resetColumn(7);
        }

        private void btnMultiplierCol6_Click(object sender, EventArgs e)
        {
            txtbxCol6.Text = txtbxCol6.Text == "x" ? "0" : "x";
            resetColumn(6);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // close Program
            Application.Exit();
        }

        private void cmbbxDivision_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateDivisionGuide();
        }

        private void Introduction_bnt(object sender, EventArgs e)
        {
            this.Visible = false;

            Abacus_Welcome newfom = new Abacus_Welcome();

            newfom.Visible = true;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Form aboutform = new Form();

            //Label thislable = new Label();

            //thislable.Text = "Hello there";

            //aboutform.Controls.Add(thislable);

            //aboutform.Text = "About Ubacus\n";

            //aboutform.ShowDialog();

           
        }
    }
}
