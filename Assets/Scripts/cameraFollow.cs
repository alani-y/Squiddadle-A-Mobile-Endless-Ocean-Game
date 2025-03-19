using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Assign the player GameObject in the Inspector
    public float smoothSpeed = 0.125f; // Adjust for smooth camera movement
    public Vector2 minBounds;
    public Vector2 maxBounds;


    private float cameraHalfWidth;
    private float cameraHalfHeight;

    void Start()
    {
        // get the camera size in world units
        Camera cam = Camera.main;
        cameraHalfHeight = cam.orthographicSize;
        cameraHalfWidth = cam.aspect * cameraHalfHeight;

        SpriteRenderer background = GameObject.Find("Ocean").GetComponent<SpriteRenderer>();
        minBounds = background.bounds.min;
        maxBounds = background.bounds.max;
    }

    void LateUpdate()
    {
        if (player == null) return;

        // target position following the player
        Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);

        // clamp the position to prevent going out of bounds
        float clampedX = targetPosition.x;
        float clampedY = Mathf.Clamp(targetPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        // apply the clamped position
        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}
