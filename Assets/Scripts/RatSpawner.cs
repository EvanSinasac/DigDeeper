using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Evan Sinasac - 104571345
 * Last Worked On: February 9, 2021
 * RatSpawner to spawn rats
 * */

public class RatSpawner : MonoBehaviour
{

    public GameObject ratPrefab;
    //public bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(ratPrefab, transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
