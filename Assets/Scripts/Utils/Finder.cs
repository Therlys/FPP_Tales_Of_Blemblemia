using UnityEngine;

namespace Utils
{
    public static class Finder
    {
        private const string GAME_CONTROLLER_TAG = "GameController";
        private static GameController gameController;
        
        private const string GRID_CONTROLLER_TAG = "GridController";
        private static GridController gridController;

        public static GameController GameController
        {
            get
            {
                if (gameController == null)
                    gameController = GameObject.FindWithTag(GAME_CONTROLLER_TAG).GetComponent<GameController>();
                return gameController;
            }
        }
        
        public static GridController GridController
        {
            get
            {
                Debug.Log(GameObject.FindWithTag(GRID_CONTROLLER_TAG).GetComponent<GridController>());
                if (gridController == null)
                    gridController = GameObject.FindWithTag(GRID_CONTROLLER_TAG).GetComponent<GridController>();
                return gridController;
            }
        }
    }
}