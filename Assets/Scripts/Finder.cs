using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game
{
    public static class Finder
    {
        private const string GAME_CONTROLLER_TAG = "GameController";
        private const string CELL_GRID_TAG = "CellGrid";
        private static GridController gridController;
        private static List<LevelCellGrid> levelCellGrids = new List<LevelCellGrid>();

        public static List<LevelCellGrid> LevelCellGrids
        {
            get => levelCellGrids;
            set => levelCellGrids = value;
        }

        /*public static CellGridCreator CellGridCreator
        {
            get
            {
                if (cellGridCreator == null)
                    cellGridCreator = GameObject.Find(CELL_GRID_TAG).GetComponent<CellGridCreator>();
                return cellGridCreator;
            }
        }*/

        public static GridController GridController
        {
            get
            {
                if (gridController == null)
                    gridController = GameObject.FindWithTag(GAME_CONTROLLER_TAG).GetComponent<GridController>();
                return gridController;
            }
        }
    }
}