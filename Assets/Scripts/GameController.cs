using System.Collections;
using Actors;
using DefaultNamespace;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string startingSceneName = Constants.LEVEL_3_SCENE_NAME;
    private CharacterOwner currentPlayer;
    private HumanPlayer player1;
    private ComputerPlayer player2;
    void Start()
    {
        StartCoroutine(LoadLevel(startingSceneName));
    }
    
    private IEnumerator LoadLevel(string levelname)
    {

        if(!SceneManager.GetSceneByName(levelname).isLoaded)
            yield return SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelname));
        if (SceneManager.GetSceneByName(startingSceneName).isLoaded)
        {
            Debug.Log("Loading " + levelname);
            player1 = new HumanPlayer();
            player2 = new ComputerPlayer();
            
            object[] allies = FindObjectsOfType(typeof (Ally));
            foreach (var ally in allies)
            {
                var character = (Character) ally;
                player1.AddOwnedCharacter(character);
            }
            
            object[] enemies = FindObjectsOfType(typeof (Enemy));
            foreach (var enemy in enemies)
            {
                var character = (Character) enemy;
                player2.AddOwnedCharacter(character);
            }
            
            CharacterOwner.Players.Add(player1);
            CharacterOwner.Players.Add(player2);
            currentPlayer = player1;
            player1.OnTurnGiven();
        }
    }
    
    void Update()
    {
        if (currentPlayer == null) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPlayer = CharacterOwner.Players.Find(own => own is HumanPlayer);
            currentPlayer.OnTurnGiven();
        }
        if (currentPlayer.HasWon())
        {
            currentPlayer.Win();
            if(SceneManager.GetSceneByName(startingSceneName).isLoaded) 
                SceneManager.UnloadSceneAsync(startingSceneName);
        }
        if (currentPlayer.HasNoMoreMovableCharacters())
        {
            currentPlayer = currentPlayer.GiveTurnToNextPlayer();
            currentPlayer.OnTurnGiven();
        } 
        if (currentPlayer.HaveAllCharactersDied())
        {
            currentPlayer.Lose();
        }
        else
        {
            currentPlayer.Play();
        }

        
        for(int i = CharacterOwner.Players.Count-1; i >= 0; i--)
        {
            if (CharacterOwner.Players[i].HasLost)
            {
                if (currentPlayer == CharacterOwner.Players[i])
                {
                    currentPlayer = CharacterOwner.Players[i].GiveTurnToNextPlayer();
                }
                CharacterOwner.Players[i].RemoveFromPlayers();
                break;
            }
        }
    }
}
