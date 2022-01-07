using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
public class Menu : MonoBehaviour
{
    // graphraycast to take gameobject information--
    private GraphicRaycaster graph;
    private PointerEventData data;
    private EventSystem eventsystem;
    // ints -----------------------------------------
    private int savekeyindex;
    public int GraphicisValue = 2;
    // bools -----------------------------------------
    private bool lerpoutofscene = false;
    private bool settingsLerpIn = false;
    private bool ComeBack = false, SubComeBack = false;
    private bool canclickbuttens = false;
    private bool isbindingbutten = true;
    // floats -----------------------------------------
    private float ButtenCooldown = 6;
    private Vector3 scaleupandownsave;
    // lists ------------------------------------------
    private string[] GraphicisTiersStrings = { "Low", "Medium", "High" };
    [SerializeField] private List<Vector3> savemenupos;
    [SerializeField] private List<Vector3> savesettingspos;
    [SerializeField] private List<Transform> sliders;
    public KeyCode[] savekey;
    // gameobjects -------------------------------------
    private GameObject transiton;
    private void Start()
    {
        graph = GameObject.Find("Canvas").GetComponent<GraphicRaycaster>();
        eventsystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

        foreach (Transform i in GameObject.Find("SettingsPanel").transform)
        {
            sliders.Add(i);

        }
        foreach (Transform i in GameObject.Find("Menu").transform)
        {
            savemenupos.Add(i.localPosition);
        }
        // save.Reverse();
        foreach (Transform i in GameObject.Find("SettingsOptions").transform)
        {
            savesettingspos.Add(i.localPosition);
        }

        HoldVariables data = SaveSystem.load();

        sliders[0].transform.GetChild(0).transform.GetComponent<Slider>().value = data.sens;
        sliders[0].transform.GetChild(1).transform.GetComponent<Slider>().value = data.fov;
        sliders[0].transform.GetChild(2).transform.GetComponent<Slider>().value = data.brightness;
        sliders[2].transform.GetChild(0).transform.GetComponent<Slider>().value = data.MasterVolume;
        GraphicisValue = data.graphicisvalue;
        sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[data.graphicisvalue];
        savekey = new KeyCode[7];
        savekey = data.keys;
        sliderischanged();
        transiton = GameObject.Find("Transition");
        GameObject.Find("SettingsOptions").SetActive(false);
        for (int i = 0; i < 7; i++)
        {
            sliders[3].GetChild(0).GetChild(0).GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text = data.keys[i].ToString();
        }
    }
    private void LateUpdate()
    {
        ButtenCooldown += 1 * Time.deltaTime;

        if (ButtenCooldown >= 6)
        {
            canclickbuttens = true;

        }
        else if (ButtenCooldown <= 6)
        {
            canclickbuttens = false;
        }
        StartCoroutine(MenuDelay());
    }

    public void sliderischanged()
    {
        // only one decimal -----------------------------------------------------------------------------------------------------------------------------------------------------------------
        sliders[0].transform.GetChild(2).transform.GetComponent<Slider>().value = Mathf.RoundToInt(sliders[0].transform.GetChild(2).transform.GetComponent<Slider>().value * 10.0f) * 0.1f;
        sliders[2].transform.GetChild(0).transform.GetComponent<Slider>().value = Mathf.RoundToInt(sliders[2].transform.GetChild(0).transform.GetComponent<Slider>().value * 10.0f) * 0.1f;
        // texts show value-------------------------------------------------------------------------------------------------------------------------------------------------------------------
        sliders[0].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sliders[0].transform.GetChild(0).transform.GetComponent<Slider>().value.ToString();
        sliders[0].transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sliders[0].transform.GetChild(1).transform.GetComponent<Slider>().value.ToString();
        sliders[0].transform.GetChild(2).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sliders[0].transform.GetChild(2).transform.GetComponent<Slider>().value.ToString();
        sliders[2].transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = sliders[2].transform.GetChild(0).transform.GetComponent<Slider>().value.ToString();
    }

