using UnityEngine;
using System.Collections;

public class ClassMate : MonoBehaviour
{
    public Transform arm;
    public bool armUp = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (armUp)
	    {
	        RotateArm(-180);
	    }
	}

    public void RotateArm(float angle)
    {
        armUp = false;
        Debug.Log("Rotating arm");

        Quaternion endRotation = Quaternion.identity;
        endRotation.eulerAngles = new Vector3(angle, 0, 0);
        StartCoroutine(rotateObject(arm, endRotation));
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
