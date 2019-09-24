using System.Collections;
using System.Collections.Generic;
using Game;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Tilemap level3Tilemap;
    [SerializeField] private Tilemap level1Tilemap;
    void Start()
    {
        StartCoroutine(LoadLevel("Level3"));
    }
    
    private IEnumerator LoadLevel(string levelname)
    {
        //TODO : Show Loading Screen
        if(!SceneManager.GetSceneByName(levelname).isLoaded)
            yield return SceneManager.LoadSceneAsync(levelname, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByName(levelname));
        Tilemap tilemapToLoad;
        if (levelname == "Level1")
        {
            tilemapToLoad = level1Tilemap;
        }
        else
        {
            tilemapToLoad = level3Tilemap;
        }
        Finder.CellGridCreator.CreateCellsDependingOnTilemap(tilemapToLoad);
        //TODO : Hide Loading Screen
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
