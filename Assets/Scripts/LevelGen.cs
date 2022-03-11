using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGen : MonoBehaviour
{
    [SerializeField] GameObject[] objects;

    private void Start()
    {
        int rand = Random.Range(0, objects.Length);
        Instantiate(objects[rand], new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        GameManager.instance.floorLevel.text = PlayerPrefs.GetInt("currentLevel").ToString();

    }
}
