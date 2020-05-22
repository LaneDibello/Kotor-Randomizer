using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2
{
    public static class Randomize
    {
        [ThreadStatic]
        private static Random Local;

        public static Random Rng
        {
            get
            {
                return Local ?? (Local = new Random(Seed));
            }
        }

        public static int Seed { get; private set; } = unchecked(Environment.TickCount * 31 + System.Threading.Thread.CurrentThread.ManagedThreadId);

        public static int GenerateSeed()
        {
            Seed = DateTime.Now.Millisecond * DateTime.Now.Second;
            return Seed;
        }

        public static void SetSeed(int seed)
        {
            Seed = seed;
            Local = null;
        }

        public static void RestartRng()
        {
            Local = null;
        }

        public static void FisherYatesShuffle<T>(IList<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;
                int k = Rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
