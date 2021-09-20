using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SWAY : MonoBehaviour
{
    public float x , y , xrotation , yrotation;
    public float sensitivity = 2f;
    public float rotationsenstivity = 3f;
    public Animator anim;
    private bool xstop;
    private bool  ystop;
    private bool xrotationstop;
    private bool yrotationstop;
    public Vector3 xaxisleft , xaxisright , yup ,ydown;
    public Quaternion xaxisrotationleft, xaxisrotationright, yuprotation, ydownrotation;
    void Update()
    {
         x -= sensitivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
         y += sensitivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
         xrotation += rotationsenstivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
         yrotation += rotationsenstivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
        // x axis-----------------------------------------------------------
        if (x < 0.51f && xstop == false)
        {
           
         x += Time.deltaTime * 1f ;
        }
        else if(x > 0.51f  )
        {
         x -= Time.deltaTime * 1f ;
        }

        if(x > 0.49)
        {
            xstop = true;
        }
        else
        {
            xstop = false;
        }
        // y axis ----------------------------------------------------------------
        if (y < 0.51f && ystop == false)
        {
            y += Time.deltaTime * 1f;
        }
        else if (y > 0.51f)
        {
            y -= Time.deltaTime * 1f;
        }

        if (y > 0.49)
        {
            ystop = true;
        }
        else
        {
            ystop = false;
        }



        //xrotation -------------------------------------------------------------------
        if (xrotation < 0.51f && xrotationstop == false)
        {
            xrotation += Time.deltaTime * 1f;
        }
        else if (xrotation > 0.51f)
        {
            xrotation -= Time.deltaTime * 1f;
        }

        if (xrotation > 0.49)
        {
            xrotationstop= true;
        }
        else
        {
            xrotationstop = false;
        }

        
        // yrot ----------------------------------------------------------------
        if (yrotation < 0.51f && yrotationstop == false)
        {
            yrotation += Time.deltaTime * 1f;
        }
        else if (yrotation > 0.51f)
        {
            yrotation -= Time.deltaTime * 1f;
        }

        if (yrotation > 0.49)
        {
            yrotationstop = true;
        }
        else
        {
            yrotationstop = false;
        }

        // sway move code --------------------------------------------------------
        transform.localPosition = Vector3.Lerp(yup, ydown, y) + Vector3.Lerp(xaxisleft,xaxisright , x);
        transform.localRotation = Quaternion.Lerp(yuprotation, ydownrotation, yrotation) * Quaternion.Lerp(xaxisrotationleft, xaxisrotationright,xrotation); ;
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            anim.SetTrigger("axe");
        }
      x = Mathf.Clamp(x, 0, 1);
        y = Mathf.Clamp(y, 0, 1);
        xrotation = Mathf.Clamp(xrotation, 0, 1);
        yrotation = Mathf.Clamp(yrotation, 0, 1);
    }
}
