using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game
{
    public class CharacterOwner
    {
        public static List<CharacterOwner> Players = new List<CharacterOwner>();
        private List<Character> ownedCharacters = new List<Character>();
        private List<Character> playableCharacters = new List<Character>();
        private bool hasLost = false;

        public bool HasLost
        {
            get => hasLost;
            set => hasLost = value;
        }

        public CharacterOwner()
        {
            //Players.Add(this);
        }

        public void Play()
        {
            for(int i = 0; i < playableCharacters.Count; i++)
            {
                if (playableCharacters[i].IsPlayable && playableCharacters[i].MovesLeft <= 0)
                {
                    RemoveCharacterFromPlayableCharacters(playableCharacters[i]);
                }
            }

            for(int i = 0; i < ownedCharacters.Count; i++)
            {
                if (ownedCharacters[i].Died())
                {
                    RemoveOwnedCharacter(ownedCharacters[i]);
                }
            }
        }

        public virtual void Lose()
        {
            MakeOwnedCharactersUnplayable();
            hasLost = true;
        }

        public void RemoveFromPlayers()
        {
            Players.Remove(this);
        }

        public virtual void Win()
        {
            
        }
        
        public bool HasWon()
        {
            return Players.Contains(this) && Players.Count <= 1;
        }
        
        public CharacterOwner GiveTurnToNextPlayer()
        {
            MakeOwnedCharactersUnplayable();
            int nextPlayerIndex = Players.IndexOf(this) + 1;
            if (nextPlayerIndex >= Players.Count)
            {
                nextPlayerIndex = 0;
            }
            if(Players.ElementAt(nextPlayerIndex) != null)
            return Players.ElementAt(nextPlayerIndex);

            return this;
        }

        public void MakeOwnedCharactersUnplayable()
        {
            foreach (Character character in playableCharacters)
            {
                character.IsPlayable = false;
            }
        }

        public void MakeOwnedCharactersPlayable()
        {
            foreach (Character character in playableCharacters)
            {
                character.IsPlayable = true;
                character.MovesLeft = 3;
            }
        }

        public bool HasNoMoreMovableCharacters()
        {
            return playableCharacters.Count <= 0;
        }

        public bool HaveAllCharactersDied()
        {
            return ownedCharacters.Count <= 0;
        }

        public void OnTurnGiven()
        {
            foreach(Character character in ownedCharacters)
            playableCharacters.Add(character);
            MakeOwnedCharactersPlayable();
        }

        public void AddOwnedCharacter(Character character)
        {
            ownedCharacters.Add(character);
        }

        public void RemoveOwnedCharacter(Character character)
        {
            character.IsPlayable = false;
            if (playableCharacters.Contains(character))
                playableCharacters.Remove(character);
            if (ownedCharacters.Contains(character))
                ownedCharacters.Remove(character);
        }

        public void RemoveCharacterFromPlayableCharacters(Character character)
        {
            character.IsPlayable = false;
            playableCharacters.Remove(character);
        }
    }
}