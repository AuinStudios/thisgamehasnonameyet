
using UnityEngine;
using UnityEngine.UI;
public class Meleesystem : MonoBehaviour
{
    private ParticleSystem max;
    public AudioSource swing;
    public AudioClip hitt;
    public KeyCode[] keybinds;
    // pos + gameobjects ---------------------------
    private GameObject Ground;
    public GameObject spawneffect;
    private Transform Spherecastpos;
    public ScriptableObectStorage playerhp;
    // bool ----------------------------------------
    private bool ISResetingDamage;
    private bool spawneffectdelay;
    // ui + sliders -------------------------
    private Slider Slider;
    // floats -------------------------------
    private float sphereradius = 0.5f, sphererange = 2f;
    public float Damage;
    private float TimeUntllResetDamage;
    public float cooldown;
    private float StabOrChargeing;
    // Animator ---------------------------------------
    private Animator Anim;
    // most of the script ----------------------------------
    private void Start()
    {
        HoldVariables data = SaveSystem.load();
        keybinds = new KeyCode[2];
        keybinds[0] = data.keys[0];
        keybinds[1] = data.keys[1];
        //cameramove = Camera.main.GetComponent<Cameramove>();
        // getstuff im to lazy to drag in lmao -------------------
        max = spawneffect.GetComponent<ParticleSystem>();
        Ground = GameObject.Find("Ground");
        Anim = gameObject.GetComponent<Animator>();
        Slider = GameObject.Find("Meleeui").GetComponent<Slider>();
        Spherecastpos = gameObject.transform.GetChild(0).transform;
    }
    public void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<customsliderhealthui>().health -= Damage;
            // spawn it -----------------------------------------------------
            spawneffectdelay = true;
            //col.gameObject.GetComponent<Rigidbody>().AddForce(camtransform.forward * 20000);
        }

        if (col.gameObject && !col.gameObject.CompareTag("Player") && cooldown != 0)
        {
            swing.PlayOneShot(hitt);
        }
    }

    void Update()
    {
        RaycastHit hit;
        if (Physics.SphereCast(Spherecastpos.position, sphereradius, Spherecastpos.up, out hit, sphererange) && spawneffectdelay == true)
        {
            max.trigger.SetCollider(0, Ground.transform);
            Instantiate(spawneffect, hit.point, spawneffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hit.normal));
            spawneffectdelay = false;
        }
        // the main function -----------------------------------------------
        if (Input.GetKey(keybinds[0]) && !Input.GetKey(keybinds[1]) && cooldown == 0)
        {
            // call script for CameraMove
            //cameramove.triggerDot = true;
            if(StabOrChargeing  <= 10)
            {
             StabOrChargeing += 0.35f * Time.deltaTime;
            }
            
            // chargeing attack
            if (StabOrChargeing > 0.1f && cooldown == 0)
            {
                Slider.maxValue = 30;
                Slider.value = Damage;
                Anim.SetBool("Chargeing", true);
            }
            //chargeing damages --------------------------------------
            if (StabOrChargeing >= 0.1f && cooldown == 0 && Damage <= 30)
            {
                Damage += Time.deltaTime * 13;
            }
        }

        // plays the animation and enables hitbox whenever u let go of the mouse to attack and add cooldown
        else if ((Input.GetKeyUp(keybinds[0]) && !Input.GetKey(keybinds[1]) && cooldown == 0))
        {

            if (StabOrChargeing > 0.1f && cooldown == 0)
            {

                Anim.SetBool("Chargeing", false);
                ISResetingDamage = true;
                cooldown = 5;
                sphererange = 3;
                sphereradius = 1;
                swing.PlayDelayed(0.2f);
                max.maxParticles = 6;
            }
            else if (StabOrChargeing < 0.1f && cooldown == 0)
            {
                Damage = 10f;
                swing.Play();
                Anim.SetTrigger("Axestab");
                ISResetingDamage = true;
                cooldown = 3.5f;
                sphererange = 2;
                sphereradius = 0.5f;
                max.maxParticles = 2;
            }
        }
        if (Input.GetKey(keybinds[1]) && cooldown == 0)
        {
            Anim.SetBool("block", true);
            playerhp.isblocking = true;
        }
        else if (Input.GetKeyUp(keybinds[1]) && cooldown == 0)
        {
            Anim.SetBool("block", false);
            playerhp.isblocking = false;
            ISResetingDamage = true;
            cooldown = 1.3f;
        }

        // timers to help make the damage values not go away instanly---------------------------------------------
        if (TimeUntllResetDamage >= 0.5f && StabOrChargeing <= 0.1f)
        {
            StabOrChargeing = 0;
            Damage = 0;
            ISResetingDamage = false;
        }
        else if (TimeUntllResetDamage >= 0.8f && StabOrChargeing >= 0.1f)
        {
            Damage = 0;
            StabOrChargeing = 0;
            ISResetingDamage = false;
        }
        // bool for the timers to help damage values not go away instanly-----------------------------------------
        if (ISResetingDamage == true)
        {
            TimeUntllResetDamage += Time.deltaTime;
        }
        else if (ISResetingDamage == false)
        {
            TimeUntllResetDamage = 0;
        }

        //  decrease the cooldown over time and enable cooldownslider background ------------------------------------------------------------------------
        if (cooldown > 0 )
        {
            cooldown -= Time.deltaTime;
            Slider.maxValue = 5;
            Slider.value = cooldown;
        }
        else if( cooldown < 0)
        {
            cooldown = 0;
        }
    }
}