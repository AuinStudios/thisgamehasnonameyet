using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class entranceElevatorScript : MonoBehaviour
{
    [Header("UI properties")]
    public CanvasGroup disableUI;

    [Header("Animators")]
    [SerializeField]
    private Animator fireExitDoorAnim;
    [SerializeField]
    private Animator headPosAnim;
    [SerializeField]
    private Animator ElevatorDoorMove;
    
    [Header("Lighting")]
    [SerializeField]
    private GameObject transiton;
    [SerializeField]
    private GameObject darkEffect;
    
    public GameObject lights;
    public Material Lightbulbs;
    
    [Header("Camera properties")]
    [SerializeField]
    private NewPlayer stopMoving;
    [SerializeField]
    private Transform camPlacement;
    [SerializeField]
    private Transform saveCamPos;
    
    private Headpos posCam;
    private Transform mainCamera;
    
    // General unsorted variables --------------------------------

    private bool areButtonsEnabled = true;
    private bool isBackAndForth = true;
    private Vector3 test1;
    private Vector3 test2;
    private float timeLerp;

    // Static/readonly variables --------------------------------

    void Start()
    {
        posCam = transform.parent.GetComponent<Headpos>();
        
        mainCamera = Camera.main.transform;
        
        darkEffect.SetActive(false);

        #region For reminding reasons
        /* For reminder reasons.
         * stopmoveing = GameObject.Find("Player").GetComponent<NewPlayer>();
         * headPosAnim = GameObject.Find("Head").GetComponent<Animator>();
         * ElevatorDooranim = GameObject.Find("elevator").GetComponent<Animator>();
         * dooranim = GameObject.Find("firexitdoor").GetComponent<Animator>();
         * camPlacement = GameObject.Find("camplacement").transform;
         * saveCamPos = GameObject.Find("fixcamposhead").transform;
         * transiton = GameObject.Find("Transition");
         * darkEffect = GameObject.Find("Box Volume");
         * anim = GameObject.Find("elevator").GetComponent<Animator>();
         */
        #endregion

    }

    private void Awake()
    {
            lights.SetActive(false);
        Lightbulbs.DisableKeyword("_EMISSION");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3))
            {
                if (!hit.transform.CompareTag("Ground") && !hit.transform.CompareTag("Buttens"))
                {

                    hit.transform.gameObject.tag = "Ground";
                    areButtonsEnabled = true;
                    posCam.enabled = false;
                    stopMoving.enabled = false;
                    mainCamera.GetComponent<Cameramove>().enabled = false;
                    headPosAnim.SetBool("walking", false);
                    headPosAnim.SetBool("running", false);
                    Cursor.visible = true;
                    Cursor.lockState = CursorLockMode.None;
                    hit.transform.GetComponent<BoxCollider>().enabled = false;
                    //poscam.transform.position = savecampos.position;
                    StartCoroutine(SelectButtons(hit.transform.gameObject, mainCamera.rotation));


                }
                else if (hit.transform.name == "door" && !fireExitDoorAnim.GetCurrentAnimatorStateInfo(0).IsName("doorhandleanimation"))
                {
                    fireExitDoorAnim.SetTrigger("handleanim");
                }
                else if (hit.transform.name == "emergencylightswtich")
                {
                    lights.SetActive(true);
                    Lightbulbs.EnableKeyword("_EMISSION");
                   
                    ElevatorDoorMove.SetTrigger("OpenDoors");
                    hit.transform.name = "lightswtichon";
                    hit.transform.GetChild(1).localRotation = Quaternion.Euler(-90.879f,90 ,-90 );
                }
            }

            //else
            //{
            //   Debug.DrawRay(transform.position, transform.forward * -10 , Color.red ,10);
            //}
        }
    }

    private IEnumerator SelectButtons(GameObject hitt, Quaternion camrot)
    {
        //RaycastHit hit;

        //WaitForFixedUpdate wait = new WaitForFixedUpdate();
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Ray ra = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ra, out RaycastHit hit, 10))
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

                            yield return new WaitForFixedUpdate() ;
                        }
                        tempfloat = 0;
                        while (tempfloat < 0.3f)
                        {
                            tempfloat += 0.4f * Time.deltaTime;
                            hit.transform.localPosition = Vector3.Lerp(hit.transform.localPosition, normalbuttenpos, tempfloat / 1);

                            yield return new WaitForFixedUpdate() ;
                        }
                        if (hit.transform.name == "butten 2 start game")
                        {
                            ElevatorDoorMove.SetBool("closedoors", true);
                            StartCoroutine(shakecamera());
                            StartCoroutine(Lights());

                            areButtonsEnabled = false;
                            yield return new WaitForSeconds(0.4f);
                            break;
                        }
                    }

                }
            }
            timeLerp += 0.25f * Time.deltaTime;
            disableUI.alpha = Mathf.Lerp(disableUI.alpha, 0, timeLerp);
            posCam.transform.position = Vector3.Lerp(posCam.transform.position, camPlacement.position, timeLerp / 1.0f);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, camPlacement.rotation, timeLerp / 1.0f);
            yield return new WaitForFixedUpdate() ;
            //camera.position = Vector3.Lerp(camera.position, camplacement.position, timelerp / 1.0f);
        }
        timeLerp = 0;

        while (timeLerp < 0.3)
        {
            timeLerp += 0.5f * Time.deltaTime;
            disableUI.alpha = Mathf.Lerp(disableUI.alpha, 1, timeLerp);
            //  camera.position = Vector3.Lerp(camera.position, savecampos.position, timelerp / 1.0f);
            mainCamera.rotation = Quaternion.Lerp(mainCamera.rotation, camrot, timeLerp / 1.0f);
            posCam.transform.position = Vector3.Lerp(posCam.transform.position, saveCamPos.position, timeLerp / 1.0f);
            yield return new WaitForFixedUpdate() ;

        }
        timeLerp = 0;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mainCamera.GetComponent<Cameramove>().enabled = true;
        if (areButtonsEnabled == true)
        {
            hitt.tag = "Untagged";
            hitt.transform.GetComponent<BoxCollider>().enabled = true;
        }
        else
        {
            hitt.tag = "Ground";
            hitt.transform.GetComponent<BoxCollider>().enabled = true;
        }

        posCam.enabled = true;
        stopMoving.enabled = true;

    }
    IEnumerator Lights()
    {
        //WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitforsec = new WaitForSeconds(16);
        //WaitForSeconds waitfordelay = new WaitForSeconds(Random.Range(0.2f , 0.4f));
        yield return waitforsec;
        int i = 0;
        while (i <= 1000)
        {
            lights.SetActive(false);
            Lightbulbs.DisableKeyword("_EMISSION");
            darkEffect.SetActive(true);
            //Lightbulbs.globalIlluminationFlags = MaterialGlobalIlluminationFlags.EmissiveIsBlack;
            // Lightbulbs.SetColor("_EmissionColor", Color.black);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            lights.SetActive(true);
            Lightbulbs.EnableKeyword("_EMISSION");
            darkEffect.SetActive(false);
            yield return new WaitForSeconds(Random.Range(0.2f, 0.4f));
            i++;
            yield return new WaitForFixedUpdate();
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
        //WaitForFixedUpdate wait = new WaitForFixedUpdate();
        WaitForSeconds waitforsec = new WaitForSeconds(14);
        yield return waitforsec;
        while (i <= 1000)
        {

            if (timer <= 1 && isBackAndForth == true)
            {
                timer += 7f * Time.deltaTime;

            }
            else
            {
                timer -= 7f * Time.deltaTime;
            }
            if (timer >= 1)
            {
                isBackAndForth = false;
            }
            if (timer <= 0)
            {
                isBackAndForth = true;
            }
            test1 = Vector3.Lerp(xy, xyminus, timer / 1);
            test2 = Vector3.Lerp(xy2, xyminus2, timer / 1);

            transform.localPosition = Vector3.Lerp(test1, test2, timer / 1);
            //transform.parent.localRotation = Quaternion.Lerp(rot, rot2, timer / 1);
            i++;
            yield return new WaitForFixedUpdate();
        }
        StartCoroutine(Transition());

    }


    public IEnumerator Transition()
    {
        WaitForSeconds abitdelay = new WaitForSeconds(0.2f);
        while (transiton.transform.GetChild(1).localPosition.x <= 50f && transiton.transform.GetChild(2).localPosition.x >= 39)
        {
            Vector2 imagelogo = new Vector2(-28f, 40);
            transiton.transform.GetChild(0).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(0).transform.localPosition, imagelogo, 2.2f * Time.deltaTime);
            Vector2 firstranstionimagepos = new Vector2(51f, 17f);

            transiton.transform.GetChild(1).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(1).transform.localPosition, firstranstionimagepos, 3f * Time.deltaTime);

            Vector2 secoundtransitionimagepos = new Vector2(40.04361f, 5.493075f);

            transiton.transform.GetChild(2).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(2).transform.localPosition, secoundtransitionimagepos, 2.2f * Time.deltaTime);
            yield return new WaitForFixedUpdate();
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(3);
        yield return abitdelay;
        while (!operation.isDone)
        {
            transiton.transform.GetChild(0).Rotate(0, 0, 1);
            SaveSystem.Savesystem();
            yield return new WaitForFixedUpdate();
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
}

