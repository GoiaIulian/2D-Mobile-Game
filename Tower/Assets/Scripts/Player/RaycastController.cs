using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class RaycastController : MonoBehaviour
{
    public RaycastOrigins raycastOrigins;

    [HideInInspector]
    public float horizontalRaySpacing;
    [HideInInspector]
    public float verticalRaySpacing;


    public int horizontalRayCount = 4;
    public int verticalRayCount = 4;

    public LayerMask collisionMask;

    public const float skinWidth = .01f;

    [HideInInspector]
    public BoxCollider2D _collider;

    public virtual void Start()
    {
        
        CalculateRaySpacing();
    }

    public virtual void Awake()
    {
        _collider = GetComponent<BoxCollider2D>();
    }

    public void UpdateRaycastOrigins()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    public void CalculateRaySpacing()
    {
        Bounds bounds = _collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    public struct RaycastOrigins
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }
}
