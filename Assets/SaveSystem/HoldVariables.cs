using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class HoldVariables
{
    public float sens;
    public float fov;
    public float brightness;
    public int graphicisvalue;
    public float MasterVolume;
    public HoldVariables()
    {
        MasterVolume = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(2).GetChild(0).transform.GetComponent<Slider>().value;
        graphicisvalue = GameObject.Find("Main Camera").transform.GetComponent<Menu>().graphicisvalue;
        brightness = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(2).transform.GetComponent<Slider>().value;
        sens = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(0).transform.GetComponent<Slider>().value;
        fov = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(1).transform.GetComponent<Slider>().value;
    }
}
