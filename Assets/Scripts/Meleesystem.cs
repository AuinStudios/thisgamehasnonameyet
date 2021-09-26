using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Meleesystem : MonoBehaviour
{
    public Animator anim;
    public GameObject nocharge;
    public float Damage;
    public Slider value;
    public Slider cooldownslider;
    public Image Chargebarbackground;
    public Image cooldownbarbackground;
    private bool enabletimerun;
    private float timeruntllreset;
    private float cooldown;
    private float clickorhold;
    void Update()
    { 
        //clamps values
        cooldown = Mathf.Clamp(cooldown, 0, 5);
         Damage = Mathf.Clamp(Damage, 0, 10);
        clickorhold = Mathf.Clamp(clickorhold, 0, 10);
        // slider ------------------------------------------------
        value.value = Damage;
        cooldownslider.value = cooldown;

        // the main function -----------------------------------------------
        if (Input.GetKey(KeyCode.Mouse0)  && cooldown == 0)
        {
          nocharge.GetComponent<CanvasGroup>().alpha = 0;

          clickorhold += 0.35f * Time.deltaTime;
          // chargeing attack
          if ( clickorhold > 0.3f &&  cooldown == 0)
          {
              nocharge.GetComponent<CanvasGroup>().alpha = 1;
                Chargebarbackground.GetComponent<CanvasGroup>().alpha = 1f;
                cooldownslider.GetComponent<CanvasGroup>().alpha = 0f;
                cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 0f;
                value.GetComponent<CanvasGroup>().alpha = 0.5f;
              anim.SetBool("Chargeing", true);
          }
         
          // stab and  chargeing damages
          if (clickorhold <= 0.045f && cooldown == 0)
          {
              Damage = 5f;
          }
          else if(clickorhold >= 0.3f && cooldown == 0)
          {
              Damage += Time.deltaTime * 8;
          }
          else
          {
              Damage = 0;
          }
        }
        // plays the animation and enables hitbox whenever u let go of the mouse to attack and add cooldown
        else if ((Input.GetKeyUp(KeyCode.Mouse0)   && cooldown == 0))
        {
            if(clickorhold > 0.3f && cooldown == 0)
            {
                
                anim.SetBool("Chargeing", false);
                enabletimerun = true;
                cooldown = 5;
                cooldownslider.GetComponent<CanvasGroup>().alpha = 0.5f;
                cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 1f;
                nocharge.GetComponent<CanvasGroup>().alpha = 0;
                value.GetComponent<CanvasGroup>().alpha = 0;
                Chargebarbackground.GetComponent<CanvasGroup>().alpha = 0f;
            }
            else if(clickorhold < 0.3f && cooldown == 0)
            {
                anim.SetTrigger("Axestab");
                enabletimerun = true;
                cooldown = 2;
            nocharge.GetComponent<CanvasGroup>().alpha = 0;
            value.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        // timers to help make the damage values not go away instanly---------------------------------------------
         if(timeruntllreset >= 0.5f && clickorhold <= 0.3f)
         {
          clickorhold = 0;
          Damage = 0;
             enabletimerun = false;
         }
         else if (timeruntllreset >= 0.8f && clickorhold >= 0.3f)
         {
            Damage = 0;
            clickorhold = 0;
            enabletimerun = false;
         } 
         // bool for the timers to help damage values not go away instanly-----------------------------------------
        if(enabletimerun == true)
        {
            timeruntllreset += Time.deltaTime;
        }
        else if(enabletimerun == false)
        {
            timeruntllreset = 0;
        }

        //  decrease the cooldown over time and enable cooldownslider background ------------------------------------------------------------------------
        if (cooldown != 0)
        {
            cooldown -= Time.deltaTime;
            //cooldownslider.GetComponent<CanvasGroup>().alpha = 0.5f;
          //  cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 1f;
        }
        else
        {
            //cooldownslider.GetComponent<CanvasGroup>().alpha = 0f;
            //cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 0f;
        }
    }
}
