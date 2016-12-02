using UnityEngine;
using System.Collections;

public class ClassMate : MonoBehaviour
{
    public Animator myAnimator;
    public enum classMateStates
    {
        Idle,Sleeping, Drawing,HandsUp
    }

    public enum classMateType
    {
        male,female
    }


    public enum emotion
    {
        neutral, anglry
    }
    public classMateType myType;
    public classMateStates myState;
    public emotion myEmotion;
    public Texture2D[] MaleTextures;
    public Texture2D[] FemaleTextures;
    private Texture2D[] myTexture2DArray;

    public Renderer myRenderer;
    // Use this for initialization
    void Start ()
    {
       
        switch (myType)
        {
            case classMateType.male:
                myTexture2DArray = MaleTextures;
                break;
            case classMateType.female:
                myTexture2DArray = FemaleTextures;
                break;
        }

        myAnimator = GetComponentInChildren<Animator>();
        myAnimator.applyRootMotion = false;
        switch (myState)
	    {
	        case classMateStates.Idle:
            myAnimator.SetBool("Idle", true);
            break;
            case classMateStates.Drawing:
            myAnimator.SetBool("Drawing", true);
	        break;
            case classMateStates.Sleeping:
            myAnimator.SetBool("Sleeping", true);
	        break;
        }
       // StartTalking();
    }
	
	// Update is called once per frame
	void Update () {
	   
	}

    private int myTexture;
    public bool talking = true;
    [Range(0.1f,2)]
    public float talkSpeed = 1;
    public IEnumerator Talk()
    {
        int i = 0;
        while (true)
        {
            myRenderer.material.SetTexture("_MainTex",myTexture2DArray[i%2]);
            yield return new WaitForSeconds(Random.Range((talkSpeed/10f)- 0.1f, (talkSpeed / 10f) + 0.1f));
            i++;
        }
    }

    public void GetAngry()
    {
        if(myTexture2DArray.Length > 2)
        myRenderer.material.SetTexture("_MainTex", myTexture2DArray[2]);
    }

    public void GetNeutral()
    {
        myRenderer.material.SetTexture("_MainTex", myTexture2DArray[0]);
    }
    public void StartTalking()
    {
        StartCoroutine(Talk());
    }

    public void StopTalking()
    {
        StopCoroutine(Talk());
        StopAllCoroutines();
        myRenderer.material.mainTexture = myTexture2DArray[0];
    }
    public void HandsUp()
    {
       myAnimator.SetBool("Hand Raised", true);
    }
    public void HandsDown()
    {
        myAnimator.SetBool("Hand Raised", false);
    }
    public void HandsUpIdle()
    {
        myAnimator.SetBool("Hand up idle", true);
    }

}
