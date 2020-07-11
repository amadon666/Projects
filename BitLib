```c#
using System;
namespace BitSharp
{
	public class BitUtility {
		public const int MAX_INDEX_BYTE_BIT = 8;
		public const int MAX_INDEX_INT16_BIT = 16;
		public const int MAX_INDEX_INT32_BIT = 32;
		public const int MAX_INDEX_INT64_BIT = 64;
		
		public static int Rol(int number, int len) {
			if ((number % 2) == 1 ) {
	        	return ((number << len)|0x80);
	    	}
	    	return number << len;
	    	// Можно короче:
	    	// return (((x) << (n)) | ((x) >> (64-(n))));
		}
		
		public static int Ror(int number, int len) {
			if ((number % 2) == 1 ) {
	        	return ((number >> len)|0x80);
	    	}
	    	return number >> len;
	    	// Можно короче:
	    	// return (((x) >> (n)) | ((x) << (64-(n))));
		}
		
		public static int Sal(int number, int len) {
			return number << len;
		}
		
		public static bool GetBoolBit(long bit, int index) {
			return (bit & (1 << index)) != 0;
		}
		
		public static int GetBit(long bit, int index) {
			return (GetBoolBit(bit, index)) ? 1 : 0;
		}
		
		public static void PrintBits(byte @byte) {
			for (int i = 0; i < MAX_INDEX_BYTE_BIT; ++i) {
				Console.Write(GetBit(@byte, i) + " ");
			}
		}
		
		public static void PrintBits(short @short) {
			for (int i = 0; i < MAX_INDEX_INT16_BIT; ++i) {
				Console.Write(GetBit(@short, i) + " ");
			}
		}
		
		public static void PrintBits(int @int) {
			for (int i = 0; i < MAX_INDEX_INT32_BIT; ++i) {
				Console.Write(GetBit(@int, i) + " ");
			}
		}
		
		public static void PrintBits(long @long) {
			for (int i = 0; i < MAX_INDEX_INT64_BIT; ++i) {
				Console.Write(GetBit(@long, i) + " ");
			}
		}
		
		public static byte SetBit(int bit, int index, bool bit_value) {
			int tmpval = 1;
			tmpval = tmpval << index;
			bit = (byte)bit & (~tmpval);
		
		   if (bit_value) {
				bit = (byte)bit | (tmpval);
		   }
			return Convert.ToByte(bit);
		}
		
		public static long SetBit(long bit, int index, bool bit_value) {
			int tmpval = 1;
			tmpval = tmpval << index;
			bit = bit & (~tmpval);
		
		   if (bit_value) {
				bit = bit | (tmpval);
		   }
		  return bit;
		}
		
		public static byte ReverseBits(byte number) {
			int result = 0;

		    for (int i = 0; i < MAX_INDEX_BYTE_BIT; ++i) {
		         result = result << 1;
		         	
		         if (((number >> i) & 1) > 0) {
		             result = result | 1;
		         }
		    }
		   return (byte) result;
		}
		
		public static int ReverseBits(int number) {
			int result = 0;

		    for (int i = 0; i < MAX_INDEX_INT32_BIT; ++i) {
		         result = result << 1;
		         	
		         if (((number >> i) & 1) > 0) {
		             result = result | 1;
		         }
		    }
		   return result;
		}
		
		public static int BitSwapRequired(int a, int b) {
			int count = 0;
			for (int c = a ^ b; c != 0; c = c & (c - 1)) {
		      ++count;
		   }
   		 return count;
		}
		// Возвращает кол-во бит которые используются в бинарном представлении числа
		public static int BitLength(int number) {
			int bitsCounter = 0;
		    while ((1 << bitsCounter) <= number) ++bitsCounter;
		    return bitsCounter;
		}
		
		public static short ToInt16(byte high, byte low) {
			int res = high << 8;
		    res = res | (low & 0xFF);
		    return Convert.ToInt16(res & 0xFFFF);
		}
		
		public static int SetZero(int num, int index) {
			return num & ~(1 << index);
		}
		
		public static int InvertBit(int num, int index) {
			return num ^ (1 << index);
		}
		
		public static int Equivalence(int x, int y) {
			return ~(x ^ y);
		}
		// Сравнение битов
		// Возвращает 0 если биты равны, иначе -1
		public static int CompareBytes(byte a, byte b) {
			unchecked
		    {
		        int ab = (int)a - (int)b;
		        int ba = (int)b - (int)a;
		        return (ab >> 31) | (int)((uint)ba >> 31);
		    }
		}
		
		public struct UInt24 {
		   private Byte _b0;
		   private Byte _b1;
		   private Byte _b2;
		
		   public UInt24(Int32 value) {
		       _b0 = (byte)(value & 0xFF);
		       _b1 = (byte)(value >> 8); 
		       _b2 = (byte)(value >> 16);
		   }
		
		   public unsafe Byte* Byte0 { get { fixed (Byte* b = &_b0) { return b; }; } }
		   public Int32 Value { get { return _b0 | ( _b1 << 8 ) | ( _b2 << 16 ); } }
		}
		
	}
```
