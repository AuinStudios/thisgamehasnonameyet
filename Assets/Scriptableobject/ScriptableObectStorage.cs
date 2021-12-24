using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DistancesScriptableObject", order = 1)]
public class ScriptableObectStorage : ScriptableObject
{
    // public Collider[] hits;
    public bool isblocking = false;
    public float Damage = 30;
    public int count = 0;
    public float namechanger = 0;
}
