using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{
    // bools -----------------------------------------
    private bool lerpoutofscene = false;
    private bool settingslerpin = false;
    private bool ComeBack = false, SubComeBack = false;
    private bool canclickbuttens = false;
    public GameObject test;
    // floats -----------------------------------------
    private float ButtenCooldown = 6;
    private float howmanytimes;
    // lists ------------------------------------------
    public List<Vector3> save;
    public List<Vector3> savesettingspos;

    private void Start()
    {
        HoldVariables data =   SaveSystem.load();
        GameObject.Find("sensitivity slider").transform.GetComponent<Slider>().value = data.sens;
        GameObject.Find("Fov Slider").transform.GetComponent<Slider>().value = data.fov;
       
        foreach (Transform i in GameObject.Find("Menu").transform)
        {
            save.Add(i.localPosition);
        }
        
        foreach (Transform i in GameObject.Find("SettingsOptions").transform)
        {
            savesettingspos.Add(i.localPosition);
        }
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
        StartCoroutine(delay());
        StartCoroutine(settingsdelay());
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

            
            SceneManager.LoadSceneAsync(2);
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

    private IEnumerator backtomenu()
    {
         settingslerpin = false;
        SubComeBack = true;
        yield return new WaitForSeconds(4.2f);
        SubComeBack = false;
        GameObject.Find("Canvas").transform.GetChild(1).transform.gameObject.SetActive(true);
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(false);
        ComeBack = true;
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
    private IEnumerator delay()
    {
        if (lerpoutofscene == true)
        {
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                if(i.name != "Menu")
                {
                 Vector3 y = new Vector3(i.transform.localPosition.x, 1000, i.transform.localPosition.z);
                i.localPosition = Vector3.Lerp(i.transform.localPosition, y, 0.6f * Time.deltaTime);
                }
                yield return new WaitForSeconds(0.4f);
            }
           
        }
        if (SubComeBack == true)
        {
            int B = 0;
            foreach(Transform i in GameObject.Find("SettingsOptions").transform)
            {
                i.localPosition = Vector3.Lerp(i.localPosition, savesettingspos[B], 1f* Time.deltaTime);
                B++;
                yield return new WaitForSeconds(0.6f);
            }
        }
        if(ComeBack == true)
        {
            int T = 0;
            foreach (Transform i in GameObject.Find("Menu").transform)
            {

                i.localPosition = Vector3.Lerp(i.localPosition , save[T] , 2f *Time.deltaTime);
                T++;
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForSeconds(4);
            ComeBack = false;
        }
    }
    
    private IEnumerator settingsdelay()
    {
        if( settingslerpin == true )
        {
            GameObject.Find("SettingsOptions").transform.GetChild(4).position = Vector3.Lerp (GameObject.Find("SettingsOptions").transform.GetChild(4).position, GameObject.Find("Canvas").transform.position, 3 * Time.deltaTime);
            foreach(Transform t  in GameObject.Find("SettingsOptions").transform)
            {
                if(t.name != "SettingsOptions" && t.name != "SettingsPanel")
                {
                    Vector3 r = new Vector3(t.localPosition.x , 725, t.localPosition.z);
                    t.localPosition = Vector3.Lerp(t.localPosition, r, 3 * Time.deltaTime);
                }
                yield return new WaitForSeconds(0.6f);
            }
        }
        
    }
}
