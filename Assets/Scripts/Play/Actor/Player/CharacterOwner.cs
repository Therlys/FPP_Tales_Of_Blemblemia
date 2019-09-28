using System.Collections.Generic;

namespace Game
{
    //Author: Mike Bédard
    public class CharacterOwner
    {
        public string name = "";
        private readonly List<Character> ownedCharacters = new List<Character>();
        private readonly List<Character> playableCharacters = new List<Character>();
        private bool hasLost = false;

        public string Name => name;

        public bool HasLost
        {
            get => hasLost;
            set => hasLost = value;
        }

        protected CharacterOwner(string name)
        {
            this.name = name;
        }

        public void Play()
        {
            for(int i = 0; i < playableCharacters.Count; i++)
            {
                if (!playableCharacters[i].CanMove)
                {
                    RemoveCharacterFromPlayableCharacters(playableCharacters[i]);
                }
            }

            for(int i = 0; i < ownedCharacters.Count; i++)
            {
                if (ownedCharacters[i].IsDead)
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

        public virtual void Win()
        {
            
        }

        public void MakeOwnedCharactersUnplayable()
        {
            foreach (Character character in playableCharacters)
            {
                character.CanPlay = false;
            }
        }

        public void MakeOwnedCharactersPlayable()
        {
            foreach (Character character in playableCharacters)
            {
                character.CanPlay = true;
                character.ResetNumberOfMovesLeft();
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
            foreach(Character character in ownedCharacters) playableCharacters.Add(character);
            MakeOwnedCharactersPlayable();
        }

        public void AddOwnedCharacter(Character character)
        {
            ownedCharacters.Add(character);
        }

        public void RemoveOwnedCharacter(Character character)
        {
            character.CanPlay = false;
            if (playableCharacters.Contains(character))
                playableCharacters.Remove(character);
            if (ownedCharacters.Contains(character))
                ownedCharacters.Remove(character);
        }

        public void RemoveCharacterFromPlayableCharacters(Character character)
        {
            character.CanPlay = false;
            playableCharacters.Remove(character);
        }
    }
}