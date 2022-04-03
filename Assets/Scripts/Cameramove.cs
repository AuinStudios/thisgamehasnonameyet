using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameramove : MonoBehaviour
{
    // sound effects --------------------------------------------
    AudioSource sound;

    // Bools -------------------------------------------------------
    public bool triggerDot = false;
    private bool closeToAnEnemy = false;

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

    // transforms and gameobjects -----------------------------------
    private Transform orientation;
    public List<Transform> ithitholder = new List<Transform>();
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
            int index = ithitholder.Count;

            ithitholder.Add(whatdidithit);
            ithitholder.ToArray()[index].parent.gameObject.layer = 0;
            GameObject PlayerBeenHit = null;
            Vector3 size = new Vector3(ithitholder.ToArray()[index].lossyScale.x * 7, ithitholder.ToArray()[index].lossyScale.y, ithitholder.ToArray()[index].lossyScale.z);
            Vector3 originalpos = (ithitholder.ToArray()[index].localPosition);
            Vector3 topos = new Vector3(ithitholder.ToArray()[index].localPosition.x, ithitholder.ToArray()[index].localPosition.y + 5f, ithitholder.ToArray()[index].localPosition.z);
            sound.PlayDelayed(0.2f);

            while (i < 100)
            {
                t += 0.1f * Time.deltaTime;
                cooldownfordoors -= 3 * Time.deltaTime;
                ithitholder.ToArray()[index].localPosition = Vector3.Lerp(ithitholder.ToArray()[index].localPosition, topos, t / 1);

                i++;
                col = Physics.OverlapBox(ithitholder.ToArray()[index].position + ithitholder.ToArray()[index].TransformDirection(new Vector3(0, -3, 0)), size, ithitholder.ToArray()[index].rotation);

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

                    col = Physics.OverlapBox(ithitholder.ToArray()[index].position + ithitholder.ToArray()[index].TransformDirection(new Vector3(0, -3, 0)), size, ithitholder.ToArray()[index].rotation);

                    foreach (Collider hit in col)
                    {
                        if (hit.gameObject.CompareTag("Player"))
                        {
                            resetArray = 8.5f;

                            cooldownfordoors = 7f;
                            t = 0;
                            FixLerpTimer += 0.3f * Time.deltaTime;
                            PlayerBeenHit = hit.gameObject;
                            ithitholder.ToArray()[index].localPosition = Vector3.Lerp(ithitholder.ToArray()[index].localPosition, topos, FixLerpTimer / 1);
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
                    ithitholder.ToArray()[index].localPosition = Vector3.Lerp(ithitholder.ToArray()[index].localPosition, originalpos, t / 1);

                    i++;
                }

                yield return Waitforfixed;
            }

            ithitholder.ToArray()[index].localPosition = originalpos;
            ithitholder.ToArray()[index].parent.gameObject.layer = 10;
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
        

        if (resetArray <= 0 && ithitholder.Count > 0)
        {
            ithitholder.Clear();
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

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayCastRange) && (triggerDot == true || closeToAnEnemy == true))
        {
            runCount++;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayCastRange, Color.red);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
                indicator.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);
            }

        }
        else
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayCastRange, Color.white);
            indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);

            triggerDot = false;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayCastRange) && Input.GetKeyDown(KeyCode.E))
        {
            if (hit.collider.gameObject.layer == 10)
            {
                whatdidithit = hit.transform;
                whatdidithit = whatdidithit.GetChild(0);
                if (clearancelevel >= int.Parse(whatdidithit.name.Substring(4, 1)))
                {
                    StartCoroutine(opendoor());
                }
            }
        }
    }
}