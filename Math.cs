using System;
using System.Collections.Generic;
using System.Numerics;

namespace MathF
//FIXME: Complex - добавить функции Asin, Acos и т.п.
{
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    sealed class DynamicallyInvokableAttribute : Attribute { }
    //Функция преобразования.
    public delegate double TransformFunc(double x);
    //Интегрируемая функция

    public delegate double IntegFunc(double x);
    /// <summary>
    /// Константы библиотеки
    /// </summary>
    [DynamicallyInvokable]
    class LibraryConst
    {
        public const double Eps = 1e-5;
        //Число Эйлера
        public const double E = 2.718281828459045;
        //Число PI
        public const double PI = 3.1415926535897932385;
        //Число TAU
        public const double TAU = 2 * PI;
        ///Pi деленное на 2
        public const double PI_2 = 1.57079632679489661923;
        ///Pi деленное на 4
        public const double PI_4 = 0.785398163397448309616;
        public const double RAD2DEG = 180 / PI;
        public const double DEG2RAD = PI / 180;
        ///2 / sqrt(PI)
        public const double _2_SQRT_PI = 1.12837916709551257390;
        ///1/PI
        public const double _1_PI = 0.318309886183790671538;
        ///2/PI
        public const double _2_PI = 0.636619772367581343076;
        ///log2(e)
        public const double LOG2E = 1.44269504088896340736;
        ///log10(e)
        public const double LOG10E = 0.434294481903251827651;
        ///ln(2)
        public const double LN2 = 0.693147180559945309417;
        ///ln(10)
        public const double LN10 = 2.30258509299404568402;
        ///sqrt(2)
        public const double SQRT2 = 1.41421356237309504880;
        ///1/ sqrt(2)
        public const double _1_SQRT2 = 0.707106781186547524401;
        ///ln(PI)
        public const double LNPI = 1.1447298858494001741;
        ///Sqrt(E)
        public const double SqrtE = 1.6487212707001281468;
        ///Константа Миллса
        public const double Mills = 1.3063778838630806904686144926;
        ///Константа Апери
        public const double Aperi = 1.2020569031595942853997381615114;
        ///Постоянная Гельфонда-Шнайдера(2 ** sqrt(2))
        public const double GS = 2.6651441426902251886502972498;
        ///Пластическое число
        public const double Plast = 1.324717957244746025960908;
        ///Золотое сечение
        public const double GoldenRatio = 1.6180339887498948482;
        ///Серебряное сечение
        public const double SilverRatio = 2.4142135623;
        ///Бронзовое сечение
        public const double BronseRatio = 3.30277563773;
        ///Сверхзолотое сечение
        public const double SuperGoldenRation = 1.46557123187676802665;
        ///Постоянная Эйлера — Маскерони
        public const double EulerMascheroni = 0.5772156649015328606;
        ///Постоянная Каэна
        public const double Kaen = 0.64341054629;
        ///Константа Майсселя — Мертенса
        public const double MaisselMertens = 0.26149721284764278375542683860869585;
        ///Предел Лапласа
        public const double LimitLaplas = 0.6627441934918158097474209710925290;
        ///Константа Ландау — Рамануджана
        public const double LandauRamanudgana = 0.76422365358922066;
        ///Константа Хайтина
        public const double Haitin = 0.0078749969978123844;
        ///Константа Лежандра
        public const double Legandr = 1;
        public const double Glaisher = 1.2824271291006226369;
        public const double Khinchin = 2.6854520010653064453;
        ///Скорость света в вакууме
        public const double SpeedOfLight = 2.99792458e+8;
        public const double MagneticPermeability = 1.2566370614359172954e-6;
        public const double ElectricPermittivity = 8.8541878171937079245e-12;
        public const double CharacteristicImpedanceVacuum = 376.7303134617706554682;
        ///Гравитационная постоянная
        public const double GravitationalConstant = 6.67429e-11;
        ///Постоянная Планка
        public const double PlancksConstant = 6.62606896e-34;
        ///Постоянная Дирака
        public const double DiracsConstant = 1.054571629e-34;
        ///Планковская масса
        public const double PlancksMass = 2.17644e-8;
        ///Планковская температура
        public const double PlancksTemperature = 1.416786e+32;
        ///Планковская длина
        public const double PlancksLength = 1.616253e-35;
        ///Планковское время
        public const double PlancksTime = 5.39124e-44;
        ///Элементарный заряд
        public const double ElementaryCharge = 1.602176487e-19;
        public const double MagneticFluxQuantum = 2.067833668e-15;
        public const double ConductanceQuantum = 7.7480917005e-5;
        public const double JosephsonConstant = 483597.891e+9;
        public const double VonKlitzingConstant = 25812.807557;
        public const double BohrMagneton = 927.400915e-26;
        public const double NuclearMagneton = 5.05078324e-27;
        public const double FineStructureConstant = 7.2973525376e-3;
        public const double RydbergConstant = 10973731.568528;
        public const double BohrRadius = 0.52917720859e-10;
        public const double HartreeEnergy = 4.35974394e-18;
        public const double QuantumOfCirculation = 3.6369475199e-4;
        public const double FermiCouplingConstant = 1.16637e-5;
        public const double WeakMixingAngle = 0.22256;
        public const double Avogadro = 6.0221412927e23;
        //Электрон
        public const double ElectronMass = 9.10938215e-31;
        public const double ElectronMassEnergyEquivalent = 8.18710438e-14;
        public const double ElectronMolarMass = 5.4857990943e-7;
        public const double ComptonWavelength = 2.4263102175e-12;
        public const double ClassicalElectronRadius = 2.8179402894e-15;
        public const double ThomsonCrossSection = 0.6652458558e-28;
        public const double ElectronMagneticMoment = -928.476377e-26;
        public const double ElectronGFactor = -2.0023193043622;
        //Мюоны
        public const double MuonMass = 1.88353130e-28;
        public const double MuonMassEnegryEquivalent = 1.692833511e-11;
        public const double MuonMolarMass = 0.1134289256e-3;
        public const double MuonComptonWavelength = 11.73444104e-15;
        public const double MuonMagneticMoment = -4.49044786e-26;
        public const double MuonGFactor = -2.0023318414;
        //Тау
        public const double TauMass = 3.16777e-27;
        public const double TauMassEnergyEquivalent = 2.84705e-10;
        public const double TauMolarMass = 1.90768e-3;
        public const double TauComptonWavelength = 0.69772e-15;
        //Протон
        public const double ProtonMass = 1.672621637e-27;
        public const double ProtonMassEnergyEquivalent = 1.503277359e-10;
        public const double ProtonMolarMass = 1.00727646677e-3;
        public const double ProtonComptonWavelength = 1.3214098446e-15;
        public const double ProtonMagneticMoment = 1.410606662e-26;
        public const double ShieldedProtonMagneticMoment = 1.410570419e-26;
        public const double ProtonGFactor = 5.585694713;
        public const double ProtonGyromagneticRatio = 2.675222099e8;
        public const double ShieldedProtonGyromagneticRatio = 2.675153362e8;
        //Нейтрон
        public const double NeutronMass = 1.674927212e-27;
        public const double NeutronMassEnegryEquivalent = 1.505349506e-10;
        public const double NeutronMolarMass = 1.00866491597e-3;
        public const double NeutronComptonWavelength = 1.3195908951e-1;
        public const double NeutronMagneticMoment = -0.96623641e-26;
        public const double NeutronGFactor = -3.82608545;
        public const double NeutronGyromagneticRatio = 1.83247185e8;
        //Дейтрон
        public const double DeuteronMass = 3.34358320e-27;
        public const double DeuteronMassEnegryEquivalent = 3.00506272e-10;
        public const double DeuteronMolarMass = 2.013553212725e-3;
        public const double DeuteronMagneticMoment = 0.433073465e-26;
        //Гелион
        public const double HelionMass = 5.00641192e-27;
        public const double HelionMassEnegryEquivalent = 4.49953864e-10;
        public const double HelionMolarMass = 3.0149322473e-3;
        //Приставки
        public const double Deca = 1e1;
        public const double Hecto = 1e2;
        public const double Kilo = 1e3;
        public const double Mega = 1e6;
        public const double Giga = 1e9;
        public const double Tera = 1e12;
        public const double Peta = 1e15;
        public const double Exa = 1e18;
        public const double Zetta = 1e21;
        public const double Yotta = 1e24;
        public const double Deci = 1e-1;
        public const double Centi = 1e-2;
        public const double Milli = 1e-3;
        public const double Micro = 1e-6;
        public const double Nano = 1e-9;
        public const double Pico = 1e-12;
        public const double Femto = 1e-15;
        public const double Atto = 1e-18;
        public const double Zepto = 1e-21;
        public const double Yocto = 1e-24;
    }
    /// <summary>
    /// Генератор псевдослучайных чисел
    /// </summary>
    [DynamicallyInvokable]
    static class Rand
    {
        private static Random random;
        private static Object locked = new object();

        public static int Next()
        {
            lock (Rand.locked)
            {
                return Rand.random.Next();
            }
        }
        public static int Next(int max)
        {
            lock (Rand.locked)
            {
                return Rand.random.Next(max);
            }
        }
        public static int Next(int min, int max)
        {
            lock (Rand.locked)
            {
                return Rand.random.Next(min, max);
            }
        }
        public static double NextDouble()
        {
            lock (Rand.locked)
            {
                return Rand.random.NextDouble();
            }
        }

        public static void NextBytes(byte[] buffer)
        {
            lock (Rand.locked)
            {
                Rand.random.NextBytes(buffer);
            }
        }
    }
    /// <summary>
    /// Двухмерная точка
    /// </summary>
    [DynamicallyInvokable]
    class Point2 : ICloneable, IEquatable<Point2>
    {
        internal double X { get; set; }
        internal double Y { get; set; }

        public Point2(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Point2() { }

        /// <summary>
        /// Возвращает дистанцию от данной точки до точки p
        /// </summary>
        /// <param name="p">Точка</param>
        /// <returns></returns>
        public double DistanceTo(Point2 p)
        {
            return Math.Sqrt(BaseMath.Sqr(p.X - X) + BaseMath.Sqr(p.Y - Y));
        }
        public double GetRadiusVectorLength() => Math.Sqrt(X * X + Y * Y);
        public object Clone() => new Point2(X, Y);
        public Point2 CloneAs() => (Point2)Clone();
        public bool Equals(Point2 p) => (Math.Abs(X - p.X) < LibraryConst.Eps) && (Math.Abs(Y - p.Y) < LibraryConst.Eps);
        #region Перегрузки операторов
        public static bool operator ==(Point2 a, Point2 b) => a.Equals(b);
        public static bool operator !=(Point2 a, Point2 b) => !a.Equals(b);
        #endregion
        public override bool Equals(object obj)
        {
            if (obj is Point2)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override string ToString()
        {
            return $"{X} {Y}";
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void Println()
        {
            Console.WriteLine(ToString());
        }
    }
    /// <summary>
    /// Трехмерная точка
    /// </summary>
    [DynamicallyInvokable]
    class Point3 : ICloneable, IEquatable<Point3>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }

        public Point3() { }
        public Point3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public object Clone() => new Point3(X, Y, Z);
        public Point3 CloneAs() => (Point3)Clone();
        #region Перегрузки операторов
        public static bool operator ==(Point3 a, Point3 b) => a.Equals(b);
        public static bool operator !=(Point3 a, Point3 b) => !a.Equals(b);
        #endregion
        public override string ToString()
        {
            return $"{X} {Y} {Z}";
        }
        public bool Equals(Point3 p) => (Math.Abs(X - p.X) < LibraryConst.Eps) && (Math.Abs(Y - p.Y) < LibraryConst.Eps) && (Math.Abs(Z - p.Z) < LibraryConst.Eps);
        public override bool Equals(object obj)
        {
            if (obj is Point3)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Четырехмерная точка
    /// </summary>
    [DynamicallyInvokable]
    class Point4 : ICloneable, IEquatable<Point4>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        internal double W { get; set; }

        public Point4() { }
        public Point4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public object Clone() => new Point4(X, Y, Z, W);
        public Point4 CloneAs() => (Point4)Clone();
        #region Перегрузки операторов
        public static bool operator ==(Point4 a, Point4 b) => a.Equals(b);
        public static bool operator !=(Point4 a, Point4 b) => !a.Equals(b);
        #endregion
        public override string ToString() => $"{X} {Y} {Z} {W}";
        public bool Equals(Point4 p) => (Math.Abs(X - p.X) < LibraryConst.Eps) && (Math.Abs(Y - p.Y) < LibraryConst.Eps) && (Math.Abs(Z - p.Z) < LibraryConst.Eps) && (Math.Abs(W - p.W) < LibraryConst.Eps);
        public override bool Equals(object obj)
        {
            if (obj is Point4)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Пятимерная точка
    /// </summary>
    [DynamicallyInvokable]
    class Point5 : ICloneable, IEquatable<Point5>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        internal double W { get; set; }
        internal double V { get; set; }

        public Point5() { }
        public Point5(double x, double y, double z, double w, double v)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            V = v;
        }

        public object Clone() => new Point5(X, Y, Z, W, V);
        public Point5 CloneAs() => (Point5)Clone();
        #region Перегрузки операторов
        public static bool operator ==(Point5 a, Point5 b) => a.Equals(b);
        public static bool operator !=(Point5 a, Point5 b) => !a.Equals(b);
        #endregion

        public override string ToString() => $"{X}, {Y}, {Z}, {W}, {V})";
        public bool Equals(Point5 p) => (Math.Abs(X - p.X) < LibraryConst.Eps) && (Math.Abs(Y - p.Y) < LibraryConst.Eps) && (Math.Abs(Z - p.Z) < LibraryConst.Eps) && (Math.Abs(W - p.W) < LibraryConst.Eps) && (Math.Abs(V - p.V) < LibraryConst.Eps);
        public override bool Equals(object obj)
        {
            if (obj is Point5)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Представляет методы для генерации арифметическеой и геометрической прогрессий
    /// </summary>
    [DynamicallyInvokable]
    class Progress
    {
        private double[] progresses;
        public Progress(int count = 10)
        {
            progresses = new double[count];
        }
        //Арифметическая прогрессия
        public double[] Arifmetic(int begin, int step, int count)
        {
            progresses = new double[count];

            for (var i = 0; i < count; i++)
            {
                progresses[i] = begin;
                begin += step;
            }

            return progresses;
        }
        //Геометрическая прогрессия
        public double[] Geometric(int begin, int step, int count)
        {
            progresses = new double[count];

            for (var i = 0; i < count; i++)
            {
                progresses[i] = begin;
                begin *= step;
            }

            return progresses;
        }
        //Вычисляет сумму первых n элементов арифметической прогрессии
        public double SumArithmetic(int n)
        {
            if (n > progresses.Length) throw new Exception("N должно быть меньше кол-ва элементов прогрессии!");
            var res = ((progresses[0] + progresses[n - 1]) * n) / 2;
            return res;
        }
        //Вычисляет сумму первых n элементов геометрической прогрессии
        public double SumGeometrical(int n)
        {
            if (n > progresses.Length) throw new Exception("N должно быть меньше кол-ва элементов прогрессии!");
            var step = progresses[2] / progresses[1];
            double res = 0;

            if (step > 1)
            {
                res = (progresses[n] * step - progresses[1]) / (step - 1);
            }
            else if (step < 1)
            {
                res = (progresses[1] - progresses[n] * step) / (1 - step);
            }
            return res;
        }
    }
    /// <summary>
    /// Функции базовой математики(часть функций взята из Паскаля и Python)
    /// </summary>
    [DynamicallyInvokable]
    class BaseMath
    {
        //Универсальная функция вычисления степени m числа n
        static public double Pow(double n, double m) => Math.Exp(Math.Log(n) * m);
        //Извлекает корень кубический из числа
        static public double Cbrt(double num) => Pow(num, 1 / 3);
        //Извлекает корень четвертой степени из числа
        static public double Tqrt(double num) => Pow(num, 1 / 4);
        //Извлекает корень степени n из числа num 
        static public double Nqrt(double num, double n) => Pow(num, 1 / n);
        //Равны ли значения чисел
        static public bool IsEq(double l, double r) => (l == r) ? true : false;
        //Является ли первое число меньше другого
        static public bool IsLe(double l, double r) => (l < r) ? true : false;
        //Является ли первое число больше другого
        static public bool IsGe(double l, double r) => (l > r) ? true : false;
        static public double Int(double x) => (x >= 0) ? Math.Floor(x) : Math.Ceiling(x);
        static public double Frac(double x) => x - Int(x);
        static public double Trunc(double x) => Convert.ToInt32(Math.Truncate(x));
        static public double Sqr(double x) => x * x;
        static public uint RotateRight(uint x, int n) => x >> n | x << 32 - n;
        static public uint Ch(uint x, uint y, uint z) => (uint)((int)x & (int)y ^ ((int)x ^ -1) & (int)z);
        static public uint Maj(uint x, uint y, uint z) => (uint)((int)x & (int)y ^ (int)x & (int)z ^ (int)y & (int)z);
        static public uint Sigma_0x(uint x) => RotateRight(x, 7) ^ RotateRight(x, 18) ^ x >> 3;
        static public uint Sigma_1x(uint x) => RotateRight(x, 17) ^ RotateRight(x, 19) ^ x >> 10;
        static public uint Sigma_0(uint x) => RotateRight(x, 2) ^ RotateRight(x, 13) ^ RotateRight(x, 22);
        static public uint Sigma_1(uint x) => RotateRight(x, 6) ^ RotateRight(x, 11) ^ RotateRight(x, 25);
        static public int HiWord(int Number) => Number >> 16 & (int)ushort.MaxValue;
        static public int LoWord(int Number) => Number & (int)ushort.MaxValue;
        static public int MakeLong(int LoWord, int HiWord) => HiWord << 16 | LoWord & (int)ushort.MaxValue;
        public static uint Abs(int a)
        {
            uint num = (uint)(a >> 31);
            return ((uint)a ^ num) - num;
        }
        public static double Abs(double value) => Math.Abs(value);
        public static long Abs(long value) => Math.Abs(value);
        public static double Acos(double d) => Math.Acos(d);
        public static double Asin(double d) => Math.Asin(d);
        public static double Atan(double d) => Math.Atan(d);
        public static double Atan2(double y, double x) => Math.Atan2(x, y);
        public static double Max(double val1, double val2) => Math.Max(val1, val2);
        public static double Min(double val1, double val2) => Math.Min(val1, val2);
        public static int Max(int val1, int val2) => Math.Max(val1, val2);
        public static int Min(int val1, int val2) => Math.Min(val1, val2);
        public static long Max(long val1, long val2) => Math.Max(val1, val2);
        public static long Min(long val1, long val2) => Math.Min(val1, val2);
        public static double Exp(double d) => Math.Exp(d);
        public static double Log(double d) => Math.Log(d);
        public static double Log10(double d) => Math.Log10(d);
        public static double Log(double a, double newBase) => Math.Log(a, newBase);
        public static double Pow<T1, T2>(T1 x, T2 y)
                  where T1 : IConvertible
                  where T2 : IConvertible
        {
            return Math.Pow(x.ToDouble(null), y.ToDouble(null));
        }
        public static double Sqrt(double d) => Math.Sqrt(d);
        public static double Sqrt(int d) => Math.Sqrt((double)d);
        public static double Sqrt(long d) => Math.Sqrt((double)d);
        public static double Cosh(double value) => Math.Cosh(value);
        public static double Sinh(double value) => Math.Sinh(value);
        public static double Tanh(double value) => Math.Tanh(value);
        public static T max<T>(T val1, T val2) where T : IComparable<T> => val1.CompareTo(val1) > 0 ? val1 : val2;
        public static T min<T>(T val1, T val2) where T : IComparable<T> => val1.CompareTo(val1) > 0 ? val2 : val1;
        public static T Max<T>(T val1, T val2) where T : IComparable<T> => max<T>(val1, val2);
        public static T Min<T>(T val1, T val2) where T : IComparable<T> => min<T>(val1, val2);
        public static double max(params double[] vals)
        {
            double val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val < vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static double min(params double[] vals)
        {
            double val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val > vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static int max(params int[] vals)
        {
            int val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val < vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static int min(params int[] vals)
        {
            int val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val > vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static long max(params long[] vals)
        {
            long val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val < vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static long min(params long[] vals)
        {
            long val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val > vals[index])
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static T max<T>(params T[] vals) where T : IComparable<T>
        {
            T val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val.CompareTo(vals[index]) < 0)
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static T min<T>(params T[] vals) where T : IComparable<T>
        {
            T val = vals[0];
            int index = 1;
            while (index < vals.Length)
            {
                if (val.CompareTo(vals[index]) > 0)
                    val = vals[index];
                checked { ++index; }
            }
            return val;
        }
        public static double SScalarcalar(double[] A, double[] B)
        {
            if (A.Length != B.Length)
                throw new ArgumentException(" A.Length != B.Length ");
            double num = 0.0;
            int index = 0;
            while (index < A.Length)
            {
                num += A[index] * B[index];
                checked { ++index; }
            }
            return num;
        }
        public static int Scalar(int[] A, int[] B)
        {
            if (A.Length != B.Length)
                throw new ArgumentException(" A.Length != B.Length ");
            int num = 0;
            int index = 0;
            while (index < A.Length)
            {
                checked { num += A[index] * B[index]; }
                checked { ++index; }
            }
            return num;
        }
        public static long Scalar(long[] A, long[] B)
        {
            if (A.Length != B.Length)
                throw new ArgumentException(" A.Length != B.Length ");
            long num = 0;
            int index = 0;
            while (index < A.Length)
            {
                checked { num += A[index] * B[index]; }
                checked { ++index; }
            }
            return num;
        }
        public static double Norm(params double[] vals)
        {
            double d = 0.0;
            int index = 0;
            while (index < vals.Length)
            {
                d += vals[index] * vals[index];
                checked { ++index; }
            }
            return Math.Sqrt(d);
        }
        public static double Norm(params int[] vals)
        {
            int num = 0;
            int index = 0;
            while (index < vals.Length)
            {
                checked { num += vals[index] * vals[index]; }
                checked { ++index; }
            }
            return Math.Sqrt((double)num);
        }
        public static double Norm(params long[] vals)
        {
            long num = 0;
            int index = 0;
            while (index < vals.Length)
            {
                checked { num += vals[index] * vals[index]; }
                checked { ++index; }
            }
            return Math.Sqrt((double)num);
        }
        public static bool All(bool a, bool b) => a && b;
        public static bool All(bool a, bool b, bool c) => a && b && c;
        public static bool All(bool a, bool b, bool c, bool d) => a && b && c && d;
        public static bool All(params bool[] vals)
        {
            int index = 0;
            while (index < vals.Length)
            {
                if (!vals[index])
                    return false;
                checked { ++index; }
            }
            return true;
        }
        public static bool All<T>(params T[] vals) where T : IConvertible
        {
            int index = 0;
            while (index < vals.Length)
            {
                if (!vals[index].ToBoolean(null))
                    return false;
                checked { ++index; }
            }
            return true;
        }
        public static T[] Join<T>(T a, T b) => new T[2] { a, b };
        public static T[] Join<T>(T a, T b, T c) => new T[3] { a, b, c };
        public static T[] Join<T>(T a, T b, T c, T d) => new T[4] { a, b, c, d };
        public static T[] Join<T>(params T[] vals) => vals.Clone() as T[];
        public static double Factorial(int n)
        {
            if (n < 0)
                throw new ArithmeticException("Factorial(n), параметр n отрицателен");
            double num = n;
            while (checked(--n) > 0 && num != double.PositiveInfinity)
                num *= n;
            return num;
        }
        public static double Factorial(long n)
        {
            if (n < 0L)
                throw new ArithmeticException("Factorial(n), параметр n отрицателен");
            double num = n;
            while (checked(--n) > 0L && num != double.PositiveInfinity)
                num *= n;
            return num;
        }
        //Перевод полярных координат в декартовы 
        public static Point2 PolarToCartesian(Polar p) => p.ToCartesian();
        //Перевод сферических координат в декартовы
        public static Point3 SpheralToCartesian(Spheral s) => s.ToCartesian();
        //Перевод цилиндрических координат в декартовы
        public static Point3 CylinderalToCartesian(Cylinderal c) => c.ToSpheral().ToCartesian();
        //Перевод гиперболических координат в декартовы
        public static Point2 HyperbolarToCartesian(Hyperbolar h) => h.ToCartesian();
        //Перевод параболических координат в декартовы
        public static Point3 ParabolarToCartesian(Parabolar p) => p.ToCartesian();
        //Перевод конических координат в декартовы
        public static Point3 ConicalToCartesian(Conical c) => c.ToSpheral().ToCartesian();
        public static Vector2 Vect2(double x, double y) => new Vector2(x, y);
        public static Vector3 Vect3(double x, double y, double z) => new Vector3(x, y, z);
        public static Vector4 Vect4(double x, double y, double z, double w) => new Vector4(x, y, z, w);
        public static Vector5 Vect5(double x, double y, double z, double w, double v) => new Vector5(x, y, z, w, v);
        //Является ли число простым
        public static bool IsSimple(int n)
        {
            for (int i = 2; i < Trunc(n / 2); i++)
            {
                if (n % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
        //Возвращает последовательность целых чисел от a до b
        public static IEnumerable<int> Range(int a, int b)
        {
            if (b < a) return System.Linq.Enumerable.Empty<int>();
            else return System.Linq.Enumerable.Range(a, b - a + 1);
        }
    }
    /// <summary>
    /// Обеспечивает обмен переменных
    /// </summary>
    [DynamicallyInvokable]
    class Swap
    {
        static public void Swap2<T>(ref T x, ref T y)
        {
            T temp;
            temp = y;
            y = x;
            x = temp;
        }
        static public void Swap3<T>(ref T x, ref T y, ref T z)
        {
            T temp;
            temp = z;
            z = y;
            y = x;
            x = temp;
        }
        static public void Swap4<T>(ref T x, ref T y, ref T z, ref T w)
        {
            T temp;
            temp = w;
            w = z;
            z = y;
            y = x;
            x = temp;
        }
        static public void Swap5<T>(ref T x, ref T y, ref T z, ref T w, ref T v)
        {
            T temp;
            temp = v;
            v = w;
            w = z;
            z = y;
            y = x;
            x = temp;
        }
    }
    /// <summary>
    /// Диапазон
    /// </summary>
    [DynamicallyInvokable]
    class Range : ICloneable
    {   /// <summary>
        ///Нижняя граница 
        /// </summary>
        private double A { get; set; }
        /// <summary>
        /// Верхняя граница
        /// </summary>
        private double B { get; set; }
        private bool IsIncludeA { get; set; }
        private bool IsIncludeB { get; set; }

        public Range() { }
        private void TrySwap(ref double less, ref double most)
        {
            if (most - less < -LibraryConst.Eps)
            {
                Swap.Swap2<double>(ref less, ref most);
            }
        }
        public bool AreEqual(double a, double b) => Math.Abs(a - b) <= LibraryConst.Eps;
        public Range(double a, double b)
        {
            TrySwap(ref a, ref b);
            A = a;
            B = b;
        }
        public bool IsIn(double c) => ((c - A > LibraryConst.Eps) || IsIncludeA && AreEqual(A, c)) && ((B - c > LibraryConst.Eps) || IsIncludeB && AreEqual(B, c));
        public bool IsIn(Range r) => (IsIn(r.A)) && (IsIn(r.B));
        public object Clone() => new Range(A, B);
        public Range CloneAs() => (Range)Clone();
        public bool Equals(Range r) => (Math.Abs(A - r.A) < LibraryConst.Eps) && (Math.Abs(B - r.B) < LibraryConst.Eps);
        public override string ToString() => $"[{A}, {B}]";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Окружность
    /// </summary>
    class Circle : ICloneable, IEquatable<Circle>
    {
        public Point2 Center { get; set; }
        public double R { get; set; }
        public double GetLength() => 2 * LibraryConst.PI * R;
        public Circle(Point2 center, double r)
        {
            Center = center;
            R = r;
        }
        // Возвращает true, если окружности пересекаются 
        public bool IntersectsWith(Circle other) => Center.DistanceTo(other.Center) < R + other.R;
        // Возвращает true, если окружности касаются друг друга
        public bool ConcernWith(Circle other) => Center.DistanceTo(other.Center) == R + other.R;
        public bool Equals(Circle c) => (Center.Equals(c.Center)) && (Math.Abs(R - c.R) < LibraryConst.Eps);
        public object Clone() => new Circle(Center.CloneAs(), R);
        public Circle CloneAs() => (Circle)Clone();
        public override string ToString() => $"Circle(C = {Center}; R = {R})";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Дробь
    /// </summary>
    [DynamicallyInvokable]
    class Frac : ICloneable, IEquatable<Frac>
    {
        internal double Numerator { get; set; }
        internal double Denominator { get; set; }
        private bool IsAlwaysReduce { get; set; }
        private bool IsTryCutOutput { get; set; }
        /// <summary>
        /// Сокращение дроби
        /// </summary>
        private void Reduce()
        {
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
        private void TryReduce()
        {
            if (IsAlwaysReduce) Reduce();
        }
        private void SetDenominator(int v)
        {
            if (v == 0) throw new Exception("Знаменатель не может быть нулем");
            Denominator = v;
        }
        private void SetTryAlwaysReduce(bool v)
        {
            IsAlwaysReduce = v;
            TryReduce();
        }
        public Frac() { }
        public Frac(double n, double d = 0)
        {
            Numerator = n;
            Denominator = d;
        }
        public double ToReal() => Numerator / Denominator;
        public Frac FromReal(double a)
        {
            var n = BaseMath.Frac(a);
            var d = 1;

            while (BaseMath.Frac(n) != 0)
            {
                n *= 10;
                d *= 10;
            }
            return new Frac(BaseMath.Trunc(n), BaseMath.Trunc(d));
        }

        #region Арифметические операции
        //Сложение дробей
        public Frac Add(Frac f)
        {
            if (Numerator == f.Numerator)
            {
                Numerator += f.Numerator;
            }
            else
            {
                Numerator = Numerator * f.Denominator + f.Numerator * Denominator;
                Denominator *= f.Denominator;
            }
            TryReduce();
            return new Frac(Numerator, Denominator);
        }
        //Вычитание дробей
        public void Sub(Frac f)
        {
            if (Numerator == f.Numerator)
            {
                Numerator -= f.Numerator;
            }
            else
            {
                Numerator = Numerator * f.Denominator - f.Numerator * Denominator;
                Denominator *= f.Denominator;
            }
            TryReduce();
        }
        //Умножение дробей
        public void Mul(Frac f)
        {
            Numerator *= f.Numerator;
            Denominator *= f.Denominator;
            TryReduce();
        }
        //Деление дробей
        public void Div(Frac f)
        {
            Numerator *= f.Denominator;
            Denominator *= f.Numerator;
            TryReduce();
        }
        #endregion
        #region Перегрузки операторов
        public static Frac operator +(Frac f1, Frac f2)
        {
            var result = f1.CloneAs();
            return result += f2;
        }
        public static Frac operator -(Frac f1, Frac f2)
        {
            var result = f1.CloneAs();
            return result -= f2;
        }
        public static Frac operator *(Frac f1, Frac f2)
        {
            var result = f1.CloneAs();
            return result *= f2;
        }
        public static Frac operator /(Frac f1, Frac f2)
        {
            var result = f1.CloneAs();
            return result /= f2;
        }
        public static bool operator >(Frac f1, Frac f2) => f1.CompareTo(f2) == 1;
        public static bool operator >=(Frac f1, Frac f2) => (f1 - f2).ToReal() >= 0;
        public static bool operator <(Frac f1, Frac f2) => f1.CompareTo(f2) == -1;
        public static bool operator <=(Frac f1, Frac f2) => (f1 - f2).ToReal() <= 0;
        public static Frac operator -(Frac f1) => new Frac(-f1.Numerator, -f1.Denominator);
        public static bool operator ==(Frac f1, Frac f2) => f1.Equals(f2);
        public static bool operator !=(Frac f1, Frac f2) => !f1.Equals(f2);
        #endregion
        public object Clone() => new Frac(Numerator, Denominator);
        public Frac CloneAs() => (Frac)Clone();
        public int CompareTo(Frac f)
        {
            var outcome = CloneAs();
            outcome.Sub(f);
            var r = outcome.ToReal();
            if (r > 0)
            {
                return 1;
            }
            else if (r < 0)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
        public bool Equals(Frac f) => (Numerator == f.Numerator) && (Denominator == f.Denominator);
        public override bool Equals(object obj)
        {
            if (obj is Frac)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => base.GetHashCode();
        public override string ToString()
        {
            if (!IsTryCutOutput)
            {
                return String.Format("{0} / {1}", Numerator, Denominator);
            }
            else if (Denominator == 1)
            {
                return Numerator.ToString();
            }
            else
            {
                return "";
            }
        }
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Генератор псевдослучайных дробей
    /// </summary>
    [DynamicallyInvokable]
    class RandFrac
    {
        private static Random random = new Random();
        private static object locked = new object();

        public static Frac Next()
        {
            int num, den;

            lock (RandFrac.locked)
            {
                num = RandFrac.random.Next();
                den = RandFrac.random.Next();
            }

            if (den == 0)
            {
                return new Frac(1);
            }
            else
            {
                return new Frac(num, den);
            }
        }
        public static Frac Next(int max)
        {
            int num, den;

            lock (RandFrac.locked)
            {
                num = RandFrac.random.Next(max);
                den = RandFrac.random.Next(max);
            }

            if (den == 0)
            {
                return new Frac(1);
            }
            else
            {
                return new Frac(num, den);
            }
        }
        public static Frac Next(int min, int max)
        {
            int num, den;

            lock (RandFrac.locked)
            {
                num = RandFrac.random.Next(min, max);
                den = RandFrac.random.Next(min, max);
            }

            if (den == 0)
            {
                return new Frac(1);
            }
            else
            {
                return new Frac(num, den);
            }
        }
        public static Frac NextDouble()
        {
            double num, den;

            lock (RandFrac.locked)
            {
                num = RandFrac.random.NextDouble();
                den = RandFrac.random.NextDouble();
            }

            if (den == 0.0)
            {
                return new Frac(1);
            }
            else
            {
                return new Frac(num, den);
            }
        }
        public static void NextBytes(byte[] buffer)
        {
            lock (RandFrac.locked)
            {
                RandFrac.random.NextBytes(buffer);
            }
        }
    }
    /// <summary>
    /// Двухмерный вектор
    /// </summary>
    [DynamicallyInvokable]
    class Vector2 : ICloneable, IEquatable<Vector2>
    {
        internal double X { get; set; }
        internal double Y { get; set; }

        public Vector2() { }
        public Vector2(double x, double y)
        {
            X = x;
            Y = y;
        }
        public Vector2(double value)
        {
            X = Y = value;
        }
        //Нулевой вектор
        public static Vector2 Zero() => new Vector2(0.0, 0.0);
        //Единичный вектор(орт)
        public static Vector2 One() => new Vector2(1.0, 1.0);
        //Ось X
        public static Vector2 UnitX() => new Vector2(1.0, 0.0);
        //Ось Y
        public static Vector2 UnitY() => new Vector2(0.0, 1.0);
        ///Вправо
        public static Vector2 Right() => new Vector2(1.0, 0.0);
        ///Влево
        public static Vector2 Left() => new Vector2(-1.0, 0.0);
        ///Назад
        public static Vector2 Backward() => new Vector2(0.0, 1.0);
        ///Вперед
        public static Vector2 Forward() => new Vector2(0.0, -1.0);
        public Vector2 Rotate(Vector2 v, double angle)
        {
            var length = v.Length();
            X = Math.Cos(angle) * length;
            Y = Math.Sin(angle) * length;
            return this;
        }
        //Длина вектора
        public double Length() => Math.Sqrt(X * X + Y * Y);
        //Возвращает корень из длины вектора
        public double LengthSquared() => X * X + Y * Y;
        //Возвращает расстояние от одного вектора до другого
        public double Distance(Vector2 v1, Vector2 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            return Math.Sqrt(num1 * num1 + num2 * num2);
        }
        //Возвращает корень из расстояния от одного вектора до другого
        public double DistanceSquared(Vector2 v1, Vector2 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            return num1 * num1 + num2 * num2;
        }
        //Возвращает нормализованный вектор
        public Vector2 Normalize(Vector2 v)
        {
            var length = v.Length();
            v.X /= length;
            v.Y /= length;
            return v;
        }
        //Ограничивает минимальное и максимальное значение вектора
        public static Vector2 Clamp(Vector2 v, Vector2 min, Vector2 max)
        {
            var x1 = v.X;
            var y1 = v.Y;
            var num1 = (x1 > max.X) ? max.X : x1;
            var x2 = (num1 < min.X) ? min.X : num1;
            var num2 = (y1 > max.Y) ? max.Y : y1;
            var y2 = (num2 < min.Y) ? min.Y : num2;
            return new Vector2(x2, y2);
        }
        //Выполняет линейную интерполяцию между двумя векторами на основе заданного взвешивания
        public static Vector2 Lerp(Vector2 v1, Vector2 v2, double amount) => v1 + (v2 - v1) * amount;
        //Возвращает отражение вектора от поверхности, которая имеет заданную нормаль
        public static Vector2 Reflect(Vector2 v, Vector2 normal)
        {
            var num = Dot(v, normal);
            return new Vector2(v.X - 2 * num * normal.X, v.Y - 2 * num * normal.Y);
        }
        //Возвращает вектор противоположный данному
        public static Vector2 Negate(Vector2 v) => new Vector2(-v.X, -v.Y);
        //Модуль из вектора
        public static Vector2 Abs(Vector2 v) => new Vector2(Math.Abs(v.X), Math.Abs(v.Y));
        //Корень квадратный из вектора
        public static Vector2 Sqrt(Vector2 v) => new Vector2(Math.Sqrt(v.X), Math.Sqrt(v.Y));
        //Возвращает вектор, элементы которого являются минимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector2 Min(Vector2 v1, Vector2 v2) => new Vector2((v1.X < v2.X) ? v1.X : v2.X, (v1.Y < v2.Y) ? v1.Y : v2.Y);
        //Возвращает вектор, элементы которого являются максимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector2 Max(Vector2 v1, Vector2 v2) => new Vector2((v1.X > v2.X) ? v1.X : v2.X, (v1.Y > v2.Y) ? v1.Y : v2.Y);

        #region Арифметические операции над векторами
        public static double Dot(Vector2 v1, Vector2 v2) => v1.X * v2.X + v1.Y * v2.Y;
        public static Vector2 Add(Vector2 v1, Vector2 v2) => new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        public static Vector2 Sub(Vector2 v1, Vector2 v2) => new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        public static Vector2 Mul(Vector2 v1, Vector2 v2) => new Vector2(v1.X * v2.X, v1.Y * v2.Y);
        public static Vector2 Mul(Vector2 v1, double scalar) => new Vector2(v1.X * scalar, v1.Y * scalar);
        public static Vector2 Div(Vector2 v1, Vector2 v2) => new Vector2(v1.X / v2.X, v1.Y / v2.Y);
        public static Vector2 Div(Vector2 v1, double scalar) => new Vector2(v1.X / scalar, v1.Y / scalar);
        #endregion

        #region Перегрузки операторов
        public static Vector2 operator +(Vector2 v1, Vector2 v2) => Add(v1, v2);
        public static Vector2 operator -(Vector2 v1, Vector2 v2) => Sub(v1, v2);
        public static Vector2 operator *(Vector2 v1, Vector2 v2) => Mul(v1, v2);
        public static Vector2 operator /(Vector2 v1, Vector2 v2) => Div(v1, v2);
        public static Vector2 operator *(Vector2 v1, double scalar) => Mul(v1, scalar);
        public static Vector2 operator /(Vector2 v1, double scalar) => Div(v1, scalar);
        public static bool operator ==(Vector2 v1, Vector2 v2) => v1.Equals(v2);
        public static bool operator !=(Vector2 v1, Vector2 v2) => !v1.Equals(v2);
        #endregion
        // Возвращает вектор, получаемый суммированием всех векторов, указанных в параметрах
        public static Vector2 VectorSum(params Vector2[] vectors)
        {
            var result = new Vector2(0, 0);
            foreach (Vector2 vector in vectors)
            {
                result += vector;
            }
            return result;
        }
        //Возвращает массив элементов вектора
        public double[] CopyTo(Vector2 v) => new double[2] { v.X, v.Y };
        //Направляюший косинус Альфа
        public double GuidingCosA() => X / Length();
        //Направляюший косинус Бета
        public double GuidingCosB() => Y / Length();
        //Возвращает значение, указывающее, равен ли данный экземпляр другому вектору
        public object Clone() => new Vector2(X, Y);
        public Vector2 CloneAs() => (Vector2)Clone();
        public bool Equals(Vector2 other)
        {
            if (X == other.X)
            {
                return (Y == other.Y);
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector2)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode();
        //Преобразовывает строку к Vector2 если это возможно
        public Vector2 Parse(string str)
        {
            if (str == null || str.Length == 0)
            {
                throw new ArgumentNullException("Строка имела неверный формат");
            }
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 2)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[0]);
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
            return this;
        }
        public override string ToString() => $"{X} : {Y}";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Трехмерный вектор
    /// </summary>
    [DynamicallyInvokable]
    class Vector3 : ICloneable, IEquatable<Vector3>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }

        public Vector3() { }
        public Vector3(double x, double y, double z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public Vector3(double value)
        {
            X = Y = Z = value;
        }
        public Vector3(Vector2 a, double val)
        {
            X = a.X;
            Y = a.Y;
            Z = val;
        }
        //Нулевой вектор
        public static Vector3 Zero() => new Vector3(0.0, 0.0, 0.0);
        //Единичный вектор(орт)
        public static Vector3 One() => new Vector3(1.0, 1.0, 1.0);
        //Ось X
        public static Vector3 UnitX() => new Vector3(1.0, 0.0, 0.0);
        //Ось Y
        public static Vector3 UnitY() => new Vector3(0.0, 1.0, 0.0);
        //Ось Z
        public static Vector3 UnitZ() => new Vector3(0.0, 0.0, 1.0);
        //Вверх
        public static Vector3 Up() => new Vector3(0.0, 0.0, 1.0);
        //Вниз
        public static Vector3 Down() => new Vector3(0.0, 0.0, -1.0);
        //Вправо
        public static Vector3 Right() => new Vector3(1.0, 0.0, 0.0);
        //Влево
        public static Vector3 Left() => new Vector3(-1.0, 0.0, 0.0);
        //Назад
        public static Vector3 Backward() => new Vector3(0.0, 1.0, 0.0);
        //Вперед
        public static Vector3 Forward() => new Vector3(0.0, -1.0, 0.0);
        //Возвращает вектор, который получается при повороте вектора по оси X на угол angle
        public Vector3 RotateX(Vector3 v, double angle)
        {
            var radAng = angle * LibraryConst.DEG2RAD;
            X = v.X;
            Y = (v.Y * Math.Cos(radAng)) - (v.Z * Math.Sin(radAng));
            Z = (v.Y * Math.Sin(radAng)) + (v.Z * Math.Cos(radAng));
            return this;
        }
        //Возвращает вектор, который получается при повороте вектора по оси Y на угол angle
        public Vector3 RotateY(Vector3 v, double angle)
        {
            var radAng = angle * LibraryConst.DEG2RAD;
            X = (v.X * Math.Cos(radAng)) - (v.Z * Math.Sin(radAng));
            Y = v.Y;
            Z = (v.Z * Math.Sin(radAng)) + (v.X * Math.Cos(radAng));
            return this;
        }
        //Возвращает вектор, который получается при повороте вектора по оси Z на угол angle
        public Vector3 RotateZ(Vector3 v, double angle)
        {
            var radAng = angle * LibraryConst.DEG2RAD;
            X = (v.X * Math.Cos(radAng)) - (v.Y * Math.Sin(radAng));
            Y = (v.Y * Math.Sin(radAng)) + (v.X * Math.Cos(radAng));
            Z = v.Z;
            return this;
        }
        //Длина вектора
        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z);
        //Возвращает корень из длины вектора
        public double LengthSquared() => X * X + Y * Y + Z * Z;
        //Возвращает расстояние от одного вектора до другого
        public double Distance(Vector3 v1, Vector3 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            return Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3);
        }
        //Возвращает корень из расстояния от одного вектора до другого
        public double DistanceSquared(Vector3 v1, Vector3 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            return num1 * num1 + num2 * num2 + num3 * num3;
        }
        //Возвращает нормализованный вектор
        public Vector3 Normalize(Vector3 v)
        {
            var length = v.Length();
            v.X /= length;
            v.Y /= length;
            v.Z /= length;
            return v;
        }
        //Ограничивает минимальное и максимальное значение вектора
        public static Vector3 Clamp(Vector3 v, Vector3 min, Vector3 max)
        {
            var x1 = v.X;
            var y1 = v.Y;
            var z1 = v.Z;
            var num1 = (x1 > max.X) ? max.X : x1;
            var x2 = (num1 < min.X) ? min.X : num1;
            var num2 = (y1 > max.Y) ? max.Y : y1;
            var y2 = (num2 < min.Y) ? min.Y : num2;
            var num3 = (z1 > max.Z) ? max.Z : z1;
            var z2 = (num3 < min.Z) ? min.Z : num3;
            return new Vector3(x2, y2, z2);
        }
        //Выполняет линейную интерполяцию между двумя векторами на основе заданного взвешивания
        public static Vector3 Lerp(Vector3 v1, Vector3 v2, double amount) => v1 + (v2 - v1) * amount;
        //Возвращает отражение вектора от поверхности, которая имеет заданную нормаль
        public static Vector3 Reflect(Vector3 v, Vector3 normal)
        {
            var num1 = Dot(v, normal);
            var num2 = normal.X * num1 * 2.0;
            var num3 = normal.Y * num1 * 2.0;
            var num4 = normal.Z * num1 * 2.0;
            return new Vector3(v.X - num2, v.Y - num3, v.Z - num4);
        }
        //Возвращает вектор противоположный данному
        public static Vector3 Negate(Vector3 v) => new Vector3(-v.X, -v.Y, -v.Z);
        //Модуль из вектора
        public static Vector3 Abs(Vector3 v) => new Vector3(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z));
        //Корень квадратный из вектора
        public static Vector3 Sqrt(Vector3 v) => new Vector3(Math.Sqrt(v.X), Math.Sqrt(v.Y), Math.Sqrt(v.Z));
        //Возвращает вектор, элементы которого являются минимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector3 Min(Vector3 v1, Vector3 v2) => new Vector3((v1.X < v2.X) ? v1.X : v2.X, (v1.Y < v2.Y) ? v1.Y : v2.Y, (v1.Z < v2.Z) ? v1.Z : v2.Z);
        //Возвращает вектор, элементы которого являются максимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector3 Max(Vector3 v1, Vector3 v2) => new Vector3((v1.X > v2.X) ? v1.X : v2.X, (v1.Y > v2.Y) ? v1.Y : v2.Y, (v1.Z > v2.Z) ? v1.Z : v2.Z);

        #region Арифметические операции над векторами
        public static double Dot(Vector3 v1, Vector3 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z + v2.Z;
        public static Vector3 Cross(Vector3 v1, Vector3 v2) => new Vector3(v1.Y * v2.Z - v1.Z * v2.Y, v1.Z * v2.X - v1.X * v2.Z, v1.X * v2.Y - v1.Y * v2.X);
        public static Vector3 Add(Vector3 v1, Vector3 v2) => new Vector3(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z);
        public static Vector3 Sub(Vector3 v1, Vector3 v2) => new Vector3(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z);
        public static Vector3 Mul(Vector3 v1, Vector3 v2) => new Vector3(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z);
        public static Vector3 Mul(Vector3 v1, double scalar) => new Vector3(v1.X * scalar, v1.Y * scalar, v1.Z * scalar);
        public static Vector3 Div(Vector3 v1, Vector3 v2) => new Vector3(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z);
        public static Vector3 Div(Vector3 v1, double scalar) => new Vector3(v1.X / scalar, v1.Y / scalar, v1.Z / scalar);
        #endregion

        #region Перегрузки операторов
        public static Vector3 operator +(Vector3 v1, Vector3 v2) => Add(v1, v2);
        public static Vector3 operator -(Vector3 v1, Vector3 v2) => Sub(v1, v2);
        public static Vector3 operator *(Vector3 v1, Vector3 v2) => Mul(v1, v2);
        public static Vector3 operator /(Vector3 v1, Vector3 v2) => Div(v1, v2);
        public static Vector3 operator *(Vector3 v1, double scalar) => Mul(v1, scalar);
        public static Vector3 operator /(Vector3 v1, double scalar) => Div(v1, scalar);
        public static bool operator ==(Vector3 v1, Vector3 v2) => v1.Equals(v2);
        public static bool operator !=(Vector3 v1, Vector3 v2) => !v1.Equals(v2);
        #endregion
        // Возвращает вектор, получаемый суммированием всех векторов, указанных в параметрах
        public static Vector3 VectorSum(params Vector3[] vectors)
        {
            var result = new Vector3(0, 0, 0);
            foreach (Vector3 vector in vectors)
            {
                result += vector;
            }
            return result;
        }
        //Возвращает массив элементов вектора
        public double[] CopyTo(Vector3 v) => new double[3] { v.X, v.Y, v.Z };
        //Направляюший косинус Альфа
        public double GuidingCosA() => X / Length();
        //Направляюший косинус Бета
        public double GuidingCosB() => Y / Length();
        //Направляюший косинус Гамма
        public double GuidingCosG() => Z / Length();
        public object Clone() => new Vector3(X, Y, Z);
        public Vector3 CloneAs() => (Vector3)Clone();
        //Возвращает значение, указывающее, равен ли данный экземпляр другому вектору
        public bool Equals(Vector3 other)
        {
            if ((X == other.X) && (Y == other.Y))
            {
                return (Z == other.Z);
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector3)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode();
        //Преобразовывает строку к Vector3 если это возможно
        public Vector3 Parse(string str)
        {
            if (str == null || str.Length == 0)
            {
                throw new ArgumentNullException("Строка имела неверный формат");
            }
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 3)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                Z = Convert.ToDouble(vals[2]);
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
            return this;
        }
        public override string ToString() => $"{X} : {Y} : {Z}";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Четырехмерный вектор
    /// </summary>
    [DynamicallyInvokable]
    class Vector4 : ICloneable, IEquatable<Vector4>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        internal double W { get; set; }

        public Vector4() { }
        public Vector4(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Vector4(double value)
        {
            X = Y = Z = W = value;
        }
        public Vector4(Vector2 a, double z, double w)
        {
            X = a.X;
            Y = a.Y;
            Z = z;
            W = w;
        }
        public Vector4(Vector3 a, double scalar)
        {
            X = a.X;
            Y = a.Y;
            Z = a.Z;
            W = scalar;
        }
        //Нулевой вектор
        public static Vector4 Zero() => new Vector4(0.0, 0.0, 0.0, 0.0);
        //Единичный вектор(орт)
        public static Vector4 One() => new Vector4(1.0, 1.0, 1.0, 1.0);
        //Ось X
        public static Vector4 UnitX() => new Vector4(1.0, 0.0, 0.0, 0.0);
        //Ось Y
        public static Vector4 UnitY() => new Vector4(0.0, 1.0, 0.0, 0.0);
        //Ось Z
        public static Vector4 UnitZ() => new Vector4(0.0, 0.0, 1.0, 0.0);
        //Ось W
        public static Vector4 UnitW() => new Vector4(0.0, 0.0, 0.0, 1.0);
        //Вверх
        public static Vector4 Up() => new Vector4(0.0, 0.0, 1.0, 0.0);
        //Вниз
        public static Vector4 Down() => new Vector4(0.0, 0.0, -1.0, 0.0);
        //Вправо
        public static Vector4 Right() => new Vector4(1.0, 0.0, 0.0, 0.0);
        //Влево
        public static Vector4 Left() => new Vector4(-1.0, 0.0, 0.0, 0.0);
        //Назад
        public static Vector4 Backward() => new Vector4(0.0, 1.0, 0.0, 0.0);
        //Вперед
        public static Vector4 Forward() => new Vector4(0.0, -1.0, 0.0, 0.0);
        //Ана
        public static Vector4 Ana() => new Vector4(0.0, 0.0, 0.0, 1.0);
        //Ката
        public static Vector4 Cata() => new Vector4(0.0, 0.0, 0.0, -1.0);
        //Длина вектора
        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        //Возвращает корень из длины вектора
        public double LengthSquared() => X * X + Y * Y + Z * Z + W * W;
        //Возвращает расстояние от одного вектора до другого
        public double Distance(Vector4 v1, Vector4 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            var num4 = v1.W - v2.W;
            return Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4);
        }
        //Возвращает корень из расстояния от одного вектора до другого
        public double DistanceSquared(Vector4 v1, Vector4 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            var num4 = v1.W - v2.W;
            return num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4;
        }
        //Возвращает нормализованный вектор
        public Vector4 Normalize(Vector4 v)
        {
            var length = v.Length();
            v.X /= length;
            v.Y /= length;
            v.Z /= length;
            v.W /= length;
            return v;
        }
        //Ограничивает минимальное и максимальное значение вектора
        public static Vector4 Clamp(Vector4 v, Vector4 min, Vector4 max)
        {
            var x1 = v.X;
            var y1 = v.Y;
            var z1 = v.Z;
            var w1 = v.W;
            var num1 = (x1 > max.X) ? max.X : x1;
            var x2 = (num1 < min.X) ? min.X : num1;
            var num2 = (y1 > max.Y) ? max.Y : y1;
            var y2 = (num2 < min.Y) ? min.Y : num2;
            var num3 = (z1 > max.Z) ? max.Z : z1;
            var z2 = (num3 < min.Z) ? min.Z : num3;
            var num4 = (w1 > max.W) ? max.W : w1;
            var w2 = (num4 < min.W) ? min.W : num4;
            return new Vector4(x2, y2, z2, w2);
        }
        //Выполняет линейную интерполяцию между двумя векторами на основе заданного взвешивания
        public static Vector4 Lerp(Vector4 v1, Vector4 v2, double amount) => v1 + (v2 - v1) * amount;
        //Возвращает отражение вектора от поверхности, которая имеет заданную нормаль
        public static Vector4 Reflect(Vector4 v, Vector4 normal)
        {
            var num1 = Dot(v, normal);
            var num2 = normal.X * num1 * 2.0;
            var num3 = normal.Y * num1 * 2.0;
            var num4 = normal.Z * num1 * 2.0;
            var num5 = normal.W * num1 * 2.0;
            return new Vector4(v.X - num2, v.Y - num3, v.Z - num4, v.W - num5);
        }
        //Возвращает вектор противоположный данному
        public static Vector4 Negate(Vector4 v) => new Vector4(-v.X, -v.Y, -v.Z, -v.W);
        //Модуль из вектора
        public static Vector4 Abs(Vector4 v) => new Vector4(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W));
        //Корень квадратный из вектора
        public static Vector4 Sqrt(Vector4 v) => new Vector4(Math.Sqrt(v.X), Math.Sqrt(v.Y), Math.Sqrt(v.Z), Math.Sqrt(v.W));
        //Возвращает вектор, элементы которого являются минимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector4 Min(Vector4 v1, Vector4 v2) => new Vector4((v1.X < v2.X) ? v1.X : v2.X, (v1.Y < v2.Y) ? v1.Y : v2.Y, (v1.Z < v2.Z) ? v1.Z : v2.Z, (v1.W < v2.W) ? v1.W : v2.W);
        //Возвращает вектор, элементы которого являются максимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector4 Max(Vector4 v1, Vector4 v2) => new Vector4((v1.X > v2.X) ? v1.X : v2.X, (v1.Y > v2.Y) ? v1.Y : v2.Y, (v1.Z > v2.Z) ? v1.Z : v2.Z, (v1.W > v2.W) ? v1.W : v2.W);

        #region Арифметические операции над векторами
        public static double Dot(Vector4 v1, Vector4 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z + v2.Z + v1.W + v2.W;
        public static Vector4 Add(Vector4 v1, Vector4 v2) => new Vector4(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        public static Vector4 Sub(Vector4 v1, Vector4 v2) => new Vector4(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        public static Vector4 Mul(Vector4 v1, Vector4 v2) => new Vector4(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W);
        public static Vector4 Mul(Vector4 v1, double scalar) => new Vector4(v1.X * scalar, v1.Y * scalar, v1.Z * scalar, v1.W + scalar);
        public static Vector4 Div(Vector4 v1, Vector4 v2) => new Vector4(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W);
        public static Vector4 Div(Vector4 v1, double scalar) => new Vector4(v1.X / scalar, v1.Y / scalar, v1.Z / scalar, v1.W / scalar);
        #endregion

        #region Перегрузки операторов
        public static Vector4 operator +(Vector4 v1, Vector4 v2) => Add(v1, v2);
        public static Vector4 operator -(Vector4 v1, Vector4 v2) => Sub(v1, v2);
        public static Vector4 operator *(Vector4 v1, Vector4 v2) => Mul(v1, v2);
        public static Vector4 operator /(Vector4 v1, Vector4 v2) => Div(v1, v2);
        public static Vector4 operator *(Vector4 v1, double scalar) => Mul(v1, scalar);
        public static Vector4 operator /(Vector4 v1, double scalar) => Div(v1, scalar);
        public static bool operator ==(Vector4 v1, Vector4 v2) => v1.Equals(v2);
        public static bool operator !=(Vector4 v1, Vector4 v2) => !v1.Equals(v2);
        #endregion
        // Возвращает вектор, получаемый суммированием всех векторов, указанных в параметрах
        public static Vector4 VectorSum(params Vector4[] vectors)
        {
            var result = new Vector4(0, 0, 0, 0);
            foreach (Vector4 vector in vectors)
            {
                result += vector;
            }
            return result;
        }
        //Возвращает массив элементов вектора
        public double[] CopyTo(Vector4 v) => new double[4] { v.X, v.Y, v.Z, v.W };
        //Направляюший косинус Альфа
        public double GuidingCosA() => X / Length();
        //Направляюший косинус Бета
        public double GuidingCosB() => Y / Length();
        //Направляюший косинус Гамма
        public double GuidingCosG() => Z / Length();
        //Направляюший косинус Дельта
        public double GuidingCosD() => W / Length();

        public object Clone() => new Vector4(X, Y, Z, W);
        public Vector4 CloneAs() => (Vector4)Clone();
        //Возвращает значение, указывающее, равен ли данный экземпляр другому вектору
        public bool Equals(Vector4 other)
        {
            if ((X == other.X) && (Y == other.Y) && (Z == other.Z))
            {
                return (W == other.W);
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector4)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode();
        //Преобразовывает строку к Vector4 если это возможно
        public Vector4 Parse(string str)
        {
            if (str == null || str.Length == 0)
            {
                throw new ArgumentNullException("Строка имела неверный формат");
            }
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 4)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                Z = Convert.ToDouble(vals[2]);
                W = Convert.ToDouble(vals[3]);
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
            return this;
        }
        public override string ToString() => $"{X} : {Y} : {Z} : {W}";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Пятимерный вектор
    /// </summary>
    [DynamicallyInvokable]
    class Vector5 : ICloneable, IEquatable<Vector5>
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        internal double W { get; set; }
        internal double V { get; set; }

        public Vector5() { }
        public Vector5(double x, double y, double z, double w, double v)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
            V = v;
        }
        public Vector5(double value)
        {
            X = Y = Z = W = V = value;
        }
        public Vector5(Vector2 a, double z, double w, double v)
        {
            X = a.X;
            Y = a.Y;
            Z = z;
            W = w;
            V = v;
        }
        public Vector5(Vector3 a, double w, double v)
        {
            X = a.X;
            Y = a.Y;
            Z = a.Z;
            W = w;
            V = v;
        }
        public Vector5(Vector4 a, double scalar)
        {
            X = a.X;
            Y = a.Y;
            Z = a.Z;
            W = a.W;
            V = scalar;
        }
        //Нулевой вектор
        public static Vector5 Zero() => new Vector5(0.0, 0.0, 0.0, 0.0, 0.0);
        //Единичный вектор(орт)
        public static Vector5 One() => new Vector5(1.0, 1.0, 1.0, 1.0, 1.0);
        //Ось X
        public static Vector5 UnitX() => new Vector5(1.0, 0.0, 0.0, 0.0, 0.0);
        //Ось Y
        public static Vector5 UnitY() => new Vector5(0.0, 1.0, 0.0, 0.0, 0.0);
        //Ось Z
        public static Vector5 UnitZ() => new Vector5(0.0, 0.0, 1.0, 0.0, 0.0);
        //Ось W
        public static Vector5 UnitW() => new Vector5(0.0, 0.0, 0.0, 1.0, 0.0);
        //Ось V
        public static Vector5 UnitV() => new Vector5(0.0, 0.0, 0.0, 0.0, 1.0);
        //Вверх
        public static Vector5 Up() => new Vector5(0.0, 0.0, 1.0, 0.0, 0.0);
        //Вниз
        public static Vector5 Down() => new Vector5(0.0, 0.0, -1.0, 0.0, 0.0);
        //Вправо
        public static Vector5 Right() => new Vector5(1.0, 0.0, 0.0, 0.0, 0.0);
        //Влево
        public static Vector5 Left() => new Vector5(-1.0, 0.0, 0.0, 0.0, 0.0);
        //Назад
        public static Vector5 Backward() => new Vector5(0.0, 1.0, 0.0, 0.0, 0.0);
        //Вперед
        public static Vector5 Forward() => new Vector5(0.0, -1.0, 0.0, 0.0, 0.0);
        //Ана
        public static Vector5 Ana() => new Vector5(0.0, 0.0, 0.0, 1.0, 0.0);
        //Ката
        public static Vector5 Cata() => new Vector5(0.0, 0.0, 0.0, -1.0, 0.0);
        //Инвальд
        public static Vector5 Invald() => new Vector5(0.0, 0.0, 0.0, 0.0, 1.0);
        //Аутвальд
        public static Vector5 Autvald() => new Vector5(0.0, 0.0, 0.0, 0.0, -1.0);
        //Длина вектора
        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z + W * W + V * V);
        //Возвращает корень из длины вектора
        public double LengthSquared() => X * X + Y * Y + Z * Z + W * W + V * V;
        //Возвращает расстояние от одного вектора до другого
        public double Distance(Vector5 v1, Vector5 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            var num4 = v1.W - v2.W;
            var num5 = v1.V - v2.V;
            return Math.Sqrt(num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4 + num5 * num5);
        }
        //Возвращает корень из расстояния от одного вектора до другого
        public double DistanceSquared(Vector5 v1, Vector5 v2)
        {
            var num1 = v1.X - v2.X;
            var num2 = v1.Y - v2.Y;
            var num3 = v1.Z - v2.Z;
            var num4 = v1.W - v2.W;
            var num5 = v1.V - v2.V;
            return num1 * num1 + num2 * num2 + num3 * num3 + num4 * num4 + num5 * num5;
        }
        //Возвращает нормализованный вектор
        public Vector5 Normalize(Vector5 v)
        {
            var length = v.Length();
            v.X /= length;
            v.Y /= length;
            v.Z /= length;
            v.W /= length;
            v.V /= length;
            return v;
        }
        //Ограничивает минимальное и максимальное значение вектора
        public static Vector5 Clamp(Vector5 v, Vector5 min, Vector5 max)
        {
            var x1 = v.X;
            var y1 = v.Y;
            var z1 = v.Z;
            var w1 = v.W;
            var v1 = v.V;
            var num1 = (x1 > max.X) ? max.X : x1;
            var x2 = (num1 < min.X) ? min.X : num1;
            var num2 = (y1 > max.Y) ? max.Y : y1;
            var y2 = (num2 < min.Y) ? min.Y : num2;
            var num3 = (z1 > max.Z) ? max.Z : z1;
            var z2 = (num3 < min.Z) ? min.Z : num3;
            var num4 = (w1 > max.W) ? max.W : w1;
            var w2 = (num4 < min.W) ? min.W : num4;
            var num5 = (v1 > max.V) ? max.V : v1;
            var v2 = (num5 < min.V) ? min.V : num5;
            return new Vector5(x2, y2, z2, w2, v2);
        }
        //Выполняет линейную интерполяцию между двумя векторами на основе заданного взвешивания
        public static Vector5 Lerp(Vector5 v1, Vector5 v2, double amount) => v1 + (v2 - v1) * amount;
        //Возвращает отражение вектора от поверхности, которая имеет заданную нормаль
        public static Vector5 Reflect(Vector5 v, Vector5 normal)
        {
            var num1 = Dot(v, normal);
            var num2 = normal.X * num1 * 2.0;
            var num3 = normal.Y * num1 * 2.0;
            var num4 = normal.Z * num1 * 2.0;
            var num5 = normal.W * num1 * 2.0;
            var num6 = normal.V * num1 * 2.0;
            return new Vector5(v.X - num2, v.Y - num3, v.Z - num4, v.W - num5, v.V - num6);
        }
        //Возвращает вектор противоположный данному
        public static Vector5 Negate(Vector5 v) => new Vector5(-v.X, -v.Y, -v.Z, -v.W, -v.V);
        //Модуль из вектора
        public static Vector5 Abs(Vector5 v) => new Vector5(Math.Abs(v.X), Math.Abs(v.Y), Math.Abs(v.Z), Math.Abs(v.W), Math.Abs(v.V));
        //Корень квадратный из вектора
        public static Vector5 Sqrt(Vector5 v) => new Vector5(Math.Sqrt(v.X), Math.Sqrt(v.Y), Math.Sqrt(v.Z), Math.Sqrt(v.W), Math.Abs(v.V));
        //Возвращает вектор, элементы которого являются минимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector5 Min(Vector5 v1, Vector5 v2) => new Vector5((v1.X < v2.X) ? v1.X : v2.X, (v1.Y < v2.Y) ? v1.Y : v2.Y, (v1.Z < v2.Z) ? v1.Z : v2.Z, (v1.W < v2.W) ? v1.W : v2.W, (v1.V < v2.V) ? v1.V : v2.V);
        //Возвращает вектор, элементы которого являются максимальными значениями каждой пары элементов в двух заданных векторах
        public static Vector5 Max(Vector5 v1, Vector5 v2) => new Vector5((v1.X > v2.X) ? v1.X : v2.X, (v1.Y > v2.Y) ? v1.Y : v2.Y, (v1.Z > v2.Z) ? v1.Z : v2.Z, (v1.W > v2.W) ? v1.W : v2.W, (v1.V > v2.V) ? v1.V : v2.V);

        #region Арифметические операции над векторами
        public static double Dot(Vector5 v1, Vector5 v2) => v1.X * v2.X + v1.Y * v2.Y + v1.Z + v2.Z + v1.W + v2.W + v1.V + v2.V;
        public static Vector5 Add(Vector5 v1, Vector5 v2) => new Vector5(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W, v1.V + v2.V);
        public static Vector5 Sub(Vector5 v1, Vector5 v2) => new Vector5(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W, v1.V - v2.V);
        public static Vector5 Mul(Vector5 v1, Vector5 v2) => new Vector5(v1.X * v2.X, v1.Y * v2.Y, v1.Z * v2.Z, v1.W * v2.W, v1.V * v2.V);
        public static Vector5 Mul(Vector5 v1, double scalar) => new Vector5(v1.X * scalar, v1.Y * scalar, v1.Z * scalar, v1.W * scalar, v1.V * scalar);
        public static Vector5 Div(Vector5 v1, Vector5 v2) => new Vector5(v1.X / v2.X, v1.Y / v2.Y, v1.Z / v2.Z, v1.W / v2.W, v1.V / v2.V);
        public static Vector5 Div(Vector5 v1, double scalar) => new Vector5(v1.X / scalar, v1.Y / scalar, v1.Z / scalar, v1.W / scalar, v1.V / scalar);
        #endregion

        #region Перегрузки операторов
        public static Vector5 operator +(Vector5 v1, Vector5 v2) => Add(v1, v2);
        public static Vector5 operator -(Vector5 v1, Vector5 v2) => Sub(v1, v2);
        public static Vector5 operator *(Vector5 v1, Vector5 v2) => Mul(v1, v2);
        public static Vector5 operator /(Vector5 v1, Vector5 v2) => Div(v1, v2);
        public static Vector5 operator *(Vector5 v1, double scalar) => Mul(v1, scalar);
        public static Vector5 operator /(Vector5 v1, double scalar) => Div(v1, scalar);
        public static bool operator ==(Vector5 v1, Vector5 v2) => v1.Equals(v2);
        public static bool operator !=(Vector5 v1, Vector5 v2) => !v1.Equals(v2);
        #endregion
        // Возвращает вектор, получаемый суммированием всех векторов, указанных в параметрах
        public static Vector5 VectorSum(params Vector5[] vectors)
        {
            var result = new Vector5(0, 0, 0, 0, 0);
            foreach (Vector5 vector in vectors)
            {
                result += vector;
            }
            return result;
        }
        //Возвращает массив элементов вектора
        public double[] CopyTo(Vector5 v) => new double[5] { v.X, v.Y, v.Z, v.W, v.V };
        //Направляюший косинус Альфа
        public double GuidingCosA() => X / Length();
        //Направляюший косинус Бета
        public double GuidingCosB() => Y / Length();
        //Направляюший косинус Гамма
        public double GuidingCosG() => Z / Length();
        //Направляюший косинус Дельта
        public double GuidingCosD() => W / Length();
        //Направляюший косинус Эпсилон
        public double GuidingCosE() => V / Length();
        public object Clone() => new Vector5(X, Y, Z, W, V);
        public Vector5 CloneAs() => (Vector5)Clone();
        //Возвращает значение, указывающее, равен ли данный экземпляр другому вектору
        public bool Equals(Vector5 other)
        {
            if ((X == other.X) && (Y == other.Y) && (Z == other.Z) && (W == other.W))
            {
                return (V == other.V);
            }
            return false;
        }
        public override bool Equals(object obj)
        {
            if (obj is Vector5)
            {
                return obj.Equals(this);
            }
            return false;
        }
        public override int GetHashCode() => X.GetHashCode() + Y.GetHashCode() + Z.GetHashCode() + W.GetHashCode() + V.GetHashCode();
        //Преобразовывает строку к Vector5 если это возможно
        public Vector5 Parse(string str)
        {
            if (str == null || str.Length == 0)
            {
                throw new ArgumentNullException("Строка имела неверный формат");
            }
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 5)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                Z = Convert.ToDouble(vals[2]);
                W = Convert.ToDouble(vals[3]);
                V = Convert.ToDouble(vals[4]);
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
            return this;
        }
        public override string ToString() => $"{X} : {Y} : {Z} : {W} : {V}";
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Линейный оператор
    /// </summary>
    [DynamicallyInvokable]
    class LinearOperator
    {
        internal double A { get; set; }
        internal double B { get; set; }
        internal double C { get; set; }
        internal double D { get; set; }
        public LinearOperator(Vector2 vi, Vector2 vj, TransformFunc xF, TransformFunc yF)
        {
            A = xF(vi.X);
            B = xF(vj.X);
            C = yF(vi.Y);
            D = yF(vj.Y);
        }
        public LinearOperator(double va, double vb, double vc, double vd)
        {
            A = va;
            B = vb;
            C = vc;
            D = vd;
        }
        //Возвращает оператор масштабирования
        public static LinearOperator ScaleOperator(int k) => new LinearOperator(k, 0, 0, k);
        //Возвращает оператор сдвига плоскости
        public static LinearOperator BiasOperator() => new LinearOperator(1, 1, 0, 1);
        //Применяет преобразование к вектору
        public Vector2 ApplyToVector(Vector2 v) => new Vector2(v.X * A + v.Y * B, v.X * C + v.Y * D);
        public override string ToString() => $"[{A} {B} {C} {D}]";
        public void Println() => Console.WriteLine(ToString());

    }
    //---------------- Функции перевода из разных систем координат --------------//
    /// <summary>
    /// Полярная система координат
    /// </summary>
    [DynamicallyInvokable]
    class Polar
    {
        internal double R { get; set; }
        internal double Angle { get; set; }
        public Polar(Point2 coord)
        {
            R = Math.Sqrt(coord.Y * coord.Y + coord.X * coord.X);
            Angle = Math.Atan(coord.Y / coord.X);
        }
        //Перевести в декартову систему координат
        public Point2 ToCartesian()
        {
            var cart = new Point2
            {
                X = R * Math.Cos(Angle),
                Y = R * Math.Sin(Angle)
            };
            return cart;
        }
        public override string ToString() => $"[{R} {Angle}]";
    }
    /// <summary>
    /// Сферическая система координат
    /// </summary>
    [DynamicallyInvokable]
    class Spheral
    {
        internal double R { get; set; }
        internal double Phi { get; set; }
        internal double Theta { get; set; }
        public Spheral(double r, double phi, double theta)
        {
            R = r;
            Phi = phi;
            Theta = theta;
        }
        public Spheral(Point3 coord)
        {
            R = Math.Sqrt(coord.X * coord.X + coord.Y * coord.Y + coord.Z * coord.Z);
            Phi = Math.Atan(coord.Y / coord.X);
            Theta = Math.Atan((Math.Sqrt(coord.X * coord.X + coord.Y * coord.Y)) / coord.Z);
        }

        public Spheral()
        {
        }

        public Point3 ToCartesian()
        {
            var cart = new Point3
            {
                X = R * Math.Sin(Theta) * Math.Cos(Phi),
                Y = R * Math.Sin(Theta) * Math.Sin(Phi),
                Z = R * Math.Cos(Phi)
            };
            return cart;
        }
        public override string ToString() => $"[{R},{Phi},{Theta}]";
    }
    /// <summary>
    /// Цилиндрическая система координат
    /// </summary>
    [DynamicallyInvokable]
    class Cylinderal
    {
        internal double Ro { get; set; }
        internal double Phi { get; set; }
        internal double Z { get; set; }

        public Cylinderal(Spheral coord)
        {
            Ro = coord.R * Math.Sin(coord.Theta);
            Phi = coord.Phi;
            Z = coord.R * Math.Cos(coord.Theta);
        }
        //Перевести в сферическую систему координат
        public Spheral ToSpheral()
        {
            var sprl = new Spheral
            {
                R = Math.Sqrt(Ro * Ro + Z * Z),
                Phi = Phi,
                Theta = Math.Atan(Ro / Z)
            };
            return sprl;
        }
        public override string ToString() => $"[{Ro},{Phi},{Z}]";
    }

    /// <summary>
    /// Гиперболическая система координат 
    /// </summary>
    [DynamicallyInvokable]
    class Hyperbolar
    {
        internal double U { get; set; }
        internal double V { get; set; }

        public Hyperbolar(Point2 coord)
        {
            U = Math.Log(Math.Sqrt(coord.X / coord.Y));
            V = Math.Sqrt(coord.X * coord.Y);
        }
        public Point2 ToCartesian()
        {
            var cart = new Point2();
            cart.X = V * Math.Exp(U);
            cart.Y = V * Math.Exp(-U);
            return cart;
        }
        public override string ToString() => $"[{U},{V}]";
    }
    /// <summary>
    /// Параболическая система координат
    /// </summary>
    [DynamicallyInvokable]
    class Parabolar
    {
        internal double Eta { get; set; }
        internal double Xsi { get; set; }
        internal double Phi { get; set; }

        public Parabolar() { }
        public Parabolar(Point3 coord)
        {
            Eta = -coord.Z + Math.Sqrt(coord.X * coord.X + coord.Y * coord.Y + coord.Z * coord.Z);
            Xsi = coord.Z + Math.Sqrt(coord.X * coord.X + coord.Y * coord.Y + coord.Z * coord.Z);
            Phi = Math.Atan(coord.Y / coord.X);
        }
        public Point3 ToCartesian()
        {
            var cart = new Point3
            {
                X = Math.Sqrt(Xsi * Eta) * Math.Cos(Phi),
                Y = Math.Sqrt(Xsi * Eta) * Math.Sin(Phi),
                Z = (Xsi - Eta) / 2
            };
            return cart;
        }
        public override string ToString() => $"[{Eta},{Xsi},{Phi}]";
    }
    /// <summary>
    /// Коническая система координат
    /// </summary>
    [DynamicallyInvokable]
    class Conical
    {
        internal double Xsi { get; set; }
        internal double Psi { get; set; }
        internal double Dseta { get; set; }
        public Conical() { }
        public Conical(Spheral sp)
        {
            Xsi = sp.R * Math.Cos(sp.Phi * Math.Sin(sp.Theta));
            Psi = sp.R * Math.Sin(sp.Phi * Math.Sin(sp.Theta));
            Dseta = sp.Theta;
        }
        //Перевести в сферические координаты
        public Spheral ToSpheral()
        {
            var sp = new Spheral
            {
                R = Math.Sqrt(Xsi * Xsi + Psi * Psi),
                Phi = (1 / Math.Sin(Dseta)) * (Math.Atan(Psi / Xsi)),
                Theta = Dseta
            };
            return sp;
        }
        public override string ToString() => $"[{Xsi},{Psi},{Dseta}]";
    }
    //---------------- Функции перевода из разных систем координат --------------//
    /// <summary>
    /// Грамиан системы векторов
    /// </summary>
    [DynamicallyInvokable]
    class Gramian
    {
        internal double[,] A { get; set; }
        internal Vector2[] Vects { get; set; }
        internal int Count { get; set; }
        public Gramian(params Vector2[] Vects)
        {
            this.Vects = Vects;
            Count = Vects.Length;
            A = new double[Count, Count];
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    A[i, j] = Vector2.Dot(this.Vects[i], Vects[j]);
                }
            }
        }
        public void Println()
        {
            for (int i = 0; i < Count; i++)
            {
                for (int j = 0; j < Count; j++)
                {
                    Console.Write(A[i, j] + ' ');
                }
                Console.WriteLine();
            }
        }
    }
    /// <summary>
    /// Сфера
    /// </summary>
    [DynamicallyInvokable]
    class Sphere
    {
        internal Vector3 Pos { get; set; }
        internal double Radius { get; set; }
        public Sphere(Vector3 Pos, double Radius)
        {
            this.Pos = Pos;
            this.Radius = Radius;
        }
        public override string ToString() => $"[Center = {Pos.ToString()}) : Radius = {Radius.ToString()}]";

    }
    /// <summary>
    /// Определенный интеграл
    /// </summary>

    [DynamicallyInvokable]
    class Integral
    {
        internal IntegFunc F { get; set; }
        internal double A, B;// границы интегрирования
        internal double S, d, h;
        internal int n;// n = 160

        public Integral(IntegFunc f, double a, double b, int n)
        {
            F = f;
            A = a;
            B = b;
            this.n = n;
        }

        public double Calc()
        {
            h = (B - A) / n;
            for (int i = 1; i < n; i++)
            {
                S += F(A + h * i);
            }
            return h * ((F(A) + F(B)) / 2 + S);
        }
    }
    /// <summary>
    /// Представляет комплексное число
    /// </summary>
    [DynamicallyInvokable]
    class Complex : ICloneable, IEquatable<Complex>
    {
        //Целая часть
        internal double X { get; set; }
        //Мнимая часть
        internal double Y { get; set; }

        public Complex() { }
        public Complex(double real, double imaginary)
        {
            X = real;
            Y = imaginary;
        }

        public static Complex Zero() => new Complex(0.0, 0.0);
        public static Complex One() => new Complex(1.0, 0.0);
        public static Complex ImaginaryOne() => new Complex(0.0, 1.0);
        public double Real() => X;
        public double Imaginary() => Y;
        public double Magnitude() => Abs(this);
        public double Phase() => Math.Atan2(Y, X);
        public static Complex FromPolarCoordinates(double magnitude, double phase)
        {
            return new Complex(magnitude * Math.Cos(phase), magnitude * Math.Sin(phase));
        }
        public static double Abs(Complex v)
        {
            if (double.IsInfinity(v.X) || (double.IsInfinity(v.Y)))
            {
                return double.PositiveInfinity;
            }
            var num1 = Math.Abs(v.X);
            var num2 = Math.Abs(v.Y);

            if (num1 > num2)
            {
                var num3 = num2 / num1;
                return num1 * Math.Sqrt(1.0 + num3 * num3);
            }
            if (num2 == 0)
            {
                return num1;
            }
            var num4 = num1 / num2;
            return num2 * Math.Sqrt(1.0 + num4 * num4);
        }
        public static Complex Conjugate(Complex v) => new Complex(v.X, -v.Y);
        public static Complex Reciprocal(Complex v)
        {
            if ((v.X == 0) && (v.Y == 0))
            {
                return Zero();
            }
            return One() / v;
        }
        public Complex DoubleToComplex(double d) => new Complex(d, 0);
        public static Complex Log(Complex z) => new Complex(Math.Log(Abs(z)), Math.Atan2(z.Y, z.X));
        public static Complex Sqrt(Complex z) => Complex.FromPolarCoordinates(Math.Sqrt(z.Magnitude()), (z.Phase() / 2.0));
        public static Complex Log(Complex z, double base_) => Log(z) / Log((Complex)base_);
        #region Тригонометрические функции
        public Complex Exp(Complex z)
        {
            var num = Math.Exp(z.Y);
            return new Complex(num * Math.Cos(z.Y), num * Math.Sin(z.Y));
        }
        public Complex Sin(Complex z) => new Complex(Math.Sin(z.X) * Math.Sin(z.Y), Math.Cos(z.X) * Math.Sinh(z.Y));
        public Complex Sinh(Complex z) => new Complex(Math.Sinh(z.X) * Math.Cos(z.Y), Math.Cosh(z.X) * Math.Sin(z.Y));
        public Complex Cos(Complex z) => new Complex(Math.Cos(z.X) * Math.Cosh(z.Y), -(Math.Sin(z.X) * Math.Sinh(z.Y)));
        public Complex Cosh(Complex z) => new Complex(Math.Cosh(z.X) * Math.Cos(z.Y), Math.Sinh(z.X) * Math.Sin(z.Y));
        public Complex Tan(Complex z) => Sin(z) / Cos(z);
        public Complex Ctg(Complex z) => 1 / Tan(z);
        public Complex Tanh(Complex z) => Sinh(z) / Cosh(z);
        public Complex Ctgh(Complex z) => 1 / Tanh(z);
        public Complex Sec(Complex z) => 1 / Cos(z);
        public Complex Cosec(Complex z) => 1 / Sin(z);
        public Complex Sch(Complex z) => 1 / Cosh(z);
        public Complex Csch(Complex z) => 1 / Sinh(z);
        public Complex Versin(Complex z) => 1 - Cos(z);
        public Complex Vercos(Complex z) => 1 - Sin(z);
        public Complex Gaversin(Complex z) => Versin(z) / 2;
        public Complex Gavercos(Complex z) => Vercos(z) / 2;
        public Complex Exsec(Complex z) => Sec(z) - 1;
        public Complex Excsc(Complex z) => Cosec(z) - 1;

        #endregion
        #region Арифметические операции
        public static Complex Negate(Complex v) => new Complex(-v.X, -v.Y);
        public static Complex Add(Complex v1, Complex v2) => new Complex(v1.X + v2.X, v1.Y + v2.Y);
        public static Complex Sub(Complex v1, Complex v2) => new Complex(v1.X - v2.X, v1.Y - v2.Y);
        public static Complex Mul(Complex v1, Complex v2) => new Complex(v1.X * v2.X, v1.Y * v2.Y);
        public static Complex Div(Complex v1, Complex v2)
        {
            var real1 = v1.X;
            var img1 = v1.Y;
            var real2 = v2.X;
            var img2 = v2.Y;

            if (Math.Abs(img2) < Math.Abs(real2))
            {
                var num = img2 / real2;
                return new Complex((real1 + img1 * num) / (real2 + img2 * num), (img1 - real1 * num) / (real2 + img2 * num));
            }
            else
            {
                var num1 = real2 / img2;
                return new Complex((img1 + real1 * num1) / (img2 + real2 * num1), (-real1 + img1 * num1) / (img2 + real2 * num1));
            }
        }
        public static Complex Mul(Complex v1, double scalar) => new Complex(v1.X * scalar, v1.Y * scalar);
        public static Complex Div(Complex v1, double scalar) => new Complex(v1.X / scalar, v1.Y / scalar);
        #endregion
        #region Перегрузки операторов
        public static Complex operator -(Complex v) => Negate(v);
        public static Complex operator +(Complex v1, Complex v2) => Add(v1, v2);
        public static Complex operator -(Complex v1, Complex v2) => Sub(v1, v2);
        public static Complex operator *(Complex v1, Complex v2) => Mul(v1, v2);
        public static Complex operator /(Complex v1, Complex v2) => Div(v1, v2);
        public static Complex operator *(Complex v1, double scalar) => Mul(v1, scalar);
        public static Complex operator /(Complex v1, double scalar) => Div(v1, scalar);
        public static implicit operator Complex(sbyte v) => new Complex(Convert.ToDouble(v), 0.0);
        public static implicit operator Complex(int v) => new Complex(Convert.ToDouble(v), 0.0);
        public static implicit operator Complex(ulong v) => new Complex(Convert.ToDouble(v), 0.0);
        public static implicit operator Complex(double v) => new Complex(Convert.ToDouble(v), 0.0);
        public static explicit operator Complex(decimal v) => new Complex(Convert.ToDouble(v), 0.0);
        #endregion

        public object Clone() => new Complex(X, Y);
        public Complex CloneAs() => (Complex)Clone();
        public static Complex Scale(Complex v, double factor) => new Complex(factor * v.X, factor * v.Y);
        public bool Equals(Complex v) => (X == v.X) ? (Y == v.Y) : false;
        public override bool Equals(object obj)
        {
            if (obj is Complex) return obj.Equals(this);
            else return false;
        }
        public override int GetHashCode()
        {
            return Convert.ToInt32(Math.Pow(X.GetHashCode() % 99999997, Y.GetHashCode()));
        }
        public Complex Parse(string str)
        {
            if ((str == null) || (str.Length == 0)) throw new ArgumentNullException(nameof(str));
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 2)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                return this;
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
        }
        public override string ToString() => $"{X}+{Y}i";
        public void Print() => Console.Write(ToString());
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Генератор псевдослучайных комплексных чисел
    /// </summary>
    [DynamicallyInvokable]
    class RandComplex
    {
        private static Random r = new Random();
        private static object locked = new object();

        public static Complex Next()
        {
            int x, y;
            lock (locked)
            {
                x = r.Next();
                y = r.Next();
            }
            return new Complex(x, y);
        }
        public static Complex Next(int max)
        {
            int x, y;
            lock (locked)
            {
                x = r.Next(max);
                y = r.Next(max);
            }
            return new Complex(x, y);
        }
        public static Complex Next(int min, int max)
        {
            int x, y;
            lock (locked)
            {
                x = r.Next(min, max);
                y = r.Next(min, max);
            }
            return new Complex(x, y);
        }
        public static Complex NextDouble()
        {
            double x, y;
            lock (locked)
            {
                x = r.Next();
                y = r.Next();
            }
            return new Complex(x, y);
        }
        public static void NextBytes(byte[] buffer)
        {
            lock (locked)
            {
                r.NextBytes(buffer);
            }
        }
    }
    [DynamicallyInvokable]
    class Quaternion
    {
        internal double X { get; set; }
        internal double Y { get; set; }
        internal double Z { get; set; }
        internal double W { get; set; }
        public Quaternion() { }
        public Quaternion(double x, double y, double z, double w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }
        public Quaternion(Vector3 vectorPart, double scalarPart)
        {
            X = vectorPart.X;
            Y = vectorPart.Y;
            Z = vectorPart.Z;
            W = scalarPart;
        }

        public static Quaternion Identity() => new Quaternion(0.0, 0.0, 0.0, 1.0);
        public static Quaternion Zero() => new Quaternion(0.0, 0.0, 0.0, 0.0);
        public bool IsIdentity() => (X == 0.0 && Y == 0.0 && Z == 0.0)? W == 0: false;
        public double Length() => Math.Sqrt(X * X + Y * Y + Z * Z + W * W);
        public double LengthSquared() => (X * X + Y * Y + Z * Z + W * W);
        public static Quaternion Normalize(Quaternion value)
        {
            var len  = 1.0 / value.Length();
            return new Quaternion(value.X * len, value.Y * len, value.Z * len, value.W * len);
        }
        public static Quaternion Conjugate(Quaternion value) => new Quaternion(-value.X, -value.Y, -value.Z, value.W);
        public static Quaternion Inverse(Quaternion value)
        {
            var len = 1.0 / value.Length();
            return new Quaternion(-value.X * len, -value.Y * len, -value.Z * len, value.W * len);
        }
        public static Quaternion CreateFromAxisAngle(Vector3 axis, double angle)
        {
            var num1 = angle * 0.5;
            var num2 = Math.Sin(num1);
            var num3 = Math.Cos(num1);
            var q = new Quaternion
            {
                X = axis.X * num2,
                Y = axis.Y * num2,
                Z = axis.Z * num2,
                W = num3
            };
            return q;
        }
        public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            var num1 = roll * 0.5;
            var num2 = Math.Sin(num1);
            var num3 = Math.Cos(num1);
            var num4 = pitch * 0.5;
            var num5 = Math.Sin(num4);
            var num6 = Math.Cos(num4);
            var num7 = yaw * 0.5;
            var num8 = Math.Sin(num7);
            var num9 = Math.Cos(num7);
            var q = new Quaternion
            {
                X = (num9 * num5 * num3) + (num8 * num6 * num2),
                Y = (num8 * num6 * num3) - (num9 * num5 * num2),
                Z = (num9 * num6 * num2) - (num8 * num5 * num3),
                W = (num9 * num6 * num3) + (num8 * num5 * num2)
            };
            return q;
        }
        public static double Dot(Quaternion q1, Quaternion q2) => q1.X * q2.X + q1.Y * q2.Y + q1.Z * q2.Z + q1.W * q2.W;
        public static Quaternion Negate(Quaternion v) => new Quaternion(-v.X, -v.Y, -v.Z, -v.W);
        public object Clone() => new Quaternion(X, Y, Z, W);
        public Quaternion CloneAs() => (Quaternion)Clone();
        public Quaternion DoubleToQuaternion(double d) => new Quaternion(d, 0, 0, 0);

        #region Арифметические операции
        public static Quaternion Add(Quaternion v1, Quaternion v2) => new Quaternion(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);
        public static Quaternion Sub(Quaternion v1, Quaternion v2) => new Quaternion(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);
        public static Quaternion Mul(Quaternion v1, Quaternion v2)
        {
            var q = new Quaternion();
            var x1 = v1.X;
            var y1 = v1.Y;
            var z1 = v1.Z;
            var w1 = v1.W;
            var x2 = v2.X;
            var y2 = v2.Y;
            var z2 = v2.Z;
            var w2 = v2.W;
            var num1  = (y1 * z2 - z1 * y2);
            var num2  = (z1 * x2 - x1 * z2);
            var num3  = (x1 * y2 - y1 * x2);
            var num4  = (x1 * x2 + y1 * y2 + z1 * z2);
            q.X  = (x1 * w2 + x2 * w1) + num1;
            q.Y  = (y1 * w2 + y2 * w1) + num2;
            q.Z  = (z1 * w2 + z2 * w1) + num3;
            q.W  = w1 * w2 - num4;
            return q;
        }
        public static Quaternion Div(Quaternion v1, Quaternion v2)
        {
            var q = new Quaternion();
            var x = v1.X;
            var y = v1.Y;
            var z = v1.Z;
            var w = v1.W;
            var num1 = 1.0 / v2.Length();
            var num2 = -v2.X * num1;
            var num3 = -v2.Y * num1;
            var num4 = -v2.Z * num1;
            var num5 = v2.W * num1;
            var num6 = (y * num4 - z * num3);
            var num7 = (z * num2 - x * num4);
            var num8 = (x * num3 - y * num2);
            var num9 = (x * num2 + y * num3 + z * num4);
            q.X = (x * num5 + num2 * w) + num6;
            q.Y = (y * num5 + num3 * w) + num7;
            q.Z = (z * num5 + num4 * w) + num8;
            q.W = w * num5 - num9;
            return q;
        }
        public static Quaternion Mul(Quaternion v1, double scalar) => new Quaternion(v1.X * scalar, v1.Y * scalar, v1.Z * scalar, v1.W * scalar);
        public static Quaternion Div(Quaternion v1, double scalar) => (scalar != 0) ? (new Quaternion(v1.X / scalar, v1.Y / scalar, v1.Z / scalar, v1.W / scalar)) : throw new DivideByZeroException(nameof(scalar));
        #endregion
        #region Перегрузки операторов
        public static Quaternion operator -(Quaternion v) => Negate(v);
        public static Quaternion operator +(Quaternion v1, Quaternion v2) => Add(v1, v2);
        public static Quaternion operator -(Quaternion v1, Quaternion v2) => Sub(v1, v2);
        public static Quaternion operator *(Quaternion v1, Quaternion v2) => Mul(v1, v2);
        public static Quaternion operator /(Quaternion v1, Quaternion v2) => Div(v1, v2);
        public static Quaternion operator *(Quaternion v1, double scalar) => Mul(v1, scalar);
        public static Quaternion operator *(double scalar, Quaternion v1) => Mul(v1, scalar);
        public static Quaternion operator /(Quaternion v1, double scalar) => Div(v1, scalar);
        public static Quaternion operator /(double scalar, Quaternion v1) => Div(v1, scalar);
        public static bool operator ==(Quaternion v1, Quaternion v2) => v1.Equals(v2);
        public static bool operator !=(Quaternion v1, Quaternion v2) => !v1.Equals(v2);
        #endregion

        public bool Equals(Quaternion other) => (X == other.X && Y == other.Y && Z == other.Z) ? (W == other.W): false;
        public override bool Equals(object obj)
        {
            if (obj is Quaternion) return obj.Equals(this);
            else return false;
        }
        public Quaternion Parse(string str)
        {
            if ((str == null) || (str.Length == 0)) throw new ArgumentNullException(nameof(str));
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 4)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                Z = Convert.ToDouble(vals[2]);
                W = Convert.ToDouble(vals[3]);
                return this;
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
        }
        public override string ToString() => $"{X} + {Y}i + {Z}j + {W}k";
        public void Print() => Console.Write(ToString());
        public void Println() => Console.WriteLine(ToString());
    }

    //FIXME: Дописать  класс Quaternion


    /// <summary>
    /// Дуальное число(комплексное число параболического типа)
    /// </summary>
    [DynamicallyInvokable]
    class Dual : ICloneable
    {
        internal double X;
        internal double Y;

        public Dual(double x, double y)
        {
            X = x;
            Y = y;
        }
        public static Dual Negate(Dual d1) => new Dual(-d1.X, -d1.Y);
        public static Dual Add(Dual d1, Dual d2) => new Dual(d1.X + d2.X, d1.Y + d2.Y);
        public static Dual Sub(Dual d1, Dual d2) => new Dual(d1.X - d2.X, d1.Y - d2.Y);
        public static Dual Mul(Dual d1, Dual d2) => new Dual(d1.X * d2.X, d1.Y * d2.X + d1.X * d2.Y);
        public static Dual Div(Dual d1, Dual d2) => new Dual(d1.X / d2.X, (d1.Y * d2.X - d1.X * d2.Y) / Math.Pow(d2.X, 2));
        public static Dual Power(Dual d, int n) => new Dual(Math.Pow(d.X, n), n * Math.Pow(d.X, (n - 1)) * d.Y);
        public static Dual Sqrt(Dual d, int n) => new Dual(BaseMath.Nqrt(d.X, n), d.Y / (n * BaseMath.Nqrt(Math.Pow(d.X, n - 1), n)));
        public static string Exp(double x) => $"1 + {x}E";
        #region Перегрузки операторов
        public static Dual operator -(Dual d) => Negate(d);
        public static Dual operator +(Dual d1, Dual d2) => Add(d1, d2);
        public static Dual operator -(Dual d1, Dual d2) => Sub(d1, d2);
        public static Dual operator *(Dual d1, Dual d2) => Mul(d1, d2);
        public static Dual operator /(Dual d1, Dual d2) => Div(d1, d2);
        #endregion
        public object Clone() => new Dual(X, Y);
        public Dual Parse(string str)
        {
            if ((str == null) || (str.Length == 0)) throw new ArgumentNullException(nameof(str));
            string[] vals = str.Split(new char[1] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (vals.Length != 2)
            {
                throw new FormatException("Строка имела неверный формат");
            }

            try
            {
                X = Convert.ToDouble(vals[0]);
                Y = Convert.ToDouble(vals[1]);
                return this;
            }
            catch
            {
                throw new FormatException("Строка имела неверный формат");
            }
        }
        public override string ToString() => $"{X}+{Y}E";
        public void Print() => Console.Write(ToString());
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Бикватернион(дуальный кватернион)
    /// </summary>
    [DynamicallyInvokable]
    class DualQuaternion
    {
        internal Dual DualX { get; set; }
        internal Dual DualY { get; set; }
        internal Dual DualZ { get; set; }
        internal Dual DualW { get; set; }
        public DualQuaternion(Dual dX, Dual dY, Dual dZ, Dual dW)
        {
            DualX = dX;
            DualY = dY;
            DualZ = dZ;
            DualW = dW;
        }
        public Dual Normalize() => Dual.Power(DualX, 2) + Dual.Power(DualY, 2) + Dual.Power(DualZ, 2) + Dual.Power(DualW, 2);
        public override string ToString()
        {
            return $"({DualX.ToString()}) + ({DualY.ToString()})i + ({DualZ.ToString()})j + ({DualW.ToString()})k";
        }
        public void Print() => Console.Write(ToString());
        public void Println() => Console.WriteLine(ToString());
    }
    /// <summary>
    /// Описывает виды факториалов
    /// </summary>
    [DynamicallyInvokable]
    class Factorials
    {
        //Вычисляет факториал (через рекурсию)
        public static int Factorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            else return Factorial(n - 1) * n;
        }
        public static BigInteger BigFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            else return BigFactorial(n - 1) * n;
        }
        //Вычисляет двойной факториал (через рекурсию)
        public static int DoubleFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            else return DoubleFactorial(n - 2) * n;
        }
        //Вычисляет тройной факториал (через рекурсию)
        public static int TripleFactorial(int n)
        {
            if (n == 0 || n == 1) return 1;
            else return TripleFactorial(n - 3) * n;
        }
        //Вычисляет праймориал
        public static int Primorial(int x)
        {
            int primes = 1;
            if (x > 0)
            {
                foreach (int each in BaseMath.Range(2, x + 1))
                {
                    if (BaseMath.IsSimple(each))
                    {
                        primes *= each;
                    }
                }
                return primes;
            }
            return -1;
        }
        //Вычисляет праймориал
        public static BigInteger BigPrimorial(int x)
        {
            BigInteger primes = 1;
            if (x > 0)
            {
                foreach (int each in BaseMath.Range(2, x + 1))
                {
                    if (BaseMath.IsSimple(each))
                    {
                        primes *= each;
                    }
                }
                return primes;
            }
            return -1;
        }
        //Вычисляет суперфакториал числа
        public static int SuperFactorial(int n)
        {
            int res = 1;
            for (int i = 1; i < n; i++)
            {
                res *= Factorial(i);
            }
            return res;
        }
        //Вычисляет гиперфакториал (через рекурсию)
        public static BigInteger HyperFactorial(int n)
        {
            int res = 1;
            for (int i = 1; i < n; i++)
            {
                res *= SuperFactorial(i);
            }
            return res;
        }

    }
    /// <summary>
    /// Описывает разные группы чисел с названиями
    /// </summary>
    [DynamicallyInvokable]
    class MathNumbers
    {
        //Вычисляет указанное число Леонардо 
        public static double Leonardo(double n)
        {
            if (n == 0 || n == 1) return 1;
            else return Leonardo(n - 1) + Leonardo(n - 2) + 1;
        }
        //Вычисляет указанное число Фибоначчи
        public static double Fib(double n)
        {
            if (n == 1 || n == 2) return 1;
            else return Fib(n - 1) + Fib(n - 2);
        }
        //Вычисляет указанное число трибоначчи
        public static double Trib(double n)
        {
            if (n == 1 || n == 2) return 0;
            else if (n == 3 || n == 4) return 1;
            else return Trib(n - 1) + Trib(n - 2) + Trib(n - 3);
        }
        //Вычисляет указанное число Люка
        public static double Luk(double n)
        {
            if (n < 0) throw new Exception(nameof(n));
            if (n == 0) return 2;
            else if (n == 1) return 1;
            else return Luk(n - 1) + Luk(n - 2);
        }
        //Вычисляет указанное число Сабита
        public static double Sabit(double n)
        {
            if (n >= 0) return 3 * BaseMath.Pow(2, n) - 1;
            else throw new Exception(nameof(n));
        }
        //Вычисляет указанное число Ферма
        public static double Ferma(double n)
        {
            if (n >= 0) return BaseMath.Pow(2, BaseMath.Pow(2, n)) + 1;
            else throw new Exception(nameof(n));
        }
        //Вычисляет указанное число Каллена
        public static double Callen(double n)
        {
            if (n == 1) return 1;
            else return n * BaseMath.Pow(2, n) + 1;
        }
        //Вычисляет указанное число Вудала
        public static double Vudal(double n)
        {
            if (n == 1) return 1;
            else return n * BaseMath.Pow(2, n) - 1;
        }
        //Вычисляет указанное число Мерсенна
        public static double Mersenn(double n)
        {
            if (n == 1) return 1;
            else return BaseMath.Pow(2, n) - 1;
        }
        //Вычисляет указанное число Каталана
        public static double Catalan(double n)
        {
            if (n == 0) return 1;
            else return ((2 * (2 * n - 1)) / (n + 1)) * (Catalan(n - 1));
        }
        //Вычисляет указанное число Пелля
        public static double Pell(double n)
        {
            if (n == 0) return 0;
            else if (n == 1) return 1;
            else return 2 * Pell(n - 1) + Pell(n - 2);
        }
        //Вычисляет указанное число Падована
        public static double Padovan(double n)
        {
            if (n == 1 || n == 2 || n == 3) return 1;
            else return Padovan(n - 2) + Padovan(n - 3);
        }
    }
    /// <summary>
    /// Фигурные числа
    /// </summary>
    [DynamicallyInvokable]
    class FigureNumbers
    {
        //Треугольные числа
        public static double TriangleNumbers(int n) => BaseMath.Trunc((n * (n + 1)) / 2);
        //Квадратные числа
        public static double QuadrNumbers(int n) => n * n;
        //Пятиугольные числа
        public static double PentagonalNumbers(int n) => BaseMath.Trunc((n * (3 * n - 1)) / 2);
        //Шестиугольные числа
        public static double HexagonalNumbers(int n) => BaseMath.Trunc(2 * BaseMath.Pow(n, 2) - n);
        //Семиугольные числа
        public static double HeptagonalNumbers(int n) => BaseMath.Trunc((5 * BaseMath.Pow(n, 2) - 3 * n) / 2);
        //Восьмиугольные числа
        public static double OctagonalNumbers(int n) => BaseMath.Trunc((6 * BaseMath.Pow(n, 2) - 4 * n) / 2);
        //Девятиугольные числа
        public static double NineAngleNumber(int n) => BaseMath.Trunc((7 * BaseMath.Pow(n, 2) - 5 * n) / 2);
        //Десятиугольные числа
        public static double TenAngleNumbers(int n) => BaseMath.Trunc((8 * BaseMath.Pow(n, 2) - 6 * n) / 2);
        //Одиннадцатиугольные числа
        public static double ElevenAngleNumbers(int n) => BaseMath.Trunc((9 * BaseMath.Pow(n, 2) - 7 * n) / 2);
        //Двенадцатиугольные числа 
        public static double TwelveAngleNumbers(int n) => BaseMath.Trunc(5 * BaseMath.Pow(n, 2) - 4 * n);
        //10000-угольные числа
        public static double TenThousandAngleNumbers(int n) => BaseMath.Trunc((9998 * BaseMath.Pow(n, 2) - 9996 * n) / 2);

    }
    /// <summary>
    /// Пространственные фигурные числа
    /// </summary>
    [DynamicallyInvokable]
    class SpatialFigureNumbers
    {
        // k-угольное пирамидальное число
        public static double PyramidalNumbers(int n, int k) => BaseMath.Trunc((n * (n + 1) * ((k - 2) * n - k + 5)) / 6);
        //Кубические числа
        public static double CubicNumbers(int n) => BaseMath.Trunc(BaseMath.Pow(n, 3));
        //Октаэдральные числа
        public static double OctahedralNumbers(int n) => BaseMath.Trunc((n * (2 * BaseMath.Pow(n, 2) + 1)) / 3);
        //Додекаэдральные числа
        public static double DodecahedralNumbers(int n) => BaseMath.Trunc((n * (3 * n - 1) * (3 * n - 2)) / 2);
        //Икосаэдральные числа
        public static double IcosahedralNumbers(int n) => BaseMath.Trunc(n * (5 * BaseMath.Pow(n, 2) - 5 * n + 2) / 2);
        //Звездчатые октаэдральные числа
        public static double StellaOctangulaNumbers(int n) => BaseMath.Trunc(n * (2 * BaseMath.Pow(n, 2) - 1));
    }
    /// <summary>
    /// Многомерные числа
    /// </summary>
    [DynamicallyInvokable]
    class MultiDimensionalNumbers
    {
        //Симплексные числа (d - измерение пространства)
        public static double SimplexNumbers(int n, int d) => Convert.ToInt32((Factorials.Factorial(n - 1 + d)) / Factorials.Factorial(n - 1) * Factorials.Factorial(d));
        //Гиперкубические числа
        public static double HyperCubicNumbers(int n, int d) => BaseMath.Trunc(BaseMath.Pow(n, d));
        //Пентатопные числа(гипертетраэдальные числа)
        public static double PentatopeNumbers(int n) => BaseMath.Trunc((n * (n + 1) * (n + 2) * (n + 3)) / 24);
    }
    /// <summary>
    /// Специальные функции
    /// </summary>
    [DynamicallyInvokable]
    class SpecialFunction
    {
        //Гамма-функция для целочисленного аргумента
        public static int Gamma(int n)
        {
            if (n == 1 || n == 2) return 1;
            else return Factorials.Factorial(n - 1);
        }
        //Бета-функция
        public static double Beta(int x, int y)
        {
            return (Gamma(x) + Gamma(y)) / Gamma(x + y);
        }
        //Пи-функция для целочисленного аргумента
        public static int Pi(int n)
        {
            return Gamma(n + 1);
        }
        //G-Функция Барнса
        public static int Barns(int n)
        {
            if (n == 1) return 1;
            else return Factorials.SuperFactorial(n - 2);
        }
        //K-функция 
        public static double K(int n)
        {
            return BaseMath.Pow(Gamma(n), n - 1) / Barns(n);
        }
    }
    //-------------------------------- Геометрия --------------------------------//
    /// <summary>
    /// Треугольник
    /// </summary>
    [DynamicallyInvokable]
    class Triangle
    {
        //Стороны треугольника
        internal double AB { get; set; }
        internal double BC { get; set; }
        internal double AC { get; set; }
        public Triangle(double ab, double bc, double ac)
        {
            AB = ab;
            BC = bc;
            AC = ac;
        }
        //Вычисляет периметр треугольника
        public double Perimeter() => AB + AC + BC;
        //Вычисляет площадь теугольника
        public double Area()
        {
            double p = (AB + BC + AC) / 2.0;
            return Math.Sqrt(p * (p - AB) * (p - BC) * (p - AC));
        }
        //Существует ли данный треугольник
        public bool IsExist() => ((AB + BC > AC) && (AB + AC > BC) && (BC + AC > AB));
        //Является ли треугольник равносторонним
        public bool IsEquilateral() => (IsExist()) ? (AB == BC && BC == AC) : false;
        //Является ли треугольник прямоугольным
        public bool IsRectangular() => (IsExist()) ? (AB * AB + BC * BC == AC * AC) : false;
        //Возвращает тип треугольника
        public string GetTypeTriangle()
        {
            if (IsExist())
            {
                if (IsEquilateral()) return "Равносторонний треугольник";
                else if ((AB == BC) || (AB == AC) || (BC == AC)) return "Равнобедренный треугольник";
                else return "Разносторонний треугольник";
            }
            else
            {
                throw new Exception("Данный треугольник не существует");
            }
        }
        public override string ToString() => $"Стороны: {AB},{BC},{AC}";
    }
    /// <summary>
    /// Геометрические фигуры на плоскости
    /// </summary>
    [DynamicallyInvokable]
    class Geometry2D
    {
        //Площадь квадрата
        public static double SQ_Square(double a) => a * a;
        //Площадь ромба
        //a-сторона ромба, h - высота ромба
        public static double SQ_Rhombus(double a, double h) => a * h;
        //Вычисляет площадь параллелограмма
        //a-сторона параллелограмма, h - высота параллелограмма
        public static double SQ_Parallelogram(double a, double h) => a * h;
        //Вычисляет площадь трапеции
        //a,b - основания трапеции, h - высота трапеции
        public static double SQ_Trapeze(double a, double b, double h) => ((a + b) / 2) * h;
        //Вычисляет площадь круга
        //r - радиус круга
        public static double SQ_Circle(double r) => LibraryConst.PI * BaseMath.Pow(r, 2);
    }
    /// <summary>
    /// Геометрические фигуры в пространстве
    /// </summary>
    [DynamicallyInvokable]
    class Geometry3D
    {
        //Площадь поверхности параллелепипеда
        //a,b,c - измерения параллелепипеда 
        public static double SQ_Parallelepiped(double a, double b, double c) => 2 * (a * b + b * c + a * c);
        //Объем параллелепипеда
        public static double V_Parallelepiped(double a, double b, double c) => a * b * c;
        //Площадь цилиндра
        //r - радиус основания, h - высота цилиндра
        public static double SQ_Cylinder(double r, double h) => 2 * LibraryConst.PI * r * r + h);
        //Объем цилиндра
        public static double V_Cylinder(double r, double h) => 2 * BaseMath.Pow(r, 2) * h;
        //Площадь конуса
        //r - радиус основания,l - длина образующей конуса
        public static double SQ_Cone(double r, double l) => LibraryConst.PI * r * (r + l);
        //Объем конуса
        //r - радиус основания, h - высота конуса
        public static double V_Cone(double r, double h) => (LibraryConst.PI * BaseMath.Pow(r, 2) * h) / 3;
        //Площадь сферы
        //r - радиус сферы
        public static double SQ_Sphere(double r) => 4 * LibraryConst.PI * BaseMath.Pow(r, 2);
        //Объем сферы
        ///r - радиус сферы
        public static double V_Sphere(double r) => (4 * LibraryConst.PI * BaseMath.Pow(r, 3)) / 3;
    }
    /// <summary>
    /// Геометрические фигуры N- измерения
    /// </summary>
    [DynamicallyInvokable]
    class GeometryND
    {
        //Площадь гиперкуба
        //N - кол-во измерений, a - длина стороны
        public static double SQ_GiperCube(int n, double a) => 2 * n * BaseMath.Pow(a, n - 1);
        //Объем гиперкуба
        //N - кол-во измерений, a - длина стороны
        public static double V_GiperCube(int n, double a) => BaseMath.Pow(a, n);
    }
    //-------------------------------- Геометрия --------------------------------//
    class Program
    {
        static void Main(string[] args)
        {
        }
    }
}
