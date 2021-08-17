using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Meleesystem : MonoBehaviour
{
    public GameObject nocharge;
    public float Damage;
    public Slider value;
    public float cooldown;
    public float clickorhold;
    void Update()
    { 
        cooldown = Mathf.Clamp((float)cooldown, 0, 5);
        value.value = Damage;
        if (Input.GetKey(KeyCode.Mouse0) && clickorhold <= 0.2f && cooldown == 0)
        {
            clickorhold += 0.35f *  Time.deltaTime;
            if(clickorhold <= 0.045f)
            {
             Damage = 0.5f;

            }
            else
            {
                Damage = 0;
            }
            nocharge.GetComponent<CanvasGroup>().alpha = 0;
        }
        else if ((Input.GetKeyUp(KeyCode.Mouse0) && cooldown == 0))
        {
            clickorhold = 0;
            Damage = 0;
            cooldown = 2;
            value.GetComponent<CanvasGroup>().alpha = 0;
        }
        if (Input.GetKey(KeyCode.Mouse0) && (clickorhold >= 0.2f) && cooldown == 0)
        {
                nocharge.GetComponent<CanvasGroup>().alpha = 1;
                Damage += Time.deltaTime;
             value.GetComponent<CanvasGroup>().alpha = 0.5f;
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && clickorhold >= 0.2f && cooldown == 0)
        {
            cooldown = 5;
            Damage = 0;
            
            value.GetComponent<CanvasGroup>().alpha = 0;
        }
     
        if(cooldown != 0)
        {
            cooldown -= Time.deltaTime;
        }
    }

 
   
}
