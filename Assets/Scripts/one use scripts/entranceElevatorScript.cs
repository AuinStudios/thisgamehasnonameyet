using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entranceElevatorScript : MonoBehaviour
{
    public Animator anim;
    private bool backandforth = true;
    private Vector3 test1;
    private Vector3 test2;
    // Start is called before the first frame update
    void Start()
    {
       // anim = GameObject.Find("elevator").GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 2))
            {
                if (!hit.transform.CompareTag("Ground"))
                {
                    hit.transform.gameObject.tag = "Ground";
                    anim.SetBool("closedoors", true);
                    StartCoroutine(shakecamera());
                }
            }
            //else
            //{
            //   Debug.DrawRay(transform.position, transform.forward * -10 , Color.red ,10);
            //}
        }
    }
    IEnumerator shakecamera()
    {
        //GameObject ogrot = gameObject.transform.rotation;
        //  Quaternion rot = Quaternion.Euler(transform.rotation.x , transform.rotation.y, 7);
        //  Quaternion rot2 = Quaternion.Euler(transform.parent.localRotation.x, transform.parent.localRotation.y, -7);
        Vector3 xy = new Vector3(-0.15f, transform.localPosition.y, transform.localPosition.z);
        Vector3 xyminus = new Vector3(0.15f, transform.localPosition.y, transform.localPosition.z);
        Vector3 xy2 = new Vector3(transform.localPosition.x, 0.1f , transform.localPosition.z);
        Vector3 xyminus2 = new Vector3(transform.localPosition.x, -0.1f , transform.localPosition.z);
        float timer = 0.5f;
        int i = 0;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitforsec = new WaitForSeconds(10);
        yield return waitforsec;
        while(i <= 1000)
        {
            if(timer <= 1 &&  backandforth == true)
            {
             timer += 7f * Time.deltaTime;

            }
            else 
            {
                timer -= 7f * Time.deltaTime;
            }
            if(timer >= 1)
            {
                backandforth = false;
            }
            if(timer <= 0)
            {
                backandforth = true;
            }
            test1 = Vector3.Lerp(xy, xyminus, timer / 1);
            test2 = Vector3.Lerp(xy2, xyminus2, timer / 1); 
           
           transform.localPosition = Vector3.Lerp(test1,test2, timer / 1)   ;
            //transform.parent.localRotation = Quaternion.Lerp(rot, rot2, timer / 1);
            i++;
                yield return wait;
        }
       
      
    }
    private void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.CompareTag("Player"))
        //{
        //    transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
        //    anim.SetBool("closedoors", true);
        //}
    }
}
