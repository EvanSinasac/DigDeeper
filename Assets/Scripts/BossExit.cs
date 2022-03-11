using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossExit : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
            PlayerPrefs.SetInt("currentHealth", 100);

            GameManager.instance.GetComponent<UIManager>().exitMine();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
            PlayerPrefs.SetInt("currentHealth", 100);

            GameManager.instance.GetComponent<UIManager>().exitMine();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
            PlayerPrefs.SetInt("currentHealth", 100);

            GameManager.instance.GetComponent<UIManager>().exitMine();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
        {
            PlayerPrefs.SetInt("currentLevel", 1);
            PlayerPrefs.SetInt("currentHealth", 100);

            GameManager.instance.GetComponent<UIManager>().exitMine();
        }
    }
}
