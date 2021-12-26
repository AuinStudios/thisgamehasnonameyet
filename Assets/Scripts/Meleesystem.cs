#region Doeverythingformeunitylol
using UnityEngine;
using UnityEngine.UI;
public class Meleesystem : MonoBehaviour
{
    private ParticleSystem max;
    public AudioSource swing;
    public AudioClip hitt;
    private KeyCode[] keybinds;
    // pos + gameobjects ---------------------------
    public GameObject spawneffect;
    private Transform Spherecastpos;
     public ScriptableObectStorage playerhp;
    // bool ----------------------------------------
     private bool enabletimerun;
    private bool spawneffectdelay;
    // ui + sliders -------------------------
    private Slider Slider;
    // floats -------------------------------
    private float sphereradius = 0.5f, sphererange = 2f;
    public float Damage;
    private float timeruntllreset;
    public float cooldown;
    private float clickorhold;
    // leftoverthings ---------------------------------
    #region andreasthing left for now
    
    private Cameramove cameramove;
    private void Start()
    {
        HoldVariables  data = SaveSystem.load();
        keybinds = new KeyCode[2];
        keybinds[0] = data.keys[0];
        keybinds[1] = data.keys[1];
      cameramove = Camera.main.GetComponent<Cameramove>();
        max = spawneffect.GetComponent<ParticleSystem>();
    }
    #endregion
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
    // most of the script ----------------------------------
    private void Awake()
    {
      
       
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
        
         if ( col.gameObject && !col.gameObject.CompareTag("Player") && cooldown !=0)
         {
           swing.PlayOneShot(hitt);
         }
    }
   
    void Update()
    {
        RaycastHit hit;
        if(Physics.SphereCast( Spherecastpos.position, sphereradius , Spherecastpos.up, out hit , sphererange) && spawneffectdelay == true)
        {
        max.trigger.SetCollider(0, GameObject.Find("Ground").transform);
         Instantiate(spawneffect ,hit.point, spawneffect.transform.rotation = Quaternion.FromToRotation(Vector3.forward , hit.normal));
        spawneffectdelay = false;
        }
        //clamps values
        cooldown = Mathf.Clamp(cooldown, 0, 5);
        Damage = Mathf.Clamp(Damage, 0, 30);
        clickorhold = Mathf.Clamp(clickorhold, 0, 10);
        // the main function -----------------------------------------------
        if (Input.GetKey(keybinds[0])  && !Input.GetKey(keybinds[1])&& cooldown == 0)
        {
            // call script for CameraMove
            cameramove.triggerDot = true;

            clickorhold += 0.35f * Time.deltaTime;
            // chargeing attack
            if (clickorhold > 0.1f && cooldown == 0)
            {
                   Slider.maxValue = 30;
                Slider.value = Damage;
                gameObject.GetComponent<Animator>().SetBool("Chargeing", true);
            }

            // stab and  chargeing damages --------------------------------------
            if (clickorhold <= 0.045f && cooldown == 0)
            {
                Damage = 10f;
            }
            else if (clickorhold >= 0.1f && cooldown == 0)
            {
                Damage += Time.deltaTime * 13;
            }
            else
            {
                Damage = 0;
            }
        }

       
        // plays the animation and enables hitbox whenever u let go of the mouse to attack and add cooldown
        else if ((Input.GetKeyUp(keybinds[0]) && !Input.GetKey(keybinds[1])&& cooldown == 0))
        {
            
            if (clickorhold > 0.1f && cooldown == 0)
            {
             
                gameObject.GetComponent<Animator>().SetBool("Chargeing", false);
                enabletimerun = true;
                cooldown = 5;
                sphererange = 3;
                sphereradius = 1;
                swing.PlayDelayed(0.2f);
                max.maxParticles = 6;
            }
            else if (clickorhold < 0.1f && cooldown == 0)
            {
                swing.Play();
                gameObject.GetComponent<Animator>().SetTrigger("Axestab");
                enabletimerun = true;
                cooldown = 3.5f;
                sphererange = 2;
                sphereradius = 0.5f;
                max.maxParticles = 2;
            }
        }
        if (Input.GetKey(keybinds[1]) && cooldown == 0)
        {
           gameObject.GetComponent<Animator>().SetBool("block", true);
            playerhp.isblocking = true;
        } 
        else if(Input.GetKeyUp(keybinds[1]) && cooldown == 0)
        {
            gameObject.GetComponent<Animator>().SetBool("block",false);
            playerhp.isblocking = false;
            enabletimerun = true;
            cooldown = 1.3f;
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
            Slider.maxValue = 5;
           Slider.value = cooldown;
        }
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawWireSphere(transform.GetChild(1).position, 0.5f);
    //}

}
#endregion