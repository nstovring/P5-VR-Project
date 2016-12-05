using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class RelaxSceneTransition : MonoBehaviour
{
    private int nextScene;
	// Use this for initialization
	IEnumerator Start ()
	{
	    nextScene = PlayerPrefs.GetInt("NextScene");
	    yield return new WaitForSeconds(90);
        Camera.main.GetComponent<VRCameraFade>().FadeOut(2, true);
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(nextScene);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
