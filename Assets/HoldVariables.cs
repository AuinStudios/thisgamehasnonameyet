using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HoldVariables
{
    public float sens;
    public float fov;
    public HoldVariables()
    {
        sens = GameObject.Find("sensitivity slider").transform.GetComponent<Slider>().value;
        fov = GameObject.Find("Fov Slider").transform.GetComponent<Slider>().value;
    }
}
