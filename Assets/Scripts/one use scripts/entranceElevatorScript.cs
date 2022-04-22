using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entranceElevatorScript : MonoBehaviour
{
    public CanvasGroup disableui;
    private Animator dooranim;
    private Animator headposanim;
    private Animator ElevatorDooranim;
    public GameObject lights;
    public Material Lightbulbs;
    // buttenpanel view -------------
    private Headpos poscam;
    private NewPlayer stopmoveing;
    private Transform camera;
    private Transform camplacement;
    private Transform savecampos;
    // private Renderer material;
    private bool isbuttensenabled = true;
    private bool backandforth = true;
    private Vector3 test1;
    private Vector3 test2;
    private float timelerp;
    // Start is called before the first frame update
    void Start()
    {
        poscam = transform.parent.GetComponent<Headpos>();
        stopmoveing =  GameObject.Find("Player").GetComponent<NewPlayer>();
        headposanim = GameObject.Find("Head").GetComponent<Animator>();
        ElevatorDooranim =  GameObject.Find("elevator").GetComponent<Animator>();
        dooranim = GameObject.Find("firexitdoor").GetComponent<Animator>();
        camera = GameObject.Find("MainCam").transform;
        camplacement = GameObject.Find("camplacement").transform;
        savecampos = GameObject.Find("fixcamposhead").transform;
        // anim = GameObject.Find("elevator").GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 3))
            {
                if (!hit.transform.CompareTag("Ground") && !hit.transform.CompareTag("Buttens"))
                {

                    hit.transform.gameObject.tag = "Ground";
                    isbuttensenabled = true;
                    poscam.enabled = false;
                    stopmoveing.enabled = false;
                    camera.GetComponent<Cameramove>().enabled = false;
                    headposanim.SetBool("walking", false);
                    headposanim.SetBool("running", false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    hit.transform.GetComponent<BoxCollider>().enabled = false;
                    //poscam.transform.position = savecampos.position;
                    StartCoroutine(selectbuttens(hit.transform.gameObject, camera.rotation));
                    
                    
                }
                else if (hit.transform.name == "door" && !dooranim.GetCurrentAnimatorStateInfo(0).IsName("doorhandleanimation"))
                {
                    dooranim.SetTrigger("handleanim");
                }
            }
            //else
            //{
            //   Debug.DrawRay(transform.position, transform.forward * -10 , Color.red ,10);
            //}
        }
    }

    IEnumerator selectbuttens(GameObject hitt, Quaternion camrot)
    {
        RaycastHit hit;

        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ra = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ra, out hit, 10))
                {
                    if (hit.transform.CompareTag("Buttens"))
                    {
                        Vector3 normalbuttenpos = new Vector3(hit.transform.localPosition.x, hit.transform.localPosition.y, hit.transform.localPosition.z);
                        Vector3 pressedbuttenpos = new Vector3(0.0217f, hit.transform.localPosition.y, hit.transform.localPosition.z);
                        
                        float tempfloat = 0;
                        while (tempfloat < 0.2f)
                        {
                            tempfloat += 0.4f * Time.deltaTime;
                            hit.transform.localPosition = Vector3.Lerp(hit.transform.localPosition, pressedbuttenpos, tempfloat / 1);

                            yield return wait;
                        }
                        tempfloat = 0;
                        while (tempfloat < 0.3f)
                        {
                            tempfloat += 0.4f * Time.deltaTime;
                            hit.transform.localPosition = Vector3.Lerp(hit.transform.localPosition, normalbuttenpos, tempfloat / 1);

                            yield return wait;
                        }
                        if(hit.transform.name == "butten 2 start game")
                        {
                            ElevatorDooranim.SetBool("closedoors", true);
                            StartCoroutine(shakecamera());
                            StartCoroutine(Lights());
                            
                            isbuttensenabled = false;
                            break;
                        }
                    }
                    
                }
            }
            timelerp += 0.25f * Time.deltaTime;
            disableui.alpha = Mathf.Lerp(disableui.alpha, 0, timelerp);
            poscam.transform.position = Vector3.Lerp(poscam.transform.position, camplacement.position, timelerp / 1.0f);
            camera.rotation = Quaternion.Lerp(camera.rotation, camplacement.rotation, timelerp / 1.0f);
            yield return wait;
            //camera.position = Vector3.Lerp(camera.position, camplacement.position, timelerp / 1.0f);
        }
        timelerp = 0;

        while (timelerp < 0.3)
        {
            timelerp += 0.5f * Time.deltaTime;
            disableui.alpha = Mathf.Lerp(disableui.alpha, 1, timelerp);
            //  camera.position = Vector3.Lerp(camera.position, savecampos.position, timelerp / 1.0f);
            camera.rotation = Quaternion.Lerp(camera.rotation, camrot, timelerp / 1.0f);
            poscam.transform.position = Vector3.Lerp(poscam.transform.position, savecampos.position, timelerp / 1.0f);
            yield return wait;

        }
        timelerp = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camera.GetComponent<Cameramove>().enabled = true;
        if(isbuttensenabled == true)
        {
          hitt.tag = "Untagged";
          hitt.transform.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            hitt.tag = "Ground";
            hitt.transform.GetComponent<BoxCollider>().enabled = true;
        }
        
        poscam.enabled = true;
        stopmoveing.enabled = true;
       
    }
    IEnumerator Lights()
    {
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitforsec = new WaitForSeconds(16);
        //WaitForSeconds waitfordelay = new WaitForSeconds(Random.Range(0.2f , 0.4f));
        yield return waitforsec;
        int i = 0;
        while (i <= 1000)
        {
            lights.SetActive(false);
            Lightbulbs.DisableKeyword("_EMISSION");
            //Lightbulbs.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            // Lightbulbs.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(Random.Range(0.2f , 0.4f));
            lights.SetActive(true);
            Lightbulbs.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            i++;
            yield return wait;
        }

        // yield return null;
    }
    IEnumerator shakecamera()
    {
        //GameObject ogrot = gameObject.transform.rotation;
        //  Quaternion rot = Quaternion.Euler(transform.rotation.x , transform.rotation.y, 7);
        //  Quaternion rot2 = Quaternion.Euler(transform.parent.localRotation.x, transform.parent.localRotation.y, -7);
        Vector3 xy = new Vector3(-0.15f, transform.localPosition.y, transform.localPosition.z);
        Vector3 xyminus = new Vector3(0.15f, transform.localPosition.y, transform.localPosition.z);
        Vector3 xy2 = new Vector3(transform.localPosition.x, 0.1f, transform.localPosition.z);
        Vector3 xyminus2 = new Vector3(transform.localPosition.x, -0.1f, transform.localPosition.z);
        float timer = 0.5f;
        int i = 0;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitforsec = new WaitForSeconds(14);
        yield return waitforsec;
        while (i <= 1000)
        {

            if (timer <= 1 && backandforth == true)
            {
                timer += 7f * Time.deltaTime;

            }
            else
            {
                timer -= 7f * Time.deltaTime;
            }
            if (timer >= 1)
            {
                backandforth = false;
            }
            if (timer <= 0)
            {
                backandforth = true;
            }
            test1 = Vector3.Lerp(xy, xyminus, timer / 1);
            test2 = Vector3.Lerp(xy2, xyminus2, timer / 1);

            transform.localPosition = Vector3.Lerp(test1, test2, timer / 1);
            //transform.parent.localRotation = Quaternion.Lerp(rot, rot2, timer / 1);
            i++;
            yield return wait;
        }


    }
  //  private void OnTriggerEnter(Collider other)
  //  {
  //      //if (other.gameObject.CompareTag("Player"))
  //      //{
  //      //    transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
  //      //    anim.SetBool("closedoors", true);
  //      //}
  //  }
}
