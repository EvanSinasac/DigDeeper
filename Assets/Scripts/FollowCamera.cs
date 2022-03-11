using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Matthew Pizzo - 
 * Last Worked On: January 17, 2021
 * */

public class FollowCamera : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset = new Vector3(0,0,-1);
    [SerializeField] bool playerFound = false;

    // Start is called before the first frame update
    void Update()
    {
        if (playerFound == false)
        {
            target = GameObject.FindGameObjectWithTag("Player");
            if (target != null)
                playerFound = true;
        }
    }

    private void FixedUpdate()
    {
        if (target) {
            transform.position = new Vector3(target.transform.position.x + offset.x, target.transform.position.y + offset.y, offset.z);
        }
    }
}
