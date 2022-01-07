using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorV2 : MonoBehaviour
{
    private Vector3 _endPositionOfDoor = Vector3.one;
    private Vector3 _startPositionDoor = Vector3.zero;
    private float _doorOpenDelay;

    [Range(0.1f, 5.0f)]
    public float doorSpeed = 1.0f;
    
    [Range(0.0f,5.0f)]
    public float delayToCloseDoor = 0.0f;

    private void Start()
    {
        _startPositionDoor = this.transform.position;

        if (_endPositionOfDoor == Vector3.one)
        {
            _endPositionOfDoor = new Vector3(_startPositionDoor.x, _startPositionDoor.y + 5, _startPositionDoor.z);
        }

        // calculate time it takes to open/close door
        _doorOpenDelay = (Vector3.Distance(_startPositionDoor, _endPositionOfDoor) + (Vector3.Distance(_startPositionDoor, _endPositionOfDoor) / 5)) / doorSpeed;
    }

    public void OpenDoor()
    {
        // open door
        StartCoroutine(OpeningDoor());
    }

    private IEnumerator OpeningDoor()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < _doorOpenDelay)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _endPositionOfDoor, doorSpeed * Time.deltaTime);

            elapsedTime+=Time.deltaTime; 
            yield return new WaitForFixedUpdate();
        }

        StartCoroutine(ClosingDoor());
    }
  
    private IEnumerator ClosingDoor()
    {
        yield return new WaitForSeconds(delayToCloseDoor);
        
        float elapsedTime = 0.0f;
        while (elapsedTime < _doorOpenDelay)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, _startPositionDoor, doorSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return new WaitForFixedUpdate();
        }
    }
}