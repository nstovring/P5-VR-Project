using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class DescriptionSceneController : MonoBehaviour
{
    public AudioSource SceneAudioSource;
    public AudioClip[] NarrationAudioClips = new AudioClip[4];
    public Light[] spotLights = new Light[2];

    public float lightDelay = 1;
    public float soundDelay = 3;
    public float houseSpeed = 1;
    public Transform[] houseAnimation = new Transform[3];

    // Use this for initialization
    private IEnumerator Start ()
	{
	    foreach (var spotLight in spotLights)
	    {
	    spotLight.enabled = false;
        }
        yield return StartCoroutine(RullebåndsScene());
    }


    private IEnumerator RullebåndsScene()
    {
        yield return new WaitForSeconds(lightDelay);
        foreach (var spotLight in spotLights)
        {
            spotLight.enabled = true;
        }
        yield return StartCoroutine(lerpObject(houseAnimation[0], houseAnimation[1], houseSpeed));
        SceneAudioSource.PlayOneShot(NarrationAudioClips[0]);
        soundDelay = NarrationAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);

        Camera.main.GetComponent<VRCameraFade>().FadeOut(2,false);
        foreach (var spotLight in spotLights)
        {
            spotLight.enabled = false;
        }
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }


    IEnumerator lerpObject(Transform curObject,Transform endPosition, float speed)
    {
        float step = 0.01f * speed;
        while (Vector3.Distance(curObject.position,endPosition.position) > 0.1)
        {
            curObject.transform.position = Vector3.MoveTowards(curObject.transform.position, endPosition.transform.position,
                step);
            //step+=0.1f;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator rotateObject(Transform curObject, Quaternion endRotation, float speed)
    {
        float step = 0.01f * speed;
        while(Quaternion.Angle(curObject.transform.rotation,endRotation) > 0.1)
        {
            curObject.transform.rotation = Quaternion.Lerp(curObject.transform.rotation, endRotation, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
    }

    private bool entireSceneOver = false;

    // Update is called once per frame
    void Update () {
       
        if (entireSceneOver && Input.anyKeyDown)
        {
            entireSceneOver = false;
            SceneManager.LoadScene(0);
        }

    }
}
