using UnityEngine;

public class PlayerStats : ScriptableObject
{
    #region JUMP

    public static Vector2 WallClimb = new Vector2(10,15);
    public static Vector2 WallLeap = new Vector2(15, 20);
    public static float ClimbTimeout = .4f;
    public static float MinJumpHeight = 4;
    public static float MaxjumpHeight = 5;
    public static float TimeToJumpApex = .5f;
    public static bool CanJump = false;

    #endregion

    #region MOVEMENT

    public static bool CanMove = true;
    public static string EntranceKey;
    public static Vector3 InitialPosition = Vector3.zero;

    public static float MaxFallSpeed = 20f;
    public static float WallSlideSpeedMax = 3f;
    public static float MoveSpeed = 8;
    public static float WallStickTime = .45f;

    public static float AccelerationTimeAirborn = .2f;
    public static float AccelerationTimeGrounded = .1f;

    #endregion

    #region HEALTH

    public static float MaxHitPoints = 10f;
    public static float HitPoints = MaxHitPoints;

    public static void TakeDamage(float damage)
    {
        HitPoints -= damage;

        if (HitPoints <=0)
        {
            //TODO:
            //Create death event
        }
    }

    public static void Heal(float healAmmount)
    {
        HitPoints += healAmmount;

        if (HitPoints > MaxHitPoints)
        {
            HitPoints = MaxHitPoints;
        }
    }

    #endregion

    #region WORLD

    public static int LastSceneIndex = 2;

    #endregion
}