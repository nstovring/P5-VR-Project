using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Event : MonoBehaviour
{

    public int AudioPlayer;
    public int AudioArray;
    public int AudioSelection;

    public float eventLength = 2;
    public string debugLog;

    //private GameObject _eventManager;
    private AudioSource _audiosource;
    private AudioClip _clip;
    private float soundDelay;

    public IEnumerator eventStart()
    {

        yield return PlaySound(AudioArray, AudioSelection, AudioPlayer);

        print(debugLog);

        yield return new WaitForSeconds(eventLength);
    }



    IEnumerator PlaySound(int audioarray, int clip, int audioplayer)
    {
        GameObject _eventManager = GameObject.FindGameObjectWithTag("Event_Manager");

        if (audioarray == 0)
        {
            _clip = _eventManager.GetComponent<Event_manager>().Narration[clip];
        }
        if (audioarray == 1)
        {
            _clip = _eventManager.GetComponent<Event_manager>().Dialogue[clip];
        }
        if (audioarray == 2)
        {
            _clip = _eventManager.GetComponent<Event_manager>().Background[clip];
        }

        _audiosource = _eventManager.GetComponent<Event_manager>().Audio_Players[audioplayer].GetComponent<AudioSource>();
        _audiosource.spatialize = true;
        _audiosource.PlayOneShot(_clip);

        soundDelay = _clip.length;

        eventLength += soundDelay;
        yield return new WaitForSeconds(soundDelay);
    }






}
