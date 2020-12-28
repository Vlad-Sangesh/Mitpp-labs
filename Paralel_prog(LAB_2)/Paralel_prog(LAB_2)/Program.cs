using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Paralel_prog_LAB_2_
{

    class Program
    {
        static object locker = new object();
        static void Main(string[] args)
        {
            List<object> B = new List<object>();
            List<object> A = new List<object>();
            

            Action<object> AtoB = (a) => {
               int b = Convert.ToInt32(a) * 10 / 3 + 16 - 22;
                B.Add(b);                
            };
            Action<int> func = (a) =>
            {
                int i = 0;
                var timeout = TimeSpan.FromMilliseconds(500);
                while (true)
                {
                    if (Monitor.TryEnter(A[a + i], timeout))
                    {
                        try
                        {
                            AtoB(A[a + i]);
                        }
                        finally
                        {
                            lock (locker)
                            {
                                Console.WriteLine("{0} -> {1}", a + i, Thread.CurrentThread.ManagedThreadId);
                                Console.WriteLine("{0} Did number {1}", Thread.CurrentThread.ManagedThreadId, A[a + i]);
                                Monitor.Exit(A[a + i]);
                            }
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Locked!");
                        i++;
                    }
                }
            };
            for (int i = 0; i < 1000; i++)
            {
                A.Add(i);
            }
            //_______________________________________________________________________________________________________________
            Console.WriteLine("----------------------------------------------------------------------------------------------");
            Parallel.For(0, 1000, func);
            int min = Convert.ToInt32(B[0]);
            int index = 0;
            int minIndex=0;
            Action<object> minD = (listItem) =>
            {
                if (Convert.ToInt32(listItem) < min)
                {
                    min = Convert.ToInt32(listItem);
                    minIndex = index;
                }
            };
            foreach (int item in B)
            {
                Console.WriteLine(item);
            }
            foreach (int item in B)
            {
                minD(item);
                index++;
            }
            Console.WriteLine("Minimal value in the list is {0}, and index is {1}", min, minIndex);
            Console.ReadLine();
        }
    }

}
