using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameramove : MonoBehaviour
{   // meleesystem ----------------------------------------------
    //public Meleesystem melee;
    // array ------------------------------------------------------
    public ActiveOrNot[] weaponactive;
    // Melee System -----------------------------------------------
    public Transform MeleeSystem;
    // sound effects --------------------------------------------
    AudioSource sound;

    // Bools -------------------------------------------------------
    public bool triggerDot = false;
    private bool closeToAnEnemy = false;
    public bool canopendoor = true;
    // Ui -----------------------------------------------------------
    private Image indicator;

    // colliders to detect the player if  hes under the door -------
    private Collider[] col;

    // floats ------------------------------------------------------  
    private float xRotation = 0f;
    public float sensitivity = 50f;
    private float sensMultiplier = 1f;
    private float rayCastRange = 4.5f;
    private float resetArray = -1;

    // ints --------------------------------------------------------
    public int runCount = 0;
    private int clearancelevel = 1;
    public int arrayindex;
    // transforms and gameobjects -----------------------------------
    public Transform meleesystem;
    private Transform orientation;
    public List<Transform> DoorHolder = new List<Transform>();
    public List<Transform> enemies = new List<Transform>();
    private Transform player;
    private Transform whatdidithit;
    private WaitForSeconds waitsecs;
    private WaitForFixedUpdate Waitforfixed;
    public void Start()
    {
        indicator = GameObject.Find("indicator").GetComponent<Image>();

        orientation = transform.parent;

        sound = gameObject.GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = false;

        HoldVariables data = SaveSystem.load();
        Camera.main.fieldOfView = data.fov;
        sensitivity = data.sens;

        player = GameObject.FindGameObjectWithTag("Player").transform;
        Waitforfixed = new WaitForFixedUpdate();
        waitsecs =new WaitForSeconds(0.4f);
    }

    private IEnumerator opendoor()
    {
        for (int g = 0; g < 1; g++)
        {
            resetArray = 14.5f;
            float cooldownfordoors = 13f;
            float FixLerpTimer = 0f;
            int i = 0;
            float t = 0f;
            int index = DoorHolder.Count;

            DoorHolder.Add(whatdidithit);
            DoorHolder.ToArray()[index].parent.gameObject.layer = 0;
            GameObject PlayerBeenHit = null;
            Vector3 size = new Vector3(DoorHolder.ToArray()[index].lossyScale.x * 7, DoorHolder.ToArray()[index].lossyScale.y, DoorHolder.ToArray()[index].lossyScale.z);
            Vector3 originalpos = (DoorHolder.ToArray()[index].localPosition);
            Vector3 topos = new Vector3(DoorHolder.ToArray()[index].localPosition.x, DoorHolder.ToArray()[index].localPosition.y + 5f, DoorHolder.ToArray()[index].localPosition.z);
            sound.PlayDelayed(0.2f);

            while (i < 100)
            {
                t += 0.1f * Time.deltaTime;
                cooldownfordoors -= 3 * Time.deltaTime;
                DoorHolder.ToArray()[index].localPosition = Vector3.Lerp(DoorHolder.ToArray()[index].localPosition, topos, t / 1);

                i++;
                col = Physics.OverlapBox(DoorHolder.ToArray()[index].position + DoorHolder.ToArray()[index].TransformDirection(new Vector3(0, -3, 0)), size, DoorHolder.ToArray()[index].rotation);

                yield return Waitforfixed;
            }

            yield return waitsecs;
            t = 0;

            foreach (Collider hit in col)
            {
                if (hit.gameObject.CompareTag("Player"))
                {
                    PlayerBeenHit = hit.gameObject;
                }
            }

            while (cooldownfordoors > 0)
            {
                cooldownfordoors -= 3 * Time.deltaTime;

                if (cooldownfordoors >= 6f)
                {

                    col = Physics.OverlapBox(DoorHolder.ToArray()[index].position + DoorHolder.ToArray()[index].TransformDirection(new Vector3(0, -3, 0)), size, DoorHolder.ToArray()[index].rotation);

                    foreach (Collider hit in col)
                    {
                        if (hit.gameObject.CompareTag("Player"))
                        {
                            resetArray = 8.5f;

                            cooldownfordoors = 7f;
                            t = 0;
                            FixLerpTimer += 0.3f * Time.deltaTime;
                            PlayerBeenHit = hit.gameObject;
                            DoorHolder.ToArray()[index].localPosition = Vector3.Lerp(DoorHolder.ToArray()[index].localPosition, topos, FixLerpTimer / 1);
                        }
                        else
                        {
                            PlayerBeenHit = null;
                        }
                    }
                }

                if (PlayerBeenHit == null)
                {
                    resetArray -= 0.001f * Time.deltaTime;
                    FixLerpTimer = 0;
                    t += 0.1f * Time.deltaTime;
                    DoorHolder.ToArray()[index].localPosition = Vector3.Lerp(DoorHolder.ToArray()[index].localPosition, originalpos, t / 1);

                    i++;
                }

                yield return Waitforfixed;
            }

            DoorHolder.ToArray()[index].localPosition = originalpos;
            DoorHolder.ToArray()[index].parent.gameObject.layer = 10;
        }
    }

    public void Update()
    {
       
        if(Input.GetAxis("Mouse X") != 0  || Input.GetAxis("Mouse Y") != 0)
        {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);
        }
        

        if (resetArray <= 0 && DoorHolder.Count > 0)
        {
            DoorHolder.Clear();
        }

        if (resetArray > 0 && resetArray != 12.2f)
        {
            resetArray -= 3 * Time.deltaTime;
        }

        for (byte i = 0; i < enemies.Count; i++)
        {
            float enemyDistanceToPlayer = Vector3.Distance(player.position, enemies[i].position);
            if (enemyDistanceToPlayer < rayCastRange)
            {
                closeToAnEnemy = true;
            }
            else
            {
                closeToAnEnemy = false;
            }
        }

       

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out RaycastHit hit, rayCastRange) && (triggerDot == true || closeToAnEnemy == true))
        {
            runCount++;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayCastRange, Color.red);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                indicator.color = new Vector4(255, 0, 0, 255);
            }

        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayCastRange, Color.white);
            indicator.color = new Vector4(255, 255, 255, 255);

            triggerDot = false;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayCastRange) && Input.GetKeyDown(KeyCode.E) )
        {
            if (hit.collider.gameObject.layer == 10 && canopendoor == true)
            {
                whatdidithit = hit.transform;
                whatdidithit = whatdidithit.GetChild(0);
                if (clearancelevel >= int.Parse(whatdidithit.name.Substring(4, 1)))
                {
                    StartCoroutine(opendoor());
                }
            }
            if (hit.collider.CompareTag("pickableweapons"))
            {
                for(int i  = 0; i < meleesystem.transform.childCount - 1; i++)
                {
                    meleesystem.GetChild(i).gameObject.SetActive(false);
                }

                arrayindex = hit.collider.gameObject.GetComponent<ActiveOrNot>().test;
                
                  
                    weaponactive[arrayindex].canbeactive = true;
                meleesystem.GetChild(arrayindex).gameObject.SetActive(true);
                
                Destroy(hit.collider.gameObject);
               
            }
        }
    }
}