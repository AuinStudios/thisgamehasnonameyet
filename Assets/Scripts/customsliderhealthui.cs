using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class customsliderhealthui : MonoBehaviour
{
    public float health;
    private float maxhealth = 100;
    private float lerpspeed;
    public Image image;
    public float lerpspeedvalue;
    public GameObject pos;
    public GameObject lookatpos;
    public void Start()
    {
        health = maxhealth;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(lookatpos.transform.position);
      lerpspeed = lerpspeedvalue * Time.deltaTime;
        //if (Input.GetKeyDown(KeyCode.T))
        //{
        //   health-= 10;
        //    
        //}
        //else if (Input.GetKeyDown(KeyCode.R))
        //{
        //    health += 10;
        //  
        //}
     
        transform.position = pos.transform.position;

        gameObject.GetComponent<Slider>().value = Mathf.Lerp(gameObject.GetComponent<Slider>().value, health, lerpspeed);

        Color healkthcolor = Color.Lerp(Color.red.gamma, Color.green.gamma, health /maxhealth);
        image.color = healkthcolor;
        health = Mathf.Clamp(health, 0, 100);
       maxhealth = Mathf.Clamp(maxhealth, 0, 100);
}
 
  
}
