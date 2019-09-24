using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game
{
    public class Level
    {
        public List<Level> levels = new List<Level>();
        
        [SerializeField] private String name;
        [SerializeField] private Tilemap background;
        [SerializeField] private Tilemap foreground;
        [SerializeField] private Tilemap obstacles;
        [SerializeField] private Tilemap characters;
        private int width;
        private int height;

        public int Width
        {
            get => width;
            set => width = value;
        }

        public int Height
        {
            get => height;
            set => height = value;
        }

        public Level()
        {
            levels.Add(this);
        }
        
        public string Name
        {
            get => name;
            set => name = value;
        }

        public Tilemap Background
        {
            get => background;
            private set => background = value;
        }
        
        public Tilemap Foreground
        {
            get => foreground;
            private set => foreground = value;
        }
        
        public Tilemap Obstacles
        {
            get => obstacles;
            private set => obstacles = value;
        }
        
        public Tilemap Characters
        {
            get => characters;
            private set => characters = value;
        }
    }
}