using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerStats.CanJump = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerStats.CanJump = false;
    }
}
