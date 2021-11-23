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
    public void Start()
    {
        health = maxhealth;
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(GameObject.Find("Player").transform.GetChild(0).transform.position);
        lerpspeed = lerpseedvalue * Time.deltaTime;

        transform.position = GameObject.FindGameObjectWithTag("Enemy").transform.GetChild(0).transform.position;

        gameObject.GetComponent<Slider>().value = Mathf.Lerp(gameObject.GetComponent<Slider>().value, health, lerpspeed);

        Color healkthcolor = Color.Lerp(Color.red.gamma, Color.green.gamma, health /maxhealth);
        gameObject.transform.GetChild(0).GetComponent<Image>().color = healkthcolor;
        health = Mathf.Clamp(health, 0, 100);
       maxhealth = Mathf.Clamp(maxhealth, 0, 100);
}
 
  
}
