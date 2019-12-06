using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixResolution : MonoBehaviour {
    //Resolution[] resolutions = Screen.resolutions;
    //Debug.Log(resolutions);
    int screenHeight = 512;
    int screenWidth = 256;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        if (Screen.height != screenHeight || Screen.width != screenWidth || Screen.fullScreen)
        {
            Screen.SetResolution(screenHeight, screenWidth, false);
        }
    }
}
