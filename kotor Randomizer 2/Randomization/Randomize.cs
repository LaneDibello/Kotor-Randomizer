using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace kotor_Randomizer_2
{
    // Slightly adjusted version of the 'ThreadSafeRandom' used in previous versions.
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
            // Base seeding off of current time.
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

        public static void RandomizeFiles(IEnumerable<FileInfo> files, string outPath)
        {
            if (files.Any())
            {
                var randList = files.ToList();
                FisherYatesShuffle(randList);

                int i = 0;
                foreach (FileInfo fi in files)
                {
                    File.Copy(fi.FullName, outPath + randList[i].Name, true);
                    ++i;
                }
            }
        }

        public static double GetRandomDouble(double min, double max)
        {
            if (min == max) return 0.0; // Handle 0 range without advancing the seed.
            return Rng.NextDouble() * (max - min) + min;
        }
    }
}
