using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTest : MonoBehaviour
{
    public Transform target;
    public List<Transform> trans;
    public float distance = 10;
    public Transform pointsmanager;
    //private int pointstate = 1;
    private Rigidbody rigidBody;
    private float speed = 6;
    // Start is called before the first frame update
    void Start()
    {
        foreach(Transform i in pointsmanager)
        {
            trans.Add(i);
        }
        rigidBody = gameObject.GetComponent<Rigidbody>();
        StartCoroutine(wander());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private IEnumerator wander()
    {
        // int ii = 0;
        int i = 0;
        //float distance = 0;
        WaitForFixedUpdate wait = new WaitForFixedUpdate();
        for(i = 0; i  < trans.Count;i++)
        {

           
                
            
            while (distance >= 1)
            {
                distance = Vector3.Distance(transform.position, trans.ToArray()[i].position);
                Vector3 pos = Vector3.MoveTowards(transform.position, trans.ToArray()[i].position, speed * Time.deltaTime);
                rigidBody.MovePosition(pos);
                yield return wait;
            }
            distance = 10;
           
            
        }
        yield return new WaitWhile(() => i < trans.Count);
        trans.Reverse();
        for ( i = 0; i < trans.Count; i++)
        {
             
            


            while (distance >= 1)
            {
                
                distance = Vector3.Distance(transform.position, trans.ToArray()[i].position);
                Vector3 pos = Vector3.MoveTowards(transform.position, trans.ToArray()[i].position, speed * Time.deltaTime);
                rigidBody.MovePosition(pos);
                yield return wait;
            }
            distance = 10;
          

        }
        yield return new WaitWhile(() => i < trans.Count);
        trans.Reverse();
        StartCoroutine(wander());
            
        // foreach (Transform i in pointsmanager)
        // {
        //     do
        //     {
        //         Debug.Log("j");
        //         distance = Vector3.Distance(transform.position, i.position);
        //         Vector3 pos = Vector3.MoveTowards(transform.position, i.position, speed * Time.deltaTime);
        //         rigidBody.MovePosition(pos);
        //         yield return wait;
        //     }
        //     while (distance >= 1);
        //
        //     yield return null;
        //     //yield return new WaitWhile(() => distance <= 1);
        // }
    }
    private void stateatttack()
    {
        Vector3 pos=  Vector3.MoveTowards(transform.position , target.position,  speed* Time.deltaTime);
        rigidBody.MovePosition(pos);

    }
}
