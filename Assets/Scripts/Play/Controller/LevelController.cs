using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    //Author: Mike Bédard
    public class LevelController : MonoBehaviour
    {
        private CharacterOwner currentPlayer;
        [NonSerialized] private readonly List<CharacterOwner> players = new List<CharacterOwner>();

        private void Start()
        {
            players.Clear();
            InitializePlayersAndCharacters();
            currentPlayer = players[0];
            players[0].OnTurnGiven();
        }
        
        protected void Update()
        {
            if(currentPlayer == null) throw new NullReferenceException("Current player is null!");
            CheckForComputerTurnSkip();
            CheckForCurrentPlayerWin();
            CheckForCurrentPlayerLoss();
            CheckForCurrentPlayerEndOfTurn();
            currentPlayer.Play();
        }
        
        private void InitializePlayersAndCharacters()
        {
            CharacterOwner player1 = new HumanPlayer();
            CharacterOwner player2 = new ComputerPlayer();
            GiveCharacters(GetCharactersOfType(typeof(Ally)),player1);
            GiveCharacters(GetCharactersOfType(typeof(Enemy)),player2);
            players.Add(player1);
            players.Add(player2);
        }

        private List<Character> GetCharactersOfType(Type type)
        {
            return ((Character[])FindObjectsOfType(type)).ToList();
        }
        
        private void GiveCharacters(List<Character> characters, CharacterOwner characterOwner)
        {
            foreach (Character character in characters)
            {
                characterOwner.AddOwnedCharacter(character);
            }
        }
        public void CheckForComputerTurnSkip()
        {
            if (Input.GetKeyDown(Constants.SKIP_COMPUTER_TURN_KEY))
            {
                currentPlayer = players.Find(player => player is HumanPlayer);
                currentPlayer.OnTurnGiven();
            }
        }

        public void CheckForCurrentPlayerEndOfTurn()
        {
            if (currentPlayer.HasNoMoreMovableCharacters())
            {
                GiveTurnToNextPlayer();
                currentPlayer.OnTurnGiven();
            } 
        }

        public void CheckForCurrentPlayerWin()
        {
            if (HasWon(currentPlayer))
            {
                currentPlayer.Win();
                Finder.GameController.WinDisplayText.text = currentPlayer.Name + " Won!";
                if(SceneManager.GetActiveScene().isLoaded) SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().name);
            }
        }

        public void CheckForCurrentPlayerLoss()
        {
            if (currentPlayer.HaveAllCharactersDied())
            {
                currentPlayer.Lose();
                CharacterOwner playerWhoLost = currentPlayer;
                GiveTurnToNextPlayer();
                players.Remove(playerWhoLost);
            }
        }
        
        public bool HasWon(CharacterOwner characterOwner)
        {
            return players.Contains(characterOwner) && players.Count <= 1;
        }
        
        public void GiveTurnToNextPlayer()
        {
            currentPlayer.MakeOwnedCharactersUnplayable();
            int nextPlayerIndex = players.IndexOf(currentPlayer) + 1;
            if (nextPlayerIndex >= players.Count)
            {
                nextPlayerIndex = 0;
            }
            if(players.ElementAt(nextPlayerIndex) != null)
                currentPlayer = players.ElementAt(nextPlayerIndex);
        }
    }
}