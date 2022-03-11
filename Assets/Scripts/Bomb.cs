using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomb : MonoBehaviour
{

    ParticleSystem particle;
    SpriteRenderer sr;

    public float timer = 3.0f;
    bool first = true;

    // Start is called before the first frame update
    void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 0)
        {
            if (first)
            {
                sr.enabled = false;
                particle.Play();
                timer = 1f;
                first = false;
            } else
            {
                particle.Stop();
                Destroy(gameObject);
            }
        }
        else
            timer -= Time.deltaTime;

    }
}
