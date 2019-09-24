using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class CellGridCreator : MonoBehaviour
    {
        public GameObject EmptyCellPrefab;
        [SerializeField] public GameObject ForestCellPrefab = null;
        [SerializeField] public GameObject ObstacleCellPrefab = null;
        [SerializeField] public GameObject FortressCellPrefab = null;
        [SerializeField] public TileBase ForestTile = null;
        [SerializeField] public TileBase ObstacleTile = null;
        [SerializeField] public TileBase FortressTile = null;
        [SerializeField] public GameObject Grid;

        private void Awake()
        {
            //EmptyCellPrefab.transform.SetParent(Grid.transform);
            //ForestCellPrefab.transform.SetParent(Grid.transform);
            //ObstacleCellPrefab.transform.SetParent(Grid.transform);
            //FortressCellPrefab.transform.SetParent(Grid.transform);
        }

        public void CreateCellsDependingOnTilemap(Tilemap tilemap)
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
            Rect cellGridRectangle = GetComponent<RectTransform>().rect;
            int minX = ((int)bounds.size.x - (int)cellGridRectangle.width)/2;
            int minY = ((int)bounds.size.y - (int)cellGridRectangle.height)/2;
            int maxX = bounds.size.x - minX;
            int maxY = bounds.size.y - minY;
            if(minX < 0) minX = 0;
            if(minY < 0) minY = 0; 
            if(maxX > bounds.size.x) maxX = bounds.size.x;
            if(maxY > bounds.size.y) maxY = bounds.size.y;
            GetComponent<RectTransform>().sizeDelta = new Vector2(maxX - minX,maxY - minY);
            for (int y = maxY - 1; y >= 0 + minY; y--) {
                for (int x = 0 + minX; x < maxX; x++) {
                    TileBase tile = allTiles[x + y * (bounds.size.x)];
                    //Debug.Log(tile);
                    if (tile == null)
                    {
                        GameObject.Instantiate(EmptyCellPrefab, transform);
                    }
                    else
                    {
                        if (tile == ForestTile)
                        {
                            GameObject.Instantiate(ForestCellPrefab, transform);
                        }

                        else if (tile == ObstacleTile)
                        {
                            GameObject.Instantiate(ObstacleCellPrefab, transform);
                        }

                        else if (tile == FortressTile)
                        {
                            GameObject.Instantiate(FortressCellPrefab, transform);
                        }
                        else
                        {
                            Instantiate(EmptyCellPrefab, transform);
                        }
                    }
                }
            }
        }
    }
}