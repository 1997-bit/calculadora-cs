using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Button = System.Windows.Forms.Button;

namespace Calculadora
{
    public partial class Calculadora : Form
    {
        private double Number1 = 0;
        private double Number2 = 0;
        private double Result = 0;
        private char Operator = '\0';
        private Theme TypeTheme = Calculadora.Theme.STD;

        SqlDataReader SqlListar;
        SqlCommand SqlConsulta;
        SqlConnection SqlConexion;

        public enum Theme
        {
            STD,
            NEODARK,
            NEODARKSTD,
        }
        public Calculadora()
        {
            InitializeComponent();
        }
        private void Calculadora_Load(object sender, EventArgs e)
        {
            this.ThemeCheck();
            this.SqlConnectionHistorial();
        }


        private void SqlConnectionHistorial()
        {
            string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Calc;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";
            try
            {
                this.SqlConexion = new SqlConnection(SqlConnectionString);
                this.SqlConexion.Open();

                //this.SqlConsulta = new SqlCommand("SELECT [Num1], [Op], [Num2], [Result], [DateCalc] FROM [dbo].[Historial]", this.SqlConexion);
                this.SqlConsulta = new SqlCommand("SELECT [Num1], [Op], [Num2], [Result] FROM [dbo].[Historial]", this.SqlConexion);


                this.SqlListar = this.SqlConsulta.ExecuteReader();

                this.lstHistorial.Items.Clear();
                //this.lstHistorial.Items.Add($"Num1, OP, Num2, Result, DateCalc");
                this.lstHistorial.Items.Add($"Num1, OP, Num2, Result");

                while (this.SqlListar.Read())
                {
                    //this.lstHistorial.Items.Add($"{this.SqlListar["Num1"].ToString()}, {this.SqlListar["Op"].ToString()}, {this.SqlListar["Num2"].ToString()}, {this.SqlListar["Result"].ToString()}. {this.SqlListar["DateCalc"].ToString()}");
                    this.lstHistorial.Items.Add($"{this.SqlListar["Num1"].ToString()}, {this.SqlListar["Op"].ToString()}, {this.SqlListar["Num2"].ToString()}, {this.SqlListar["Result"].ToString()}");
                }

                this.SqlConexion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Err]: conexión sql. {ex}");
                return;
            }
        }

