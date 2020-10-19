using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Windows.Forms;
namespace CALC
{
	class Program
	{
		public static class Error {
			public static void Show(string message) {
				MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		public static class CalculatorState {
			internal static void Standart() {
				MAIN.f.Width = 280;
				MAIN.f.Height = 370;
				MAIN.tbDisplay.Width = 260;
				MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Scientific() {
				MAIN.f.Width = 830;
				MAIN.f.Height = 370;
				MAIN.tbDisplay.Width = 800;
				MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Frac() {
				MAIN.f.Width =1240;
				MAIN.f.Height = 370;
	            MAIN.tbDisplay.Width = 800;
	           
	            MAIN.gFrac.Visible = true;
	            MAIN.gComplex.Visible = false;
	            MAIN.gQuadr.Visible = false;
	            MAIN.gTemperature.Visible = false;
	            MAIN.gMultiply.Visible = false;
	            MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Complexis() {
				MAIN.f.Width =1240;
				MAIN.f.Height = 370;
	            MAIN.tbDisplay.Width = 800;
	      
	            MAIN.gComplex.Visible = true;
	            MAIN.gQuadr.Visible = false;
	            MAIN.gTemperature.Visible = false;
	            MAIN.gMultiply.Visible = false;
	            MAIN.gFrac.Visible = false;
	            MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Quadratic() {
				MAIN.f.Width = 1240;
				MAIN.f.Height = 370;
	            MAIN.tbDisplay.Width = 800;
	           
	            MAIN.gQuadr.Visible = true;
	            MAIN.gComplex.Visible = false;
	            MAIN.gTemperature.Visible = false;
	            MAIN.gMultiply.Visible = false;
	            MAIN.gFrac.Visible = false;
	            MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Temperature() {
				MAIN.f.Width = 1240;
				MAIN.f.Height = 370;
	            MAIN.tbDisplay.Width = 800;
	            MAIN.tbDisplayValue.Focus();
	     
	            MAIN.gTemperature.Visible = true;
	            MAIN.gQuadr.Visible = false;
	            MAIN.gComplex.Visible = false;
	            MAIN.gMultiply.Visible = false;
	            MAIN.gFrac.Visible = false;
	            MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Multiplication() {
				MAIN.f.Width =1230;
				MAIN.f.Height = 370;
	            MAIN.tbDisplay.Width = 800;
	            MAIN.tMultiply.Focus();
	            MAIN.gComplex.Visible = false;
	            MAIN.gQuadr.Visible = false;
	            MAIN.gTemperature.Visible = false;
	            MAIN.gMultiply.Visible = true;
	            MAIN.gFrac.Visible = false;
	            MAIN.gMultiply.Location = new Point(860, 30);
	            MAIN.gProgrammer.Visible = false;
			}
			
			internal static void Programmer() {
				MAIN.f.Width = 830;
				MAIN.f.Height = 550;
				MAIN.tbDisplay.Width = 800;
				
	            MAIN.gComplex.Visible = false;
	            MAIN.gQuadr.Visible = false;
	            MAIN.gTemperature.Visible = false;
	            MAIN.gMultiply.Visible = false;
	            MAIN.gFrac.Visible = false;
	            MAIN.gProgrammer.Visible = true;
			}
			
			// Копировать число
			internal static void CopyNumber(TextBox tb) {
				tb.Copy();
			}
			// Вставить число 
			internal static void PasteNumber(TextBox tb) {
				tb.Paste();
			}
			
			internal static void ClearFields(Control control = null, string replacement = "") {
				if (control == null) return;
				if (control is TextBox || control is Label) {
					control.Text = replacement;
				}
			}
			// Очистить поле ввода
			internal static void ClearInputField() {
				CalculatorState.ClearFields(MAIN.tbDisplay, "0");
				CalculatorState.ClearFields(MAIN.lExample, "0");
			}
		}
		
		public static class CalculatorAPI {
			internal static long Factorial(int x) {
				long fact = 1;
				
				for (int i = 1; i < x; ++i) {
					fact*=i;
				}
				
				return fact;
			}
			
			internal static long DoubleFactorial(int x) {
				if ((x == 0 ) || (x == 1)) return 1;
            	else return DoubleFactorial(x-2) * x;
			}
			
			internal static long TripleFactorial(int x) {
				if ((x == 0 ) || (x == 1) || (x == 2)) return 1;
            	else return TripleFactorial(x-3) * x;
			}
			
			internal static long SuperFactorial(int x) {
				long superfact = 1;
				
				for (int i = 0; i < x; ++i) {
					superfact *= CalculatorAPI.Factorial(i);
				}
				
				return superfact;
			}
			
			internal static long HyperFactorial(int x) {
				long hyperfact = 1;
				
				for (int i = 0; i < x; ++i) {
					hyperfact *= CalculatorAPI.SuperFactorial(i);
				}
				
				return hyperfact;
			}
			
			internal static double Sec(double x) {
				return 1 / Math.Cos(x);
			}
			
			internal static double Cosec(double x) {
				if (x == 0) Error.Show("Ошибка деления на 0");
				return 1 / Math.Sin(x);
			}
			
			internal static double Sech(double x) {
				return 1 / Math.Cosh(x);
			}
			
			internal static double Csch(double x) {
				return 1 / Math.Sinh(x);
			}
			
			internal static double Trunc(double x) {
				return Convert.ToInt32(Math.Truncate(x));
			}
			
			internal static bool IsSimple(int n) {
				for (int i = 2; i < Trunc(n / 2); ++i) {
					if (n % 2 == 0) {
						return false;
					}
				}
				return true;
			}
			// Возвращает последовательность целых от a до b 
			internal static IEnumerable<int> Range(int a, int b) {
				if (b < a) {
					return Enumerable.Empty<int>();
				}
				return Enumerable.Range(a, b - a + 1);
			}
			
			internal static BigInteger Primorial(int x) {
				if (x > 0) {
					BigInteger primes = 1;
					foreach (int each in Range(2, x + 1)) {
						if (IsSimple(each)) {
							primes *= each;
						}
					}
					return primes;
				}
				throw new ArithmeticException("Нельзя вычислить праймориал для отрицательных значений");
			}
			
			internal static double RadToDeg(double x) {
				return x * (Math.PI / 180);
			}
			
			internal static double DegToRad(double x) {
				return x * (180 / Math.PI);
			}
			
			internal static double GCD(double a, double b) {
				double c;
				do {
					c = a % b;
					a = b;
					b = c;
				} while(c == 0);
				return a;
			}
		}
		
		public static class BitwiseAPI {
			private const int MAX_INDEX_BYTE_BIT = 8;
			private const int MAX_INDEX_INT16_BIT = 16;
			private const int MAX_INDEX_INT32_BIT = 32;
			private const int MAX_INDEX_INT64_BIT = 64;
			
			private static bool GetBitNative(long num, int index) {
				return (num & (1 << index)) != 0;
			}
			
			private static int GetBit(long num, int index) {
				return (GetBitNative(num, index)) ? 1 : 0;
			}
			
			public static string PrintBitsFromByte(long b) {
				StringBuilder sb = new StringBuilder();
				
				for (int i = 0; i < MAX_INDEX_BYTE_BIT; ++i) {
					sb.Append(GetBit(b,i));
				}
				return sb.ToString();
			}
			
			public static string PrintBitsFromInt16(long b) {
				StringBuilder sb = new StringBuilder();
				
				for (int i = 0; i < MAX_INDEX_INT16_BIT; ++i) {
					sb.Append(GetBit(b,i));
				}
				return sb.ToString();
			}
			
			public static string PrintBitsFromInt32(long b) {
				StringBuilder sb = new StringBuilder();
				
				for (int i = 0; i < MAX_INDEX_INT32_BIT; ++i) {
					sb.Append(GetBit(b,i));
				}
				return sb.ToString();
			}
			
			public static string PrintBitsFromInt64(long b) {
				StringBuilder sb = new StringBuilder();
				
				for (int i = 0; i < MAX_INDEX_INT64_BIT; ++i) {
					sb.Append(GetBit(b,i));
				}
				return sb.ToString();
			}
		}
		
		public static class CalculatorOperation {
			//Удаляет последний символ
			internal static void RemoveSymbol() {
				if (MAIN.tbDisplay.Text.Length > 0) {
					MAIN.tbDisplay.Text = MAIN.tbDisplay.Text.Remove(MAIN.tbDisplay.Text.Length - 1, 1);
				}
				
				if (MAIN.tbDisplay.Text == "") {
					CalculatorState.ClearFields(MAIN.tbDisplay, "0");
				}
				
				MAIN.lExample.Text = MAIN.tbDisplay.Text;
			}
			
			
		}

		public class Frac {
			public double Numerator { get; set; }
        	public double Denominator { get; set; }
			
        	public Frac() {
        		Numerator = 0;
        		Denominator = 0;
        	}
        	
        	public Frac(double numerator, double denominator) {
        		Numerator = numerator;
        		Denominator = denominator;
        	}
        	
        	private void Reduce() {
	            double a = Numerator;
	            double b = Denominator;
	
	            while ((a != 0) && (b != 0))
	            {
	                if (a > b)
	                {
	                    a = Math.Round(a) % Math.Round(b);
	                }
	                else
	                {
	                    b = Math.Round(b) % Math.Round(a);
	                }
	            }
	
	            var gcd = a + b;
	
	            Numerator = Math.Round(Numerator) / Math.Round(gcd);
	            Denominator = Math.Round(Denominator) / Math.Round(gcd);
	        }
        	
			//Сложение дробей
			public Frac Add(Frac f) {
				if (Denominator == f.Denominator) {
        			Numerator += f.Numerator;
				} else {
        			Numerator = Numerator * f.Denominator + f.Numerator * Denominator;
        			Denominator *= f.Denominator;
				}
      			Reduce();
      			return new Frac(Numerator, Denominator);
			}
		    //Вычитание дробей
		    public Frac Subtract(Frac f) {
		    	if (Denominator == f.Denominator) {
		        	Numerator -= f.Numerator;
		    	} else {
		       	 	Numerator = Numerator * f.Denominator - f.Numerator * Denominator;
		        	Denominator *= f.Denominator;
		    	}
		      Reduce();
		      return new Frac(Numerator, Denominator);
		    }
		    //Умножение дробей
		    public Frac Multiply(Frac f) {
		      Numerator *= f.Numerator;
		      Denominator *= f.Denominator;
		      Reduce();
		      return new Frac(Numerator, Denominator);
		    }
		    //Деление дробей
		    public Frac Divide(Frac f) {
		      Numerator *= f.Denominator;
		      Denominator *= f.Numerator;
		      Reduce();
		      return new Frac(Numerator, Denominator);
		    }
		}
		
		public class CalculatorForm : Form {
			 #region "Переменные формы"
			 public Form f = new Form();
			 public ToolStripMenuItem file = new ToolStripMenuItem();
			 public ToolStripMenuItem standard = new ToolStripMenuItem();
			 public	ToolStripMenuItem scientific = new ToolStripMenuItem();
			 public ToolStripMenuItem frac = new ToolStripMenuItem();
			 public ToolStripMenuItem complex = new ToolStripMenuItem();
		     public ToolStripMenuItem quadratic = new ToolStripMenuItem();
		     public ToolStripMenuItem temperature = new ToolStripMenuItem();
			 public ToolStripMenuItem multiplication = new ToolStripMenuItem();
			 public ToolStripMenuItem programmer = new ToolStripMenuItem();
		     public ToolStripMenuItem copy = new ToolStripMenuItem();
			 public ToolStripMenuItem paste = new ToolStripMenuItem();
			 public ToolStripMenuItem deg = new ToolStripMenuItem();
			 public ToolStripMenuItem rad = new ToolStripMenuItem();
			 public ToolStripMenuItem exit = new ToolStripMenuItem();
			 public ToolStripMenuItem edit = new ToolStripMenuItem();
			 public ToolStripMenuItem help = new ToolStripMenuItem();
			 public ToolStripMenuItem view = new ToolStripMenuItem();
			 public ToolStripMenuItem bitcalc = new ToolStripMenuItem();
			 public MenuStrip menu = new MenuStrip();
			 public TextBox tbDisplay = new TextBox();
			 
			 public Label lExample = new Label();
			 public Button b0, b1, b2, b3, b4, b5, b6, b7, b8, b9, bC, bCE, bPoint, bPlus, bMinus, bMul, bDiv, bEqual, bPM, bClDelete;
			 public Button bPi,bPower2, bPow, bLog, bSinh, bCosh, bTanh,bSech, bSec, bCSec, bCSech, bExp, bSin, bCos, bTan, bNqrt, bSqrt, b1_x, bLn, bPercent, bDec, bBin, bHex, bOct;
			 public Button bFact, bGamma, bPFunc, bSf, bInt, bFact2, bFact3,bPrim,bHFact, bNod, BAntLog;
				    //Temperature GroupBox
			 public Label lEnterValue = new Label();
			 public TextBox tbDisplayValue = new TextBox();
			 public Label lResVal = new Label();
			 public Label lConvert = new Label();
			 public RadioButton lCelsToFahr = new RadioButton();
			 public RadioButton lFahrToCels = new RadioButton();
			 public RadioButton lKelvin = new RadioButton();
			 public Label lRes = new Label();
			 public Button bConvert, bReset;
			 public GroupBox gTemperature = new GroupBox();
				    //Multiply GroupBox
			 public TextBox tMultiply = new TextBox();
			 public Button bMultiply = new Button();
			 public Button bResetMultiply = new Button();
			 public ListBox listMultiply = new ListBox();
			 public GroupBox gMultiply = new GroupBox();
				    //Complex GroupBox
			 public Label lfirstNumber =new Label();
			 public Label lsecondNumber =new Label();
			 public Label lOperationComplex =new Label();
			 public TextBox tbRE_1 = new TextBox();
			 public TextBox tbIM_1 = new TextBox();
			 public TextBox tbRE_2 = new TextBox();
			 public TextBox tbIM_2 = new TextBox();
			 public Label lPlus =new Label();
			 public Label lPlus_1 =new Label();
			 public Label l_I =new Label();
			 public Label l_I_1 =new Label();
			 public ComboBox cbOperation = new ComboBox();
			 public Label lResult = new Label();
			 public Button bCalcComplex = new Button();
			 public Button bResetComplex = new Button(); 
			 public GroupBox gComplex = new GroupBox();  
				    //Quadratic GroupBox
			 public Label lInfo = new Label();
			 public Label lA = new Label();
			 public Label lB = new Label();
			 public Label lC = new Label();
			 public TextBox tbA = new TextBox();
			 public TextBox tbB = new TextBox();
			 public TextBox tbC = new TextBox();
			 public Button bCalc = new Button();
			 public Button bResetQuadr = new Button();
			 public GroupBox gQuadr = new GroupBox();
			 public Label lRoots = new Label();
				   //Frac GroupBox
			 public Label ltypeFrac,lResEqual;
			 public RadioButton pFrac,mixedFrac;
			 public ComboBox operationFrac;
			 public TextBox tb1,tbNumerator1,tbDenominator1,tb2,tbNumerator2,tbDenominator2,tbNumeratorRes,tbDenominatorRes;
			 public Button bRes, bClear;
			 public GroupBox gFrac = new GroupBox();
			 // Programmer GroupBox
			 public Label lByte, lInt16, lInt32, lInt64;
			 public TextBox tbByte, tbInt16, tbInt32, tbInt64;
			 public Button bGetBits;
			 public GroupBox gProgrammer = new GroupBox();
				    
			 public double NUMBER1, NUMBER2, RESULT;
			 public string OPERATOR = "";
			 public bool enter_value = false;
			 public double iCelsius, iFahrenheit, iKelvin;
    		 public char iOperation;
    		 bool isDeg, isRad; // режим: Градусы или Радианы
			 #endregion
				 
			 public CalculatorForm() {
			 	Initialize();
			 }
			 
			 public void Initialize() {
			 	f.ClientSize = new Size(275, 350);
				f.Font= new Font("Courier New",16,FontStyle.Italic);
				f.ForeColor= Color.Turquoise;
				f.BackColor = Color.Black;
				f.Text = "Scientific Calculator";
				f.MaximizeBox = false;
				f.FormBorderStyle  = FormBorderStyle.FixedSingle;
				// menuStrip1
				menu.Location  = new Point(0, 0);
				menu.Size  = new Size(284, 24);
				
				standard.Size  = new Size(159, 22);
				standard.Text  = "Standard";
				standard.Click += STANDART_STATE;
				
				scientific.Size  = new Size(159, 22);
				scientific.Text  = "Scientific";
				scientific.Click += SCIENTIFIC_STATE;
				
				frac.Size  = new Size(159, 22);
				frac.Text  = "Frac";
				frac.Click += FRAC_STATE;
				
				complex.Size  = new Size(159, 22);
				complex.Text  = "Complex";
				complex.Click += COMPLEX_STATE;
				
				quadratic.Size  = new Size(159, 22);
				quadratic.Text  = "Quadratic Equality";
				quadratic.Click += QUADRATIC_STATE;
				
				temperature.Size  = new Size(159, 22);
				temperature.Text  = "Temperature";
				temperature.Click += TEMPERATURE_STATE;
				
				multiplication.Size  = new Size(159, 22);
				multiplication.Text  = "Multiplication";
				multiplication.Click += MULTIPLICATION_STATE;
				
				programmer.Size  = new Size(159, 22);
				programmer.Text  = "Programmer";
				programmer.Click += PROGRAMMER_STATE;
				
				exit.Size  = new Size(159, 22);
				exit.Text  = "Exit";
				exit.Click += EXIT;
				
				// fileToolStripMenuItem 
				file.DropDownItems.AddRange(new ToolStripItem[9] { standard, scientific, complex, frac, quadratic, temperature,multiplication,programmer, exit });
				file.Size  = new Size(37, 20);
				file.Text  = "File";
				
				copy.Size  = new Size(159, 22);
				copy.Text  = "Copy";
				copy.Click += CopyNumber;
				
				paste.Size  = new Size(159, 22);
				paste.Text  = "Paste";
				paste.Click += PasteNumber;
				
				deg.Size  = new Size(159, 22);
				deg.Text  = "Enable Degree";
				deg.Click += EnableDegrees;
				//Включить режим с градусами по умолчанию
				isDeg = true;
				
				rad.Size  = new Size(159, 22);
				rad.Text  = "Enable Radian";
				rad.Click += EnableRadians;
				
				edit.DropDownItems.AddRange(new ToolStripItem[4] {copy, paste,deg,rad });
				edit.Size  = new Size(39, 20);
				edit.Text  = "Edit";
				
				help.Size  = new Size(159, 22);
				help.Text  = "Help";
				//thelp.Click += HELP_;
				view.DropDownItems.AddRange(new ToolStripItem[1] { help });
				view.Size  = new Size(44, 20);
				view.Text  = "View";
				
				bitcalc.Size  = new Size(44, 20);
				bitcalc.Text  = "BitCalc";
				bitcalc.Click += BITCALC_EXECUTE;
				
				lExample.Top =30;
				lExample.ForeColor = Color.DarkTurquoise;
				lExample.Width =570;
				lExample.Text = "";
				
				tbDisplay.Width =260;
				tbDisplay.Height =50;
				tbDisplay.Multiline  = true;
				tbDisplay.Top =53;
				tbDisplay.Left =5;
				tbDisplay.Font = new Font("Courier New",19,FontStyle.Italic);
				tbDisplay.TextAlign  = HorizontalAlignment.Right;
				tbDisplay.BorderStyle  = BorderStyle.FixedSingle;
				tbDisplay.ForeColor = Color.LightBlue;
				tbDisplay.BackColor = Color.Black;
				//-------------------------------------------------//
				bClDelete =new Button();
				bClDelete.Top = 110;
				bClDelete.Left = 10;
				bClDelete.Width = 60;
				bClDelete.Height =40;
				bClDelete.Text = "<";
				bClDelete.Click += RemoveSymbol;
				
				bCE =new Button();
				bCE.Top = 110;
				bCE.Left = 75;
				bCE.Width = 60;
				bCE.Height =40;
				bCE.Text = "CE";
				bCE.Click += ClearDisplay;
				
				bC =new Button();
				bC.Top = 110;
				bC.Left = 140;
				bC.Width = 60;
				bC.Height =40;
				bC.Text = "C";
				bC.Click += ClearDisplay;
				
				bPM =new Button();
				bPM.Top = 110;
				bPM.Left = 205;
				bPM.Width = 60;
				bPM.Height =40;
				bPM.Text = "+/-";
				bPM.Click += ReverseSign;
				
				bSinh = new Button();
				bSinh.Top = 110;
				bSinh.Left = 280;
				bSinh.Width = 70;
				bSinh.Height =40;
				bSinh.Text = "Sinh";
				bSinh.Click += TrigonometricOperations;
				
				bSin =new Button();
				bSin.Top = 110;
				bSin.Left = 355;
				bSin.Width = 60;
				bSin.Height =40;
				bSin.Text = "Sin";
				bSin.Click += TrigonometricOperations;
				
				bSqrt =new Button();
				bSqrt.Top = 110;
				bSqrt.Left = 420;
				bSqrt.Width = 80;
				bSqrt.Height =40;
				bSqrt.Text = "Sqrt";
				bSqrt.Click += Sqrt;
				
				bPower2 =new Button();
				bPower2.Top = 110;
				bPower2.Left = 505;
				bPower2.Width = 60;
				bPower2.Height =40;
				bPower2.Text = "x^2";
				bPower2.Click += Power2;
				//-------------------------------------------------//
				b7 =new Button();
				b7.Top = 155;
				b7.Left = 10;
				b7.Width = 60;
				b7.Height =40;
				b7.Text = "7";
				b7.Click += AddDigitsAndPoint;
				
				b8 =new Button();
				b8.Top = 155;
				b8.Left = 75;
				b8.Width = 60;
				b8.Height =40;
				b8.Text = "8";
				b8.Click += AddDigitsAndPoint;
				
				b9 =new Button();
				b9.Top = 155;
				b9.Left = 140;
				b9.Width = 60;
				b9.Height =40;
				b9.Text = "9";
				b9.Click += AddDigitsAndPoint;
				
				bPlus =new Button();
				bPlus.Top = 155;
				bPlus.Left = 205;
				bPlus.Width = 60;
				bPlus.Height =40;
				bPlus.Text = "+";
				bPlus.Click += MemorizeFirstNumberAndOperator;
				
				bCosh = new Button();
				bCosh.Top = 155;
				bCosh.Left = 280;
				bCosh.Width = 70;
				bCosh.Height =40;
				bCosh.Text = "Cosh";
				bCosh.Click += TrigonometricOperations;
				
				bCos =new Button();
				bCos.Top = 155;
				bCos.Left = 355;
				bCos.Width = 60;
				bCos.Height =40;
				bCos.Text = "Cos";
				bCos.Click += TrigonometricOperations;
				
				bNqrt =new Button();
				bNqrt.Top = 155;
				bNqrt.Left = 420;
				bNqrt.Width = 80;
				bNqrt.Height =40;
				bNqrt.Text = "Nqrt";
				bNqrt.Click += MemorizeFirstNumberAndOperator;
				
				bPow =new Button();
				bPow.Top = 155;
				bPow.Left = 505;
				bPow.Width = 60;
				bPow.Height =40;
				bPow.Text = "^";
				bPow.Click += MemorizeFirstNumberAndOperator;
				//-------------------------------------------------//
				b4 =new Button();
				b4.Top = 200;
				b4.Left = 10;
				b4.Width = 60;
				b4.Height =40;
				b4.Text = "4";
				b4.Click += AddDigitsAndPoint;
				
				b5 =new Button();
				b5.Top = 200;
				b5.Left = 75;
				b5.Width = 60;
				b5.Height =40;
				b5.Text = "5";
				b5.Click += AddDigitsAndPoint;
				
				b6 =new Button();
				b6.Top = 200;
				b6.Left = 140;
				b6.Width = 60;
				b6.Height =40;
				b6.Text = "6";
				b6.Click += AddDigitsAndPoint;
				
				bMinus =new Button();
				bMinus.Top = 200;
				bMinus.Left = 205;
				bMinus.Width = 60;
				bMinus.Height =40;
				bMinus.Text = "-";
				bMinus.Click += MemorizeFirstNumberAndOperator;
				
				bTanh = new Button();
				bTanh.Top = 200;
				bTanh.Left = 280;
				bTanh.Width = 70;
				bTanh.Height =40;
				bTanh.Text = "Tanh";
				bTanh.Click += TrigonometricOperations;
				
				bTan =new Button();
				bTan.Top = 200;
				bTan.Left = 355;
				bTan.Width = 60;
				bTan.Height =40;
				bTan.Text = "Tan";
				bTan.Click += TrigonometricOperations;
				
				bLog =new Button();
				bLog.Top = 200;
				bLog.Left = 420;
				bLog.Width = 80;
				bLog.Height =40;
				bLog.Text = "Log";
				bLog.Click += OtherOperation;
				
				bLn =new Button();
				bLn.Top = 200;
				bLn.Left = 505;
				bLn.Width = 60;
				bLn.Height =40;
				bLn.Text = "Ln";
				bLn.Click += OtherOperation;
				//-------------------------------------------------//
				b1 =new Button();
				b1.Top = 245;
				b1.Left = 10;
				b1.Width = 60;
				b1.Height =40;
				b1.Text = "1";
				b1.Click += AddDigitsAndPoint;
				
				b2 =new Button();
				b2.Top = 245;
				b2.Left = 75;
				b2.Width = 60;
				b2.Height =40;
				b2.Text = "2";
				b2.Click += AddDigitsAndPoint;
				
				b3 =new Button();
				b3.Top = 245;
				b3.Left = 140;
				b3.Width = 60;
				b3.Height =40;
				b3.Text = "3";
				b3.Click += AddDigitsAndPoint;
				
				bMul =new Button();
				bMul.Top = 245;
				bMul.Left = 205;
				bMul.Width = 60;
				bMul.Height =40;
				bMul.Text = "*";
				bMul.Click += MemorizeFirstNumberAndOperator;
				
				bSech = new Button();
				bSech.Top = 245;
				bSech.Left = 280;
				bSech.Width = 70;
				bSech.Height =40;
				bSech.Text = "Sech";
				bSech.Click += TrigonometricOperations;
				
				bSec =new Button();
				bSec.Top = 245;
				bSec.Left = 355;
				bSec.Width = 60;
				bSec.Height =40;
				bSec.Text = "Sec";
				bSec.Click += TrigonometricOperations;
				
				b1_x =new Button();
				b1_x.Top = 245;
				b1_x.Left = 420;
				b1_x.Width = 80;
				b1_x.Height =40;
				b1_x.Text = "1/x";
				b1_x.Click += OtherOperation;
				
				bPercent =new Button();
				bPercent.Top = 245;
				bPercent.Left = 505;
				bPercent.Width = 60;
				bPercent.Height =40;
				bPercent.Text = "%";
				bPercent.Click += Percent;
				//-------------------------------------------------//
				b0 =new Button();
				b0.Top = 290;
				b0.Left = 10;
				b0.Width = 60;
				b0.Height =40;
				b0.Text = "0";
				b0.Click += AddDigitsAndPoint;
				
				bPoint =new Button();
				bPoint.Top = 290;
				bPoint.Left = 75;
				bPoint.Width = 60;
				bPoint.Height =40;
				bPoint.Text = ".";
				bPoint.Click += AddDigitsAndPoint;
				
				bEqual =new Button();
				bEqual.Top = 290;
				bEqual.Left = 140;
				bEqual.Width = 60;
				bEqual.Height =40;
				bEqual.Text = "=";
				bEqual.Click += GetResult;
				
				bDiv =new Button();
				bDiv.Top = 290;
				bDiv.Left = 205;
				bDiv.Width = 60;
				bDiv.Height =40;
				bDiv.Text = "/";
				bDiv.Click += MemorizeFirstNumberAndOperator;
				
				bCSech = new Button();
				bCSech.Top = 290;
				bCSech.Left = 280;
				bCSech.Width = 70;
				bCSech.Height =40;
				bCSech.Text = "Csch";
				bCSech.Click += TrigonometricOperations;
				
				bCSec =new Button();
				bCSec.Top = 290;
				bCSec.Left = 355;
				bCSec.Width = 60;
				bCSec.Height =40;
				bCSec.Text = "Csc";
				bCSec.Click += TrigonometricOperations;
				
				bPi =new Button();
				bPi.Top = 290;
				bPi.Left = 420;
				bPi.Width = 80;
				bPi.Height =40;
				bPi.Text = "PI";
				bPi.Click += OtherOperation;
				
				bExp =new Button();
				bExp.Top = 290;
				bExp.Left = 505;
				bExp.Width = 60;
				bExp.Height =40;
				bExp.Text = "EXP";
				bExp.Click += OtherOperation;
				
				bInt =new Button();
				bInt.Top = 110;
				bInt.Left = 570;
				bInt.Width = 80;
				bInt.Height =40;
				bInt.Text = "INT";
				bInt.Click += Int;
				
				bFact =new Button();
				bFact.Top = 155;
				bFact.Left = 570;
				bFact.Width = 80;
				bFact.Height =40;
				bFact.Text = "n!";
				bFact.Click += Factorial;
				
				bFact2 =new Button();
				bFact2.Top = 200;
				bFact2.Left = 570;
				bFact2.Width = 80;
				bFact2.Height =40;
				bFact2.Text = "n!!";
				bFact2.Click += DoubleFactorial;
				
				bFact3 =new Button();
				bFact3.Top = 245;
				bFact3.Left = 570;
				bFact3.Width = 80;
				bFact3.Height =40;
				bFact3.Text = "n!!!";
				bFact3.Click += TripleFactorial;
				
				bSf =new Button();
				bSf.Top = 290;
				bSf.Left = 570;
				bSf.Width = 80;
				bSf.Height =40;
				bSf.Text = "Sf";
				bSf.Click += SuperFactorial;
				//--------------------------------------------------------------------//
				bPrim =new Button();
				bPrim.Top = 110;
				bPrim.Left = 655;
				bPrim.Width = 70;
				bPrim.Height =40;
				bPrim.Text = "n#";
				bPrim.Click += Primorial;
				
				bGamma =new Button();
				bGamma.Top = 155;
				bGamma.Left = 655;
				bGamma.Width = 70;
				bGamma.Height =40;
				bGamma.Text = "G";
				bGamma.Click += GammaFunction;
				
				bPFunc =new Button();
				bPFunc.Top = 200;
				bPFunc.Left = 655;
				bPFunc.Width = 70;
				bPFunc.Height =40;
				bPFunc.Text = "P";
				bPFunc.Click += PiFunction;
				
				bHFact =new Button();
				bHFact.Top = 245;
				bHFact.Left = 655;
				bHFact.Width = 70;
				bHFact.Height =40;
				bHFact.Text = "Hf";
				bHFact.Click += HyperFactorial;
				
				bNod =new Button();
				bNod.Top = 290;
				bNod.Left = 655;
				bNod.Width = 70;
				bNod.Height =40;
				bNod.Text = "GCD";
				bNod.Click += MemorizeFirstNumberAndOperator;
				//-----------------------------------------------------------------//
				bDec =new Button();
				bDec.Top = 110;
				bDec.Left = 730;
				bDec.Width = 70;
				bDec.Height =40;
				bDec.Text = "DEC";
				bDec.Click += NumericalSystems;
				
				bBin =new Button();
				bBin.Top = 155;
				bBin.Left = 730;
				bBin.Width = 70;
				bBin.Height =40;
				bBin.Text = "BIN";
				bBin.Click += NumericalSystems;
				
				bHex =new Button();
				bHex.Top = 200;
				bHex.Left = 730;
				bHex.Width = 70;
				bHex.Height =40;
				bHex.Text = "HEX";
				bHex.Click += NumericalSystems;
				
				bOct = new Button();
				bOct.Top = 245;
				bOct.Left = 730;
				bOct.Width = 70;
				bOct.Height =40;
				bOct.Text = "OCT";
				bOct.Click += NumericalSystems;
				
				BAntLog = new Button();
				BAntLog.Top = 290;
				BAntLog.Left = 730;
				BAntLog.Width = 70;
				BAntLog.Height = 40;
				BAntLog.Text = "ALG";
				BAntLog.Click += MemorizeFirstNumberAndOperator;
				
				#region Group-Box-ы
				// Programmer GroupBox
				lByte = new Label();
				lInt16 = new Label();
				lInt32 = new Label();
				lInt64 = new Label();
				tbByte = new TextBox();
				tbInt16 = new TextBox();
				tbInt32 = new TextBox();
				tbInt64 = new TextBox();
				bGetBits = new Button();
				
				lByte.Text = "Byte: ";
				lByte.Top = 30;
				lByte.Left = 15;
				lByte.Font = new Font("Courier New",13,FontStyle.Regular);
				lInt16.Text = "Int16: ";
				lInt16.Top = 68;
				lInt16.Left = 15;
				lInt16.Font = new Font("Courier New",13,FontStyle.Regular);
				lInt32.Text = "Int32: ";
				lInt32.Top = 98;
				lInt32.Left = 15;
				lInt32.Font = new Font("Courier New",13,FontStyle.Regular);
				lInt64.Text = "Int64: ";
				lInt64.Top = 128;
				lInt64.Left = 15;
				lInt64.Font = new Font("Courier New",13,FontStyle.Regular);
				
				tbByte.Top = 30;
				tbByte.Left = 115;
				tbByte.Width = 100;
				tbByte.BackColor = Color.Black;
				tbByte.BorderStyle  = BorderStyle.FixedSingle;
				tbByte.ForeColor = Color.LightBlue;
				tbByte.Font = new Font("Courier New",14,FontStyle.Italic);
				
				bGetBits.Top = 30;
				bGetBits.Left = 445;
				bGetBits.Width = 150;
				bGetBits.Text = "Get Bits";
				bGetBits.BackColor = Color.Black;
				bGetBits.ForeColor = Color.LightBlue;
				bGetBits.Font = new Font("Courier New",14,FontStyle.Italic);
				bGetBits.Click += PrintBitsFromNumber;
				
				tbInt16.Top = 65;
				tbInt16.Left = 115;
				tbInt16.Width = 188;
				tbInt16.BackColor = Color.Black;
				tbInt16.BorderStyle  = BorderStyle.FixedSingle;
				tbInt16.ForeColor = Color.LightBlue;
				tbInt16.Font = new Font("Courier New",14,FontStyle.Italic);
				
				tbInt32.Top = 98;
				tbInt32.Left = 115;
				tbInt32.Width = 367;
				tbInt32.BackColor = Color.Black;
				tbInt32.BorderStyle  = BorderStyle.FixedSingle;
				tbInt32.ForeColor = Color.LightBlue;
				tbInt32.Font = new Font("Courier New",14,FontStyle.Italic);
				
				tbInt64.Top = 130;
				tbInt64.Left = 115;
				tbInt64.Width = 677;
				tbInt64.BackColor = Color.Black;
				tbInt64.BorderStyle  = BorderStyle.FixedSingle;
				tbInt64.ForeColor = Color.LightBlue;
				tbInt64.Font = new Font("Courier New",14,FontStyle.Italic);
				
				gProgrammer.Top = 340;
				gProgrammer.Left = 15;
				gProgrammer.Width = 800;
				gProgrammer.Height = 170;
				gProgrammer.Text = "Programmer";
				gProgrammer.Controls.AddRange(new Control[9] { lByte, lInt16, lInt32, lInt64, tbByte, tbInt16, tbInt32, tbInt64, bGetBits });
				//-----------------------------------
				// Temperature GroupBox
				lEnterValue.Top = 50;
				lEnterValue.Left = 10;
				lEnterValue.Width = 255;
				lEnterValue.Text = "Enter Value to Convert:";
				lEnterValue.Font = new Font("Courier New",13,FontStyle.Bold);
				
				tbDisplayValue.Top = 45;
				tbDisplayValue.Left = 280;
				tbDisplayValue.Width = 90;
				tbDisplayValue.Font = new Font("Courier New",14,FontStyle.Bold);
				
				lCelsToFahr.Top = 80;
				lCelsToFahr.Text = "Celsius to Fahrenheit";
				lCelsToFahr.Font = new Font("Courier New",13,FontStyle.Italic);
				lCelsToFahr.Left = 30;
				lCelsToFahr.Width = 250;
				lCelsToFahr.CheckedChanged += CelsToFahrChecked;
				
				lFahrToCels.Top = 110;
				lFahrToCels.Text = "Fahrenheit to Celsius";
				lFahrToCels.Font = new Font("Courier New",13,FontStyle.Italic);
				lFahrToCels.Left = 30;
				lFahrToCels.Width = 250;
				lFahrToCels.CheckedChanged += FahrToCelsChecked;
				
				lKelvin.Top = 140;
				lKelvin.Text = "Kelvin";
				lKelvin.Font = new Font("Courier New",13,FontStyle.Italic);
				lKelvin.Left = 30;
				lKelvin.Width = 250;
				lKelvin.CheckedChanged += KelvinChecked;
				
				lResVal.Top = 180;
				lResVal.Text = "Result of Converted Value:";
				lResVal.Font = new Font("Courier New",13,FontStyle.Italic);
				lResVal.Left = 30;
				lResVal.Width = 290;
				
				lConvert.Top = 210;
				lConvert.Text = "Convert";
				lConvert.Font = new Font("Courier New",13,FontStyle.Italic);
				lConvert.Left = 40;
				lConvert.Width = 100;
				
				lRes.Top = 210;
				lRes.Text = "C";
				lRes.Font = new Font("Courier New",13,FontStyle.Italic);
				lRes.Left = 230;
				lRes.Width = 100;
				
				bConvert = new Button();
				bConvert.Top = 250;
				bConvert.Text = "Convert";
				bConvert.Font = new Font("Courier New",14,FontStyle.Italic);
				bConvert.Left = 50;
				bConvert.Width = 100;
				bConvert.Click += ConvertTemperature;
				
				bReset = new Button();
				bReset.Top = 250;
				bReset.Text = "Reset";
				bReset.Font = new Font("Courier New",14,FontStyle.Italic);
				bReset.Left = 190;
				bReset.Width = 100;
				bReset.Click += ResetTemperature;
				
				gTemperature.Top = 30;
				gTemperature.Left = 830;
				gTemperature.Width = 380;
				gTemperature.Height = 300;
				gTemperature.Text = "Temperature";
				gTemperature.Controls.AddRange(new Control[10] { lEnterValue, tbDisplayValue,lCelsToFahr,lFahrToCels,lKelvin, lResVal, lConvert, bConvert, bReset, lRes });
				//--------------------Multiply--------------------------//
				tMultiply.Top = 60;
				tMultiply.Left = 180;
				tMultiply.Width = 100;
				
				bMultiply.Top = 160;
				bMultiply.Left = 180;
				bMultiply.Width = 130;
				bMultiply.Text = "Multiply";
				bMultiply.Click += MultiplyTable;
				
				bResetMultiply.Top = 190;
				bResetMultiply.Left = 180;
				bResetMultiply.Width = 100;
				bResetMultiply.Text = "Reset";
				bResetMultiply.Click += ResetMultiplyTable;
				
				listMultiply.Top = 30;
				listMultiply.Left = 20;
				listMultiply.Width = 150;
				listMultiply.Height = 260;
				
				gMultiply.Top = 30;
				gMultiply.Left = 1400;
				gMultiply.Width = 320;
				gMultiply.Height = 300;
				gMultiply.Text = "Multiply";
				gMultiply.Controls.AddRange(new Control[4] { listMultiply, bMultiply, bResetMultiply, tMultiply });
				//---------------------- Complex --------------------------//
				lfirstNumber.Top = 40;
				lfirstNumber.Text = "Number 1:";
				lfirstNumber.Font = new Font("Courier New",13,FontStyle.Italic);
				lfirstNumber.Left = 10;
				lfirstNumber.Width = 110;
				
				tbRE_1.Top = 40;
				tbRE_1.Left = 140;
				tbRE_1.Width = 60;
				tbRE_1.Font = new Font("Courier New",13,FontStyle.Italic);
				
				lPlus.Top = 40;
				lPlus.Left = 210;
				lPlus.Width = 20;
				lPlus.Text = "+";
				
				tbIM_1.Top = 40;
				tbIM_1.Left = 240;
				tbIM_1.Width = 60;
				tbIM_1.Font = new Font("Courier New",13,FontStyle.Italic);
				
				l_I.Top = 40;
				l_I.Left = 310;
				l_I.Width = 20;
				l_I.Text = "i";
				
				lsecondNumber.Top = 90;
				lsecondNumber.Text = "Number 2:";
				lsecondNumber.Font = new Font("Courier New",13,FontStyle.Italic);
				lsecondNumber.Left = 10;
				lsecondNumber.Width = 110;
				
				tbRE_2.Top = 90;
				tbRE_2.Left = 140;
				tbRE_2.Width = 60;
				tbRE_2.Font = new Font("Courier New",13,FontStyle.Italic);
				
				lPlus_1.Top = 90;
				lPlus_1.Left = 210;
				lPlus_1.Width = 20;
				lPlus_1.Text = "+";
				
				tbIM_2.Top = 90;
				tbIM_2.Left = 240;
				tbIM_2.Width = 60;
				tbIM_2.Font = new Font("Courier New",13,FontStyle.Italic);
				
				l_I_1.Top = 90;
				l_I_1.Left = 310;
				l_I_1.Width = 20;
				l_I_1.Text = "i";
				
				lOperationComplex.Top = 148;
				lOperationComplex.Text = "Operation:";
				lOperationComplex.Font = new Font("Courier New",13,FontStyle.Italic);
				lOperationComplex.Left = 10;
				lOperationComplex.Width = 120;
				
				cbOperation.Top = 148;
				cbOperation.Text = "Operation";
				cbOperation.Font = new Font("Courier New",13,FontStyle.Italic);
				cbOperation.Left = 160;
				cbOperation.Width = 120;
				cbOperation.Items.AddRange(new Object[15] { "Add(N+N)", "Sub(N-N)", "Mul(N*N)", "Div(N/N)", "Conjugate(N)", "Exp(N)","Sin(N)","Cos(N)","Tan(N)","Arcsin(N)","Arccos(N)","Arctan(N)","Log(N)","Pow(N)","Sqrt(N)" });
				
				lResult.Top = 190;
				lResult.Text = "Result:  2 + 3i";
				lResult.Font = new Font("Courier New",13,FontStyle.Italic);
				lResult.Left = 10;
				lResult.Width = 350;
				
				bCalcComplex.Top = 240;
				bCalcComplex.Left = 30;
				bCalcComplex.Width = 140;
				bCalcComplex.Text = "Calculate";
				bCalcComplex.Click += ComplexOperations;
				
				bResetComplex.Top = 240;
				bResetComplex.Left = 210;
				bResetComplex.Width = 100;
				bResetComplex.Text = "Reset";
				bResetComplex.Click += ResetComplex;
				
				gComplex.Top = 30;
				gComplex.Left = 830;
				gComplex.Width = 380;
				gComplex.Height = 300;
				gComplex.Text = "Complex Calculate";
				gComplex.Controls.AddRange(new Control[15] { lfirstNumber,tbRE_1,lPlus, tbIM_1,l_I, lsecondNumber, tbRE_2,lPlus_1, tbIM_2 ,l_I_1,lOperationComplex ,cbOperation , lResult,bCalcComplex, bResetComplex });
				//---------------------------------------------------------------------
				//Quadratic GroupBox
				lInfo.Top = 40;
				lInfo.Text = "Bведите коэффициенты(a <> 0):";
				lInfo.Font = new Font("Courier New",13,FontStyle.Bold);
				lInfo.Left = 10;
				lInfo.Width = 330;
				
				lA.Top = 85;
				lA.Text = "A =";
				lA.Font = new Font("Courier New",13,FontStyle.Bold);
				lA.Left = 10;
				lA.Width = 60;
				
				lB.Top = 120;
				lB.Text = "B = ";
				lB.Font = new Font("Courier New",13,FontStyle.Bold);
				lB.Left = 10;
				lB.Width = 60;
				
				lC.Top = 155;
				lC.Text = "C =";
				lC.Font = new Font("Courier New",13,FontStyle.Bold);
				lC.Left = 10;
				lC.Width = 60;
				
				tbA.Top = 80;
				tbA.Left = 90;
				tbA.Width = 60;
				tbA.Font = new Font("Courier New",13,FontStyle.Italic);
				
				tbB.Top = 115;
				tbB.Left = 90;
				tbB.Width = 60;
				tbB.Font = new Font("Courier New",13,FontStyle.Italic);
				
				tbC.Top = 150;
				tbC.Left = 90;
				tbC.Width = 60;
				tbC.Font = new Font("Courier New",13,FontStyle.Italic);
				
				bCalc.Top = 90;
				bCalc.Left = 180;
				bCalc.Width = 140;
				bCalc.Text = "Calculate";
				bCalc.Click += QuadrEquality;
				
				bResetQuadr.Top = 130;
				bResetQuadr.Left = 180;
				bResetQuadr.Width = 140;
				bResetQuadr.Text = "Reset";
				bResetQuadr.Click += ResetQuadrEquality;
				
				lRoots.Top = 185;
				lRoots.Text = "Корни: ";
				lRoots.Font = new Font("Courier New",14,FontStyle.Italic);
				lRoots.Left = 10;
				lRoots.Width = 360;
				
				gQuadr.Top = 30;
				gQuadr.Left = 830;
				gQuadr.Width = 380;
				gQuadr.Height = 300;
				gQuadr.Text = "Quadratic Equality";
				gQuadr.Controls.AddRange(new Control[10] { lInfo, lA,lB,lC,tbA,tbB,tbC,bCalc,bResetQuadr, lRoots });
				//-----------------------------------Frac------------------------------------//
				ltypeFrac = new Label();
				ltypeFrac.Text = "Вид дроби: ";
				ltypeFrac.Top =  30;
				ltypeFrac.Left = 20;
				ltypeFrac.Width =150;
				pFrac = new RadioButton();
				pFrac.Text ="простые";
				pFrac.Top = 60;
				pFrac.Left = 20;
				pFrac.Width =170;
				mixedFrac = new RadioButton();
				mixedFrac.Text ="смешанные";
				mixedFrac.Top = 60;
				mixedFrac.Left =190;
				mixedFrac.Width =170;
				//поля для ввода текста
				tb1 =new TextBox();
				tbNumerator1 = new TextBox();
				tbDenominator1 = new TextBox();
				tb2 =new TextBox();
				tbNumerator2 = new TextBox();
				tbDenominator2 = new TextBox();
				tbNumeratorRes = new TextBox();
				tbDenominatorRes = new TextBox();
				//1-я дробь
				tb1.Left =50;
				tb1.Top =120;
				tb1.Left =10;
				tb1.Width =30;
				tbNumerator1.Left =50;
				tbNumerator1.Top =100;
				tbNumerator1.Width =40;
				tbDenominator1.Left =50;
				tbDenominator1.Top =140;
				tbDenominator1.Width =40;
				//список с операциями
				operationFrac = new ComboBox();
				operationFrac.Left = 100;
				operationFrac.Top =120;
				operationFrac.Width =70;
				operationFrac.Text ="O";
				operationFrac.Items.Add("Add(+)");operationFrac.Items.Add("Sub(-)");
				operationFrac.Items.Add("Mul(*)");operationFrac.Items.Add("Div(/)");
				//знак "="
				lResEqual = new Label();
				lResEqual.Text = "=";
				lResEqual.Left =280;
				lResEqual.Top = 120;
				lResEqual.Width =20;
				//2-я дробь
				tb2.Left =190;
				tb2.Top =120;
				tb2.Width =30;
				tbNumerator2.Left =230;
				tbNumerator2.Top =100;
				tbNumerator2.Width =40;
				tbDenominator2.Left =230;
				tbDenominator2.Top =140;
				tbDenominator2.Width =40;
				//3-я дробь
				tbNumeratorRes.Left =320;
				tbNumeratorRes.Top =100;
				tbNumeratorRes.Width =40;
				tbDenominatorRes.Left =320;
				tbDenominatorRes.Top =140;
				tbDenominatorRes.Width =40;
				bRes = new Button();
				bRes.Text ="Calc";
				bRes.Left =20;
				bRes.Top =200;
				bRes.Size =new Size(130,50);
				bRes.Click += FracOperations;
				
				bClear = new Button();
				bClear.Text ="Clear";
				bClear.Left =180;
				bClear.Top =200;
				bClear.Size =new Size(130,50);
				bClear.Click += ResetFracOperations;
				
				// gFrac
				gFrac.Controls.AddRange(new Control [15] { ltypeFrac,tb1,tb2,tbNumerator1,tbNumerator2,operationFrac,pFrac,mixedFrac,tbDenominator1,tbDenominator2,lResEqual,tbNumeratorRes,tbDenominatorRes,bRes, bClear });
				gFrac.Top = 30;
				gFrac.Left = 830;
				gFrac.Width = 380;
				gFrac.Height = 300;
				gFrac.Font = new Font("Courier New", 13, FontStyle.Italic);
				gFrac.Text  = "Дробный калькулятор";
				#endregion
				
				menu.Items.AddRange(new ToolStripItem[4] { file, edit, view, bitcalc });
				f.Controls.Add(menu);
				f.Controls.AddRange(new Control [57] { lExample, tbDisplay, b0, b1, b2, b3, b4, b5, b6, b7, b8, b9, bC, bCE, bPoint, bClDelete, bPlus, bMinus, bMul, bDiv, bEqual, bPM,
				   bPower2,bPow,bPi, bLog, bSinh, bCosh, bTanh, bExp, bSin, bCos, bTan,bSec, bSech, bCSec, bCSech, bNqrt, bSqrt, b1_x, bLn, bPercent, bDec, bBin, bHex, bOct, bFact, bGamma, bPFunc, bSf,bInt, bFact2, bFact3,bPrim,bHFact, bNod, BAntLog });
				f.Controls.Add(gTemperature);
				f.Controls.Add(gMultiply);
				f.Controls.Add(gFrac);
				f.Controls.Add(gComplex);
				f.Controls.Add(gQuadr);
				f.Controls.Add(gProgrammer);
			 }
		
		     #region "HANDLERS"
		     public void STANDART_STATE(object sender, EventArgs e) {
		     	CalculatorState.Standart();
		     }
		     
		     public void SCIENTIFIC_STATE(object sender, EventArgs e) {
		     	CalculatorState.Scientific();
		     }
		     
		     public void FRAC_STATE(object sender, EventArgs e) {
		     	CalculatorState.Frac();
		     }
		     
		     public void COMPLEX_STATE(object sender, EventArgs e) {
		     	CalculatorState.Complexis();
		     }
		     
		     public void QUADRATIC_STATE(object sender, EventArgs e) {
		     	CalculatorState.Quadratic();
		     }
		     
		     public void TEMPERATURE_STATE(object sender, EventArgs e) {
		     	CalculatorState.Temperature();
		     }
		     
		     public void MULTIPLICATION_STATE(object sender, EventArgs e) {
		     	CalculatorState.Multiplication();
		     }
		     
		     public void PROGRAMMER_STATE(object sender, EventArgs e) {
		     	CalculatorState.Programmer();
		     }
		     
		     public void EXIT(object sender, EventArgs e) {
		     	Application.Exit();
		     }
		     
		     public void BITCALC_EXECUTE(object sender, EventArgs e) {
		     	string BITCALC_PATH = @"I:\Разработки 2020\Exe 2020\CALC_BITWISECALC\BITWISE V1.4.0.0.exe";
		     	System.Diagnostics.Process.Start(BITCALC_PATH);
		     }
		     //Добавляет цифры и точку
		     public void AddDigitsAndPoint(object sender, EventArgs e) {
		     	if (tbDisplay.Text == "0" || enter_value) {
		     		tbDisplay.Text = "";
		     	}
		     	enter_value = false;
		     	Button num = (Button)sender;
		     	if (num.Text == ".") {
		     		if (!tbDisplay.Text.Contains(".")) {
		     			tbDisplay.Text += num.Text;
		     		}
		     	} else {
		     		tbDisplay.Text += num.Text;
		     	}
		     	lExample.Text = tbDisplay.Text;
		     }
		     //Запоминает первое число и оператор
		     public void MemorizeFirstNumberAndOperator(object sender, EventArgs e) {
		     	NUMBER1 = Double.Parse(tbDisplay.Text, CultureInfo.InvariantCulture);
		     	OPERATOR = ((Button)sender).Text;
		     	tbDisplay.Text = "";
		     	lExample.Text = lExample.Text + "" + OPERATOR;
		     }
		     
		     public void RemoveSymbol(object sender, EventArgs e) {
		     	CalculatorOperation.RemoveSymbol();
		     }
		     // Принимает второе число и возвращает результат
		     public void GetResult(object sender, EventArgs e) {
		     	NUMBER2 = Double.Parse(tbDisplay.Text, CultureInfo.InvariantCulture);
		     	lExample.Text = "";
		     	
		     	switch(OPERATOR) {
		     		case "+":   RESULT = NUMBER1 + NUMBER2; break; 
		     		case "-":   RESULT = NUMBER1 - NUMBER2; break;
					case "*":   RESULT = NUMBER1 * NUMBER2; break; 
		     		case "/":   RESULT = NUMBER1 / NUMBER2; break;
		     		case "^":   RESULT = Math.Pow(NUMBER1, NUMBER2); break;
		     		case "Nqrt": {
		     			//Здесь NUMBER1 - число, NUMBER2 - степень корня
		     			RESULT = Math.Pow(NUMBER1, 1/NUMBER2);
		                tbDisplay.Text = RESULT.ToString();
		                lExample.Text = String.Format("NQRT({0},{1})= {2}",NUMBER1, NUMBER2, RESULT);
		                break;
		     		}
		     		case "ALG": {
		     			//Здесь NUMBER1 - число , NUMBER2 - основание
		     			RESULT = Math.Pow(NUMBER2, NUMBER1);
	                    tbDisplay.Text = RESULT.ToString();
	                    lExample.Text = String.Format("AntiLog({0},{1})= {2}", NUMBER2, NUMBER1, RESULT);
	                    break;
		     		}
		     		case "GCD": {
		     			RESULT = CalculatorAPI.GCD(NUMBER1, NUMBER2);
	                    tbDisplay.Text = RESULT.ToString();
	                    lExample.Text = String.Format("GCD({0},{1})=  {2}", NUMBER1, NUMBER2, RESULT);
	                    break;
		     		}
		     		
		     		default : throw new ArithmeticException("Неизвестный оператор");
		     	}
		     	
		     	if (OPERATOR == "+" || OPERATOR == "-" || OPERATOR == "*" || OPERATOR == "/" || OPERATOR == "^") {
		     		tbDisplay.Text = RESULT.ToString();
          			lExample.Text = tbDisplay.Text;
		     	}
		     }
		     // Вычисляет квадратный корень
		     public void Sqrt(object sender, EventArgs e) {
		     	double result = 0.0;
		     	NUMBER1 = Convert.ToDouble(tbDisplay.Text);
		     	result = Math.Sqrt(NUMBER1);
		     	tbDisplay.Text = Convert.ToString(result);
		     	lExample.Text = "SQRT("+ NUMBER1 + ")= " + tbDisplay.Text;
		     }
		     //Вычисляет процент от числа
		     public void Percent(object sender, EventArgs e) {
		     	double percent = Convert.ToDouble(tbDisplay.Text) / Convert.ToDouble(100);
		     	tbDisplay.Text = Convert.ToString(percent);
		     }
		     
		     public void ComplexOperations(object sender, EventArgs e) {
		     	double Re_1, Re_2, Im_1, Im_2;
		     	Complex complex1, complex2, result;
		     	
		     	Re_1 = Convert.ToDouble(tbRE_1.Text);//действ. часть 1 числа
		     	Re_2 = Convert.ToDouble(tbRE_2.Text);//действ. часть 2 числа
		     	Im_1 = Convert.ToDouble(tbIM_1.Text);//мнимая часть 1 числа
		     	Im_2 = Convert.ToDouble(tbIM_2.Text);//мнимая часть 2 числа
		     	complex1 = new Complex(Re_1, Im_1);
		     	complex2 = new Complex(Re_2, Im_2);
		     	result = new Complex(0,0);
		     	
		     	int selectOperator = cbOperation.SelectedIndex;
		     	
		     	switch(selectOperator) {
		     		case 0: result = complex1 + complex2; break;
		     		case 1: result = complex1 - complex2; break;
		     		case 2: result = complex1 * complex2; break;
		     		case 3: result = complex1 / complex2; break;
		     		case 4: result = Complex.Conjugate(complex1); break;
		     		case 5: result = Complex.Exp(complex1); break;
		     		case 6: result = Complex.Sin(complex1); break;
		     		case 7: result = Complex.Cos(complex1); break;
		     		case 8: result = Complex.Tan(complex1); break;
		     		case 9: result = Complex.Asin(complex1); break;
		     		case 10: result = Complex.Acos(complex1); break;
		     		case 11: result = Complex.Atan(complex1); break;
		     		case 12: result = Complex.Log(complex1); break;
		     		case 13: result = Complex.Pow(complex1, complex2); break;
		     		case 14: result = Complex.Sqrt(complex1); break;
		     	}
		     	
		     	lResult.Text = "Result:  " + Math.Round(result.Real,3) + '+' + Math.Round(result.Imaginary,3) + 'i';
		     }
		    
		     public void ResetComplex(object sender, EventArgs e) {
		     	  tbRE_1.Clear();
			      tbRE_2.Clear();
			      tbIM_1.Clear();
			      tbIM_2.Clear();
			      lResult.Text = "Result: 0";
		     }
		     //Вывод таблицы умножения для указанного числа
		     public void MultiplyTable(object sender, EventArgs e) {
		     	 listMultiply.Items.Clear();
		     	 int mult = Convert.ToInt32(tMultiply.Text);
		     	 for (int i = 1; i < 13; ++i) {
		     	 	listMultiply.Items.Add(i + "x" + mult + "= " + i*mult);
		     	 }
		     }
		     
		     public void ResetMultiplyTable(object sender, EventArgs e) {
		     	CalculatorState.ClearFields(tMultiply);
		     	listMultiply.Items.Clear();
		     }
		     
		     public void CelsToFahrChecked(object sender, EventArgs e) {
		     	iOperation = 'C';
		     }
		     
		     public void FahrToCelsChecked(object sender, EventArgs e) {
		     	iOperation = 'F';
		     }
		     
		     public void KelvinChecked(object sender, EventArgs e) {
		     	iOperation = 'K';
		     }
		     
		     public void ConvertTemperature(object sender, EventArgs e) {
		     	switch (iOperation) {
		     			case 'C': {
		     				iCelsius = Double.Parse(tbDisplayValue.Text, CultureInfo.InvariantCulture);
                  			lRes.Text = ((((9 * iCelsius)/5) + 32).ToString());
                  			break;
		     			}
		     			case 'F': {
		     				iFahrenheit = Double.Parse(tbDisplayValue.Text, CultureInfo.InvariantCulture);
                   			lRes.Text = ((((iFahrenheit - 32)* 5)/9).ToString());
                   			break;
		     			}
		     			case 'K': {
		     				iKelvin = Double.Parse(tbDisplayValue.Text, CultureInfo.InvariantCulture);
                   			lRes.Text = (((((9 * iKelvin) / 5) + 32) + 273.15).ToString());
                   			break;
		     			}
		     	}
		     }
		     
		     public void ResetTemperature(object sender, EventArgs e) {
		     	CalculatorState.ClearFields(tbDisplayValue);
       			lRes.Text = "";
		     }
		    
		     public void GammaFunction(object sender, EventArgs e) {
		     	double gamma;
		     	int value = Convert.ToInt32(tbDisplay.Text);
		     	if ((value == 1) || (value == 2)) gamma = 1;
		        else gamma = CalculatorAPI.Factorial(value-1);
		        
		        tbDisplay.Text = gamma.ToString();
		        lExample.Text = "GAMMA("+ value + ")= " + tbDisplay.Text;
		     }
		     
		     public void PiFunction(object sender, EventArgs e) {
		     	long pifunc;
		     	int value = Convert.ToInt32(tbDisplay.Text);
		     	pifunc = CalculatorAPI.Factorial(value);
		     	
		     	tbDisplay.Text = pifunc.ToString();
      			lExample.Text = "PIFUNC(" + value + ")= " + tbDisplay.Text;
		     }
		     
		     public void Power2(object sender, EventArgs e) {
		       double value = Convert.ToDouble(tbDisplay.Text);
		       tbDisplay.Text = Convert.ToString(Math.Pow(value,2)); 
		       lExample.Text = value + "^2 = " + tbDisplay.Text;
		     }
		     
		     public void Int(object sender, EventArgs e) {
		     	  NUMBER1 = Convert.ToDouble(tbDisplay.Text, CultureInfo.InvariantCulture);
			      RESULT = Math.Round(NUMBER1);
			      tbDisplay.Text = RESULT.ToString();
			      lExample.Text = String.Format("INT({0}) = {1}",NUMBER1, tbDisplay.Text);
		     }
		     
		     public void OtherOperation(object sender, EventArgs e) {
		     	 NUMBER1 = Convert.ToDouble(tbDisplay.Text);
		     	 
		     	 switch (((Button)sender).Text) {
		     	 	case"PI": {
		     	 		tbDisplay.Text = "3.141592653589976323";
                 		lExample.Text = tbDisplay.Text;
                 		break;
		     	 	}
		     	 	case "EXP": {
		     	 		tbDisplay.Text = Math.Exp(NUMBER1).ToString();
                 		lExample.Text = "EXP(" + NUMBER1 + ")= " + tbDisplay.Text;
                 		break;
		     	 	}
		     	 	case "Log": {
		     	 	   double ilog = Math.Log10(NUMBER1);
	                   tbDisplay.Text = Convert.ToString(ilog);
	                   lExample.Text = "LOG(" + NUMBER1 + ")= "+ tbDisplay.Text;
	                   break;
		     	 	}
		     	 	case "1/x": {
		     	 		tbDisplay.Text = Convert.ToString(1 / NUMBER1);
                        lExample.Text = "1/"+ NUMBER1 +"= " + tbDisplay.Text;
                        break;
		     	 	}
		     	 	case "Ln": {
		     	 		tbDisplay.Text = Convert.ToString(Math.Log(NUMBER1));
                 	    lExample.Text = "LN("+ NUMBER1 +")= " + tbDisplay.Text;
                 	    break;
		     	 	}
		     	 		
		     	 }
		     }
		     
		     public void Factorial(object sender, EventArgs e) {
		     	NUMBER1 = Convert.ToDouble(tbDisplay.Text);
		     	long fact = CalculatorAPI.Factorial((int)NUMBER1);
		     	lExample.Text = NUMBER1 + "! = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void DoubleFactorial(object sender, EventArgs e) {
		     	int dfact = Convert.ToInt32(Convert.ToDouble(tbDisplay.Text));
		     	long fact = CalculatorAPI.DoubleFactorial(dfact);
		     	lExample.Text = dfact + "!! = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void TripleFactorial(object sender, EventArgs e) {
		     	int tfact = Convert.ToInt32(Convert.ToDouble(tbDisplay.Text));
		     	long fact = CalculatorAPI.TripleFactorial(tfact);
		     	lExample.Text = tfact + "!!! = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void SuperFactorial(object sender, EventArgs e) {
		     	int sfact = Convert.ToInt32(Convert.ToDouble(tbDisplay.Text));
		     	long fact = CalculatorAPI.SuperFactorial(sfact);
		     	lExample.Text = "Sf("+ sfact + ") = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void HyperFactorial(object sender, EventArgs e) {
		     	int hfact = Convert.ToInt32(Convert.ToDouble(tbDisplay.Text));
		     	long fact = CalculatorAPI.HyperFactorial(hfact);
		     	lExample.Text = "Hf("+ hfact + ") = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void Primorial(object sender, EventArgs e) {
		     	int prim = Convert.ToInt32(Convert.ToDouble(tbDisplay.Text));
		     	BigInteger fact = CalculatorAPI.Primorial(prim);
		     	lExample.Text = "Pr("+ prim + ") = " + fact;
		     	tbDisplay.Text = Convert.ToString(fact);
		     }
		     
		     public void QuadrEquality(object sender, EventArgs e) {
		     	double x1, x2;
		     	double a = Convert.ToDouble(tbA.Text);
		     	double b = Convert.ToDouble(tbB.Text);
		     	double c = Convert.ToDouble(tbC.Text);
		     	double D = Math.Pow(b, 2) - 4*a*c;
		     	
		     	if (D < 0) {
		     		lRoots.Text = "Нет вещественных корней!";
		     	} else if (D  == 0) {
		     		x1 = - b / (2 * a);
      				lRoots.Text = "1 корень: " + x1;
		     	} else {
		     		x1 = (-b + Math.Sqrt(D))/(2 * a);
			        x2 = (-b - Math.Sqrt(D))/(2 * a);
			        lRoots.Text = "2 корня: " + Math.Round(x1,7)+ "; "+ Math.Round(x2,7);
		     	}
		     }
		     
		     public void ResetQuadrEquality(object sender, EventArgs e) {
		     	tbA.Clear();
			    tbB.Clear();
			    tbC.Clear();
			    lRoots.Text = "";
		     }
		     
		     public void CopyNumber(object sender, EventArgs e) {
		     	CalculatorState.CopyNumber(tbDisplay);
		     }
		     
		     public void PasteNumber(object sender, EventArgs e) {
		     	CalculatorState.PasteNumber(tbDisplay);
		     }
		     
		     public void ClearDisplay(object sender, EventArgs e) {
		     	CalculatorState.ClearInputField();
		     }
		     
		     public void ReverseSign(object sender, EventArgs e) {
		     	double value = Double.Parse(tbDisplay.Text, CultureInfo.InvariantCulture);
		     	value = -value;
		     	tbDisplay.Text = Convert.ToString(value);
      			lExample.Text = Convert.ToString(value);
		     }
		     
		     public void EnableDegrees(object sender, EventArgs e) {
		          isDeg = true;
      			  isRad = false;
		     }
		     
		     public void EnableRadians(object sender, EventArgs e) {
		     	isDeg = false;
      			isRad = true;
		     }
		     
		     public void FracOperations(object sender, EventArgs e) {
		     	Frac F1 = new Frac(Convert.ToInt32(tbNumerator1.Text),Convert.ToInt32(tbDenominator1.Text));
		     	Frac F2 = new Frac(Convert.ToInt32(tbNumerator2.Text),Convert.ToInt32(tbDenominator2.Text));
		        Frac FRES = new Frac();
		        double n = F1.Numerator;
		        double d = F1.Denominator;
		        double n1 = F2.Numerator;
		        double d1 = F2.Denominator;
		        operationFrac.SelectionStart = 0;
		        int operations = operationFrac.SelectedIndex;
		        
		        if (pFrac.Checked) {
		        	switch (operations) {
		        			case 0: {
		        				FRES = F1.Add(F2);
		        				break;
		        			}
		        			case 1: {
		        				FRES = F1.Subtract(F2);
		        				break;
		        			}
		        			case 2: {
		        				FRES = F1.Multiply(F2);
		        				break;
		        			}
		        			case 3: {
		        				FRES = F1.Divide(F2);
		        				break;
		        			}
		        	}
		        	tbNumeratorRes.Text = FRES.Numerator.ToString();
                    tbDenominatorRes.Text = FRES.Denominator.ToString();
		        } else {
		        	int t1 =Convert.ToInt32(tb1.Text);
       				int t2 =Convert.ToInt32(tb2.Text);
       				
       				double denominatorMixed;
       				
       				switch (operations) {
		        			case 0: {
       							denominatorMixed = d * d1;
       							double addMixed = d1*(d*t1+n)+ d*(d1*t2+n1);
		        				tbNumeratorRes.Text = Convert.ToString(addMixed);
                   				tbDenominatorRes.Text = Convert.ToString(denominatorMixed);
		        				break;
		        			}
		        			case 1: {
		        				denominatorMixed = d * d1;
       							double subMixed = d1*(d*t1+n)- d*(d1*t2+n1);
		        				tbNumeratorRes.Text = Convert.ToString(subMixed);
                   				tbDenominatorRes.Text = Convert.ToString(denominatorMixed);
		        				break;
		        			}
		        			case 2: {
		        				denominatorMixed = d * d1;
       							double mulMixed = d*d1*t1*t2+ d*n1*t1+ n*d1*t2+ n*n1;
		        				tbNumeratorRes.Text = Convert.ToString(mulMixed);
                   				tbDenominatorRes.Text = Convert.ToString(denominatorMixed);
		        				break;
		        			}
		        			case 3: {
		        				denominatorMixed = d* (t2*d1 + n1);
       							double divMixed = d1*(t1*d + n);
		        				tbNumeratorRes.Text = Convert.ToString(divMixed);
                   				tbDenominatorRes.Text = Convert.ToString(denominatorMixed);
		        				break;
		        			}
		        	}
		        }
		     }
		     
		     public void ResetFracOperations(object sender, EventArgs e) {
		     	 tb1.Clear();
			     tb2.Clear();
			     tbNumerator1.Clear();
			     tbNumerator2.Clear();
			     tbNumeratorRes.Clear();
			     tbDenominator1.Clear();
			     tbDenominator2.Clear();
			     tbDenominatorRes.Clear();
		     }
		     
		     public void NumericalSystems(object sender, EventArgs e) {
		     	int result = Convert.ToInt32(tbDisplay.Text);
		     	
		     	switch (((Button)sender).Text) {
		     		case "BIN": {
		     			tbDisplay.Text = Convert.ToString(result, 2);
                 		lExample.Text = result + "(2)= " + tbDisplay.Text;
                 		break;
		     		}
		     		case "HEX": {
		     				tbDisplay.Text = Convert.ToString(result, 16).ToUpper();
                 		lExample.Text = result + "(16)= " + tbDisplay.Text;
                 		break;
		     		}
		     		case "OCT": {
		     			tbDisplay.Text = Convert.ToString(result, 8);
                 		lExample.Text = result + "(8)= " + tbDisplay.Text;
                 		break;
		     		}
		     		case "DEC": {
		     			tbDisplay.Text = Convert.ToString(result, 10);
                 		lExample.Text = result + "(10)= " + tbDisplay.Text;
                 		break;
		     		}
		     	}
		     }
		     
		     public void TrigonometricOperations(object sender, EventArgs e) {
		     	NUMBER1 = Convert.ToDouble(tbDisplay.Text);
		     	
		     	if (isDeg) {
		     		#region Degrees
		     		switch(((Button)sender).Text) {
		     				case "Sin": {
		     					 RESULT = (Math.Sin(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SIN("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Cos": {
		     					 RESULT = (Math.Cos(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COS("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Tan": {
		     					 RESULT = (Math.Tan(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "TAN("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sinh": {
		     					 RESULT = (Math.Sinh(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SINH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Cosh": {
		     					 RESULT = (Math.Cosh(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COSH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Tanh": {
		     					 RESULT = (Math.Tanh(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "TANH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sec": {
		     					 RESULT = (CalculatorAPI.Sec(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SEC("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Csc": {
		     					 RESULT = (CalculatorAPI.Cosec(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COSEC("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sech": {
		     					 RESULT = (CalculatorAPI.Sech(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SECH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Csch": {
		     					 RESULT = (CalculatorAPI.Csch(CalculatorAPI.RadToDeg(NUMBER1)));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "CSCH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     		}
		     		#endregion
		     	} else {
		     		#region Radians
		     		switch(((Button)sender).Text) {
		     				case "Sin": {
		     					 RESULT = (Math.Sin(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SIN("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Cos": {
		     					 RESULT = (Math.Cos(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COS("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Tan": {
		     					 RESULT = (Math.Tan(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "TAN("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sinh": {
		     					 RESULT = (Math.Sinh(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SINH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Cosh": {
		     					 RESULT = (Math.Cosh(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COSH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Tanh": {
		     					 RESULT = (Math.Tanh(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "TANH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sec": {
		     					 RESULT = (CalculatorAPI.Sec(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SEC("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Csc": {
		     					 RESULT = (CalculatorAPI.Cosec(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "COSEC("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Sech": {
		     					 RESULT = (CalculatorAPI.Sech(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "SECH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     				case "Csch": {
		     					 RESULT = (CalculatorAPI.Csch(NUMBER1));
			                     tbDisplay.Text = Convert.ToString(RESULT);
			                     lExample.Text = "CSCH("+ NUMBER1 + ")= " + tbDisplay.Text;
			                     break;
		     				}
		     		}
		     		#endregion
		     	}
		     }
		     
		     // ADDED 11 Feb 2020
		     public void PrintBitsFromNumber(object sender, EventArgs e) {
		     	tbByte.Text = BitwiseAPI.PrintBitsFromByte(Convert.ToInt64(tbDisplay.Text));
		     	tbInt16.Text = BitwiseAPI.PrintBitsFromInt16(Convert.ToInt64(tbDisplay.Text));
		     	tbInt32.Text = BitwiseAPI.PrintBitsFromInt32(Convert.ToInt64(tbDisplay.Text));
		     	tbInt64.Text = BitwiseAPI.PrintBitsFromInt64(Convert.ToInt64(tbDisplay.Text));
		     }
		     #endregion
		 
		}
		
		public static CalculatorForm MAIN;
		public static CalculatorForm form = new CalculatorForm();
		public static void Main(string[] args)
		{
			MAIN = Program.form;
			Application.Run(form.f);
		}
	}
}
