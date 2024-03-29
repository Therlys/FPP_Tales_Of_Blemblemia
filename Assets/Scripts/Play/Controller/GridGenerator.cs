﻿using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    //Author: Mike Bédard
    public class GridGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private GameObject emptyCellPrefab = null;
        [SerializeField] private GameObject forestCellPrefab = null;
        [SerializeField] private GameObject obstacleCellPrefab = null;
        [SerializeField] private GameObject fortressCellPrefab = null;
        
        [Header("Tiles")]
        [SerializeField] private TileBase forestTile = null;
        [SerializeField] private TileBase obstacleTile = null;
        [SerializeField] private TileBase fortressTile = null;
        
        [Header("Tilemap")]
        [SerializeField] private Tilemap tilemap = null;

        public void CreateGridCells()
        {
            BoundsInt bounds = tilemap.cellBounds;
            TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
            Rect cellGridRectangle = GetComponent<RectTransform>().rect;
            
            int minX = GetMinX(bounds, cellGridRectangle);
            int minY = GetMinY(bounds, cellGridRectangle);
            int maxX = GetMaxX(bounds, cellGridRectangle);
            int maxY = GetMaxY(bounds, cellGridRectangle);
            
            GetComponent<RectTransform>().sizeDelta = new Vector2(maxX - minX,maxY - minY);
            
            for (int y = maxY - 1; y >= minY; y--) 
            {
                for (int x = minX; x < maxX; x++) 
                {
                    InstantiateCellPrefabFrom(allTiles[x + y * bounds.size.x]);
                }
            }
        }

        private int GetMinX(BoundsInt bounds, Rect cellGridRectangle)
        {
            int minX = (bounds.size.x - (int)cellGridRectangle.width)/2;
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


        private void InstantiateCellPrefabFrom(TileBase tile)
        {
            var spawningPrefab = emptyCellPrefab;
            if (tile == forestTile)
            {
                spawningPrefab = forestCellPrefab;
            }
            else if (tile == obstacleTile)
            {
                spawningPrefab = obstacleCellPrefab;
            }
            else if (tile == fortressTile)
            {
                spawningPrefab = fortressCellPrefab;
            }
            Instantiate(spawningPrefab, transform);
        }

        public void ClearGrid()
        {
            for (int i = transform.childCount - 1; i >= 0; i--) 
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        public void GenerateGrid()
        {
            ClearGrid();
            CreateGridCells();
        }
    }
}