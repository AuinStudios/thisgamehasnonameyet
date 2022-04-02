using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class NewPlayer : MonoBehaviour
{
    // animator ---------------------------------
    private Animator anim;
    // Transition start ------------------------
    private GameObject transition;
    // transforms and rigidbody------------
    private Rigidbody rig;
    private Transform orientation;
    // floats ---------------------------------
    public float health = 100f;
    public float movespeed = 17500f;
    private float maxVertSpeed = 200f;
    private float maxspeed = 300f;
    private float sensitivity = 50f;
    private float sensMultiplier = 1f;
    private float sprintime = 100f;
    private float SoundDelayValue = 0.3f;
    // bools---------------------------
    private bool enablesoundelay = true;
    private bool isgroundedboi;
    private bool sprinting;
    private bool CanLerpHealth;
    // input---------------------------
    private float x, z;
    private KeyCode[] keybinds;
    // sounds -------------------------
    public AudioSource sound;
    // ui stamina and health --------------------
    private Slider HealthValue;
    private Slider StaminaValue;
    private Image HealthColor;
    private TextMeshProUGUI StaminaText;

    private void Awake()
    {
        HoldVariables DATA = SaveSystem.load();
        AudioListener.volume = DATA.MasterVolume;
        sensitivity = DATA.sens;
        QualitySettings.SetQualityLevel(DATA.graphicisvalue);
        Color brightness_adjust = new Color(DATA.brightness, DATA.brightness, DATA.brightness);
        RenderSettings.ambientLight = brightness_adjust;
        orientation = transform.GetChild(2).transform;
        rig = this.transform.GetComponent<Rigidbody>();
        StaminaValue = GameObject.Find("Staminabar").GetComponent<Slider>();
        StaminaText = StaminaValue.transform.GetChild(2).gameObject.GetComponent<TextMeshProUGUI>();
        transition = GameObject.Find("End transition");
        transition.transform.GetChild(0).transform.localEulerAngles = new Vector3(DATA.rot[0], DATA.rot[1], DATA.rot[2]);
        keybinds = new KeyCode[5];
        keybinds[4] = DATA.keys[2];
        keybinds[0] = DATA.keys[3];
        keybinds[1] = DATA.keys[4];
        keybinds[2] = DATA.keys[5];
        keybinds[3] = DATA.keys[6];
        anim = transform.GetChild(0).transform.GetComponent<Animator>();
        HealthValue = GameObject.Find("hp").GetComponent<Slider>();
        HealthColor = HealthValue.transform.GetChild(0).GetComponent<Image>();
        CanLerpHealth = true;
        StartCoroutine(Starttransition());
    }

    private IEnumerator Starttransition()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (transition.transform.GetChild(1).localPosition.x >= -1300f || transition.transform.GetChild(2).localPosition.x <= 1400f)
        {
            Vector2 logoimage = new Vector2(1448f, 1187f);
            transition.transform.GetChild(0).localPosition = Vector2.Lerp(transition.transform.GetChild(0).localPosition, logoimage, 2f * Time.fixedDeltaTime);

            Vector2 firstboxpos = new Vector2(-1457f, -1278f);
            transition.transform.GetChild(1).localPosition = Vector2.Lerp(transition.transform.GetChild(1).localPosition, firstboxpos, 2f * Time.fixedDeltaTime);


            Vector2 secoundboxpos = new Vector2(1523f, 1162f);
            transition.transform.GetChild(2).localPosition = Vector2.Lerp(transition.transform.GetChild(2).localPosition, secoundboxpos, 2f * Time.fixedDeltaTime);
            yield return wait;
        }
        CanLerpHealth = false;
    }
    private void FixedUpdate()
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
          
        }

    }
    // sprinting function  --------------------------------------------------------------------------------------
    private void sprint()
    {
        if (sprintime >= 25)
        {
            sprinting = true;
        }
        if (sprintime == 0)
        {
            sprinting = false;
        }
        if (Input.GetKey(keybinds[4]) && sprinting == true && isgroundedboi)
        {
            StaminaValue.value = sprintime;
            StaminaText.text = Mathf.Clamp((int)sprintime, 0, int.MaxValue).ToString();
            movespeed = 200000;
            maxspeed = 16;
            anim.SetBool("walking", false);
            SoundDelayValue = 0.05f;
            if (z != 0 || x != 0)
            {
                sprintime -= 2.5f * Time.deltaTime;
            }

            if (z == 0 && x == 0)
            {
                anim.SetBool("running", false);
                sprintime += Time.deltaTime;
            }
            else
            {
                anim.SetBool("running", true);
            }

        }
        else if (sprintime <= 100)
        {

            anim.SetBool("running", false);
            movespeed = 100000;
            maxspeed = 8;
            SoundDelayValue = 0.3f;
            sprintime += Time.deltaTime;
        }
    }
    // sound delay loop -----------------------------------------------------------------------------------------------
    private IEnumerator soundelay()
    {
        WaitForSeconds waitforsound = new WaitForSeconds(SoundDelayValue);
        enablesoundelay = false;
        yield return waitforsound;
        sound.Play();
        enablesoundelay = true;
    }
    //  main functions ---------------------------------------------------------------------------------
    private void Update()
    {   
        
        // lerping for health ui -----------------------------------------------------------------------
        if (CanLerpHealth == true)
        {
            HealthValue.value = Mathf.Lerp(HealthValue.value, health, 3 * Time.deltaTime);
            HealthColor.color = Color.Lerp(Color.red, Color.green, HealthValue.value / HealthValue.maxValue);
        }

        //inputs -----------------------------------------------------------------------------------
        if (Input.GetAxis("Mouse X") != 0)
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
            orientation.Rotate(Vector3.up * mouseX);
        }
        if (Input.GetKey(keybinds[0]) && movespeed != 0)
        {
            x = 1;
        }
        else if (Input.GetKey(keybinds[1]) && movespeed != 0)
        {
            x = -1;
        }
        else
        {
            x = 0;
        }
        if (Input.GetKey(keybinds[2]) && movespeed != 0)
        {
            z = -1;
        }
        else if (Input.GetKey(keybinds[3]) && movespeed != 0)
        {
            z = 1;
        }
        else
        {
            z = 0;
        }

        // animation for headbop -----------------------------------------------------------------------------------------

        if (x == 1 || x == -1 || z == 1 || z == -1)
        {
            anim.SetBool("walking", true);

            if (enablesoundelay == true && !sound.isPlaying)
            {
                StartCoroutine(soundelay());
            }

            rig.AddForce(orientation.forward * x * movespeed * Time.deltaTime);
            rig.AddForce(orientation.right * z * movespeed * Time.deltaTime);
        }
        else
        {
            anim.SetBool("walking", false);
            StopCoroutine(soundelay());
        }
        // heal and movement force and sprinting ----------------------------------------------
        sprint();

        if (health < 100)
        {
            health += 0.5f * Time.deltaTime;
            CanLerpHealth = true;
            Debug.Log("a");
        }
        else
        {
            CanLerpHealth = false;
        }

    }

    // walking detection ----------------------------------------------------------------------------------------------

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isgroundedboi = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isgroundedboi = false;
        }
    }
}