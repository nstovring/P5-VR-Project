using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using VRStandardAssets.Utils;

public class IdaSceneController : MonoBehaviour
{
    public AudioSource SceneAudioSource;
    public AudioClip[] NarrationAudioClips = new AudioClip[4];
    public AudioClip[] CharacterAudioClips = new AudioClip[3];
    public AudioClip[] BackgroundAudioClips = new AudioClip[4];


    public float soundDelay = 3;
    public float houseSpeed = 1;
    public ClassMate teacher;
    public CharacterAnimator ida;
    public List<ClassMate> classMates;

    public Transform idaMovePoint;
    public Transform idaStartPosition;
    public GameObject IDA;

    private bool wrongSceneOver = false;

    private Animator teacherAnimator;
    private Animator idaAnimator;

    private Vector3 _idaStartPosition;

    Network_Streamer streamer;

    void Awake() {
        _idaStartPosition = IDA.transform.position;
        /*streamer = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Network_Streamer>();
        streamer.reset();
        streamer.controller3 = this;*/
    }

    // Use this for initialization
    private IEnumerator Start ()
	{
        print(_idaStartPosition);

        GameObject[] temp = GameObject.FindGameObjectsWithTag("ClassMate");

        classMates = new List<ClassMate>();
        foreach (var o in temp)
        {
            classMates.Add(o.GetComponent<ClassMate>());
        }

        if (!teacher)
        teacher = GameObject.FindGameObjectWithTag("Teacher").GetComponent<ClassMate>();
        teacherAnimator = teacher.GetComponentInChildren<Animator>();
        if(!ida)
        ida = GameObject.FindGameObjectWithTag("Ida").GetComponent<CharacterAnimator>();

        idaAnimator = ida.myAnimator;
        idaAnimator.SetBool("GETUP", false);

        idaAnimator.SetBool("Idle", false);
        idaAnimator.SetBool("Sit down", false);
        idaAnimator.speed = 1.5f;
        yield return new WaitForSeconds(3);
        myFade = Camera.main.GetComponent<VRCameraFade>();
        myFade.FadeIn(5, false);
        yield return StartCoroutine(ClassRoomSceneA());
    }

    private VRCameraFade myFade;
    private IEnumerator ClassRoomSceneA()
    {
        myFade = Camera.main.GetComponent<VRCameraFade>();


        yield return new WaitForSeconds(2);
        idaAnimator.SetBool("Walk", true);
        
        yield return StartCoroutine(MoveTowards(idaMovePoint));
        yield return StartCoroutine(RotateTowards(idaMovePoint));
        idaAnimator.SetBool("Walk", false);
        idaAnimator.SetBool("Sit down", true);
        idaAnimator.applyRootMotion = true;

        yield return new WaitForSeconds(4);
        //Uden at spørge om love bwegynder hun at tegne
        // All classmates bliver sure
        foreach (var classMate in classMates)
        {
            //lassMate.myAnimator.SetBool("Drawing", false);
            //classMate.myAnimator.SetBool("Idle", true);
            //classMate.GetAngry();
        }
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[3], idaMovePoint.position,false));

        yield return new WaitForSeconds(3f);
        wrongSceneOver = true;
        myFade.FadeOut(3f,false);
    }
    private IEnumerator MoveTowards(Transform endPoint)
    {
        Vector3 offsetmovePoint = endPoint.position;
        offsetmovePoint.y =0;

        ida.transform.LookAt(offsetmovePoint);
        while (Vector3.Distance(ida.transform.position, idaMovePoint.position) > 0.1f)
        {
            ida.transform.position = Vector3.MoveTowards(ida.transform.position, endPoint.position, 0.01f);
            yield return new WaitForSeconds(0.01f);
        }

    }
    private IEnumerator RotateTowards(Transform endPoint)
    {
        Vector3 relativePos = endPoint.position - ida.transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        ida.transform.rotation = endPoint.rotation;
        yield return new WaitForEndOfFrame();
 
        while (Vector3.Angle(ida.transform.forward, endPoint.forward) > 0.1f)
        {
            ida.transform.rotation = Quaternion.Lerp(ida.transform.rotation, rotation, 0.2f);
            yield return new WaitForEndOfFrame();
        }
     
    }

    private IEnumerator ClassRoomSceneB()
    {
        IDA.transform.position = _idaStartPosition;
        print(IDA.transform.position);


        
        idaAnimator.SetBool("GETUP", true);
       // idaAnimator.SetBool("GETUP", false);


        idaAnimator.SetBool("Idle", false);
        idaAnimator.SetBool("Sit down", false);

        yield return new WaitForSeconds(2);


        myFade = Camera.main.GetComponent<VRCameraFade>();
        myFade.FadeIn(3,false);
        wrongSceneOver = false;

        yield return new WaitForSeconds(4);
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[6], idaMovePoint.position, true));
        idaAnimator.SetBool("Walk", true);
        idaAnimator.SetBool("GETUP", false);

        yield return StartCoroutine(MoveTowards(idaMovePoint));
        yield return StartCoroutine(RotateTowards(idaMovePoint));
        idaAnimator.SetBool("Walk", false);


        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[4], idaMovePoint.position, true));
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[2], idaMovePoint.position, true));
        idaAnimator.SetBool("Sit down", true);
        idaAnimator.applyRootMotion = true;
        yield return new WaitForSeconds(2);
        idaAnimator.SetBool("Drawing", true);
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[5], idaMovePoint.position, false));
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[0], idaMovePoint.position, false));
        yield return StartCoroutine(PlaySoundAtLocation(CharacterAudioClips[1], idaMovePoint.position, false));
        yield return new WaitForSeconds(2);
        entireSceneOver = true;
    }

    IEnumerator PlaySoundAtLocation(AudioClip clip, Vector3 soundLocation, bool spatialize)
    {
        Debug.Log("playing " + clip.name);
        SceneAudioSource.spatialize = spatialize;
        SceneAudioSource.transform.position = soundLocation;
        SceneAudioSource.PlayOneShot(clip);
        soundDelay = clip.length;
        yield return new WaitForSeconds(soundDelay);
    }

    private bool entireSceneOver = false;

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyUp(KeyCode.Space) && wrongSceneOver)
        {
            //streamer.Rpc_SendAction(KeyCode.Space);
            StartCoroutine(ClassRoomSceneB());
            return;
        }

        if (entireSceneOver && Input.GetKeyUp(KeyCode.Space))
        {
            entireSceneOver = false;
            //streamer.Rpc_SendAction(KeyCode.Space);
            SceneManager.LoadScene(0);
            return;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            //streamer.Rpc_SendAction(KeyCode.Escape);
            SceneManager.LoadScene(0);
        }
    }
    public void Action(KeyCode key)
    {
        if (key == KeyCode.Space && wrongSceneOver)
        {
            StartCoroutine(ClassRoomSceneB());
            return;
        }

        if (entireSceneOver && key == KeyCode.Space)
        {
            entireSceneOver = false;
            SceneManager.LoadScene(0);
            return;
        }
        if (key == KeyCode.Escape)
        {
            SceneManager.LoadScene(0);
        }
    }
}
