using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class YoumuController : MonoBehaviour
{
    Animator YoumuAnimator;
    bool active;
    float globalTimer;


    // Use this for initialization
    void Start()
    {
        YoumuAnimator = GetComponent<Animator>();
        YoumuAnimator.SetInteger("state", 0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(YoumuAnimator.GetInteger("state"));
        Debug.Log(active);
        if (active)
        {
            globalTimer--;
            // if (globalTimer % 10 == 0) { Debug.Log(globalTimer); }
            if (globalTimer <= 0)
            {
                active = false;
            }
        }
        else
        {
            active = true;
            int nextAction = RandomInt(0, 3);
            //Debug.Log(nextAction);
            switch (nextAction)
            {
                case 1:
                    Crouch();
                    return;
                case 2:
                    SpellG();
                    return;
                default:
                    Idle();
                    return;
            }
        }
    }

    void Idle()
    {
        //Debug.Log("Idle");
        globalTimer = RandomInt();
        // YoumuAnimator.SetInteger("state", 0);
    }

    void Crouch()
    {
        float crouchTime = RandomInt(60, 300);
        globalTimer = crouchTime + 12; //16
        YoumuAnimator.SetTrigger("Trigger");
        //YoumuAnimator.SetInteger("state", 2);
        //Debug.Log("Crouch For" + globalTimer / 60.0f + "Seconds");
        Invoke("CrouchUp", globalTimer / 60.0f);
    }

    void CrouchUp()
    {
        // Debug.Log("CrouchUP");
        // Debug.Break();
        YoumuAnimator.SetInteger("state", 3);
    }

    void SpellG()
    {
        float spellTimer = RandomInt();
        globalTimer = spellTimer + 34;
        YoumuAnimator.SetInteger("state", 4);
        // Debug.Log("Spell G For" + globalTimer / 60.0f + " Seconds");
        Invoke("SpellGEnd", globalTimer / 60.0f);
    }

    void SpellGEnd()
    {
        YoumuAnimator.SetInteger("state", 5);
        // Debug.Log("SpellGEnd");
    }

    int RandomInt()
    //Default RandomInt length 2 to 8 seconds
    {
        return Mathf.RoundToInt(Random.Range(120.0f, 480.0f));
    }

    int RandomInt(int min, int max)
    // ovverride for any given range
    {
        return Mathf.CeilToInt(Random.Range(1.0f * min, 1.0f * max));
    }


}*/
