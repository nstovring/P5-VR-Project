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

    public classMateType myType;
    public classMateStates myState;

    public Texture2D[] MaleTextures;
    public Texture2D[] FemaleTextures;
    private Texture2D[] myTexture2DArray;

    public Renderer myRenderer;
    // Use this for initialization
    void Start ()
    {
        //myRenderer = GetComponentInChildren<Renderer>();
        if (myType == classMateType.female)
        {
            myTexture2DArray = FemaleTextures;
        }

        if (myType == classMateType.male)
        {
            myTexture2DArray = MaleTextures;
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
    }
	
	// Update is called once per frame
	void Update () {
	   
	}

    private int myTexture;
    public bool talking;
    [Range(0.1f,2)]
    public float talkSpeed = 1;
    public IEnumerator Talking()
    {
        while (talking)
        {
            myRenderer.material.mainTexture = myTexture2DArray[0];
            yield return new WaitForSeconds(talkSpeed/10);
            myRenderer.material.mainTexture = myTexture2DArray[1];
        }
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
}
