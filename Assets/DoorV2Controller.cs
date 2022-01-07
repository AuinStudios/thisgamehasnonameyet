using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorV2Controller : MonoBehaviour
{
    public KeyCode openDoorKeyCode = KeyCode.K;

    public List<DoorV2> doorsList;

    void Start()
    {
        doorsList = new List<DoorV2>();
        for (int i = 0; i < transform.childCount; i++)
        {
            doorsList.Add(transform.GetChild(i).GetComponent<DoorV2>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(openDoorKeyCode))
        {
            foreach (DoorV2 door in doorsList)
            {
                door.OpenDoor();
            }
        }
    }
}
