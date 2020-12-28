using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Paralel_prog_examples_LAB_1_
{
    public class Program
    {
        
        static void Main(string[] args)
        {
            ThreadPool.SetMinThreads(10, 10);
            double result = 0;
            Action calculating_task = () => { for (int i = 0; i < 35000000; i++) {
                   result = Math.Sqrt(i) + Math.Pow(i,2) + Math.Abs(i);
                }; 
                };
            Action Io_waiting = () => {
                Thread.Sleep(1000);
                result = 2 + 2;
            };
            Action<Action> measure = (body) =>
            { var startTime = DateTime.Now;
                body();
                Console.WriteLine("{0} {1}", DateTime.Now - startTime, Thread.CurrentThread.ManagedThreadId);
            };
            Action<Action> measurefull = (body) =>
            {
                var startTime = DateTime.Now;
                body();
                Console.WriteLine("Overall time is: {0} {1}", DateTime.Now - startTime, Thread.CurrentThread.ManagedThreadId);
            };
            //CPU-bound tasks (only computation on processor)
            measurefull(() =>
           {
               Console.WriteLine("Paralel CPU-bound");
               var tasks = new[]
               {
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task)),
                Task.Factory.StartNew(() => measure(calculating_task))
               };
               Task.WaitAll(tasks);
           });
            measurefull(() =>
            {
                Console.WriteLine("\n Sequetial CPU-bound");
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
                measure(calculating_task);
            });
            //Memory-bound calc(computation with high memorization
            Methods m = new Methods();
            measurefull(() =>
            {
                Console.WriteLine("\n Paralel Memory-bound");
                var tasks = new[]
                {
                Task.Factory.StartNew(() => measure(()=>m.factorial_Recursion(500))),
                Task.Factory.StartNew(() => measure(()=>m.factorial_Recursion(600))),
                Task.Factory.StartNew(() => measure(()=>m.factorial_Recursion(700))),
                Task.Factory.StartNew(() => measure(()=>m.factorial_Recursion(800))),
                Task.Factory.StartNew(() => measure(()=>m.factorial_Recursion(900))),
                Task.Factory.StartNew(() => measure(()=>m.function_pi(1,1000,0))),
                Task.Factory.StartNew(() => measure(()=>m.function_pi(1,2000,0))),
                Task.Factory.StartNew(() => measure(()=>m.function_pi(1,3000,0))),
                Task.Factory.StartNew(() => measure(()=>m.function_pi(1,4000,0))),
                Task.Factory.StartNew(() => measure(()=>m.function_pi(1,5000,0)))
               };
                Task.WaitAll(tasks);
            });
            measurefull(() =>
            {
                Console.WriteLine("\n Sequential Memory-bound");
                measure(() => m.factorial_Recursion(500));
                measure(() => m.factorial_Recursion(600));
                measure(() => m.factorial_Recursion(700));
                measure(() => m.factorial_Recursion(800));
                measure(() => m.factorial_Recursion(900));
                measure(() => m.function_pi(1, 1000, 0));
                measure(() => m.function_pi(1, 2000, 0));
                measure(() => m.function_pi(1, 3000, 0));
                measure(() => m.function_pi(1, 4000, 0));
                measure(() => m.function_pi(1, 5000, 0));
            });
            //IO-bound calc (simple calc, simulation of waiting for a response)
            measurefull(() =>
            {
                Console.WriteLine("\n Paralel IO-bound");
                var tasks = new[]
                {
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting)),
                Task.Factory.StartNew(() => measure(Io_waiting))
               };
                Task.WaitAll(tasks);
            });
            measurefull(() =>
            {
            Console.WriteLine("\n Sequetial IO-bound");
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
            measure(Io_waiting);
        });
        }
    }
}
