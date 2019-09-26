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
    // Start is called before the first frame update
    [SerializeField] private string startingSceneName;
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
        if (levelname == "SampleScene")
        {
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
            if (currentPlayer != currentPlayer.GiveTurnToNextPlayer())
            {
                currentPlayer = currentPlayer.GiveTurnToNextPlayer();
                currentPlayer.OnTurnGiven();
            }
        }
        if (currentPlayer.HasNoMoreMovableCharacters())
        {
            if (currentPlayer != currentPlayer.GiveTurnToNextPlayer())
            {
                currentPlayer = currentPlayer.GiveTurnToNextPlayer();
                currentPlayer.OnTurnGiven();
            }
        }
        else if (currentPlayer.HasWon())
        {
            Debug.Log("Current player has won...");
            currentPlayer.Win();
        }
        else if (currentPlayer.HaveAllCharactersDied())
        {
            Debug.Log("Current player has lost...");
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
