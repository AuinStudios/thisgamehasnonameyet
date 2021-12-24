using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
  private float speed = 0.1f;
    private GameObject fall;
    private Rigidbody2D fallrigid;
    public void Start()
    {
        fall = GameObject.Find("goop drop");
        fallrigid = fall.GetComponent<Rigidbody2D>();
        StartCoroutine(load());
    }
      public IEnumerator load()
      {
        WaitForSeconds wait = new WaitForSeconds(4f);
        yield return wait;
        SceneManager.LoadSceneAsync(1);
      }
    // Update is called once per frame
    public IEnumerator waitpls()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        yield return wait;

       fallrigid.bodyType = RigidbodyType2D.Dynamic;
        fall.transform.localScale = new Vector3(fall.transform.localScale.x, fallrigid.velocity.y * speed * Time.deltaTime, fall.transform.localScale.z);
    }
    public void FixedUpdate()
    {
        StartCoroutine(waitpls());
    }
}
