using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanHover : MonoBehaviour
{
    public bool bol;
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
        if(bol == true && gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta.x < 159f)
        {
           Vector2 widthchange = new Vector2(160, gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta.y);
           gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = Vector2.Lerp(gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta, widthchange, 3.5f * Time.deltaTime);
            
          Color col = gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().color;
          col.a = Mathf.Lerp(col.a, 0.5f, 3.5f * Time.deltaTime);
          gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().color = col;
        }
        else if( bol == false && gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta.x > 0.1f)
        {
           Vector2 widthchange = new Vector2(0, gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta.y);
           gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta = Vector2.Lerp(gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().rectTransform.sizeDelta, widthchange, 3.5f * Time.deltaTime);
            
          Color col = gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().color;
          col.a = Mathf.Lerp(col.a, 0, 3.5f * Time.deltaTime);
          gameObject.GetComponent<Image>().transform.GetChild(0).GetComponent<Image>().color = col;
        }
    }
}
