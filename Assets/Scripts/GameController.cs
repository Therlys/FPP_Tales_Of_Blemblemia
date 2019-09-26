using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] private string startingSceneName = "";
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
}
