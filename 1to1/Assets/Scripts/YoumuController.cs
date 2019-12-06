using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YoumuController : MonoBehaviour {
    Animator YoumuAnimator;
    bool active;
    float globalTimer;
    string action;
    string[] idleActions = {"Idle", "Crouch", "ShotA1", "SpellG"};
    string[] crouchActions = { "CrouchUp", "CrouchUp","CrouchUp", "ShotA2" };
    

	// Use this for initialization
	void Start ()
    {
        YoumuAnimator = GetComponent<Animator>();
        Invoke("Idle", 1);
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Debug.Log("Current Action " + action);
        Debug.Log("Current State " + YoumuAnimator.GetInteger("State"));
	}

    void Idle()
    {
        YoumuAnimator.SetInteger("state", 0);
        Invoke(rollAction(idleActions), RandomTime());
    }

    string rollAction(string[] actions)
    {
        return actions[RandomInt(0, actions.Length)];
    }

    void Crouch()
    {
        YoumuAnimator.SetTrigger("crouch");
        //YoumuAnimator.SetInteger("state", 2);
        Invoke(rollAction(crouchActions), RandomTime());
    }

    void CrouchUp()
    {
        YoumuAnimator.SetInteger("state", 0);
        Invoke("Idle", RandomTime());
    }

    void ShotA2()
    {
        YoumuAnimator.SetInteger("state", 201);
        Invoke("Crouch", 1);
    }

    void ShotA1()
        //41 frames
    {
        YoumuAnimator.SetInteger("state", 3);
        Invoke("Idle", 2/3);
        // SpellA1 takes less than one second to animate
    }

    void SpellG()
    {
        YoumuAnimator.SetInteger("state", 4);
        Invoke("SpellGEnd", RandomTime());
    }

    void SpellGEnd()
    {
        YoumuAnimator.SetInteger("state", 104);
        Invoke("Idle", RandomTime());
    }

    int RandomInt()
        //Default RandomInt length 1 to 4 seconds
    {
        return Mathf.RoundToInt(Random.Range(60.0f,240.0f));
    }

    int RandomInt(int min, int max)
        // override for any given range
    {
        return Mathf.FloorToInt(Random.Range(1.0f * min, 1.0f * max));
    }

    float RandomTime()
    {
        return RandomInt() / 60.0f;
    }

    float RandomTime(int min,int max)
    {
        return RandomInt(min, max) / 60.0f;
    }

}
