using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Prog_LAB_3_
{
    public class Particle_Events
    {
        private readonly Random _random = new Random();
        Object obj = new Object();
        Object dObj = new Object();
        public int doneNum = 0;
        int N, K, P, MOVES, TIME, DELAY;
        int[] _crystalStates;
        DateTime deadLine;
        public Particle_Events(int _N, int _K, int _P, int _MOVES, int _TIME, int _DELAY)
        {
            this.N = _N;
            this.K = _K;
            this.P = _P;
            this.MOVES = _MOVES;
            this.TIME = _TIME;
            this.DELAY = _DELAY;
            this._crystalStates = new int[N];
            this._crystalStates[0] = K;  
        }
        public int RandomNumber()
        {
            lock (_random)
            {
                return _random.Next(1, 100);
            }
        }
        public void Mod_1()
        {
            int prev_position = 0;
            int current_position = 0;
            deadLine = DateTime.Now;
            deadLine = deadLine.AddMilliseconds(TIME);
            while (true)
            {
                if (DateTime.Compare(DateTime.Now,deadLine)<0)
                {
                    if (RandomNumber() <= P)
                    {
                        if (current_position == (N - 1))
                        {
                            continue;
                        }
                        else
                        {
                            prev_position = current_position;
                            current_position++;
                        }

                    }
                    else
                    {
                        if (current_position == 0)
                        {
                            continue;
                        }
                        else
                        {
                            prev_position = current_position;
                            current_position--;
                        }
                    }
                    lock (obj)
                    {
                        _crystalStates[prev_position]--;
                        _crystalStates[current_position]++;
                        Console.WriteLine(Current_Crystal());
                    }
                    Thread.Sleep(DELAY);
                }
                else
                {
                    break;
                }
            }
        }
        public void Mod_2()
        {
            int prev_position = 0;
            int current_position = 0;
            for (int i = 0; i < MOVES; i++)
            {
                if (RandomNumber() <= P)
                {
                    if (current_position == (N - 1))
                    {
                        continue;
                    }
                    else
                    {
                        prev_position = current_position;
                        current_position++;
                    }

                }
                else
                {
                    if (current_position == 0)
                    {
                        continue;
                    }
                    else
                    {
                        prev_position = current_position;
                        current_position--;
                    }
                }
                lock (obj)
                {
                    _crystalStates[prev_position]--;
                    _crystalStates[current_position]++;
                }
            }
            lock(dObj)
            {
                doneNum++;
            }
        }
        public int[] GetArray()
        {
            return this._crystalStates;
        }
        public string Current_Crystal()
        {
            string result = "[";
            foreach (int x in _crystalStates)
            { result = result + " " + x; }
            result = result + " ]";
            return result;
        }
    }
}
