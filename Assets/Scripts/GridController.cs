using System;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.Tilemaps;

namespace Game
{
    public class GridController : MonoBehaviour
    {
        private Level level;

        public int Width => 100;
        public int Height => 100;

        public void LoadLevel(Level level)
        {
            this.level = level;
            //Instantiate(cellPrefab, gridLayout.transform); // Instancier prefab ui quand new tile et l'associé
        }

        public void LoadLevel2(string level)
        {
            
        }

        public Tile GetNode(int x, int y)
        {
            if(x >= Width || y >= Height) throw new ArgumentOutOfRangeException();
            return new Tile(TileType.DOOR); //tiles[x, y];
        }

        public void SetNodeType(TileType nodeType)
        {
            
        }


    }
}