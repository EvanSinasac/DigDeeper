using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{

    public string areaToLoad;
    public string areaTransitionName;
    public Animator transition;
    public float transitionTime = 1f;

    private void Start()
    {
        //areaToLoad = "ToolsSandbox";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (PlayerPrefs.HasKey("maxHealth"))
            {
                //PlayerPrefs.DeleteAll();
                PlayerPrefs.SetInt("maxHealth", 100);
                PlayerPrefs.SetInt("currentHealth", 100);
                int exitLevel = PlayerPrefs.GetInt("exitLevel");
                Debug.Log("Exit Level: " + PlayerPrefs.GetInt("exitLevel"));
                if (exitLevel > 0)
                {
                    Debug.Log("Exit Level: "+PlayerPrefs.GetInt("exitLevel"));
                    PlayerPrefs.SetInt("currentLevel", PlayerPrefs.GetInt("exitLevel"));
                    PlayerPrefs.SetInt("exitLevel", 0);
                }
                else 
                {
                    PlayerPrefs.SetInt("currentLevel", 1);
                }
                PlayerPrefs.Save();
                StartCoroutine(LoadLevel(areaToLoad));
            }
            else 
            {
                PlayerPrefs.SetInt("maxHealth", 100);
                PlayerPrefs.SetInt("currentHealth", 100);
                PlayerPrefs.SetInt("currentLevel", 1);
                PlayerPrefs.Save();
            }

        }
    }

    IEnumerator LoadLevel(string areaToLoad) {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(areaToLoad);
    }

}
