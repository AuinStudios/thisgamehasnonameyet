using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameramove : MonoBehaviour
{
    public Transform orientation;
    public Image indicator;
    private float xRotation = 0f;
    public float sensitivity = 50f;
    private float sensMultiplier = 1f;
    private  float raycastrange = 4;
    public float cooldownfordoors = 0;
    [Header("Debug")]
    public int runCount = 0;
    public  bool triggerDot = false;
    private bool closeToAnEnemy = false;
    public List<Transform> enemies = new List<Transform>();
    public Transform player;
    private Transform whatdidithit;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        HoldVariables data  = SaveSystem.load();
        Camera.main.fieldOfView = data.fov;
        sensitivity = data.sens;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private IEnumerator opendoor()
    {
        for(int g = 0; g < 1; g++)
        {
          int i = 0;
        float t = 0;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitsecs = new WaitForSeconds(0.4f);
        Vector3 originalpos = (whatdidithit.localPosition);
        Vector3 topos = new Vector3(whatdidithit.localPosition.x, whatdidithit.localPosition.y + 5f, whatdidithit.localPosition.z);
        while (i < 100)
        {
            t += 0.1f *Time.deltaTime;
                Debug.Log(t);
            whatdidithit.localPosition = Vector3.Lerp(whatdidithit.localPosition, topos, t / 1 );
            i++;
            yield return wait;
        }
        yield return waitsecs;
        t = 0;
           
        while (i < 200)
        {

            t +=  0.1f * Time.deltaTime;
                Debug.Log(t);
                whatdidithit.localPosition = Vector3.Lerp(whatdidithit.localPosition, originalpos, t / 1);
            i++;
            yield return wait;
        }
        whatdidithit.localPosition = originalpos;
        }
       
    }
    public void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);
        if (cooldownfordoors != 0)
        {
            cooldownfordoors -= 3 * Time.deltaTime;
        }
         if( cooldownfordoors < 0)
        {
            cooldownfordoors = 0;
        }

        for (byte i = 0; i < enemies.Count; i++)
        {
            float enemyDistanceToPlayer = Vector3.Distance(player.position, enemies[i].position);
            if (enemyDistanceToPlayer < raycastrange)
            {
                closeToAnEnemy = true;
            }
            else
            {
                closeToAnEnemy = false;
            }
        }
        
        // for(byte i = 0; i < Doors.Count; i++)
        // {
        //     float DoorDistanceToPlayer = Vector3.Distance(player.position,Doors[i].position);
        //    if (DoorDistanceToPlayer < raycastrange)
        //    {
        //        closetoadoor = true;
        //    }
        //    else
        //    {
        //        closetoadoor = false;
        //    }
        // }
            
        
        //makes the   camera stop moveing when the game  is paused
        RaycastHit hit;
      
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,raycastrange ) && ( triggerDot == true || closeToAnEnemy == true))
        {
            runCount++;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.red);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
             indicator.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.white);
                indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
            }
               
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.white);
            indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);

            triggerDot = false;
        }

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastrange) && cooldownfordoors == 0 && Input.GetKeyDown(KeyCode.E))
        {
          
            if (hit.collider.gameObject.layer == 10 && cooldownfordoors == 0 )
            {
                
                cooldownfordoors = 13;
                whatdidithit = hit.collider.gameObject.transform;
                whatdidithit = whatdidithit.GetChild(0);
               
                StartCoroutine(opendoor());
               

            }
        }
    }
}
