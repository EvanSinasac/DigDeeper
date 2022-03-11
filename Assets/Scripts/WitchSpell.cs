using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 23, 2021
 * WitchSpell for the Projectile launched by the Witch
 * 
 * */

public class WitchSpell : MonoBehaviour
{

    Rigidbody2D rb;
    public float despawn = 10f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (despawn < 0)
        {
            Destroy(gameObject);
        }
        despawn -= Time.deltaTime;
    }

    public void Launch(Vector2 direction, float force)
    {
        rb.AddForce(direction * force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile Collision with " + collision.gameObject);
        Destroy(gameObject);
    }
}
