using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wind : MonoBehaviour
{
    // Update is called once per frame
    public Quaternion dir;
    public float airspeed;
    public float lerpspeed = 0;
    public void FixedUpdate()
    {


        // transform.rotation = new Quaternion(dir.x , dir.y, dir.z, transform.rotation.w);

       
    }
    void Update()
    {
        //airspeed = Mathf.Lerp(0, 2, Time.deltaTime);
        // if(airspeed >= 2)
        // {
        // dir.z = airspeed = Mathf.Lerp(0,-2 , -Time.deltaTime);
        // dir.y = airspeed = Mathf.Lerp(0, -2, -Time.deltaTime);
        // }
        // else if( airspeed  < 2)
        // {
        //     dir.z = airspeed = Mathf.Lerp(0, 2, Time.deltaTime);
        //     dir.y = airspeed = Mathf.Lerp(0, 2, Time.deltaTime);
        // }
        dir.x = Random.Range(-5, 5) * Time.deltaTime;
        dir.y = Random.Range(-5, 5) * Time.deltaTime;
        dir.z = Random.Range(-5, 5) * Time.deltaTime;
        transform.rotation = Quaternion.Lerp( transform.rotation, dir , lerpspeed * Time.deltaTime);
    }
}
