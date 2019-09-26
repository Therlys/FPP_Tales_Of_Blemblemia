using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Actors;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private string startingSceneName = "";
    private CharacterOwner currentPlayer;
    private HumanPlayer player1;
    private ComputerPlayer player2;
    void Start()
    {
        StartCoroutine(LoadLevel(startingSceneName));
    }
    
    private IEnumerator LoadLevel(string levelname)
    {
        //TODO : Show Loading Screen
        if(!SceneManager.GetSceneByName(levelname).isLoaded)
            yield return SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelname));
        if (SceneManager.GetSceneByName(startingSceneName).isLoaded && startingSceneName == "SampleScene")
        {
            Debug.Log("Loading the fucking level...");
            player1 = new HumanPlayer();
            player2 = new ComputerPlayer();
            player1.AddOwnedCharacter(GameObject.Find("Franklem").GetComponent<Franklem>());
            player2.AddOwnedCharacter(GameObject.Find("Pleblem").GetComponent<Pleblem>());
            player2.AddOwnedCharacter(GameObject.Find("Pleblem (1)").GetComponent<Pleblem>());
            player2.AddOwnedCharacter(GameObject.Find("Pleblem (2)").GetComponent<Pleblem>());
            player2.AddOwnedCharacter(GameObject.Find("Pleblem (3)").GetComponent<Pleblem>());
            player2.AddOwnedCharacter(GameObject.Find("Pleblem (4)").GetComponent<Pleblem>());
            CharacterOwner.Players.Add(player1);
            CharacterOwner.Players.Add(player2);
            currentPlayer = player1;
            player1.OnTurnGiven();
        }
        
        //TODO : Hide Loading Screen
    }

    // Update is called once per frame
    void Update()
    {
        
        if (currentPlayer == null) return;

        if (Input.GetKeyUp(KeyCode.Space))
        {
            currentPlayer = CharacterOwner.Players.Find(own => own is HumanPlayer);//currentPlayer.GiveTurnToNextPlayer();
            currentPlayer.OnTurnGiven();
        }
        if (currentPlayer.HasNoMoreMovableCharacters())
        {
            currentPlayer = currentPlayer.GiveTurnToNextPlayer();
            currentPlayer.OnTurnGiven();
        }
        else if (currentPlayer.HasWon())
        {
            currentPlayer.Win();
            if(SceneManager.GetSceneByName(startingSceneName).isLoaded)
            SceneManager.UnloadSceneAsync(startingSceneName);
        }
        else if (currentPlayer.HaveAllCharactersDied())
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
                    CharacterOwner.Players[i].RemoveFromPlayers();
                    break;
                }
            }
        }
    }
}
