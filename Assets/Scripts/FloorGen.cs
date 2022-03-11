using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGen : MonoBehaviour
{
    [SerializeField] GameObject[] objects;
    [SerializeField] GameObject[] restFloors;

    private void Start()
    {
        int floorLevel = PlayerPrefs.GetInt("currentLevel");
        int rand;

        if (floorLevel % 10 != 0) {

            if (floorLevel < 10)
            {
                Debug.Log("Floor Level (<10): " + floorLevel);
                rand = Random.Range(0, 12);
            } else if (floorLevel < 20)
            {
                rand = Random.Range(0, 22);
                Debug.Log("Floor Level (<20): " + floorLevel);
            } else if (floorLevel < 30)
            {
                rand = Random.Range(12, 31);
                Debug.Log("Floor Level (<30): " + floorLevel);
            } else if (floorLevel < 40)
            {
                rand = Random.Range(22, 42);
                Debug.Log("Floor Level (<40): " + floorLevel);
            }else if (floorLevel == 51)
            {
                rand = 43;
            } else //etc. past level 40
            {
                rand = Random.Range(22, 42);
                Debug.Log("Floor Level (else): " + floorLevel);
            }

            
            Instantiate(objects[rand], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            GameManager.instance.floorLevel.text = PlayerPrefs.GetInt("currentLevel").ToString();
        }
        else {
            rand = Random.Range(0, restFloors.Length);
            Instantiate(restFloors[rand], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            GameManager.instance.floorLevel.text = PlayerPrefs.GetInt("currentLevel").ToString();
        }

    }
}
