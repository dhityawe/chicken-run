using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public bool isGoldenEgg = false;
    public UiScore uiScore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has the PlayerMovement script
        if (collision.GetComponent<PlayerMovement>() != null && !isGoldenEgg)
        {
            // Add 1 to the score
            uiScore.AddScore(1);
            
            // Destroy the egg GameObject
            Destroy(gameObject);
        }
        else 
        {
            uiScore.AddHealth(1);

            Destroy(gameObject);
        }
    }
}
