using System;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Prog_LAB_3_
{
    public class Program
    {
        private static readonly Object obj = new Object();
        public static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(15, 15);
            Console.WriteLine("Please insert program mode (1 or 2)");
            int switchB = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert number crystal cells (N)");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert the probability of particle moving right (p)");
            int p = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert number of Particles (K)");
            int k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert time of modeling in miliseconds (t)");
            int t = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert delay in miliseconds (d)");
            int d = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please insert number of moves (m)");
            int m = Convert.ToInt32(Console.ReadLine());
            int[] Crystal_states = new int[n];
            if (switchB == 1)
            {
                //-----------Mod_1----------------
                Particle_Events exe1 = new Particle_Events(n, k, p, m, t, d);
                Task[] tasks = new Task[k];
                for (int i = 0; i < k; i++)
                {
                    tasks[i] = Task.Factory.StartNew(() => exe1.Mod_1());
                }
                Task.WaitAll(tasks);
                Console.Read();
            }
            if (switchB == 2)
            {
                //------------Mod_2---------------
                Particle_Events exe2 = new Particle_Events(n, k, p, m, t, d);
                Task[] tasks = new Task[k];
                for (int i = 0; i < k; i++)
                {
                    tasks[i] = Task.Factory.StartNew(() => exe2.Mod_2());
                }
                Task.WaitAll(tasks);
                while (true)
                {
                    if (exe2.doneNum == k)
                    {
                        Console.WriteLine(exe2.Current_Crystal());
                        break;
                    }
                }
                Console.Read();
            }
        }
    }
}
