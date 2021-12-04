using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class velocitytoshape : MonoBehaviour
{
    
    public float speed;
    // Update is called once per frame
    public IEnumerator waitpls()
    {

        yield return new WaitForSeconds(2);

        gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
       transform.localScale = new Vector3(transform.localScale.x, gameObject.GetComponent<Rigidbody2D>().velocity.y  *speed  * Time.deltaTime, transform.localScale.z);
    }
    public void FixedUpdate()
    {

        StartCoroutine(waitpls());
    }
}
