using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageZone : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Character controller = collision.GetComponent<Character>();

        if (controller != null)
        {
            controller.ChangeHealth(-5);
        }
    }
}