    public void graphicisUp()
    {
        StopCoroutine(graphicisNumeratorUp());
        StartCoroutine(graphicisNumeratorUp());
    }
    public void graphicisDown()
    {
        StopCoroutine(graphicisNumeratorDown());
        StartCoroutine(graphicisNumeratorDown());
    }
    private void setings()
    {
        if (canclickbuttens == true)
        {
            StartCoroutine(Settings());
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                i.GetComponent<CanHover>().bol = false;

            }
            ButtenCooldown = 0;
        }
    }

    private void Back()
    {
        if (canclickbuttens == true)
        {
            SaveSystem.Savesystem();
            StartCoroutine(backtomenu());
            ButtenCooldown = 0;
        }

    }

    private void play()
    {
        if (canclickbuttens == true)
        {
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                i.GetComponent<CanHover>().bol = false;

            }
            SaveSystem.Savesystem();
            StartCoroutine(Transition());
            ButtenCooldown = 0;

        }

    }

    private void quit()
    {
        if (canclickbuttens == true)
        {
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                i.GetComponent<CanHover>().bol = false;

            }

            Application.Quit();
            ButtenCooldown = 0;
        }

    }

    public void selectletter()
    {
        if (isbindingbutten == true)
        {
            StartCoroutine(selectlettercoroutine());
        }
    }
    public IEnumerator selectlettercoroutine()
    {
        isbindingbutten = false;
        data = new PointerEventData(eventsystem);
        //Set the Pointer Event Position to that of the mouse position
        data.position = Input.mousePosition;
        //Create a list of Raycast Results
        List<RaycastResult> results = new List<RaycastResult>();

        //Raycast using the Graphics Raycaster and mouse click position
        graph.Raycast(data, results);
        WaitForFixedUpdate wait = new WaitForFixedUpdate();

        foreach (RaycastResult result in results)
        {
            if (result.gameObject.layer == 5)
            {
                scaleupandownsave = result.gameObject.transform.localScale;

                Vector2 a = result.gameObject.transform.localScale;
                Vector2 minus = new Vector2(0.01f, 0.01f);
                a -= minus;
                EventSystem.current.SetSelectedGameObject(result.gameObject);
                while (EventSystem.current.currentSelectedGameObject == result.gameObject)
                {

                    result.gameObject.transform.localScale = Vector2.Lerp(result.gameObject.transform.localScale, a, 3.2f * Time.fixedDeltaTime);

                    if (Input.anyKeyDown)
                    {
                        a += minus;
                        EventSystem.current.SetSelectedGameObject(null);
                        continue;
                    }

                    yield return wait;
                }

                HoldVariables d = SaveSystem.load();
                WaitForSeconds waitmore = new WaitForSeconds(0.5f);
                foreach (KeyCode kcode in Enum.GetValues(typeof(KeyCode)))
                {
                    for (int ii = 0; ii < 1; ii++)
                    {
                        if (Input.GetKeyDown(kcode))
                        {
                            savekeyindex = result.gameObject.transform.GetSiblingIndex();
                            savekey[savekeyindex] = kcode;
                            result.gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = kcode.ToString();
                            int i = 0;
                            while (i < 50)
                            {
                                result.gameObject.transform.localScale = Vector2.Lerp(result.gameObject.transform.localScale, scaleupandownsave, 3.5f * Time.fixedDeltaTime);
                                i++;

                                yield return wait;
                            }
                            result.gameObject.transform.localScale = scaleupandownsave;
                            isbindingbutten = true;

                        }

                    }

                }

            }

        }

    }
    public IEnumerator Transition()
    {
        WaitForFixedUpdate nocrash = new WaitForFixedUpdate();
        WaitForSeconds abitdelay = new WaitForSeconds(0.2f);
        while (transiton.transform.GetChild(1).localPosition.x <= 50f && transiton.transform.GetChild(2).localPosition.x >= 39)
        {
            Vector2 imagelogo = new Vector2(-28f, 40);
            transiton.transform.GetChild(0).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(0).transform.localPosition, imagelogo, 2.2f * Time.deltaTime);
            Vector2 firstranstionimagepos = new Vector2(51f, 17f);

            transiton.transform.GetChild(1).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(1).transform.localPosition, firstranstionimagepos, 3f * Time.deltaTime);

            Vector2 secoundtransitionimagepos = new Vector2(40.04361f, 5.493075f);

            transiton.transform.GetChild(2).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(2).transform.localPosition, secoundtransitionimagepos, 2.2f * Time.deltaTime);
            yield return nocrash;
        }
        AsyncOperation operation = SceneManager.LoadSceneAsync(2);
        yield return abitdelay;
        while (!operation.isDone)
        {
            transiton.transform.GetChild(0).Rotate(0, 0, 1);
            SaveSystem.Savesystem();
            yield return nocrash;
        }

    }
    public IEnumerator graphicisNumeratorUp()
    {
        if (GraphicisValue < 2)
        {
            HoldVariables data = SaveSystem.load();
            GraphicisValue += 1;
            sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[GraphicisValue];
            int WhileLoop = 0;
            WaitForEndOfFrame delay = new WaitForEndOfFrame();
            while (WhileLoop < 25)
            {
                Vector2 ArrowSize = new Vector2(0.447957f, 0.329668f);
                sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale, ArrowSize, 3f * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }

            while (WhileLoop < 50)
            {
                Vector2 ArrowSizeDown = new Vector2(0.397957f, 0.289668f);
                sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale, ArrowSizeDown, 3.5f * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }


            data.graphicisvalue = GraphicisValue;
        }

    }
    public IEnumerator graphicisNumeratorDown()
    {
        if (GraphicisValue > 0)
        {
            HoldVariables data = SaveSystem.load();
            GraphicisValue -= 1;
            sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[GraphicisValue];
            int WhileLoop = 0;
            WaitForEndOfFrame delay = new WaitForEndOfFrame();
            while (WhileLoop < 25)
            {
                Vector2 ArrowSize = new Vector2(0.447957f, 0.329668f);
                sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale, ArrowSize, 3 * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }
            while (WhileLoop < 50)
            {
                Vector2 ArrowSizeDown = new Vector2(0.397957f, 0.289668f);
                sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale, ArrowSizeDown, 3.5f * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }
            data.graphicisvalue = GraphicisValue;
        }
    }
    private IEnumerator backtomenu()
    {
        settingsLerpIn = false;
        SubComeBack = true;
        WaitForSeconds waitpls = new WaitForSeconds(4.20f);
        yield return waitpls;
        SubComeBack = false;
        GameObject.Find("Canvas").transform.GetChild(1).transform.gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Menu").transform.GetChild(2).transform.SetAsFirstSibling();
        GameObject.Find("Menu").transform.GetChild(1).transform.SetAsLastSibling();
        ComeBack = true;
        WaitForSeconds waitpls2 = new WaitForSeconds(2.5f);
        yield return waitpls2;
        foreach (Transform i in GameObject.Find("Menu").transform)
        {
            i.SetSiblingIndex(default);
        }

    }
    private IEnumerator Settings()
    {
        lerpoutofscene = true;
        WaitForSeconds delaysettings = new WaitForSeconds(2f);
        yield return delaysettings;
        lerpoutofscene = false;
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        settingsLerpIn = true;
        GameObject.Find("Menu").SetActive(false);
    }
    private IEnumerator MenuDelay()
    {

        if (settingsLerpIn == true)
        {
            //  ui effect ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            WaitForSeconds waituntllmainoptionsdone = new WaitForSeconds(0.4f);
            GameObject.Find("SettingsOptions").transform.GetChild(4).position = Vector2.Lerp(GameObject.Find("SettingsOptions").transform.GetChild(4).position, GameObject.Find("Canvas").transform.position, 2.2f * Time.fixedDeltaTime);
            yield return waituntllmainoptionsdone;
            WaitForSeconds waitfornextpanel = new WaitForSeconds(0.6f);
            foreach (Transform t in GameObject.Find("SettingsOptions").transform)
            {

                if (t.name != "SettingsOptions" && t.name != "SettingsPanel")
                {
                    Vector2 r = new Vector2(t.localPosition.x, 753);

                    t.localPosition = Vector2.Lerp(t.localPosition, r, 2f * Time.fixedDeltaTime);
                }

                yield return waitfornextpanel;

            }
            settingsLerpIn = false;
        }

        if (SubComeBack == true)
        {
            WaitForSeconds wait = new WaitForSeconds(0.6f);
            int B = 0;
            foreach (Transform i in GameObject.Find("SettingsOptions").transform)
            {
                i.localPosition = Vector2.Lerp(i.localPosition, savesettingspos[B], 1f * Time.fixedDeltaTime);
                B++;
                yield return wait;
            }
            SubComeBack = false;
        }

        if (lerpoutofscene == true)
        {
            WaitForSeconds wait = new WaitForSeconds(0.4f);
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                if (i.name != "Menu")
                {
                    Vector2 y = new Vector2(i.transform.localPosition.x, 1000);
                    i.localPosition = Vector2.Lerp(i.transform.localPosition, y, 0.6f * Time.deltaTime);
                }
                yield return wait;
            }
            lerpoutofscene = false;
        }

        if (ComeBack == true)
        {
            WaitForSeconds wait = new WaitForSeconds(0.4f);
            int T = 2;
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                ButtenCooldown = 5;
                i.localPosition = Vector2.Lerp(i.localPosition, savemenupos[T], 2f * Time.deltaTime);
                T--;
                yield return wait;
            }
            ComeBack = false;
        }
    }
}