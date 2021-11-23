
using UnityEngine;

public class DestorySelf : MonoBehaviour
{
    public void Awake()
    {
        Destroy(gameObject, 3);
    }
}
