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
    public AudioClip[] BackgroundAudioClips = new AudioClip[4];

    public float soundDelay = 3;
    public float houseSpeed = 1;
    public ClassMate teacher;
    public ClassMate emil;
    public List<ClassMate> classMates;

    private bool wrongSceneOver = false;

    private Animator teacherAnimator;
    // Use this for initialization
    private IEnumerator Start ()
	{
<<<<<<< HEAD
        
        //GameObject[] temp = GameObject.FindGameObjectsWithTag("ClassMate");
=======
        classMates = GameObject.FindGameObjectsWithTag("ClassMate");
        teacher = GameObject.FindGameObjectWithTag("Teacher");
        teacherAnimator = teacher.GetComponentInChildren<Animator>();
        emil = GameObject.FindGameObjectWithTag("Emil");
        yield return StartCoroutine(ClassRoomSceneA());
    }
>>>>>>> 83befd8... Test introduction scenes

        //classMates = new List<ClassMate>();
        //foreach (var o in temp)
        //{
        //        classMates.Add(o.GetComponent<ClassMate>());
        //}

        if (!teacher)
        teacher = GameObject.FindGameObjectWithTag("Teacher").GetComponent<ClassMate>();
        teacherAnimator = teacher.GetComponentInChildren<Animator>();
        if(!emil)
        emil = GameObject.FindGameObjectWithTag("Emil").GetComponent<ClassMate>();
        yield return new WaitForSeconds(2);
        myFade = Camera.main.GetComponent<VRCameraFade>();
        myFade.FadeIn(5, false);
        yield return StartCoroutine(ClassRoomSceneA());
    }

    private VRCameraFade myFade;
    private IEnumerator ClassRoomSceneA()
    {
<<<<<<< HEAD
        myFade = Camera.main.GetComponent<VRCameraFade>();

        //Emil Narratter
        yield return PlaySoundAtLocation(NarrationAudioClips[0], teacher.transform.position, false);
       
        yield return new WaitForSeconds(2);
        // Læren stiller et spørgsmål
        teacherAnimator.SetBool("Gesturing", true);
        teacher.talking = true;
        teacher.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[1], teacher.transform.position, true);
        teacher.StopTalking();
        teacherAnimator.SetBool("Gesturing", false);
=======
        //Emil Narratter
        SceneAudioSource.spatialize = false;
        SceneAudioSource.transform.position = Camera.main.transform.position;
        Camera.main.GetComponent<VRCameraFade>().FadeIn(2, false);
        SceneAudioSource.PlayOneShot(NarrationAudioClips[1]);
        soundDelay = NarrationAudioClips[1].length;
        yield return new WaitForSeconds(soundDelay);
        yield return new WaitForSeconds(2);
        // Læren stiller et spørgsmål
        SceneAudioSource.spatialize = true;
        SceneAudioSource.transform.position = teacher.transform.position;
        teacherAnimator.SetBool("Gesturing", true);
        SceneAudioSource.PlayOneShot(CharacterAudioClips[0]);
        soundDelay = CharacterAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);
        teacherAnimator.SetBool("Gesturing", false);
        Debug.Log("Rotating arms??");
>>>>>>> 83befd8... Test introduction scenes
        foreach (var classMate in classMates)
        {
            if (classMate.myState == ClassMate.classMateStates.Idle)
                classMate.myAnimator.SetBool("Idle", false);
                classMate.HandsUp();
        }
        yield return new WaitForSeconds(2);

        // 2!
        emil.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[0], emil.transform.position, true);
        emil.StopTalking();
        foreach (var classMate in classMates)
        {
            classMate.HandsDown();
            classMate.myAnimator.SetBool("Idle", true);
        }

        yield return new WaitForSeconds(3);

        yield return PlaySoundAtLocation(NarrationAudioClips[1], teacher.transform.position, false);

        yield return PlaySoundAtLocation(NarrationAudioClips[2], teacher.transform.position, false);

        yield return new WaitForSeconds(2);
        myFade.FadeOut(2, false);
        
        wrongSceneOver = true;
    }


    private IEnumerator ClassRoomSceneB()
    {
        wrongSceneOver = false;
        myFade.FadeIn(2, false);
        //Emil Narratter
        yield return PlaySoundAtLocation(NarrationAudioClips[3], teacher.transform.position, false);
        yield return new WaitForSeconds(2);

        //Lærer stiller spørgsmål igen
        SceneAudioSource.spatialize = true;
        SceneAudioSource.transform.position = teacher.transform.position;
        teacherAnimator.SetBool("Gesturing", true);
<<<<<<< HEAD
        teacher.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[1], teacher.transform.position, true);
        teacher.StopTalking();
        teacherAnimator.SetBool("Gesturing", false);
        emil.HandsUp();
=======
        SceneAudioSource.PlayOneShot(CharacterAudioClips[0]);
        soundDelay = CharacterAudioClips[0].length;
        yield return new WaitForSeconds(soundDelay);
        Debug.Log("Animate arms??");

       // GameObject.FindGameObjectWithTag("Emil").GetComponent<ClassMate>().RotateArm(-180);
>>>>>>> 83befd8... Test introduction scenes

        foreach (var classMate in classMates)
        {
            if(classMate.myState == ClassMate.classMateStates.Idle)
            classMate.HandsUp();
        }
        yield return new WaitForSeconds(0.4f);
      
        yield return new WaitForSeconds(1);
        //Lærer spørger emil specifikt
        teacherAnimator.SetBool("Gesturing", true);
<<<<<<< HEAD
        teacher.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[2], teacher.transform.position, true);
        teacher.StopTalking();
        teacherAnimator.SetBool("Gesturing", false);
        foreach (var classMate in classMates)
        {
            classMate.HandsDown();
=======
        SceneAudioSource.transform.position = teacher.transform.position;
        SceneAudioSource.PlayOneShot(CharacterAudioClips[2]);
        soundDelay = CharacterAudioClips[2].length;
        yield return new WaitForSeconds(soundDelay);
>>>>>>> 83befd8... Test introduction scenes

        }
        //Emil svarer
        emil.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[0], emil.transform.position, true);
        emil.StopTalking();

        // Godt emil!
        teacher.StartTalking();
        yield return PlaySoundAtLocation(CharacterAudioClips[3], teacher.transform.position, true);
        teacher.StopTalking();
        //Nogle gane spørger han mine venner det er også ok

        yield return PlaySoundAtLocation(NarrationAudioClips[4], teacher.transform.position, false);

        //når jeg rækker hånden op blah blah blah
        yield return PlaySoundAtLocation(NarrationAudioClips[5], teacher.transform.position, false);

        yield return new WaitForSeconds(5);

        myFade.FadeOut(2, false);
        entireSceneOver = true;
    }

    IEnumerator PlaySoundAtLocation(AudioClip clip, Vector3 soundLocation, bool spatialize)
    {
        SceneAudioSource.spatialize = spatialize;
        SceneAudioSource.transform.position = soundLocation;
        SceneAudioSource.PlayOneShot(clip);
        soundDelay = clip.length;
        yield return new WaitForSeconds(soundDelay);
    }

    private bool entireSceneOver = false;

    // Update is called once per frame
    void Update () {
        if (Input.anyKeyDown && wrongSceneOver)
        {
            StartCoroutine(ClassRoomSceneB());
            return;
        }

        if (entireSceneOver && Input.anyKeyDown)
        {
            entireSceneOver = false;
            SceneManager.LoadScene(0);
            return;
        }
        //if (Input.anyKeyDown)
        //{
        //    SceneManager.LoadScene(0);
        //}
    }
}
