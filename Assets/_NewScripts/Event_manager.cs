using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Event = UnityEngine.Event;

//[ExecuteInEditMode]

public class Event_manager : MonoBehaviour
{


    [Header("Audio")]
    public List<AudioClip> Narration;
    public List<AudioClip> Dialogue;
    public List<AudioClip> Background;

    [Header("Audio Players")]
    public List<GameObject> Audio_Players;

    [Header("Event")]
    public int Amount_of_Events;
    public float[] Length_of_each_event;

    public List<string> Event_Debug_Logs;

    private List<global::Event> Events;



    [Header("AudioSelection")]
    public int [] AudioPlayerSelection;
    public int [] AudioArraySelection;
    public int [] AudioClipSelection;



    //[Header("NPC")]
    //public GameObject[] NPCs;

    // public int NPC_Amount;
    // private GameObject [] _NPCs;




    private IEnumerator Start()
    {
        Events = new List<global::Event>();

        for (int i = 0; i < Amount_of_Events; i++)
        {
            Events.Add(new global::Event());

            Events[i].eventLength = Length_of_each_event[i];
            Events[i].debugLog = Event_Debug_Logs[i];

            Events[i].AudioPlayer = AudioPlayerSelection[i];
            Events[i].AudioArray = AudioArraySelection[i];
            Events[i].AudioSelection = AudioClipSelection[i];

            StartCoroutine(Events[i].eventStart());
            yield return new WaitForSeconds(Events[i].eventLength);
            print(Time.time);

        }


        print("Monkey");
        yield return new WaitForSeconds(2);
    }








    // Update is called once per frame
    void Update () {

        //Events = new int[Amount_of_Events];
        //_NPCs = GameObject.FindGameObjectsWithTag("NPC");
        //NPC_Amount = _NPCs.Length;

    }





}

