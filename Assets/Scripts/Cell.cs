using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game
{
    public class Cell : MonoBehaviour
    {
        private Tile tile;
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(OnCellClick);
        }

        private void OnCellClick()
        {
            
        }
        
    }
}