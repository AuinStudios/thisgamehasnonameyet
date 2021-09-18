using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAY : MonoBehaviour
{
    public float x , y;
    public float sensitivity = 2f;
    private bool stop;
    private bool temporaystopboolfory;
    public Vector3 xaxisleft , xaxisright , yup ,ydown;
    void Update()
    {
         x -= sensitivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
         y += sensitivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
        // x axis-----------------------------------------------------------
        if (x < 0.6f && stop == false)
        {
         x += Time.deltaTime * 1.4f ;
        }
        else if(x > 0.6f  )
        {
          x -= Time.deltaTime * 1.4f ;
        }

        if(x > 0.49)
        {
            stop = true;
        }
        else
        {
            stop = false;
        }
        // y axis ----------------------------------------------------------------
        if (y < 0.51f && temporaystopboolfory == false)
        {
            y += Time.fixedDeltaTime * 1f;
        }
        else if (y > 0.51f)
        {
            y -= Time.fixedDeltaTime * 1f;
        }

        if (y > 0.49)
        {
            temporaystopboolfory = true;
        }
        else
        {
            temporaystopboolfory = false;
        }
         // sway move code --------------------------------------------------------
        transform.localPosition = Vector3.Lerp(yup, ydown, y) + Vector3.Lerp(xaxisleft,xaxisright , x);
        x = Mathf.Clamp(x, 0, 1);
        y = Mathf.Clamp(y, 0, 1);
    }
}
