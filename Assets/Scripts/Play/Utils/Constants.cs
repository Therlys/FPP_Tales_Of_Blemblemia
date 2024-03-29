﻿using UnityEngine;

namespace Game
{
    //Authors: Jérémie Bertrand & Mike Bédard
    public static class Constants
    {
        public const int PLAYER_MOVEMENT_RANGE = 3;
        public const int ENEMY_MOVEMENT_RANGE = 3;
        public const int PLAYER_ATTACK_RANGE = 1;
        public const int ENEMY_ATTACK_RANGE = 1;
        public const string LEVEL_1_SCENE_NAME = "Level1";
        public const string LEVEL_2_SCENE_NAME = "Level2";
        public const string LEVEL_3_SCENE_NAME = "ParabeneForest";
        public const string LEVEL_4_SCENE_NAME = "Level4";
        public const string GAME_CONTROLLER_TAG = "GameController";
        public const string GRID_CONTROLLER_TAG = "GridController";
        public const string PLAYER_NAME = "Leader of Allies";
        public const string AI_NAME = "Leader of Enemies";
        public const int DEFAULT_CHARACTER_HEALTH_POINTS = 6;
        public const int NUMBER_OF_MOVES_PER_CHARACTER_PER_TURN = 3;
        public const KeyCode SKIP_COMPUTER_TURN_KEY = KeyCode.Space;
    }
}