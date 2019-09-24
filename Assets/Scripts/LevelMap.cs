using System;

namespace Game
{
    public class LevelMap
    {
        private string[,] background;
        private string[,] foreground;
        private string[,] obstacles;
        private string[,] characters;

        public string[,] Background
        {
            get => background;
            set => background = value;
        }

        public string[,] Foreground
        {
            get => foreground;
            set => foreground = value;
        }

        public string[,] Obstacles
        {
            get => obstacles;
            set => obstacles = value;
        }

        public string[,] Characters
        {
            get => characters;
            set => characters = value;
        }
    }
}