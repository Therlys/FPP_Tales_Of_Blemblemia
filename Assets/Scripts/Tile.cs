using TreeEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Tile
    {
        private TileType tileType;
        private Vector2 position;
        private Character character;
        [SerializeField] private GameObject cellPrefab;

        public bool IsWalkable => tileType != TileType.OBSTACLE && character == null;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public Tile(TileType tileType)
        {
            this.tileType = tileType;
        }
        
        public bool LinkCharacter(Character character)
        {
            if (!IsWalkable) return false;
            this.character = character;
            return character != null;
        }

        public bool UnlinkCharacter()
        {
            if (character == null) return false;
            character = null;
            return true;
        }

        public Character GetCharacter()
        {
            return character;
        }
    }

    public enum TileType
    {
        GRASS = 0,
        OBSTACLE = 1,
        FOREST = 2,
        FORTRESS = 3,
        DOOR = 4
    }
}