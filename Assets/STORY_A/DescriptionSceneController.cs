using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class DescriptionSceneController : MonoBehaviour
{
    public AudioSource SceneAudioSource;
    public AudioClip[] NarrationAudioClips = new AudioClip[4];

    public float lightDelay = 1;
    public float soundDelay = 3;
    public float houseSpeed = 1;
    public bool pictureFadeInMode;
    // Use this for initialization
    private IEnumerator Start ()
	{
	   
        if (pictureFadeInMode)
        {
            yield return StartCoroutine(PictureFadeInScene());
        }
        else
        {
            yield return StartCoroutine(BogScene());
        }
    }

    public Transform bookAnimator;
    public Transform houseScale;

    private IEnumerator BogScene()
    {
        yield return new WaitForSeconds(3);
        Quaternion quat = new Quaternion();
        Vector3 scaleVector = new Vector3(0.8928202f, 0.8928202f, 0.8928202f);
        quat.eulerAngles = new Vector3(0,0, 0);
        StartCoroutine(rotateObject(bookAnimator, quat, 0.05f));
        yield return StartCoroutine(scaleObject(houseScale, scaleVector, 0.01f));

        SceneAudioSource.PlayOneShot(NarrationAudioClips[0]);
        soundDelay = NarrationAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);

        Camera.main.GetComponent<VRCameraFade>().FadeOut(2,false);
       
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
    public Renderer[] frameRenderers;
    private IEnumerator PictureFadeInScene()
    {
        SceneAudioSource.PlayOneShot(NarrationAudioClips[0]);
        soundDelay = NarrationAudioClips[0].length;
        for (int index = 0; index < frameRenderers.Length; index++)
        {
            Material tempMat = new Material(frameRenderers[index].material);
            tempMat.name = tempMat.name + " " + index;
            frameRenderers[index].material = tempMat;
        }

        var frameRenderer = frameRenderers[0];

        yield return StartCoroutine(lerpColor(frameRenderer, 0.5f));
        //yield return new WaitForSeconds(3f);


        yield return StartCoroutine(lerpColor(frameRenderers[1], 0.2f));
        //yield return new WaitForSeconds(3f);
        yield return StartCoroutine(lerpColor(frameRenderers[2], 0.2f));
        yield return StartCoroutine(lerpColor(frameRenderers[3], 0.2f));


        yield return new WaitForSeconds(3f);
        //yield return StartCoroutine(lerpObject(houseAnimation[0], houseAnimation[1], houseSpeed));

        yield return new WaitForSeconds(soundDelay);

        Camera.main.GetComponent<VRCameraFade>().FadeOut(2, false);
      
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }

    IEnumerator lerpColor(Renderer rend, float speed)
    {
        Renderer frameRenderer = rend;
        Color color = frameRenderer.material.GetColor("_EmissionColor");

        while (color != Color.white)
        {
            color = Color.Lerp(color, Color.white, speed);
            //frameRenderer.material.SetColor("_Color", color);
            frameRenderer.material.SetColor("_EmissionColor", color);
            yield return new WaitForSeconds(0.07f);
        }
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

    IEnumerator scaleObject(Transform curObject, Vector3 endScale, float speed)
    {
        float step =  speed;
        while (Vector3.Distance(curObject.localScale, endScale) > 0.01)
        {
            //curObject.transform.localScale = Vector3.Lerp(curObject.transform.position, endScale,
            //    speed);

            curObject.localScale = Vector3.Lerp(curObject.transform.localScale, endScale , step);
            //step += 0.1f;
            yield return new WaitForEndOfFrame();
        }


    }

    IEnumerator rotateObject(Transform curObject, Quaternion endRotation, float speed)
    {
        float step = 0.01f * speed;
        while(Quaternion.Angle(curObject.transform.rotation,endRotation) > 0.1)
        {
            curObject.transform.rotation = Quaternion.Lerp(curObject.transform.rotation, endRotation, speed);
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
