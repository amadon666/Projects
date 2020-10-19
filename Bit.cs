using System;
namespace ConsoleApplication1
{
	/// <summary>
	/// Поддерживает основные операции для работы с битами
	/// </summary>
	public static class Bit {
		private const int MIN_INDEX_BIT = 0;
		private const int MAX_INDEX_BIT = 8;
		
		private static void CheckIndexBit(int index)
		{
			if (index < MIN_INDEX_BIT || index > MAX_INDEX_BIT)
			{
				throw new ArgumentOutOfRangeException("Error: Index more or less maximum index byte");
			}
		}
		/// <summary>
		/// Возвращает значение бита из байта по указанному индексу
		/// </summary>
		/// <param name="b">Исходный байт</param>
		/// <param name="index">Позиция бита</param>
		/// <returns>bool</returns>
		public static bool GetBit(this byte b, int index)
		{
			Bit.CheckIndexBit(index);
			return (b & (1 << index)) != 0;
		}
		/// <summary>
		/// Устанавливает значение бита по указанному индексу
		/// </summary>
		/// <param name="b">Исходный байт</param>
		/// <param name="index">Позиция бита</param>
		/// <param name="bit">Устанавливаемое значение(true или false)</param>
		/// <returns>byte</returns>
		public static byte SetBit(this byte b, int index, bool bit)
		{
			Bit.CheckIndexBit(index);
			byte tmpval = 1;
  			tmpval = (byte)(tmpval << index);//устанавливаем нужный бит в единицу
  			b = (byte)(b & (~tmpval));//сбрасываем в 0 нужный бит

           	if (bit)// если бит требуется установить в 1
            {
               b = (byte)(b | (tmpval));//то устанавливаем нужный бит в 1
            }
		   return b;
		}
		/// <summary>
		/// Возвращает значение бита в двоичной форме
		/// </summary>
		/// <param name="b">Исходный байт</param>
		/// <param name="index">Позиция бита</param>
		/// <returns>int</returns>
		public static int GetBinaryBit(this byte b, int index)
		{
			return (b.GetBit(index)) ? 1 : 0;
		}
		/// <summary>
		/// Выводит значения всех битов указанного байта
		/// </summary>
		/// <param name="b">Исходный байт</param>
		public static void PrintBytes(this byte b)
		{
			for (int i = 0; i < Bit.MAX_INDEX_BIT; i++)
			{
				Console.Write(b.GetBinaryBit(i) + " ");
			}
		}
		/// <summary>
		/// Переводит биты в байт
		/// </summary>
		/// <param name="bits">Строка битов(в двоичной форме записи)</param>
		/// <returns>byte</returns>
		public static byte ByteFromBits(string bits)
		{
			return Convert.ToByte(bits, 2);
		}
		/// <summary>
		/// Реверс битов в байте
		/// </summary>
		/// <param name="val">Исходный байт</param>
		/// <returns>byte</returns>
		public static byte Reverse(this byte val)
  		{
     		int i = 0;
     		byte result = 0;

     		for (i = 0; i < Bit.MAX_INDEX_BIT; i++)
     		{
         		result = (byte)(result << 1);
         	
            	if (((val >> i) & 1) > 0)
            	{
               		result = (byte)(result | 1);
            	}
         	}
        	return result;
		}
		/// <summary>
		/// Возвращает максимальное из двух чисел
		/// </summary>
		/// <param name="x">Первое число</param>
		/// <param name="y">Второе число</param>
		/// <returns>int</returns>
		public static int Max(int x, int y)
		{
			return y & ((x - y) >> 31) | x & (~(x - y) >> 31);
		}
		/// <summary>
		/// Возвращает минимальное из двух чисел
		/// </summary>
		/// <param name="x">Первое число</param>
		/// <param name="y">Второе число</param>
		/// <returns>int</returns>
		public static int Min(int x, int y)
		{
			return x & ((x - y) >> 31) | y & (~(x - y) >> 31);
		}
		/// <summary>
		/// Проверяет равны ли числа по знаку
		/// </summary>
		/// <param name="x">Первое число</param>
		/// <param name="y">Второе число</param>
		/// <returns>bool</returns>
		public static bool EqualSign(int x, int y)
		{
			return (x ^ y) >= 0; 
		}
		/// <summary>
		/// Возвращает абсолютное значение числа
		/// </summary>
		/// <param name="x">Число</param>
		/// <returns>int</returns>
		public static int Abs(int x)
		{
			return (x ^ (x >> 31)) - (x >> 31);
		}
		
		public static int Mul2(int x)
		{
			return x << 1;
		}
		
		public static int Div2(int x)
		{
			return x >> 1;
		}
		/// <summary>
		/// Проверяет числа на равенство
		/// </summary>
		/// <param name="x">Первое число</param>
		/// <param name="y">Второе число</param>
		/// <returns>bool</returns>
		public static bool Equal(int x, int y)
		{
			return (x ^ y) == 0;
		}
		// Является ли число четным
		public static bool IsEven(int x)
		{
			return (x & 1) == 1;
		}
		
		public static int Negate(int i)
		{
			i = ~i + 1; // or
            i = (i ^ -1) + 1; // i = -i
            return i;
		}
		// Среднее арифметическое 2 чисел
		public static int Average(int x, int y)
		{
			return (x + y) >> 1;
				// ((x ^ y) >> 1) + (x & y); - можно и так
		}
		// определяет количество битов, которые необходимо изменить, чтобы из целого числа А получить целое число B
		public static int bitSwapRequired(int a, int b) {
    		int count = 0;
    		for (int c = a ^ b; c != 0; c = c & (c - 1)) {
        		count++;
    		}
    		return count;
		}
	}
	
    class Program
    {
        static void Main()
        {
        	byte b = 122;
        	//Console.WriteLine(b.GetBit(2));
        	//b.PrintBytes();
        	
            Console.ReadLine();
        }
    }
}
