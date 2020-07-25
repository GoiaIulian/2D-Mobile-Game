using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneSetup : MonoBehaviour
{
    public Transform[] EntrancePoints;
    public string[] EntranceKeys;

    public Dictionary<string, Vector3> _doors = new Dictionary<string, Vector3>();

    public static event Action OnSceneLoaded;

    private void Awake()
    {
        for (var i = 0; i < EntrancePoints.Length; i++)
        {
            _doors.Add(EntranceKeys[i], EntrancePoints[i].position);
        }

        if(!string.IsNullOrEmpty(PlayerStats.EntranceKey))
        {
            PlayerStats.InitialPosition = _doors[PlayerStats.EntranceKey];
        }

        EntraceHandle.OnLeavingRoom += SetEntranceKey;
        UiManager.OnGamePause += OnPause;
        UiManager.OnGameResume += OnResume;
    }

    void Start()
    {
        OnSceneLoaded?.Invoke();
    }

    private void SetEntranceKey(int sceneIndex, string key)
    {
        PlayerStats.InitialPosition = _doors[key];
    }

    private void OnPause()
    {
        Time.timeScale = 0f;
    }
    
    private void OnResume()
    {
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        EntraceHandle.OnLeavingRoom -= SetEntranceKey;
        UiManager.OnGamePause -= OnPause;
        UiManager.OnGameResume -= OnResume;
    }
}
