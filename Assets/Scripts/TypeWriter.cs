using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TypeWriter : MonoBehaviour
{

    public float speedofwrite = 4;
    private bool cantext;
    public string textest;
    private int i = 0;
    private string current = "";
    // Update is called once per frame
    public void start()
    {
     
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
    void Update()
    {
        gameObject.GetComponent<TextMeshProUGUI>().color = Random.ColorHSV();

        if (Input.GetKeyDown(KeyCode.T) && cantext == false)
        {
            cantext = true;
            gameObject.GetComponent<Animator>().SetBool("texton", true);
           
              StartCoroutine(showtext());
            
           
        }
        if(i >= textest.Length)
        {
            gameObject.GetComponent<Animator>().SetBool("texton", false);
            cantext = false;
        }
    }
}
