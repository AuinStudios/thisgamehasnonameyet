using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class customsliderhealthui : MonoBehaviour
{
    public float health;
    private float maxhealth = 100;
    private float lerpspeed;
    public float lerpseedvalue;
    private Transform hpbar;
    private Transform playerlookat;
    public void Start()
    {
        playerlookat = GameObject.FindGameObjectWithTag("Player").transform.GetChild(0).transform;
        hpbar = gameObject.transform.GetChild(0);
        health = maxhealth;
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 lookatplayer =  hpbar.GetChild(0).localPosition- playerlookat.localPosition;
        hpbar.GetChild(0).localRotation = Quaternion.LookRotation(lookatplayer, Vector3.up);
        Vector2 currenthpscale = new Vector2(hpbar.localScale.x , 1f);
        Vector2 hpscale = new Vector2(0, 1);
        hpbar.localScale = Vector2.Lerp(currenthpscale, hpscale, 0.1f * Time.deltaTime);


       // gameObject.transform.LookAt(GameObject.Find("Player").transform.GetChild(0).transform.position);
       // lerpspeed = lerpseedvalue * Time.deltaTime;
       //
       // transform.position = GameObject.FindGameObjectWithTag("Enemy").transform.GetChild(0).transform.position;
       //
       // gameObject.GetComponent<Slider>().value = Mathf.Lerp(gameObject.GetComponent<Slider>().value, health, lerpspeed);
       //
       // Color healkthcolor = Color.Lerp(Color.red.gamma, Color.green.gamma, health /maxhealth);
       // gameObject.transform.GetChild(0).GetComponent<Image>().color = healkthcolor;
       // health = Mathf.Clamp(health, 0, 100);
       //maxhealth = Mathf.Clamp(maxhealth, 0, 100);
}
 
  
}
