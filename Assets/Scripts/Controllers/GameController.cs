using System;
using System.Collections;
using System.Collections.Generic;
using Actors;
using DefaultNamespace;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;

public class GameController : MonoBehaviour
{
    [SerializeField] private string startingSceneName = Constants.LEVEL_3_SCENE_NAME;
    private CharacterOwner currentPlayer;
    public Text WinDisplayText;
    void Start()
    {
        Finder.GameController = this;
        StartCoroutine(LoadLevel(startingSceneName));
    }
    
    private IEnumerator LoadLevel(string levelname)
    {

        if(!SceneManager.GetSceneByName(levelname).isLoaded)
            yield return SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelname));
    }
}
