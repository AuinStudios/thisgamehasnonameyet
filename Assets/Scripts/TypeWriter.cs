using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypeWriter : MonoBehaviour
{
    public TextMeshProUGUI Text;

    //public bool canactiveornot;
    // THIS INT IS VERY IMPORTANT IF ITS NOT THE SAME AS THE STRING SIZE THEN IT WILL NOT WORK
    
    public int maxstrings;
    // -----------------------------------------------------------------------
    
    public float delay;
    public float speedofwrite = 4;
    public float startext;
    public string[] Dialogue;

    private int i = 0;
    private string current = "";
    private int addstring;
    private static bool canactiveornot = true;
    private static int MakeTriggerNotMutlple = 1;

    // the dialogue changer and writer ---------------------------------------------------
    
    public IEnumerator showtext()
    {
       for(  int j  = 0; j < Dialogue.Length; j++)
       {
          addstring = j;
          j = addstring;
          
          for (int iii = 0; iii <= Dialogue[j].Length; iii++)
          {
              i = iii;
              current = Dialogue[j].Substring(0, iii);
              Text.text = current;
              yield return new WaitForSeconds(speedofwrite);
          }
          
          int max = maxstrings;
          int add = addstring;

          yield return new WaitUntil(() =>  add != max);
          yield return new WaitForSeconds(delay);
       }
    }
   
    // start  typewriter ---------------------------------------------

    public IEnumerator startfunctionbool()
    {
        for(int textmutlplestarterfixer = 0; textmutlplestarterfixer <= MakeTriggerNotMutlple; textmutlplestarterfixer++)
        {
            if (canactiveornot == true)
            {
                yield return (null);

                StartCoroutine(startfunction());
            }
        
            yield return new WaitUntil(() => textmutlplestarterfixer == 0);
        }
    }

    public IEnumerator startfunction()
    {
        canactiveornot = false;
        
        yield return new WaitForSeconds(0.5f);
        
        Text.GetComponent<Animator>().SetBool("texton", true);
        
        yield return new WaitForSeconds(startext);
        
        StartCoroutine(showtext());
    }
   
    // delay for when the writer finishes and when it starts ----------------------------------------
    public IEnumerator waituntllfinishWriter()
    {
        if (i >= Dialogue[addstring].Length && addstring >= maxstrings)
        {
            Text.GetComponent<Animator>().SetBool("texton", false);
            
            yield return new WaitForSeconds(delay);
            
            addstring = 0;
            
            yield return new WaitForSeconds(1.4f);
            
            //maketextnoglitchasmuch = false;
            canactiveornot = true;
            // GameObject.Find("subscript").GetComponent<textwriterstartscript>().textwriterstop = true;
            // gameObject.GetComponent<TypeWriter>().enabled = false;
        }
           
       
    }
    // main stuff--------------------------------------------------------------

    //public  void OnTriggerEnter(Collider col)
    //   {
    //       if (col.gameObject.CompareTag("TypeWriterStarter"))
    //       {
    //           StartCoroutine(startfunction());
    //       }
    //   }
    
    void Update()
    {
        // functions and stuff -----------------------------------------------        
        StartCoroutine(waituntllfinishWriter());
    }


 
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Text.GetComponent<Animator>().GetBool("texton") == false)
            {
                if(canactiveornot == true )
                {
                    StartCoroutine(startfunctionbool());
                }
                else
                {
                    StopCoroutine(startfunctionbool());
                }
            }
            else
            {
                canactiveornot = false;
            }
        }
    }
}
