using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ValueForAnimationTypeWriter : MonoBehaviour
{
    public float fillinvalue;

    public void Update()
    {
         GameObject.Find("TypeWriterBackground").GetComponent<Image>().fillAmount = gameObject.GetComponent<ValueForAnimationTypeWriter>().fillinvalue;
    }
   
}
