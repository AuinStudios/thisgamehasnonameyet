using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadBar : MonoBehaviour
{
    public IEnumerator load()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadSceneAsync(1);
    }
    public void Start()
    {
        StartCoroutine(load());
    }

}
