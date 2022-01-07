using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrabPlayer : MonoBehaviour
{
    // pos ----------------------------------------
    private Transform Playerpos, gameobjectpos;

    // bool -------------------------------------
    private bool hited = false;
    // floats -------------------------------------
    public ScriptableObectStorage damage;
    private float distance;
    private float cooldownofmonster = 10;
    private float cooldownofstepingonpuddle = 5;
    // vectors --------------------------------------
    private Vector3 pos;
    // meleee system -------------------------------
    private Meleesystem keybind;
    // LineRender -----------------------------
    private LineRenderer Puddlegetcomp;
    // PlayerMovement -------------------------
    private NewPlayer PlayerSpeed;
    // Update is called once per frame
  

    private void Awake()
    {
        Puddlegetcomp = gameObject.GetComponent<LineRenderer>();
        keybind = GameObject.Find("WeaponsHolder").GetComponent<Meleesystem>();
        gameobjectpos = this.transform.GetChild(0);
        Playerpos = GameObject.Find("Player").GetComponent<Transform>();
        PlayerSpeed = Playerpos.GetComponent<NewPlayer>();
        pos = new Vector3(transform.position.x, -1, transform.position.z);
        StartCoroutine(setposforgrab());

    }
    private IEnumerator setposforgrab()
    {
        WaitForSeconds Wait = new WaitForSeconds(0.5f);
        for (int i = 0; i < 5; i++)
        {
            yield return Wait;
            gameobjectpos.position = new Vector3(gameobjectpos.position.x, Playerpos.position.y, gameobjectpos.position.z);
        }
    }
    void Update()
    {
        cooldownofmonster += Time.deltaTime;
        distance = Vector3.Distance(gameobjectpos.position, Playerpos.position);

        cooldownofstepingonpuddle += Time.deltaTime;

        if (cooldownofmonster >= 10)
        {
            hited = false;
            cooldownofmonster = 0;
        }

        if (distance < 10 && hited == false)
        {
           Puddlegetcomp.SetPosition(0, pos);
            Puddlegetcomp.SetPosition(1, Vector3.Lerp(Puddlegetcomp.GetPosition(1), Playerpos.position, 10 * Time.deltaTime));

            Playerpos.position = Vector3.MoveTowards(Playerpos.position, gameobjectpos.position, 5 * Time.deltaTime);
            

            if (Input.GetKeyDown(keybind.keybinds[0]) && keybind.cooldown <= 0.1f)
            {
                hited = true;
                PlayerSpeed.movespeed = 100000;
            }
            else
            {
               PlayerSpeed.movespeed = 0;
            }

        }
        if (distance < 4.5f && cooldownofstepingonpuddle >= 4)
        {
            if (damage.isblocking == true)
            {
                damage.Damage = 10;
            }
            else
            {
                damage.Damage = 30;
            }
            PlayerSpeed.health -= damage.Damage;
            cooldownofstepingonpuddle = 0;
        }

        if (hited == true)
        {
           Puddlegetcomp.SetPosition(0, pos);
           Puddlegetcomp.SetPosition(1, Vector3.Lerp(Puddlegetcomp.GetPosition(1), pos, 3 * Time.deltaTime));
        }
    }
}
