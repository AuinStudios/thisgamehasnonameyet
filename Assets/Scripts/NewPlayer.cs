using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewPlayer : MonoBehaviour
{
    public Rigidbody rig;
    public GameObject cam;
    public Animator anim;
    public Transform orientation;
   
    // floats ------------------------- 
    private float dash = 50000f;
    public float movespeed = 17500;
    private float maxVertSpeed = 200;
     public float maxspeed = 300f;
     private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    public float sprintime = 100f;
    // bools---------------------------
    public bool isgroundedboi;
    public bool sprinting;
    public headbop bop;
    // input---------------------------
     public float x, z;
    // ui stamina and health --------------------
    public Slider sildevalue;
    public TextMeshProUGUI numbersilder;
    void FixedUpdate()
    {
        Vector3 xzVel = new Vector3(rig.velocity.x, 0, rig.velocity.z);
        Vector3 yVel = new Vector3(0, rig.velocity.y, 0);

        xzVel = Vector3.ClampMagnitude(xzVel, maxspeed);
        yVel = Vector3.ClampMagnitude(yVel, maxVertSpeed);

        rig.velocity = xzVel + yVel;


       // if (rig.velocity.magnitude > maxspeed && (!(health.Health <= 0f)))
       // {
       //     speedeffect.Play();
       // }



        //if (rig.velocity.magnitude < maxspeed)
        //{
        //    speedeffect.Stop();
        //}

        if (isgroundedboi) //&& !(health.Health <= 0))
        {
           // jumpower = 7000f;
            rig.drag = 10;
        
        }
        if (!isgroundedboi)
        {
            //jumpower = 0f;
            rig.AddForce(orientation.up * -100 * Time.fixedDeltaTime, ForceMode.Impulse);
            rig.drag = 0.5f;
            movespeed = 400;
        }
   
       // if (isgroundedboi && (Input.GetKey(KeyCode.Space)))
       // {
       //     rig.AddForce(orientation.up * jumpower * Time.fixedDeltaTime, ForceMode.Impulse);
       //
       //
       // }
    }
    
    public void  Playermovement()
    {
        rig.AddForce(orientation.forward * x * movespeed * Time.deltaTime );
        rig.AddForce(orientation.right * z * movespeed * Time.deltaTime );
    }
 
    public void sprint()
    {
        sildevalue.value = sprintime;
        numbersilder.text = Mathf.Clamp((int)sprintime, 0, int.MaxValue).ToString();
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
            sprintime -= 2.5f *  Time.deltaTime;
            anim.SetBool("running", true);
            anim.SetBool("walking", false);
        }
        else if (isgroundedboi )
        {
            anim.SetBool("running", false);
            movespeed = 100000;
            maxspeed = 8;
            sprintime += Time.deltaTime;
        }
    }
 
    void Update()
    {
         sprintime = Mathf.Clamp((float)sprintime, 0, 100 );
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        orientation.Rotate(Vector3.up * mouseX);
        z = Input.GetAxisRaw("Horizontal");
        x = Input.GetAxisRaw("Vertical");
        if (x == 1 && !bop.walking)
        {
            anim.SetBool("walking", true);
        }
        else if((x == -1 && !bop.walking))
        {
            anim.SetBool("walking", true);
        }
        if(z == 1 && !bop.walking)
        {
            anim.SetBool("walking", true);
        }
        else if(z == -1 && !bop.walking)
        {
            anim.SetBool("walking", true);
        }
        else if (z == 0 && x ==  0 && bop.walking)
        {
            anim.SetBool("walking",false);
        }
        Playermovement();
        sprint();
       
        if (Input.GetKeyDown(KeyCode.H) && !isgroundedboi)
        {
            rig.velocity = Vector3.ClampMagnitude(rig.velocity, 1000);
            rig.AddForce(orientation.forward * dash * Time.deltaTime, ForceMode.VelocityChange);

        }

       // if (health.Health <= 0)
       // {
       //     forwardspeed = 0f;
       //     backwards = -0f;
       //     right = 0f;
       //     left = -0f;
       //     sensitivity = 0f;
       //     sensMultiplier = 0;
       // }
       // if (Time.timeScale == 0)
       // {
       //     forwardspeed = 0f;
       //     backwards = -0f;
       //     right = 0f;
       //     left = -0f;
       //     sensitivity = 0f;
       //     sensMultiplier = 0;
       // }
      // if (Time.timeScale == 1 && !(health.Health <= 0))
      // {
      //     sensitivity = 50f;
      //     sensMultiplier = 1;
      // }
    }
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