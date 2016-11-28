﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class DescriptionSceneController : MonoBehaviour
{
    public AudioSource SceneAudioSource;
    public AudioSource BackgroundAudioSource;
    public AudioSource BackgroundFadeAudioSource;
    public AudioClip[] NarrationAudioClips = new AudioClip[4];
    public AudioClip[] BackgroundAudioClips = new AudioClip[4];
    public AudioMixerSnapshot backgroundSoundSnapshot;
    public AudioMixerSnapshot backgroundFadeSoundSnapshot;

    public float lightDelay = 1;
    public float soundDelay = 3;
    public float houseSpeed = 1;
    public bool pictureFadeInMode;

    public Transform User;
    public Transform bookAnimator;
    public Transform houseScale;
    public Transform framesParent;
    public List<Transform> frames;
    private List<Transform> rotatedframes;
    public Renderer[] frameRenderers;
    private IEnumerator PictureFadeInScene()
    {
        rotatedframes = new List<Transform>();
        //SceneAudioSource.PlayOneShot(NarrationAudioClips[0]);
        //soundDelay = NarrationAudioClips[0].length;
        yield return PlaySoundAndDelay(NarrationAudioClips[0]);
        framesParent.parent = null;
        StartCoroutine(RotateFramesWithHead());
        for (int index = 0; index < frameRenderers.Length; index++)
        {
            Material tempMat = new Material(frameRenderers[index].material);
            tempMat.name = tempMat.name + " " + index;
            frameRenderers[index].material = tempMat;
        }

        Renderer frameRenderer = frameRenderers[0];

        PlayBackgroundClip(BackgroundAudioClips[0]);
        backgroundFadeSoundSnapshot.TransitionTo(4f);
        yield return StartCoroutine(lerpColor(frameRenderer, 0.5f));
        rotatedframes.Add(frames[0]);
        yield return PlaySoundAndDelay(NarrationAudioClips[1]);

        PlayBackgroundFadeClip(BackgroundAudioClips[1]);
        backgroundSoundSnapshot.TransitionTo(4f);
        yield return StartCoroutine(lerpColor(frameRenderers[1], 0.2f));
        rotatedframes.Add(frames[1]);
        yield return PlaySoundAndDelay(NarrationAudioClips[2]);

        PlayBackgroundClip(BackgroundAudioClips[2]);
        backgroundFadeSoundSnapshot.TransitionTo(4f);
        yield return StartCoroutine(lerpColor(frameRenderers[2], 0.2f));
        rotatedframes.Add(frames[2]);
        yield return PlaySoundAndDelay(NarrationAudioClips[3]);

        PlayBackgroundFadeClip(BackgroundAudioClips[3]);
        backgroundSoundSnapshot.TransitionTo(4f);
        yield return StartCoroutine(lerpColor(frameRenderers[3], 0.2f));
        rotatedframes.Add(frames[3]);
        yield return PlaySoundAndDelay(NarrationAudioClips[4]);

        yield return new WaitForSeconds(3f);
        //yield return StartCoroutine(lerpObject(houseAnimation[0], houseAnimation[1], houseSpeed));

        //yield return new WaitForSeconds(soundDelay);

        Camera.main.GetComponent<VRCameraFade>().FadeOut(2, false);
      
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }

    IEnumerator PlaySoundAndDelay(AudioClip clip)
    {
        SceneAudioSource.PlayOneShot(clip);
        soundDelay = clip.length;
        yield return new WaitForSeconds(soundDelay);
    }

    void PlayBackgroundClip(AudioClip clip)
    {
        BackgroundAudioSource.clip = clip;
        BackgroundAudioSource.Play();
    }
    void PlayBackgroundFadeClip(AudioClip clip)
    {
        BackgroundFadeAudioSource.clip = clip;
        BackgroundFadeAudioSource.Play();
    }
    IEnumerator lerpColor(Renderer rend, float speed)
    {
        Renderer frameRenderer = rend;
        //Color color = frameRenderer.material.GetColor("_EmissionColor");
        float alpha = frameRenderer.material.GetFloat("_Alpha");

        while (alpha > 0)
        {
            alpha = Mathf.Lerp(alpha, 0, speed);
            //frameRenderer.material.SetColor("_Color", color);
            frameRenderer.material.SetFloat("_Alpha", alpha);
            if (alpha <= 0.01)
                alpha = 0;
            yield return new WaitForSeconds(0.1f);
        }
    }

    private float audio2Volume;
    float audio1Volume;

    void fadeIn(AudioSource audioSource)
    {
        if (audio2Volume < 1f)
        {
            audio2Volume += 0.1f * Time.deltaTime;
            audioSource.volume = audio2Volume;
        }
    }

    void fadeOut(AudioSource audioSource)
    {
        if (audio1Volume > 0.1f)
        {
            audio1Volume -= 0.1f * Time.deltaTime;
            audioSource.volume = audio1Volume;
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

    private bool Started = false;
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Space) && !Started)
        {
            Started = true;
            StartCoroutine(PictureFadeInScene());
            return;
        }
        if (entireSceneOver && Input.GetKeyUp(KeyCode.Space))
        {
            entireSceneOver = false;
            SceneManager.LoadScene(0);
            return;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator RotateFramesWithHead()
    {
        while (true)
        {
            if (rotatedframes.Count > 0)
            {
                foreach (var frame in rotatedframes)
                {
                    frame.LookAt(frameLookAtTarget);
                }
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public Transform frameLookAtTarget;
}
