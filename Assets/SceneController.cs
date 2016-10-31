using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class SceneController : MonoBehaviour
{
    public AudioSource SceneAudioSource;
    public AudioClip[] NarrationAudioClips = new AudioClip[4];
    public AudioClip[] CharacterAudioClips = new AudioClip[3];

    public float soundDelay = 3;
    public float houseSpeed = 1;
    public GameObject teacher;
    public GameObject emil;
    public GameObject[] classMates;

    private bool wrongSceneOver = false; 
    // Use this for initialization
    private IEnumerator Start ()
	{
	    yield return StartCoroutine(ClassRoomSceneA());
    }


    private IEnumerator ClassRoomSceneA()
    {
        classMates = GameObject.FindGameObjectsWithTag("ClassMate");
        teacher = GameObject.FindGameObjectWithTag("Teacher");
        emil = GameObject.FindGameObjectWithTag("Emil");

        SceneAudioSource.transform.position = emil.transform.position;
        Camera.main.GetComponent<VRCameraFade>().FadeIn(2, false);
        SceneAudioSource.PlayOneShot(NarrationAudioClips[1]);
        soundDelay = NarrationAudioClips[1].length;
        yield return new WaitForSeconds(soundDelay);

        yield return new WaitForSeconds(2);
        // Læren stiller et spørgsmål
        SceneAudioSource.transform.position = teacher.transform.position;
        SceneAudioSource.PlayOneShot(CharacterAudioClips[0]);
        soundDelay = CharacterAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);

        Debug.Log("Rotating arms??");
        foreach (var classMate in classMates)
        {
            Debug.Log("Rotating arms");
            //classMate.GetComponent<ClassMate>().RotateArm(-180);
        }
        yield return new WaitForSeconds(2);
        SceneAudioSource.transform.position = emil.transform.position;
        SceneAudioSource.PlayOneShot(CharacterAudioClips[1]);
        soundDelay = CharacterAudioClips[1].length;
        yield return new WaitForSeconds(soundDelay);

        yield return new WaitForSeconds(3);

        SceneAudioSource.PlayOneShot(NarrationAudioClips[2]);
        soundDelay = NarrationAudioClips[2].length;
        yield return new WaitForSeconds(soundDelay);
        yield return new WaitForSeconds(2);
        Camera.main.GetComponent<VRCameraFade>().FadeOut(2, false);
        foreach (var classMate in classMates)
        {
            Debug.Log("Rotating arms");
            classMate.GetComponent<ClassMate>().RotateArm(0);
        }
        wrongSceneOver = true;
    }


    private IEnumerator ClassRoomSceneB()
    {
        wrongSceneOver = false;
        classMates = GameObject.FindGameObjectsWithTag("ClassMate");
        Camera.main.GetComponent<VRCameraFade>().FadeIn(2, false);
        SceneAudioSource.transform.position = emil.transform.position;
        //Emil narrater
        SceneAudioSource.PlayOneShot(NarrationAudioClips[4]);
        soundDelay = NarrationAudioClips[4].length;
        yield return new WaitForSeconds(soundDelay);

        yield return new WaitForSeconds(2);

        //Lærer stiller spørgsmål igen
        SceneAudioSource.transform.position = teacher.transform.position;
        SceneAudioSource.PlayOneShot(CharacterAudioClips[0]);
        soundDelay = CharacterAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);
        Debug.Log("Rotating arms??");

       // GameObject.FindGameObjectWithTag("Emil").GetComponent<ClassMate>().RotateArm(-180);

        foreach (var classMate in classMates)
        {
            Debug.Log("Rotating arms");
            //classMate.GetComponent<ClassMate>().RotateArm(-180);
        }

        yield return new WaitForSeconds(1);
        //Lærer spørger emil specifikt
        SceneAudioSource.transform.position = teacher.transform.position;
        SceneAudioSource.PlayOneShot(CharacterAudioClips[2]);
        soundDelay = CharacterAudioClips[2].length;
        yield return new WaitForSeconds(soundDelay);

        //Emil svarer
        SceneAudioSource.transform.position = emil.transform.position;
        SceneAudioSource.PlayOneShot(NarrationAudioClips[5]);
        soundDelay = NarrationAudioClips[5].length;
        yield return new WaitForSeconds(soundDelay);
        yield return new WaitForSeconds(5);

        Camera.main.GetComponent<VRCameraFade>().FadeOut(2, false);
        entireSceneOver = true;
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
        if (Input.anyKeyDown && wrongSceneOver)
        {
            StartCoroutine(ClassRoomSceneB());
        }

        if (entireSceneOver && Input.anyKeyDown)
        {
            entireSceneOver = false;
            SceneManager.LoadScene(0);
        }

    }
}
