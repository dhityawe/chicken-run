using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{

    public UiScore uiScore;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has the PlayerMovement script
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            // Add 1 to the score
            uiScore.AddScore(1);
            
            // Destroy the egg GameObject
            Destroy(gameObject);
        }
    }
}
