using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    
    public int maxstrings;
    public float waitnoob;
    public float speedofwrite = 4;
    public string[] textest;
    public bool ismaxornot ;
    public  int i = 0;
    private string current = "";
    public int addstring;

    public void Start()
    {
        
        if (!gameObject.CompareTag("StartWriter"))
        {
            
          transform.GetComponent<TypeWriter>().StartCoroutine(showtext());
        }
      
    }
    public IEnumerator showtext()
    {
     
       for(  int j  = 0; j < textest.Length; j++)
       {
           addstring = j;
             
           
           for (int iii = 0; iii <= textest[j].Length; iii++)
           {
               i++;
               current = textest[j].Substring(0, iii);
               gameObject.GetComponent<TextMeshProUGUI>().text = current;
               yield return new WaitForSeconds(speedofwrite);

           }
          
            int max = maxstrings;
          int add = addstring;
             yield return new WaitUntil(() => ismaxornot == true && add != max);
            yield return new WaitForSeconds(waitnoob);
       }
       
       
    }
    

    void Update()
    {
        if (addstring == 8)
        {
         gameObject.GetComponent<TextMeshProUGUI>().color = Random.ColorHSV();
        }
        else
        {
            gameObject.GetComponent<TextMeshProUGUI>().color = new Vector4(255,255,255,255);
        }
         if (addstring >= maxstrings)
         {
            gameObject.GetComponent<Animator>().SetBool("texton", false);
        }
        if (Input.GetKeyDown(KeyCode.T)&& gameObject.CompareTag("StartWriter") )
        {
         
             StartCoroutine(showtext());
            if (gameObject.CompareTag("StartWriter"))
            {
             gameObject.GetComponent<Animator>().SetBool("texton", true);
               
            }
             
        }

       
         if (i >= textest.Length )
         {
            // cantext = false;
           
            ismaxornot = true;

         }
    }
}
