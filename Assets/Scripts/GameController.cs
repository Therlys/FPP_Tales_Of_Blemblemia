using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private string startingSceneName;
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
        //TODO : Hide Loading Screen
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
