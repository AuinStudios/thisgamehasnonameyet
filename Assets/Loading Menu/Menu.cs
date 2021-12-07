using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class Menu : MonoBehaviour
{

    private bool lerpoutofscene = false;
    private bool settingslerpin = false;
    public void play()
    {
        SceneManager.LoadSceneAsync(2);
    }
    public void quit()
    {
        Application.Quit();
    }
    public IEnumerator Settings()
    {
       
        lerpoutofscene = true;
        yield return new WaitForSeconds(4f);
        
        GameObject.Find("Canvas").transform.GetChild(0).gameObject.SetActive(true);
        settingslerpin = true;
        GameObject.Find("Settings").SetActive(false);
    }
    public IEnumerator delay()
    {
        if (lerpoutofscene == true)
        {
            foreach (Transform i in GameObject.Find("Menu").transform)
            {
                if(i.name != "Menu")
                {
                 Vector3 y = new Vector3(i.transform.localPosition.x, 1000, i.transform.localPosition.z);
                i.localPosition = Vector3.Lerp(i.transform.localPosition, y, 0.3f * Time.deltaTime);
                }
                yield return new WaitForSeconds(0.4f);
            }
           
        }

    }
    public IEnumerator settingsdelay()
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
  
    public void setings()
    {

        StartCoroutine(Settings());
        foreach(Transform i in GameObject.Find("Menu").transform)
        {
            i.GetComponent<CanHover>().enabled = false;
            Color t = i.GetComponent<Image>().color;
            t.a = 0;
            i.GetComponent<Image>().color = t;
        }


    }
    private void LateUpdate()
    {
        StartCoroutine(delay());
        StartCoroutine(settingsdelay());
    }
}
