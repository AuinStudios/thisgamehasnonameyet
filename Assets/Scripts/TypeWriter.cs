using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    
    public int maxstrings;
    public float delay;
    public float speedofwrite = 4;
    public float startext;
    public string[] textest;
   // public bool ismaxornot = true ;
    private  int i = 0;
    private string current = "";
    private int addstring;
    public float backgroundphaseinout;

    // the dialogue changer and writer ---------------------------------------------------
    public IEnumerator showtext()
    {
     
       for(  int j  = 0; j < textest.Length; j++)
       {
           addstring = j;
            j = addstring;
           
           for (int iii = 0; iii <= textest[j].Length; iii++)
           {
                i = iii;
               current = textest[j].Substring(0, iii);
               gameObject.GetComponent<TextMeshProUGUI>().text = current;
               yield return new WaitForSeconds(speedofwrite);

           }
           
            int max = maxstrings;
          int add = addstring;
             yield return new WaitUntil(() =>  add != max);
            yield return new WaitForSeconds(delay);
       }
       
       
    }
    // delay for when the writer finishes and when it starts ----------------------------------------
    public IEnumerator waituntllfinishWriter()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
         
            yield return new WaitForSeconds(startext);
            StartCoroutine(showtext());
        }

        if (i >= textest[addstring].Length && addstring >= maxstrings)
        {
           gameObject.GetComponent<Animator>().SetBool("texton", false);
             yield return new WaitForSeconds(delay);
            addstring = 0;
        }
           
       
    }
    // Update --------------------------------------------------------------
    void Update()
    {
         
        // temp stuff-----------------------------------------------------
        if (addstring == 8)
        {
         gameObject.GetComponent<TextMeshProUGUI>().color = Random.ColorHSV() * 1f;
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Vector4(255,255,255,255);
        }
        // main stuff -----------------------------------------------        
         StartCoroutine(waituntllfinishWriter());

            if (Input.GetKeyDown(KeyCode.T)&& gameObject.CompareTag("StartWriter") )
            {

             gameObject.GetComponent<Animator>().SetBool("texton", true);
            
             
            }
            if(backgroundphaseinout == 1f)
            {
             
            
            }
        // background effect----------------------------------------------------
         GameObject.Find("TypeWriterBackground").GetComponent<Image>().fillAmount = backgroundphaseinout;
    }
}
