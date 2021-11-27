using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOO : MonoBehaviour
{
    public Transform goo;
    public ScriptableObectStorage storage;
    public void OnParticleTrigger()
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();

        // particles
        List<ParticleSystem.Particle> enter = new List<ParticleSystem.Particle>();
       
        // get
        int numEnter = ps.GetTriggerParticles(ParticleSystemTriggerEventType.Enter, enter);

        
        // iterate
        for (int i = 0; i < numEnter; i++)
        {
            ParticleSystem.Particle p = enter[i];
            RaycastHit hit;
            if(Physics.Raycast(p.position , Vector3.down , out hit , 2))
            {
             Vector3 praticalpositions = new Vector3(p.position.x, hit.point.y + 0.01f, p.position.z);
             Instantiate(goo, praticalpositions, goo.rotation);
            }
           
            storage.namechanger += 1;
            goo.name = "goo" + storage.namechanger;
            enter[i] = p;
        }
    }
    }
