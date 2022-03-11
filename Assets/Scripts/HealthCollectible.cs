using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Character controller = collision.GetComponent<Character>();
        
        if (controller != null)
        {
            controller.ChangeHealth(30);
            Destroy(gameObject);
        }
    }
}
