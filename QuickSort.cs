using System;
using System.Collections;
namespace Ptr_
{
    static class Program
    {
    	unsafe static void dbleInt(void* a) {
			*((int*) a) *= 2;
		}
    	
    	unsafe static bool isOddInt(void* a) {
    		return (*((int*) a) % 2 != 0);
    	}
    	
    	unsafe static bool cmpIntDesc(void *a, void* b) {
			return *((int*) a) < *((int*) b);
		}
		
		unsafe static bool cmpIntAsc(void *a, void* b) {
			return *((int*) a) > *((int*) b);
		}
    	
    	unsafe static bool isBetweenInt(void* a, void* min, void* max) {
    		return *((int*) a) > *((int*)min) && *((int*) a) < (*(int*)max);
		}
    	
    	unsafe static void qsortRecursive(int* mas, int size) {
    		//Указатели в начало и в конец массива
		    int i = 0;
		    int j = size - 1;
		
		    //Центральный элемент массива
		    int mid = mas[size / 2];
		
		    //Делим массив
		    do {
		        //Пробегаем элементы, ищем те, которые нужно перекинуть в другую часть
		        //В левой части массива пропускаем(оставляем на месте) элементы, которые меньше центрального
		        while(mas[i] < mid) {
		            i++;
		        }
		        //В правой части пропускаем элементы, которые больше центрального
		        while(mas[j] > mid) {
		            j--;
		        }
		
		        if (i <= j) {
		            int tmp = mas[i];
		            mas[i] = mas[j];
		            mas[j] = tmp;
		
		            i++;
		            j--;
		        }
		    } while (i <= j);
		
		
		    //Рекурсивные вызовы, если осталось, что сортировать
		    if(j > 0) {
		        qsortRecursive(mas, j + 1);
		    }
		    if (i < size) {
		        qsortRecursive(&mas[i], size - i);
		    }
    	}
    	
    	private static int[] QuickSort(int[] a, int i, int j)
        {
            if (i < j)
            {
                int q = Partition(a, i, j);
                a = QuickSort(a, i, q);
                a = QuickSort(a, q + 1, j);
            }
            return a;
        }
 
        private static int Partition(int[] a, int p, int r)
        {
            int x = a[p];
            int i = p - 1;
            int j = r + 1;
            while (true)
            {
                do
                {
                    j--;
                }
                while (a[j] > x);
                do
                {
                    i++;
                }
                while (a[i] < x);
                if (i < j)
                {
                    int tmp = a[i];
                    a[i] = a[j];
                    a[j] = tmp;
                }
                else
                {
                    return j;
                }
            }
        }
    	
        static int[] temporaryArray;
        
        static void Merge(int[] array, int start, int middle, int end)
		{
			var leftPtr = start;
			var rightPtr = middle + 1;
			var length = end - start + 1;
			for (int i = 0; i < length; i++)
			{
				if (rightPtr > end || (leftPtr <= middle && array[leftPtr] < array[rightPtr]))
				{
					temporaryArray[i] = array[leftPtr];
					leftPtr++;
				}
				else
				{
					temporaryArray[i] = array[rightPtr];
					rightPtr++;
				}
			}
			for (int i = 0; i < length; i++)
				array[i + start] = temporaryArray[i];
		}
 
		static void MergeSort(int[] array, int start, int end)
		{
			if (start == end) return;
			var middle = (start + end) / 2;
			MergeSort(array, start, middle);
			MergeSort(array, middle + 1, end);
			Merge(array, start, middle, end);
 
		}
 
		static void MergeSort(int[] array)
		{
			temporaryArray = new int[array.Length];
			MergeSort(array, 0, array.Length - 1);
		}
        // Алгоритм поиска наиболее часто встречающегося значения в массиве
		static void MaxOccurrence(int[] array, Hashtable hs)
        {
            int mostCommom = array[0];
            int occurences = 0;
            foreach (int num in array)
            {
                if (!hs.ContainsKey(num))
                {
                    hs.Add(num, 1);
                }
                else
                {
                    int tempOccurences = (int)hs[num];
                    tempOccurences++;
                    hs.Remove(num);
                    hs.Add(num, tempOccurences);
 
                    if (occurences < tempOccurences)
                    {
                        occurences = tempOccurences;
                        mostCommom = num;
                    }
                }
            }
           
            Console.WriteLine("Наиболее часто встречающееся число: " + mostCommom + " и оно повторяется " + occurences + " раз");
        }
		
		// Алгоритм быстрой сортировки с использованием обобщенных типов
		static int partition<T>( T[] m, int a, int b) where T : IComparable<T>
			{
			    int i = a;
			    for (int j = a; j <= b; j++)
			    {
			        if (m[j].CompareTo( m[b]) <= 0)
			        {
			            T t = m[i];
			            m[i] = m[j];
			            m[j] = t;
			            i++;
			        }
			    }
			    return i - 1;
			}
			
			static void quicksort<T>( T[] m, int a, int b) where T : IComparable<T>
			{
			    if (a >= b) return;
			    int c = partition( m, a, b);
			    quicksort( m, a, c - 1);
			    quicksort( m, c + 1, b);
			}
		
        unsafe static void Main(string[] args)
        {
        	/* Example 1:
 *          int a = 20;
        	int* b = &a;
        	dbleInt(b);
        	Console.WriteLine(a); // 40*/
        	
        	// Example 2:
        	/*int a = 3;
        	int* ptr = &a;
        	Console.WriteLine(isOddInt(ptr));*/
        	
        	// Example 3:
        	/*int a = 34;
        	int b = 12;
        	Console.WriteLine(cmpIntDesc(&a, &b)); // false
        	Console.WriteLine(cmpIntAsc(&a, &b)); // true*/
        	
        	// Example 4:
        	/*int num = 23;
        	int min = 1;
        	int max = 200;
        	Console.WriteLine(isBetweenInt(&num, &min, &max)); // true*/
        	
        	// Example 5:
        	// Рекурсивная быстрая сортировка
        	/*int[] arr = { 54,43,64,36,34,6,3,4 };
        	
        	fixed (int* ptr = arr) {
        		qsortRecursive(ptr, arr.Length);
        	}
        	
        	for (int i = 0; i < arr.Length; i++) {
        		Console.WriteLine(arr[i]);
        	}*/
        	
        	// Example 6:
        	/*int[] arr = { 54,43,64,36,34,6,3,4 };
        	arr = QuickSort(arr, 0, arr.Length-1);
        	
        	for (int i = 0; i < arr.Length; i++) {
        		Console.WriteLine(arr[i]);
        	}*/
        	
        	// Example 7:
        	/*int[] arr = { 54,43,64,36,34,6,3,4 };
        	MergeSort(arr);
        	
        	for (int i = 0; i < arr.Length; i++) {
        		Console.WriteLine(arr[i]);
        	}*/
        	
        	// Example 8:
        	/*int[] array = new int[20] { 3, 6, 8, 5, 3, 5, 7, 6, 4, 3, 2, 3, 5, 7, 6, 4, 3, 4, 5, 7 };
            Hashtable hs = new Hashtable();
            MaxOccurrence(array, hs);*/
        	
        	// Example 9:
        	/*double[] arr = {9,1.5,34.4,234,1,56.5};
			quicksort<double>(arr,0,arr.Length-1);
			
			for (int i = 0; i < arr.Length; i++) {
        		Console.WriteLine(arr[i]);
        	}*/
        	
            Console.ReadKey();
        }
    }
}
