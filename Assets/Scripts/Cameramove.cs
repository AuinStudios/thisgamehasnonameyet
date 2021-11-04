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
    public float raycastrange;
    [Header("Debug")]
    public int runCount = 0;

    public bool triggerDot = false;
    public bool closeToAnEnemy = false;
    public List<Transform> enemies = new List<Transform>();
    public Transform player;

    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player").transform;
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

        for (byte i = 0; i < enemies.Count; i++)
        {
            float enemyDistanceToPlayer = Vector3.Distance(player.position, enemies[i].position);
            if (enemyDistanceToPlayer < raycastrange)
            {
                closeToAnEnemy = true;
            }
            else
            {
                closeToAnEnemy = false;
            }
        }

        //makes the   camera stop moveing when the game  is paused
        RaycastHit hit;
      
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,raycastrange ) && ( triggerDot == true || closeToAnEnemy == true))
        {
            runCount++;

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.red);
            if (hit.collider.gameObject.CompareTag("Enemy"))
            {
             indicator.GetComponent<Image>().color = new Vector4(255, 0, 0, 255);
            }
            else
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.white);
                indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);
            }
               
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastrange, Color.white);
            indicator.GetComponent<Image>().color = new Vector4(255, 255, 255, 255);

            triggerDot = false;
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
