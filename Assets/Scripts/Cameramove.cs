using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cameramove : MonoBehaviour
{
    public Transform orientation;
    public Image indicator;
    public float xRotation = 0f;
    public float sensitivity = 50f;
    private float sensMultiplier = 1f;
    public float testingsmooth = 0f;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Update()
    {

        
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.fixedDeltaTime * sensMultiplier;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.fixedDeltaTime * sensMultiplier;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80, 80);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        orientation.Rotate(Vector3.up * mouseX);

        //if (PlayerHealth.Health <= 0)
        //{
        //    sensitivity = 0;
        //}
        //makes the   camera stop moveing when the game  is paused
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10))
        {

           Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
             indicator.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);
            }
            else
            {
                indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
            }
                
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.white);
            indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
        }
        if (Time.timeScale == 0)
        {
            sensitivity = 0;
        }
        if (Time.timeScale == 1 )
        {
            sensitivity = 50;
        }
    }
}
