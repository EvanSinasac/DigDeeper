using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Evan Sinasac - 104571345 
 * Last Worked On: January 21, 2021
 * */

public class HealthBar : MonoBehaviour
{

    //public Transform bar;  //first solution, didn't really like
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    private void Start()
    {
        //bar = transform.Find("Bar");
    }

    /*public void SetSize (float sizeNormalized)
    {
        //bar.localScale = new Vector3(sizeNormalized, 1f);
    }
    */
    public void SetMaxHealth (int health)
    {
        slider.maxValue = health;
        slider.value = health;

        fill.color = gradient.Evaluate(1f);
    }

    public void SetHealth (int health)
    {
        slider.value = health;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

}
