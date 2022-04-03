using UnityEngine;
public class SWAY : MonoBehaviour
{
    private  float sensitivity = 1.2f;
    private  float smoothness = 10f;
    private float x, y;
    #region Old Stuff
 //  public float rotationsenstivity = 3f;
    //  public Vector3 xaxisleft , xaxisright , yup ,ydown;
    //  public Quaternion xaxisrotationleft, xaxisrotationright, yuprotation, ydownrotation;


    // private bool xstop;
    //  private bool  ystop;
    // private bool xrotationstop;
    //  private bool yrotationstop;
    #endregion
    #region Old Code
 // old code -----------------------------------------------------------------------

    // void Update()
    // {
    //      x -= sensitivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
    //      y += sensitivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
    //      xrotation += rotationsenstivity * Input.GetAxis("Mouse X") * Time.fixedDeltaTime;
    //      yrotation += rotationsenstivity * Input.GetAxis("Mouse Y") * Time.fixedDeltaTime;
    //     // x axis-----------------------------------------------------------
    //     if (x < 0.51f && xstop == false)
    //     {
    //        
    //      x += Time.deltaTime * 1f ;
    //     }
    //     else if(x > 0.51f  )
    //     {
    //      x -= Time.deltaTime * 1f ;
    //     }
    //
    //     if(x > 0.49)
    //     {
    //         xstop = true;
    //     }
    //     else
    //     {
    //         xstop = false;
    //     }
    //     // y axis ----------------------------------------------------------------
    //     if (y < 0.51f && ystop == false)
    //     {
    //         y += Time.deltaTime * 1f;
    //     }
    //     else if (y > 0.51f)
    //     {
    //         y -= Time.deltaTime * 1f;
    //     }
    //
    //     if (y > 0.49)
    //     {
    //         ystop = true;
    //     }
    //     else
    //     {
    //         ystop = false;
    //     }
    //
    //
    //
    //     //xrotation -------------------------------------------------------------------
    //     if (xrotation < 0.51f &&xrotationstop == false  )
    //     {
    //         xrotation += Time.deltaTime * 1f;
    //     }
    //     else if (xrotation > 0.51f)
    //     {
    //         xrotation -= Time.deltaTime * 1f;
    //     }
    //
    //     if (xrotation > 0.49)
    //     {
    //         xrotationstop= true;
    //     }
    //     else
    //     {
    //         xrotationstop = false;
    //     }
    //
    //     
    //     // yrot ----------------------------------------------------------------
    //     if (yrotation < 0.51f && yrotationstop == false )
    //     {
    //         yrotation += Time.deltaTime * 1f;
    //     }
    //     else if (yrotation > 0.51f)
    //     {
    //         yrotation -= Time.deltaTime * 1f;
    //     }
    //
    //     if (yrotation > 0.49)
    //     {
    //         yrotationstop = true;
    //     }
    //     else
    //     {
    //         yrotationstop = false;
    //     }
    //
    //     // sway move code --------------------------------------------------------
    //     transform.localPosition = Vector3.Lerp(yup, ydown, y) + Vector3.Lerp(xaxisleft,xaxisright , x);
    //     transform.localRotation = Quaternion.Lerp(yuprotation, ydownrotation, yrotation) * Quaternion.Lerp(xaxisrotationleft, xaxisrotationright,xrotation); ;
    //    
    //   x = Mathf.Clamp(x, 0, 1);
    //     y = Mathf.Clamp(y, 0, 1);
    //     xrotation = Mathf.Clamp(xrotation, 0, 1);
    //     yrotation = Mathf.Clamp(yrotation, 0, 1);
    // }
    #endregion
    private void Update()
    {
       // inputs -----------------------------------------------------------
        x = Input.GetAxis("Mouse X")* sensitivity * Time.fixedDeltaTime ;
        y = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime ;
        // transfer input to move the rotation and poition of the gameobject ----------------------------------------
        Quaternion rotations = new Quaternion(-y , -x , transform.localRotation.z, transform.localRotation.w);
        Vector3 positions = new Vector3(x * 1.5f, -y * 1.1f, transform.localPosition.z);
        // lerp the gameobjects rotation and position towards the mouse input value  --------------------------------------------
        transform.localRotation = Quaternion.Lerp(transform.localRotation,rotations , Time.deltaTime * smoothness );
        transform.localPosition = Vector3.Lerp(transform.localPosition,positions , Time.deltaTime * smoothness);
    }
   
}
