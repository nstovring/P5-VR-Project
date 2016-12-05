﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChooser : MonoBehaviour {

    Network_Streamer streamer;
    public static void ChooseScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
	// Use this for initialization
	void Start () {
	
	}
    void Awake()
    {
        streamer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Network_Streamer>();
        streamer.reset();
        streamer.controller1 = this;
    }
    private KeyCode[] keyCodes = {
         KeyCode.Alpha1,
         KeyCode.Alpha2,
         KeyCode.Alpha3,
         KeyCode.Alpha4,
         KeyCode.Alpha5,
         KeyCode.Alpha6,
         KeyCode.Alpha7,
         KeyCode.Alpha8,
         KeyCode.Alpha9,
     };
    // Update is called once per frame
    void Update () {
        Event e = Event.current;
	    string input = Input.inputString;
	    int selectedLevel = 0;


        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (Input.GetKeyDown(keyCodes[i]))
            {
                selectedLevel = i + 1;
                streamer.Rpc_SendAction(keyCodes[i]);
            }
        }
        if (selectedLevel < 3 && selectedLevel > 0)
	    {
            ChooseScene(selectedLevel);
        }
	
	}

    void SelectLevel(Event e)
    {
        int sceneSelected = e.numeric ? e.character : 0;
        //sceneSelected = sceneSelected%2 + 1;
        
    }
    public void Actions(KeyCode key)
    {
        int selectedLevel = 0;

        for (int i = 0; i < keyCodes.Length; i++)
        {
            if (key == keyCodes[i])
            {
                selectedLevel = i + 1;
            }
        }
        if (selectedLevel < 3 && selectedLevel > 0)
        {
            ChooseScene(selectedLevel);
        }
    }
}
