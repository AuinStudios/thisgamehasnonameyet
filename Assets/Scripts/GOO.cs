using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOO : MonoBehaviour
{
    public Transform goo;
  
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
             Instantiate(goo, p.position, goo.rotation);
            enter[i] = p;
        }
    }
    }
