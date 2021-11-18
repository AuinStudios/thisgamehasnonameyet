using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : MonoBehaviour
{
    // transform and physics stuff ------------
    private Rigidbody rig;
    private Transform orientation;
    // floats ---------------------------------
    public float health = 100;
    public  float movespeed = 17500;
    private  float maxVertSpeed = 200;
    private  float maxspeed = 300f;
    private  float sensitivity = 50f;
    private  float sensMultiplier = 1f;
    private  float sprintime = 100f;
    // bools---------------------------
    private bool isgroundedboi;
    private bool sprinting;
    private headbop bop;
    // input---------------------------
     private float x, z;
    // sounds -------------------------
     public AudioSource walk , run;
    // ui stamina and health --------------------
    private Slider StaminaValue;
    private TextMeshProUGUI StaminaText;
    public void Awake()
    {
        orientation = transform.GetChild(2).transform;
        rig = this.transform.GetComponent<Rigidbody>();
        StaminaValue = GameObject.Find("Staminabar").GetComponent<Slider>();
        StaminaText = GameObject.Find("Staminabar").transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        bop = this.transform.GetChild(0).GetComponent<headbop>();
    }
    void FixedUpdate()
    {
        Vector3 xzVel = new Vector3(rig.velocity.x, 0, rig.velocity.z);
        Vector3 yVel = new Vector3(0, rig.velocity.y, 0);

        xzVel = Vector3.ClampMagnitude(xzVel, maxspeed);
        yVel = Vector3.ClampMagnitude(yVel, maxVertSpeed);

        rig.velocity = xzVel + yVel;

        if (isgroundedboi)
        {
            rig.drag = 10;
        }
        if (!isgroundedboi)
        {
            rig.AddForce(orientation.up * -100 * Time.fixedDeltaTime, ForceMode.Impulse);
            rig.drag = 0.5f;
            movespeed = 400;
        }

    }
    
    public void  Playermovement()
    {
        rig.AddForce(orientation.forward * x * movespeed * Time.deltaTime );
        rig.AddForce(orientation.right * z * movespeed * Time.deltaTime );
    }
  
    public void sprint()
    {
        StaminaValue.value = sprintime;
        StaminaText.text = Mathf.Clamp((int)sprintime, 0, int.MaxValue).ToString();
        if (sprintime >= 25)
        {
            sprinting = true;
        }
        if(sprintime == 0)
        {
            sprinting = false;
        }
        if (Input.GetKey(KeyCode.LeftShift) && sprinting == true  && isgroundedboi )
        {
            movespeed = 200000;
             maxspeed = 16;
              transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", false);
            if (!run.isPlaying)
            {
                run.Play();
            }
            if(z != 0 || x != 0)
            {
             sprintime -= 2.5f *  Time.deltaTime;
            }
            
            if(z == 0 || x == 0)
            {
                sprintime += Time.deltaTime;
            }
              
            if(  z == 0 && x == 0)
            {
                transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", false);
                
            }
            else
            {
              transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", true);
                walk.Stop();
               
            }
           
        }
        else if (isgroundedboi )
        {
            run.Stop();
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", false);
            movespeed = 100000;
            maxspeed = 8;
          
            sprintime += Time.deltaTime;
        }
    }
 
    void Update()
    {
        // lerping for health ui -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        GameObject.Find("hp").GetComponent<Slider>().value = Mathf.Lerp(GameObject.Find("hp").GetComponent<Slider>().value , health , 3 * Time.deltaTime);
        GameObject.Find("hp").transform.GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, GameObject.Find("hp").GetComponent<Slider>().value / GameObject.Find("hp").GetComponent<Slider>().maxValue);    
        // sprint clamp-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        sprintime = Mathf.Clamp((float)sprintime, 0, 100 );
        //inputs -----------------------------------------------------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        orientation.Rotate(Vector3.up * mouseX);
        z = Input.GetAxisRaw("Horizontal");
        x = Input.GetAxisRaw("Vertical");
        // animation for headbop --------------------------------------------------------------------
        if (x == 1 && !bop.walking)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", true);
            walk.Play();
        }
        else if((x == -1 && !bop.walking))
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", true);
            walk.Play();
        }
        if(z == 1 && !bop.walking)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", true);
            walk.Play();
        }
        else if(z == -1 && !bop.walking)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", true);
            walk.Play();
        }
        else if (z == 0 && x ==  0 )
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking",false);
            walk.Stop();
        }
        // heal and movement force and sprinting ----------------------------------------------
        Playermovement();
        sprint();
        if( health  < 100)
        {
            health += 0.5f * Time.deltaTime;
        }
    }



    // walking detection ----------------------------------------------------------------------------------------------
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isgroundedboi = true;

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isgroundedboi = false;

        }
   
         

    }
}