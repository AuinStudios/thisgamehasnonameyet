using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanHover : MonoBehaviour
{
    private bool bol;
  public void hover()
  {
        bol = true;
  }
    public void exit()
    {
        bol = false;
    }
    public void Update()
    {
        if(bol == true)
        {
        Color col = gameObject.GetComponent<Image>().color;
        col.a = Mathf.Lerp(col.a, 0.5f, 5 * Time.deltaTime);
        gameObject.GetComponent<Image>().color = col;
        }
        else if( bol == false)
        {
            Color col = gameObject.GetComponent<Image>().color;
            col.a = Mathf.Lerp(col.a, 0, 5 * Time.deltaTime);
            gameObject.GetComponent<Image>().color = col;
        }
    }
}
