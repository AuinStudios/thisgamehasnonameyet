using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{
    public GameObject reset;
    public float speedofwrite = 4;
    public static float delay;
    private bool cantext;
    public bool delayenable;
    public string textest;
    public float caplock;
    private int i = 0;
    private string current = "";


    public void Start()
    {
        delayenable = true;
        if (!gameObject.CompareTag("StartWriter"))
        {
            
          transform.GetComponent<TypeWriter>().StartCoroutine(showtext());
        }
      
    }
    public IEnumerator showtext()
    {
        for(i = 0 ; i <= textest.Length; i++)
        {
            current = textest.Substring(0, i);
                gameObject.GetComponent<TextMeshProUGUI>().text = current;
            Debug.Log("test");
            yield return new WaitForSeconds(speedofwrite);
        }
    }
     public IEnumerator testthing()
     {
      for (int a = 0; a < transform.childCount; a++)
      {
         transform.GetChild(a).gameObject.SetActive(true);
            transform.GetChild(a).gameObject.GetComponent<TypeWriter>().enabled = true;
            
            transform.GetComponentInParent<TypeWriter>().enabled = false;
            
            transform.GetComponentInParent<TextMeshProUGUI>().SetText("");
            
            delay = 0;
      }
      yield return new WaitUntil(() => delay >= caplock);
     }

    void Update()
    {
        //gameObject.GetComponent<TextMeshProUGUI>().color = Random.ColorHSV();
     
        if (Input.GetKeyDown(KeyCode.T)&& gameObject.CompareTag("StartWriter") && cantext == false)
        {
            cantext = true;
            
            if (gameObject.CompareTag("StartWriter"))
            {
             gameObject.GetComponent<Animator>().SetBool("texton", true);
               
            }
              StartCoroutine(showtext());
        }
        if(delay >= caplock)
        {

          StartCoroutine(testthing());
        }
        if(delayenable == true)
        {
            delay += Time.deltaTime * 2;
        }
        if(i >= textest.Length  )
        {
            if (gameObject.CompareTag("StartWriter"))
            {
                delayenable = true;
            }

            
            
         if (gameObject.CompareTag("EndWriter"))
            {
                GameObject.Find("textwriter").GetComponent<Animator>().SetBool("texton", false);
                reset.GetComponent<TypeWriter>().enabled = true;
                GameObject.FindGameObjectWithTag("MiddleWriter").GetComponent<Transform>().gameObject.SetActive(false);
                gameObject.GetComponent<TypeWriter>().enabled = false;
                delayenable = false;
                delay = 0;
                gameObject.SetActive(false);
            }
               
            cantext = false;
        }
    }
}
