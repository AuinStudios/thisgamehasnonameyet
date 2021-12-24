using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class Menu : MonoBehaviour
{
    // bools -----------------------------------------
    private bool lerpoutofscene = false;
    private bool settingslerpin = false;
    private bool ComeBack = false, SubComeBack = false;
    private bool canclickbuttens = false;
    public float damp;
    // floats -----------------------------------------
    private float ButtenCooldown = 6;
   public int graphicisvalue = 2;
    // lists ------------------------------------------
    private string[] GraphicisTiersStrings = { "Low", "Medium", "High" };
    public List<Vector3> save;
    public List<Vector3> savesettingspos;
    public List<Transform> sliders;
    // gameobjects -------------------------------------
    private GameObject transiton;
    private void Start()
    {
        foreach (Transform i in GameObject.Find("SettingsPanel").transform)
        {
            sliders.Add(i);
      
        }
        foreach (Transform i in GameObject.Find("Menu").transform)
        {
            save.Add(i.localPosition);
        }
       // save.Reverse();
        foreach (Transform i in GameObject.Find("SettingsOptions").transform)
        {
            savesettingspos.Add(i.localPosition);
        }
        HoldVariables data =   SaveSystem.load();
        
        sliders[0].transform.GetChild(0).transform.GetComponent<Slider>().value = data.sens;
        sliders[0].transform.GetChild(1).transform.GetComponent<Slider>().value = data.fov;
        sliders[0].transform.GetChild(2).transform.GetComponent<Slider>().value = data.brightness;
        sliders[2].transform.GetChild(0).transform.GetComponent<Slider>().value = data.MasterVolume;
        graphicisvalue = data.graphicisvalue;
        sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[data.graphicisvalue];
        

        sliderischanged();
        transiton = GameObject.Find("Transition");
        GameObject.Find("SettingsOptions").SetActive(false);
    } 
    private void LateUpdate()
    {
        ButtenCooldown += 1 * Time.deltaTime;
                  
        if (ButtenCooldown >= 6)
        {
            canclickbuttens = true;
        }
        else if(ButtenCooldown <= 6)
        {
            canclickbuttens = false;
        }
        StartCoroutine(Menudelay());
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
           foreach(Transform i in GameObject.Find("Menu").transform)
           {
                i.GetComponent<CanHover>().bol = false;
            
           }
            ButtenCooldown = 0;
        }
    }

    private void Back()
    {
        if(canclickbuttens == true)
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
            StartCoroutine(Transition());
            ButtenCooldown = 0;
            SaveSystem.Savesystem();
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

               transiton.transform.GetChild(2).transform.localPosition = Vector2.Lerp(transiton.transform.GetChild(2).transform.localPosition, secoundtransitionimagepos, 2.2f* Time.deltaTime);
            yield return nocrash;
        }
         AsyncOperation operation = SceneManager.LoadSceneAsync(2);
         yield return abitdelay;
         while (!operation.isDone )
         {
           transiton.transform.GetChild(0).Rotate(0, 0, 1);  
              SaveSystem.Savesystem();
            yield return nocrash;
         } 
      
    }
    public IEnumerator graphicisNumeratorUp()
     {
        if (graphicisvalue < 2)
        {
              HoldVariables data = SaveSystem.load();
          graphicisvalue += 1;
          sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[graphicisvalue];
            int WhileLoop = 0;
            WaitForEndOfFrame delay = new WaitForEndOfFrame();
            while(WhileLoop < 25)
            {
               Vector2 ArrowSize = new Vector2(0.447957f, 0.329668f);
               sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale, ArrowSize, 3f *Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }
         
              while(WhileLoop < 50)
             {
                Vector2 ArrowSizeDown = new Vector2(0.397957f, 0.289668f);
                sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(1).transform.localScale, ArrowSizeDown, 3.5f * Time.deltaTime);
                yield return delay;
                WhileLoop++;
             }
            
           
          data.graphicisvalue = graphicisvalue;
        }
       
     }
     public IEnumerator graphicisNumeratorDown()
    {
        if(graphicisvalue > 0)
        {
            HoldVariables data = SaveSystem.load();
            graphicisvalue -= 1;
            sliders[1].transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().text = GraphicisTiersStrings[graphicisvalue];
            int WhileLoop = 0;
            WaitForEndOfFrame delay = new WaitForEndOfFrame();
            while (WhileLoop < 25)
            {
                Vector2 ArrowSize = new Vector2(0.447957f, 0.329668f);
                sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale, ArrowSize, 3 * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }
            while(WhileLoop < 50)
            {
                Vector2 ArrowSizeDown = new Vector2(0.397957f, 0.289668f);
                sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale = Vector2.Lerp(sliders[1].transform.GetChild(0).transform.GetChild(0).transform.localScale, ArrowSizeDown, 3.5f * Time.deltaTime);
                yield return delay;
                WhileLoop++;
            }
            data.graphicisvalue = graphicisvalue;
        }
     }
    private IEnumerator backtomenu()
    {
         settingslerpin = false;
        SubComeBack = true;
        yield return new WaitForSeconds(4.2f);
        SubComeBack = false;
        GameObject.Find("Canvas").transform.GetChild(1).transform.gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        GameObject.Find("Menu").transform.GetChild(2).transform.SetAsFirstSibling() ;
        GameObject.Find("Menu").transform.GetChild(1).transform.SetAsLastSibling();
        ComeBack = true;
        yield return new WaitForSeconds(2.5f);
        foreach(Transform i in GameObject.Find("Menu").transform)
        {
           i.SetSiblingIndex(default);
        }
       

    }
    private IEnumerator Settings()
    {
        lerpoutofscene = true;
        yield return new WaitForSeconds(2f);
        lerpoutofscene = false;
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        settingslerpin = true;
        GameObject.Find("Menu").SetActive(false);
    }
    private IEnumerator Menudelay()
    {
       
        if( settingslerpin == true )
        {
            //  ui effect ---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            
            GameObject.Find("SettingsOptions").transform.GetChild(4).position = Vector2.Lerp(GameObject.Find("SettingsOptions").transform.GetChild(4).position, GameObject.Find("Canvas").transform.position, 2.2f * Time.fixedDeltaTime );
            yield return new WaitForSeconds(0.4f);
            foreach(Transform t  in GameObject.Find("SettingsOptions").transform)
            {
               
                if(t.name != "SettingsOptions" && t.name != "SettingsPanel")
                {
                    Vector2 r = new Vector2(t.localPosition.x , 753);
                  
                    t.localPosition = Vector2.Lerp(t.localPosition, r, 2f * Time.fixedDeltaTime);
                }

                yield return new WaitForSeconds(0.6f);
                
            }
            settingslerpin = false;
        }

        if (SubComeBack == true)
        {
            int B = 0;
            foreach(Transform i in GameObject.Find("SettingsOptions").transform)
            {
                i.localPosition = Vector2.Lerp(i.localPosition, savesettingspos[B], 1f* Time.fixedDeltaTime);
                B++;
                yield return new WaitForSeconds(0.6f);
            }
            SubComeBack = false;
        }

        if (lerpoutofscene == true)
        {
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                if(i.name != "Menu")
                {
                 Vector2 y = new Vector2(i.transform.localPosition.x, 1000);
                i.localPosition = Vector2.Lerp(i.transform.localPosition, y, 0.6f * Time.deltaTime);
                }
                yield return new WaitForSeconds(0.4f);
            }
            lerpoutofscene = false;
        }
       
        if(ComeBack == true)
        {
            int T = 2;
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                ButtenCooldown = 5;
                i.localPosition = Vector2.Lerp(i.localPosition , save[T] , 2f *Time.deltaTime);
                T--;
                yield return new WaitForSeconds(0.4f);
            }
                ComeBack = false;
        }
    }
}
