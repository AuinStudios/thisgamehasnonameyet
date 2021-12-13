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
    private float sounddelayvalue = 0.3f;
    // bools---------------------------
    private bool enablesoundelay = true;
    private bool isgroundedboi;
    private bool sprinting;
    private headbop bop;
    // input---------------------------
     private float x, z;
    // sounds -------------------------
     public AudioSource sound;
    // ui stamina and health --------------------
    private Slider StaminaValue;
    private TextMeshProUGUI StaminaText;
    public void Awake()
    {
      HoldVariables dATA =  SaveSystem.load();
        sensitivity = dATA.sens;
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
    // movement function ---------------------------------------------------------------------------------------
    public void  Playermovement()
    {
        rig.AddForce(orientation.forward * x * movespeed * Time.deltaTime );
        rig.AddForce(orientation.right * z * movespeed * Time.deltaTime );
    }
    // sprinting function  --------------------------------------------------------------------------------------
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
            sounddelayvalue = 0.05f;
            if(z != 0 || x != 0)
            {
             sprintime -= 2.5f *  Time.deltaTime;
            }

            if(  z == 0 && x == 0)
            {
                transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", false);
                sprintime += Time.deltaTime;
            }
            else
            {
              transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", true);
            }
           
        }
        else 
        {
           
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("running", false);
            movespeed = 100000;
            maxspeed = 8;
            sounddelayvalue = 0.3f;
            sprintime += Time.deltaTime;
        }
    }
    // sound delay loop -----------------------------------------------------------------------------------------------
    public IEnumerator soundelay()
    {
        enablesoundelay = false;
        yield return new WaitForSeconds(sounddelayvalue);
        sound.Play();
        enablesoundelay = true;
    }
    //  main functions --------------------------------------------------------------------------------------------------------------------
    void Update()
    {
        // lerping for health ui -------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        GameObject.Find("hp").GetComponent<Slider>().value = Mathf.Lerp(GameObject.Find("hp").GetComponent<Slider>().value, health, 3 * Time.deltaTime);
        GameObject.Find("hp").transform.GetChild(0).GetComponent<Image>().color = Color.Lerp(Color.red, Color.green, GameObject.Find("hp").GetComponent<Slider>().value / GameObject.Find("hp").GetComponent<Slider>().maxValue);
        // sprint clamp-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        sprintime = Mathf.Clamp((float)sprintime, 0, 100);
        //inputs -----------------------------------------------------------------------------------
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        orientation.Rotate(Vector3.up * mouseX);
        z = Input.GetAxisRaw("Horizontal");
        x = Input.GetAxisRaw("Vertical");

        // Sound Walk Input ---------------------------------------------------------------------------------------------

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S))
        {

            if (enablesoundelay == true && !sound.isPlaying)
            {
                StartCoroutine(soundelay());
            }
        }

        // animation for headbop -----------------------------------------------------------------------------------------

        if (x == 1 && !bop.walking || x == -1 && !bop.walking || z == 1 && !bop.walking || z == -1 && !bop.walking)
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking", true);
         
        }
        if (z == 0 && x ==  0 )
        {
            transform.GetChild(0).transform.GetComponent<Animator>().SetBool("walking",false);
            StopCoroutine(soundelay());
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