using System;
using System.Collections.Generic;
using System.Linq;

namespace Abraca_What_AI.Model
{
    class AI
    {
        /**
         * Represents known tiles. Tiles that are visible and can't be in the player's hand.
         */
        public TilesCollection Known = new TilesCollection();

        /**
         * Represents unknown tiles. Tiles which are not visible and might be in the player's hand.
         */
        public TilesCollection Unknown = new TilesCollection();

        /**
         * Represents the number of tiles currently in the player's hand.
         */
        public int HandSize = 5;

        /**
         * Represents when a tile is confirmed to not be in the hand.
         */
        public bool[] NotInHand = { false, false, false, false, false, false, false, false };

        public AI() => Clear();

        public double CalculateProbability(Tiles tile)
        {
            var N = Unknown.CountAll();
            for (int i = 0; i < 8; i++)
            {
                if (NotInHand[i]) N -= Unknown.Count(TilesMethods.GetTileByNumber(i+1));
            }
            var k = Unknown.Count(tile);
            if (k < 1 || NotInHand[tile.GetNumber()-1]) return 0.0;
            var n = HandSize;
            if (n < 1) return 0.0;
            var x = Enumerable.Range(1, k);
            double Result = CHP(N, k, n, x);
            if (1 < Result) return 1.0;
            return Result;
        }

        public void Clear()
        {
            Known.Clear();
            Unknown.Fill();
            HandSize = 5;
        }

        /**
         * N = Population, k = Successes in population, n = Sample size, x = Success range
         */
        public double CumulativeHypergeometricProbability(int N, int k, int n, IEnumerable<int> x)
        {
            double Sum = 0.0;
            foreach (int num in x)
                Sum += C(k, num) * C(N - k, n - num) / C(N, n);
            return Sum;
        }
        public double CHP(int N, int k, int n, IEnumerable<int> x) => CumulativeHypergeometricProbability(N, k, n, x);
        
        /**
         * Calculates the number of possible combinations to draw r objects from a population of n objects.
         */
        public double Combinations(int n, int r)
        {
            return F(n) / (F(r) * F(n - r));
        }
        public double C(int n, int r) => Combinations(n, r);

        /**
         * Calculates the factorial of the number given.
         */
        public double Factorial(int number)
        {
            if (number < 0) return 1.0;
            if (0.0 <= CalculatedFactorials[number]) return CalculatedFactorials[number];
            var result = 1.0;
            var iteration = number;
            while (iteration >= 1)
            {
                result *= iteration;
                iteration--;
            }
            CalculatedFactorials[number] = result;
            return result;
        }
        public double F(int number) => Factorial(number);
        private double[] CalculatedFactorials = new double[37].Select((num) => num - 1.0).ToArray();
    }
}
