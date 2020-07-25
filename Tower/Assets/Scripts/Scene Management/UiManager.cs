using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class UiManager : MonoBehaviour
{
    [SerializeField] private Animator _anim;
    [SerializeField] private Canvas _ui = default;

    private int _nextSceneIndex;

    #region ANIMATOR VARIABLES

    private static int FadeIn = Animator.StringToHash("FadeIn");
    private static int FadeOut = Animator.StringToHash("FadeOut");
    private static int OnPlay = Animator.StringToHash("OnPlay");
    private static int OnPause = Animator.StringToHash("OnPause");
    private static int OnMenu = Animator.StringToHash("OnMenu");
    private static int OnResume = Animator.StringToHash("OnResume");

    #endregion

    #region EVENTS

    public static event Action<int> OnLoadNextScene;
    public static event Action OnGamePause;
    public static event Action OnGameResume;

    #endregion

    void Awake()
    {
        _anim = GetComponent<Animator>();

        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);

        EntraceHandle.OnLeavingRoom += OnLeavingRoomHandle;
        SceneSetup.OnSceneLoaded += OnNextSceneLoaded;
    }

    #region SCENE MANAGEMENT

    private void OnLeavingRoomHandle(int sceneIndex, string key)
    {
        _nextSceneIndex = sceneIndex;
        _anim.SetTrigger(FadeOut);
    }

    private void OnNextSceneLoaded()
    {
        _anim.SetTrigger(FadeIn);
    }

    public void LoadNextScene()
    {
        OnLoadNextScene?.Invoke(_nextSceneIndex);
    }

    public void StopPlayerMovement()
    {
        PlayerStats.CanMove = false;
    }

    public void StartPlayerMovement()
    {
        PlayerStats.CanMove = true;
    }

    #endregion

    #region BUTTONS

    public void OnMenuClicked()
    {
        _nextSceneIndex = 1;
        _anim.SetTrigger(OnMenu);
        OnGameResume?.Invoke();
    }

    public void OnPlayClicked()
    {
        _nextSceneIndex = PlayerStats.LastSceneIndex;
        _anim.SetTrigger(OnPlay);
    }

    public void OnResumeCliked()
    {
        _anim.SetTrigger(OnResume);
        OnGameResume?.Invoke();
    }

    public void OnPauseClicked()
    {
        _anim.SetTrigger(OnPause);
        OnGamePause?.Invoke();
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    #endregion


    private void OnDestroy()
    {
        EntraceHandle.OnLeavingRoom -= OnLeavingRoomHandle;
        SceneSetup.OnSceneLoaded -= OnNextSceneLoaded;
    }
}
