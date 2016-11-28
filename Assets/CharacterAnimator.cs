using UnityEngine;
using System.Collections;

public class CharacterAnimator : MonoBehaviour {

    public enum classMateStates
    {
        Idle, Sleeping, Drawing, HandsUp
    }

    public enum classMateType
    {
        male, female
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
    public Animator myAnimator;

    // Use this for initialization
    void Start () {
        switch (myType)
        {
            case classMateType.male:
                myTexture2DArray = MaleTextures;
                break;
            case classMateType.female:
                myTexture2DArray = FemaleTextures;
                break;
        }

        myAnimator = GetComponent<Animator>();
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
}
