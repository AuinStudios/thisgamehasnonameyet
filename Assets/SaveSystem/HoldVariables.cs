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
    public float[] rot;
    public KeyCode[] keys;
    public HoldVariables()
    {
       keys = new KeyCode[7];
       keys = Camera.main.GetComponent<Menu>().savekey;
      
        rot = new float[3];
        rot[0] = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(0).transform.localEulerAngles.x;
        rot[1] = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(0).transform.localEulerAngles.y;
        rot[2] = GameObject.Find("Canvas").transform.GetChild(2).transform.GetChild(0).transform.localEulerAngles.z;
        MasterVolume = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(2).GetChild(0).transform.GetComponent<Slider>().value;
        graphicisvalue = GameObject.Find("Main Camera").transform.GetComponent<Menu>().GraphicisValue;
        brightness = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(2).transform.GetComponent<Slider>().value;
        sens = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(0).transform.GetComponent<Slider>().value;
        fov = GameObject.Find("Canvas").transform.GetChild(0).transform.GetChild(4).GetChild(0).GetChild(1).transform.GetComponent<Slider>().value;
    }
}
