#region Doeverythingformeunitylol
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Meleesystem : MonoBehaviour
{
    //public Animator anim;
    public Transform camtransform;
    public GameObject nocharge;
    public GameObject spawneffect;
    public Transform spawneffectpos;
    public customsliderhealthui enemy;
    public float Damage;
    public Slider value;
    public Slider cooldownslider;
    public Image Chargebarbackground;
    public Image cooldownbarbackground;
    private bool enabletimerun;
    private float timeruntllreset;
    private float cooldown;
    private float clickorhold;

    
    public void OnTriggerEnter(Collider col)
    {
        
         if (col.gameObject.CompareTag("Enemy"))
         {
            enemy.health -= Damage;
            Instantiate(spawneffect);
            
            //col.gameObject.GetComponent<Rigidbody>().AddForce(camtransform.forward * 20000);
         }
     
    }
    
    

    void Update()
    {

        spawneffect.transform.position = spawneffectpos.position;
        //clamps values
        cooldown = Mathf.Clamp(cooldown, 0, 5);
        Damage = Mathf.Clamp(Damage, 0, 30);
        clickorhold = Mathf.Clamp(clickorhold, 0, 10);
        // slider ------------------------------------------------
        value.value = Damage;
        cooldownslider.value = cooldown;

        
        // the main function -----------------------------------------------
        if (Input.GetKey(KeyCode.Mouse0) && cooldown == 0)
        {
            nocharge.GetComponent<CanvasGroup>().alpha = 0;

            clickorhold += 0.35f * Time.deltaTime;
            // chargeing attack
            if (clickorhold > 0.1f && cooldown == 0)
            {
                nocharge.GetComponent<CanvasGroup>().alpha = 1;
                Chargebarbackground.GetComponent<CanvasGroup>().alpha = 1f;
                cooldownslider.GetComponent<CanvasGroup>().alpha = 0f;
                cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 0f;
                value.GetComponent<CanvasGroup>().alpha = 0.5f;
                gameObject.GetComponent<Animator>().SetBool("Chargeing", true);
            }

            // stab and  chargeing damages
            if (clickorhold <= 0.045f && cooldown == 0)
            {
                Damage = 10f;
            }
            else if (clickorhold >= 0.1f && cooldown == 0)
            {
                Damage += Time.deltaTime * 12;
            }
            else
            {
                Damage = 0;
            }
        }
        #region hiidestuff
        // if (enableattack == true)
        // {
        //
        //
        //     gameObject.GetComponent<BoxCollider>().enabled = true;
        //
        //
        //    // RaycastHit hit;
        //    // if (Physics.Raycast(camtransform.position, camtransform.TransformDirection(Vector3.forward), out hit, 10) )
        //    // {
        //    //     
        //    //     if(hit.collider.gameObject.CompareTag("Enemy"))
        //    //     {
        //    //     Debug.Log("killednemey");
        //    //     }
        //    //     else
        //    //     {
        //    //         Debug.Log("wasnt");
        //    //     }
        //    // }
        //    // else
        //    // {
        //    //     Debug.Log("m");
        //    //
        //    // }
        // }
        // else if (enableattack == false)
        // {
        // gameObject.GetComponent<BoxCollider>().enabled = false;
        // 
        // }
        #endregion
        // plays the animation and enables hitbox whenever u let go of the mouse to attack and add cooldown
        else if ((Input.GetKeyUp(KeyCode.Mouse0) && cooldown == 0))
        {
            if (clickorhold > 0.1f && cooldown == 0)
            {

                gameObject.GetComponent<Animator>().SetBool("Chargeing", false);
                enabletimerun = true;
                cooldown = 5;
                cooldownslider.GetComponent<CanvasGroup>().alpha = 0.5f;
                cooldownbarbackground.GetComponent<CanvasGroup>().alpha = 1f;
                nocharge.GetComponent<CanvasGroup>().alpha = 0;
                value.GetComponent<CanvasGroup>().alpha = 0;
                Chargebarbackground.GetComponent<CanvasGroup>().alpha = 0f;
            }
            else if (clickorhold < 0.1f && cooldown == 0)
            {
                gameObject.GetComponent<Animator>().SetTrigger("Axestab");
                enabletimerun = true;
                cooldown = 2;
                nocharge.GetComponent<CanvasGroup>().alpha = 0;
                value.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
        // timers to help make the damage values not go away instanly---------------------------------------------
        if (timeruntllreset >= 0.5f && clickorhold <= 0.1f)
        {
            clickorhold = 0;
            Damage = 0;
            enabletimerun = false;
        }
        else if (timeruntllreset >= 0.8f && clickorhold >= 0.1f)
        {
            Damage = 0;
            clickorhold = 0;
            enabletimerun = false;
        }
        // bool for the timers to help damage values not go away instanly-----------------------------------------
        if (enabletimerun == true)
        {
            timeruntllreset += Time.deltaTime;
        }
        else if (enabletimerun == false)
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
#endregion