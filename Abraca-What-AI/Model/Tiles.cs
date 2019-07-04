using Abraca_What_AI.Exceptions;

namespace Abraca_What_AI.Model
{
    public static class TilesMethods
    {
        public static string GetText(this Tiles tile)
        {
            var type = typeof(Tiles);
            var memInfo = type.GetMember(tile.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TilesText), false);
            return ((TilesText)attributes[0]).Text;
        }
        public static byte GetNumber(this Tiles tile)
        {
            var type = typeof(Tiles);
            var memInfo = type.GetMember(tile.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TilesNumber), false);
            return ((TilesNumber)attributes[0]).Number;
        }
        public static int Max(this Tiles tile) => tile.GetNumber();
        public static Tiles GetTileByNumber(int number)
        {
            switch (number)
            {
                case 1:
                    return Tiles.Ancient_Dragon;
                case 2:
                    return Tiles.Dark_Wanderer;
                case 3:
                    return Tiles.Sweet_Dream;
                case 4:
                    return Tiles.Night_Singer;
                case 5:
                    return Tiles.Lightning_Tempest;
                case 6:
                    return Tiles.Blizzard;
                case 7:
                    return Tiles.Fireball;
                case 8:
                    return Tiles.Magic_Elixir;
                default:
                    throw new InvalidTileException("Invalid tile number.");
            }
        }
    }
    
    public enum Tiles
    {
        [TilesName("Ancient Dragon")]
        [TilesText("Roll die, do damage to other players equal to the result.")]
        [TilesNumber(1)]
        Ancient_Dragon = 1,

        [TilesName("Dark Wanderer")]
        [TilesText("Do 1 damage to all other players, heal yourself for 1.")]
        [TilesNumber(2)]
        Dark_Wanderer = 2,

        [TilesName("Sweet Dream")]
        [TilesText("Roll the die, heal yourself for the result.")]
        [TilesNumber(3)]
        Sweet_Dream = 3,

        [TilesName("Night Singer")]
        [TilesText("Look at one of the tiles in the hidden stash.")]
        [TilesNumber(4)]
        Night_Singer = 4,

        [TilesName("Lightning Tempest")]
        [TilesText("Do 1 damage to players on your immediate left and right.")]
        [TilesNumber(5)]
        Lightning_Tempest = 5,

        [TilesName("Blizzard")]
        [TilesText("Do 1 damage to the player on your right.")]
        [TilesNumber(6)]
        Blizzard = 6,

        [TilesName("Fireball")]
        [TilesText("Do 1 damage to the player on your left.")]
        [TilesNumber(7)]
        Fireball = 7,

        [TilesName("Magic Elixir")]
        [TilesText("Heal yourself for 1.")]
        [TilesNumber(8)]
        Magic_Elixir = 8
    }
}
