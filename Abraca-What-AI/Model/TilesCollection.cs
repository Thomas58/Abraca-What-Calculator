using Abraca_What_AI.Exceptions;
using System.Linq;

namespace Abraca_What_AI.Model
{
    class TilesCollection
    {
        public byte[] TileNumbers = { 0, 0, 0, 0, 0, 0, 0, 0 };

        public int CountAll()
        {
            int sum = 0;
            foreach (byte num in TileNumbers)
                sum += num;
            return sum;
        }
        public int Count(Tiles tile) => TileNumbers[tile.GetNumber() - 1];
        public bool Has(Tiles tile) => 0 < TileNumbers[tile.GetNumber() - 1];
        public bool AtMax(Tiles tile) => tile.GetNumber() <= TileNumbers[tile.GetNumber() - 1];

        public void Add(int number)
        {
            Tiles tile = TilesMethods.GetTileByNumber(number);
            if (AtMax(tile))
                throw new InvalidTileException("Too many of the same tile in collection.");
            TileNumbers[number - 1]++;
        }
        public void Add(Tiles tile) => Add(tile.GetNumber());

        public Tiles Remove(int number)
        {
            Tiles tile = TilesMethods.GetTileByNumber(number);
            if (TileNumbers[number - 1] < 1)
                throw new InvalidTileOperationException("No tile to remove.");
            TileNumbers[number - 1]--;
            return tile;
        }
        public Tiles Remove(Tiles tile) => Remove(tile.GetNumber());

        public void Clear() => TileNumbers = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0 };
        public void Fill() => TileNumbers = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    }
}
