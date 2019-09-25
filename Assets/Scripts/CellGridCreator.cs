using System;
using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Game
{
    public class CellGridCreator : MonoBehaviour
    {
        /*Ajouter un else if pour tous les nouveaux prefabs et tiles dans la méthode*/
        [Header("Prefabs")]
        [SerializeField] private GameObject emptyCellPrefab;
        [SerializeField] private GameObject forestCellPrefab = null;
        [SerializeField] private GameObject obstacleCellPrefab = null;
        [SerializeField] private GameObject fortressCellPrefab = null;
        [Header("Tiles")]
        [SerializeField] private TileBase forestTile = null;
        [SerializeField] private TileBase obstacleTile = null;
        [SerializeField] private TileBase fortressTile = null;
        [Header("Grid")]
        [SerializeField] private GameObject grid = null;
        [Header("Tilemap")]
        [SerializeField] private Tilemap tilemap = null;
        public void CreateCellsDependingOnTilemap()
        {
            if (transform.childCount > 0)
            {
                DeleteGridChildren();
            }
            
            CheckForExceptions();
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
            Rect cellGridRectangle = grid.GetComponent<RectTransform>().rect;
            int minX = GetMinX(bounds,cellGridRectangle);
            int minY = GetMinY(bounds,cellGridRectangle);
            int maxX = GetMaxX(bounds,cellGridRectangle);
            int maxY = GetMaxY(bounds,cellGridRectangle);
            GetComponent<RectTransform>().sizeDelta = new Vector2(maxX - minX,maxY - minY);
            for (int y = maxY - 1; y >= 0 + minY; y--) 
            {
                for (int x = 0 + minX; x < maxX; x++) 
                {
                    TileBase tile = allTiles[x + y * (bounds.size.x)];
                    CreateCellDependingOnTile(tile);
                }
            }
        }
        
        
        private void CheckForExceptions()
        {
            if (tilemap == null) throw new Exception("Null Tilemap");
            if (emptyCellPrefab == null) throw new Exception("Null Empty Cell Prefab");
            if (forestCellPrefab == null) throw new Exception("Null Forest Cell Prefab");
            if (obstacleCellPrefab == null) throw new Exception("Null Obstacle Cell Prefab");
            if (fortressCellPrefab == null) throw new Exception("Null Fortress Cell Prefab");
            if (forestTile == null) throw new Exception("Null Forest Tile");
            if (obstacleTile == null) throw new Exception("Null Obstacle Tile");
            if (fortressTile == null) throw new Exception("Null Fortress Tile");
            if (grid == null) throw new Exception("Null Grid");
            if (tilemap == null) throw new Exception("Null Tilemap");
        }

        private int GetMinX(BoundsInt bounds, Rect cellGridRectangle)
        {
            int minX = ((int)bounds.size.x - (int)cellGridRectangle.width)/2;
            if(minX < 0) minX = 0;
            return minX;
        }

        private int GetMinY(BoundsInt bounds, Rect cellGridRectangle)
        {
            int minY = ((int)bounds.size.y - (int)cellGridRectangle.height)/2;
            if(minY < 0) minY = 0;
            return minY;
        }

        private int GetMaxX(BoundsInt bounds, Rect cellGridRectangle)
        {
            int maxX = bounds.size.x - GetMinX(bounds, cellGridRectangle);
            if(maxX > bounds.size.x) maxX = bounds.size.x;
            return maxX;
        }

        private int GetMaxY(BoundsInt bounds, Rect cellGridRectangle)
        {
            int maxY = bounds.size.y - GetMinY(bounds, cellGridRectangle);
            if(maxY > bounds.size.y) maxY = bounds.size.y;
            return maxY;
        }


        private void CreateCellDependingOnTile(TileBase tile)
        {
            if (tile == null)
            {
                GameObject.Instantiate(emptyCellPrefab, transform);
            }
            else
            {
                if (tile == forestTile)
                {
                    GameObject.Instantiate(forestCellPrefab, transform);
                }

                else if (tile == obstacleTile)
                {
                    GameObject.Instantiate(obstacleCellPrefab, transform);
                }

                else if (tile == fortressTile)
                {
                    GameObject.Instantiate(fortressCellPrefab, transform);
                }
                /*Ajouter un else if pour tous les nouveaux prefabs et tiles*/
                else
                {
                    Instantiate(emptyCellPrefab, transform);
                }
            }
        }

        private void DeleteGridChildren()
        {
            int childs = transform.childCount;
            for (int i = childs - 1; i >= 0; i--)
            {
                GameObject.DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}