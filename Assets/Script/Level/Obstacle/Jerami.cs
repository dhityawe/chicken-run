using UnityEngine;

public class Jerami : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the PlayerMovement component
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        
        if (playerMovement != null)
        {
            // Access and modify the moveSpeed variable
            playerMovement.moveSpeed -= 2;
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        // Check if the object exiting the trigger has the PlayerMovement component
        PlayerMovement playerMovement = other.gameObject.GetComponent<PlayerMovement>();
        
        if (playerMovement != null)
        {
            // Access and modify the moveSpeed variable
            playerMovement.moveSpeed += 2;
        }
    }
}
