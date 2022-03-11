using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickToSaveVE : MonoBehaviour
{
    bool textOn;
    float timeToOff;
    public GameObject GameSaved;

    private void Start()
    {
        textOn = false;
        GameSaved.SetActive(false);
    }

    private void OnMouseDown()
    {
        GameManager.instance.SaveInventory();
        GameManager.instance.SaveStorage();
        textOn = true;
        GameSaved.SetActive(true);
        timeToOff = 3f;
    }

    private void Update()
    {
        if (textOn)
        {
            if (timeToOff <= 0)
            {
                GameSaved.SetActive(false);
                textOn = false;
            }
            else
                timeToOff -= Time.deltaTime;
        }
    }

}
