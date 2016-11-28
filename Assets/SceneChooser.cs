using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChooser : MonoBehaviour {


    public static void ChooseScene(int sceneInt)
    {
        SceneManager.LoadScene(sceneInt);
    }
	// Use this for initialization
	void Start () {
	
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
}
