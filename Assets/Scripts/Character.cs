using UnityEngine;

namespace Game
{
    public abstract class Character : MonoBehaviour
    {
        private Tile currentTile;

        public Vector2 Position => currentTile.Position;

        private int range = Constants.BASE_RANGE;
        public Character(int range)
        {
            this.range = range;
        }

        public void MoveTo(int positionX, int positionY)
        {
            Tile tile = Finder.GridController.GetNode(positionX, positionY);

            if (tile.IsWalkable)
            {
                if (tile.LinkCharacter(this))
                {
                    currentTile = tile;
                }
            }
        }
    }
}