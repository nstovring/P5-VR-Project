using UnityEngine;
using System.Collections;

public class ClassMate : MonoBehaviour
{
    public Transform arm;
    public bool armUp = false;
    private Animator myAnimator;
    public enum classMateStates
    {
        Idle,Sleeping, Drawing,HandsUp
    }

    private classMateStates myState;
	// Use this for initialization
	void Start ()
	{
	    myAnimator = GetComponentInChildren<Animator>();
	    switch (myState)
	    {
	        case classMateStates.Idle:
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
	    //if (armUp)
	    //{
	    //    RotateArm(-180);
	    //}
	}

    public void HandsUp()
    {
       myAnimator.SetBool("Hand Raised", true);
    }
    public void HandsDown()
    {
        myAnimator.SetBool("Hand Raised", false);
    }
    IEnumerator rotateObject(Transform curObject, Quaternion endRotation)
    {
        while (Quaternion.Angle(curObject.transform.rotation, endRotation) > 0.1)
        {
            curObject.transform.rotation = Quaternion.Lerp(curObject.transform.rotation, endRotation, 0.1f);
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(2);
    }
}
