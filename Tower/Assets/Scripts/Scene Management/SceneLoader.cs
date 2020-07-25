using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    //Index of the active scene
    private static int _currentScene = 1;
    

    private void Awake()
    {
        UiManager.OnLoadNextScene += LoadRoom;
    }

    private void LoadRoom(int sceneIndex)
    {
        if (_currentScene != 0)
        {
            SceneManager.UnloadSceneAsync(_currentScene);
        }
        _currentScene = sceneIndex;
        SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);


        //remove after implementing checkpoint system
        if (_currentScene != 1)
        {
            PlayerStats.LastSceneIndex = sceneIndex;
        }
    }


    private void OnDestroy()
    {
        UiManager.OnLoadNextScene -= LoadRoom;
    }
}
