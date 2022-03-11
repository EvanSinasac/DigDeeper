using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (this.gameObject.activeInHierarchy == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //this.gameObject.SetActive(false);
            }
        }
    }
}
