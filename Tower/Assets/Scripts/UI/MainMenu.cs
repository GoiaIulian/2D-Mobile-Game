using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class MainMenu : MonoBehaviour
{
    public void LoadLastScene()
    {
        SceneManager.LoadSceneAsync(1);
    }

    #region BUTTONS

    public void OnPlayClicked()
    {
        LoadLastScene();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    #endregion
}
