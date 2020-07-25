using System;
using UnityEngine;

public class EntraceHandle : MonoBehaviour
{
    [SerializeField]
    private string _key = string.Empty;
    [SerializeField]
    private int _sceneIndex = default;

    public static event Action<int, string> OnLeavingRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Get the Door key
        PlayerStats.EntranceKey = _key;

        //Stop player movement
        PlayerStats.CanMove = false;

        //Load next scene
        OnLeavingRoom?.Invoke(_sceneIndex, _key);
    }
}
