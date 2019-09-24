using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public static class LevelConverter
    {
        
        public static LevelMap ConvertLevelToLevelMap(Level level)
        {
            LevelMap levelMap = new LevelMap();
            levelMap.Background = ConvertTilemapToStringArray(level.Background);
            levelMap.Foreground = ConvertTilemapToStringArray(level.Foreground);
            levelMap.Obstacles = ConvertTilemapToStringArray(level.Obstacles);
            levelMap.Characters = ConvertTilemapToStringArray(level.Characters);
            return levelMap;
        }

        private static string[,] ConvertTilemapToStringArray(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
            string[,] stringArray = new string[bounds.size.x,bounds.size.y];

            for (int x = 0; x < bounds.size.x; x++) {
                for (int y = 0; y < bounds.size.y; y++) {
                    TileBase tile = allTiles[x + y * bounds.size.x];
                    if (tile == null)
                    {
                        stringArray[x, y] = "";
                    }
                    else
                    {
                        stringArray[x, y] = tile.ConvertTileToString();
                    }
                }
            }

            return stringArray;
        }


        private static string ConvertTileToString(this TileBase tile)
        {
            return ""; //TileTypes.GetTileTypeOfTile(tile);
        }
    }
}