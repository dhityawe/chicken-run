using UnityEngine;

public class MapBoundary : MonoBehaviour
{
    [Header("Boundary Limits")]
    public float leftBound = -13.75f;   // Leftmost position
    public float rightBound = 13.75f;  // Rightmost position
    public float topBound = Mathf.Infinity;     // Optional top limit (use Infinity if no restriction)
    public float bottomBound = -Mathf.Infinity; // Optional bottom limit (use -Infinity if no restriction)

    private Transform player;

    void Start()
    {
        player = transform; // Reference to the player's Transform
    }

    void Update()
    {
        // Clamp the player's position within the defined boundaries
        float clampedX = Mathf.Clamp(player.position.x, leftBound, rightBound);
        float clampedY = Mathf.Clamp(player.position.y, bottomBound, topBound);

        // Update the player's position
        player.position = new Vector3(clampedX, clampedY, player.position.z);
    }
}
