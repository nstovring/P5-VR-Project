using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Network_Streamer : NetworkBehaviour {

    public SceneChooser controller1;
    public SceneController controller2;
    public IdaSceneController controller3;
    public DescriptionSceneController controller4;
    Quaternion rotations;
    Transform myTransform;

	// Use this for initialization
	void Start () {
        myTransform = transform;
	}
	void OnAwake()
    {
        //gameObject.SetActive(true);
    }
	// Update is called once per frame
	void FixedUpdate () {
        if (isServer)
        {
            Rpc_SendRotations(myTransform.rotation);
        }
	}
    void Update()
    {
        if (isServer)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                myTransform.Rotate(new Vector3(10, 0, 0));
            }
        }
        if(isClient && !isServer)
        {
            
            myTransform.rotation = rotations;
        }
    }
    [ClientRpc]
    public void Rpc_SendRotations(Quaternion rot)
    {
        Debug.Log("recieving rotation");
        rotations = rot;
    }
    public void reset()
    {
        controller1 = null;
        controller2 = null;
        controller3 = null;
    }
    [ClientRpc]
    public void Rpc_SendAction(KeyCode key)
    {
        if (!isServer)
        {
            if (controller1)
            {
                controller1.Actions(key);
                Debug.Log("recieving actions");
            }
            else if (controller2)
            {
                controller2.Action(key);
                Debug.Log("recieving actions");
            }
            else if (controller3)
            {
                //controller3.Action(key);
                Debug.Log("recieving actions");
            }
            else if (controller4)
            {
                controller4.Action(key);
                Debug.Log("recieving actions");
            }
        }
    }
}