        private void SqlInsert()
        {

            string SqlConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Calc;Integrated Security=True;Encrypt=False;TrustServerCertificate=True";

            try
            {
                this.SqlConexion = new SqlConnection(SqlConnectionString);
                this.SqlConexion.Open();

                //string SqlQuery = $"INSERT INTO Historial (Num1, Op, Num2, Result, DateCalc) VALUES({this.Number2}, '{this.Operator}', {this.Number1}, {this.Result}, '{DateTime.Now}')";
                string SqlQuery = $"INSERT INTO Historial (Num1, Op, Num2, Result) VALUES({this.Number2}, '{this.Operator}', {this.Number1}, {this.Result})";


                this.SqlConsulta = new SqlCommand(SqlQuery, this.SqlConexion);
                this.SqlConsulta.CommandType = CommandType.Text;
                this.SqlConsulta.ExecuteNonQuery();

                this.SqlConexion.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"[Err]: insert sql. {ex}");
                return;
            }
        }

        private void buttonTheme_Click(object sender, EventArgs e)
        {
            this.ThemeCheck();
        }

        private void ThemeCheck()
        {
            switch (this.TypeTheme)
            {
                case Calculadora.Theme.STD:
                    this.TypeTheme = Calculadora.Theme.NEODARK;

                    this.BackColor = Color.White;


                    this.btn0.BackColor = System.Drawing.Color.White;
                    this.btn1.BackColor = System.Drawing.Color.White;
                    this.btn2.BackColor = System.Drawing.Color.White;
                    this.btn3.BackColor = System.Drawing.Color.White;
                    this.btn4.BackColor = System.Drawing.Color.White;
                    this.btn5.BackColor = System.Drawing.Color.White;
                    this.btn6.BackColor = System.Drawing.Color.White;
                    this.btn7.BackColor = System.Drawing.Color.White;
                    this.btn8.BackColor = System.Drawing.Color.White;
                    this.btn9.BackColor = System.Drawing.Color.White;
                    this.btnFact.BackColor = System.Drawing.Color.White;
                    this.btnMod.BackColor = System.Drawing.Color.White;
                    this.btnC.BackColor = System.Drawing.Color.White;
                    this.BtnCE.BackColor = System.Drawing.Color.White;
                    this.btnCuadrado.BackColor = System.Drawing.Color.White;
                    this.btnNegativo.BackColor = System.Drawing.Color.White;

                    this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnFact.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnMod.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnC.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.BtnCE.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnCuadrado.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnRaiz.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnDividir.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnMultiplicar.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnSuma.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnResta.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnIgual.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnPunto.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnNegativo.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.btnMostrarHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Standard;
                    this.buttonTheme.FlatStyle = System.Windows.Forms.FlatStyle.Standard;

                    this.btnDel.BackColor = System.Drawing.Color.White;
                    this.btnSuma.BackColor = System.Drawing.Color.White;
                    this.btnResta.BackColor = System.Drawing.Color.White;
                    this.btnMultiplicar.BackColor = System.Drawing.Color.White;

                    this.btnDividir.BackColor = System.Drawing.Color.White;
                    this.btnRaiz.BackColor = System.Drawing.Color.White;
                    this.btnPunto.BackColor = System.Drawing.Color.White;
                    this.btnIgual.BackColor = System.Drawing.Color.White;

                    this.lblDisplay.ForeColor = System.Drawing.Color.DimGray;
                    this.lblOperacion.ForeColor = System.Drawing.Color.Black;
                    this.btnMostrarHistorial.ForeColor = System.Drawing.Color.Black;
                    this.buttonTheme.ForeColor = System.Drawing.Color.Black;
                    this.btnRaiz.ForeColor = System.Drawing.Color.Black;
                    this.btnCuadrado.ForeColor = System.Drawing.Color.Black;

                    this.lstHistorial.BackColor = System.Drawing.Color.White;

                    break;

                case Calculadora.Theme.NEODARK:

                    this.TypeTheme = Calculadora.Theme.STD;

                    this.btn0.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btn9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnFact.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnMod.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnC.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.BtnCE.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnCuadrado.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnRaiz.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnDividir.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnMultiplicar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnSuma.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnResta.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnIgual.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnPunto.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnNegativo.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.btnMostrarHistorial.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
                    this.buttonTheme.FlatStyle = System.Windows.Forms.FlatStyle.Popup;


                    this.BackColor = Color.Black;

                    this.btn0.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn1.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn2.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn3.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn4.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn5.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn6.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn7.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn8.BackColor = System.Drawing.Color.GreenYellow;
                    this.btn9.BackColor = System.Drawing.Color.GreenYellow;



                    this.btnFact.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnMod.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnCuadrado.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnNegativo.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnRaiz.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnPunto.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnIgual.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnSuma.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnResta.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnMultiplicar.BackColor = System.Drawing.Color.LimeGreen;
                    this.btnDividir.BackColor = System.Drawing.Color.LimeGreen;


                    this.btnDel.BackColor = System.Drawing.Color.MediumSeaGreen;
                    this.btnC.BackColor = System.Drawing.Color.MediumSeaGreen;
                    this.BtnCE.BackColor = System.Drawing.Color.MediumSeaGreen;



                    this.btnMostrarHistorial.ForeColor = System.Drawing.Color.GreenYellow;
                    this.buttonTheme.ForeColor = System.Drawing.Color.GreenYellow;
                    this.btnRaiz.ForeColor = System.Drawing.Color.Black;


                    this.lblDisplay.ForeColor = System.Drawing.Color.GreenYellow;
                    this.lblOperacion.ForeColor = System.Drawing.Color.GreenYellow;
                    this.lstHistorial.BackColor = System.Drawing.Color.GreenYellow;



                    break;

                default:
                    break;
            }
        }


        private void ResetNumber()
        {
            this.Number1 = 0;
            this.Number2 = 0;
        }

        private void BorrarDisplay(object sender, EventArgs e)
        {
            Button button = ((Button)sender);

            if (button.Text == "C")
            {
                this.Number1 = 0;
                this.Number2 = 0;
                this.Operator = '\0';
                this.lblOperacion.Text = "0";
                this.lblDisplay.Text = "0";
            }
        }

        private void LimpiarDisplay(object sender, EventArgs e)
        {
            Button button = ((Button)sender);

            if (button.Text == "CE")
            {
                this.lblOperacion.Text = "0";
            }
        }

        private void NumeroANegativo(object sender, EventArgs e)
        {
            this.CheckText();
            this.Number1 = Convert.ToDouble(this.lblOperacion.Text);
            this.Number1 *= -1;
            this.lblOperacion.Text = this.Number1.ToString();
        }

        private void RemoverNumero(object sender, EventArgs e)
        {
            this.CheckText();

            if (this.lblOperacion.Text.Length > 1)
            {

                this.lblOperacion.Text = this.lblOperacion.Text.Substring(0, this.lblOperacion.Text.Length - 1);
                return;
            }
            this.lblOperacion.Text = "0";

        }

        private void AgregarNumero(object sender, EventArgs e)
        {
            this.CheckText();
            // Cast
            Button button = ((Button)sender);

            if (this.lblOperacion.Text == "0")
            {
                this.lblOperacion.Text = "";
            }

            this.lblOperacion.Text += button.Text;
        }

        private void AgregarPunto(object sender, EventArgs e)
        {

            if (!this.lblOperacion.Text.Contains("."))
            {
                this.lblOperacion.Text += ".";
                return;
            }
        }

        string ErrRaiz = "Err Raiz de numero negativo";
        string ErrDiv0 = "Err No se puede dividir entre 0";
        string ErrFactorialFloatNegativo = "Err Factorial no float ni negativo";
        string ErrMod0 = "Err Mod no puede ser 0";
        private void CheckText()
        {
            if (this.lblOperacion.Text.Contains("Err")) {
                lblOperacion.Text = "0";
                lblDisplay.Text = "0";
                return;
            }

            //if (this.lblOperacion.Text == this.ErrDiv0)
            //{
            //}

            //if (this.lblOperacion.Text == this.ErrRaiz)
            //{
            //    lblOperacion.Text = "0";
            //    lblDisplay.Text = "0";
            //    return;
            //}

            //if (this.lblOperacion.Text == this.ErrFactorialFloatNegativo)
            //{
            //    lblOperacion.Text = "0";
            //    lblDisplay.Text = "0";
            //    return;
            //}
        }


        private void CalcPower()
        {
            switch (this.Operator)
            {
                case '-':
                    // sub
                    if (this.lblDisplay.Text.Contains("-"))
                    {
                        this.Result = this.Number2 - this.Number1;
                        this.lblDisplay.Text = $"{this.Number2}-{this.Number1}={this.Result}";
                        this.SqlInsert();
                        this.ResetNumber();
                        this.lblOperacion.Text = "0";
                        return;
                    }

                    this.Number2 = this.Number1;
                    this.lblDisplay.Text = $"{this.Number2}-";

                    this.lblOperacion.Text = "0";
                    return;

                case '*':
                    // Mul
                    if (this.lblDisplay.Text.Contains("*"))
                    {
                        this.Result = this.Number2 * this.Number1;
                        this.lblDisplay.Text = $"{this.Number2}*{this.Number1}={this.Result}";
                        this.SqlInsert();
                        this.ResetNumber();
                        this.lblOperacion.Text = "0";
                        return;
                    }

                    this.Number2 = this.Number1;
                    this.lblDisplay.Text = $"{this.Number2}*";

                    this.lblOperacion.Text = "0";
                    return;

                case '+':
                    // Add
                    if (this.lblDisplay.Text.Contains("+"))
                    {
                        this.Result = this.Number2 + this.Number1;
                        this.lblDisplay.Text = $"{this.Number2}+{this.Number1}={this.Result}";
                        this.SqlInsert();
                        this.ResetNumber();
                        this.lblOperacion.Text = "0";
                        return;
                    }

                    this.Number2 = this.Number1;
                    this.lblDisplay.Text = $"{this.Number2}+";

                    this.lblOperacion.Text = "0";
                    return;

                case '/':
                    // div

                    if (this.lblDisplay.Text.Contains("/"))
                    {

                        if (this.Number1 == 0)
                        {
                            //MessageBox.Show("[Err]: No se puede dividir entre 0");
                            this.lblOperacion.Text = this.ErrDiv0;
                            //this.lblOperacion.Text = "0";
                            this.ResetNumber();
                            return;
                        }

                        this.Result = this.Number2 / this.Number1;
                        this.lblDisplay.Text = $"{this.Number2}/{this.Number1}={this.Result}";
                        this.SqlInsert();
                        this.ResetNumber();
                        this.lblOperacion.Text = "0";
                        return;
                    }

                    this.Number2 = this.Number1;
                    this.lblDisplay.Text = $"{this.Number2}/";

                    this.lblOperacion.Text = "0";
                    return;

                case '%':
                    // mod
                    if (this.lblOperacion.Text.Contains("0"))
                    {
                        this.lblOperacion.Text = this.ErrMod0;
                        this.lblDisplay.Text = "0";
                        return;
                    }


                    if (this.lblDisplay.Text.Contains("%"))
                    {
                        this.Result = this.Number2 % this.Number1;
                        this.lblDisplay.Text = $"{this.Number2}%{this.Number1}={this.Result}";
                        this.SqlInsert();
                        this.ResetNumber();
                        this.lblOperacion.Text = "0";
                        return;
                    }

                    this.Number2 = this.Number1;
                    this.lblDisplay.Text = $"{this.Number2}%";

                    this.lblOperacion.Text = "0";
                    return;

                case '²':
                    // Pow2
                    if (this.lblOperacion.Text.Contains("-"))
                    {

                        this.Result = Math.Pow(this.Number1, 2);
                        this.lblDisplay.Text = $"{this.Number1}²=-{this.Result}";
                        this.lblOperacion.Text = "0";
                        this.SqlInsert();
                        this.ResetNumber();
                        return;
                    }

                    this.Result = Math.Pow(this.Number1, 2);
                    this.lblDisplay.Text = $"{this.Number1}²={this.Result}";
                    this.lblOperacion.Text = "0";
                    this.SqlInsert();
                    this.ResetNumber();
                    return;

                case '!':
                    // Fact 
                    if (this.lblOperacion.Text.Contains(".") || this.lblOperacion.Text.Contains("-"))
                    {
                        this.lblOperacion.Text = this.ErrFactorialFloatNegativo;
                        return;
                    }

                    this.Number2 = this.Number1;
                    for (int i = 1; i < this.Number1; i++)
                    {
                        this.Number2 = this.Number2 * i;
                    }

                    this.Result = this.Number2;
                    this.lblDisplay.Text = $"n!({this.Number1})={this.Result}";

                    this.Number2 = 0;
                    this.SqlInsert();
                    this.lblOperacion.Text = "0";
                    this.ResetNumber();
                    return;

                case '√':
                    // Raiz or Sqrt
                    if (this.Number1 >= 0)
                    {
                        this.Result = Math.Sqrt(this.Number1);
                        this.lblDisplay.Text = $"√({this.Number1})={this.Result}";
                        this.lblOperacion.Text = "0";
                        this.SqlInsert();
                        this.ResetNumber();
                        return;
                    }

                    //this.lblOperacion.Text = "0";
                    this.lblOperacion.Text = ErrRaiz;
                    this.ResetNumber();
                    return;

                default:
                    this.lblOperacion.Text = "0";
                    return;
            }

        }

        private void AgregarOperador(object sender, EventArgs e)
        {
            Button button = ((Button)sender);

            this.CheckText();

            if (this.lblDisplay.Text.Contains("="))
            {
                this.Result = 0;
                this.lblDisplay.Text = "0";
            }

            this.Number1 = Convert.ToDouble(this.lblOperacion.Text);
            this.Operator = Convert.ToChar(button.Tag);

            if (this.Operator == '=')
            {

                this.Operator = this.lblDisplay.Text[this.lblDisplay.Text.Length - 1];
                this.CalcPower();
                return;
            }

            this.CalcPower();
        }


        private void ToggleHistorial(object sender, EventArgs e)
        {
            bool mostrar = !this.pnlHistorial.Visible;

            switch (mostrar)
            {
                case true:
                    this.pnlHistorial.Visible = mostrar;
                    this.pnlHistorial.BringToFront();
                    break;

                case false:
                    this.pnlHistorial.Visible = mostrar;
                    this.pnlHistorial.SendToBack();
                    return;


            }
            SqlConnectionHistorial();
        }
    }
}