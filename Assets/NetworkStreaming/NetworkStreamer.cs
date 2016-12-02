using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkStreamer : NetworkBehaviour {
    public NetworkManager manager;
    Quaternion rotations;
    public SceneChooser sceneController1;
    public SceneController sceneController2;
    public IdaSceneController sceneController3;
	// Use this for initialization
	void Start () {
        //manager = GetComponent<NetworkManager>();
        //manager.OnStartHost();
        manager = GameObject.Find("Network Manager").GetComponent<NetworkManager>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (isServer)
        {
            rotations = transform.rotation;
            Rpc_SendRotations(rotations);
            //transform.rotation = Quaternion.Euler(rotations);
        }
	}
    void Update()
    {
        if (isServer)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3(10, 0, 0));
            }
            //rotations = Quaternion.ToEulerAngles(transform.rotation);
        }
        if(isClient && !isServer)
        {
            transform.rotation = rotations;
        }
    }
    //public virtual void OnStartHost()
    //{
    //    Camera = GameObject.Find("Main Camera");
    //}
    /*[Command]
    public void Cmd_SendRotations(Vector3 clientRotate)
    {
        rotations = clientRotate;
        Debug.Log("recieving stuff");
    }
    [ClientRpc]
    public void Rpc_GetRotations()
    {
        if(isClient && !isServer)
        Cmd_SendRotations(rotations);
    }*/
    [ClientRpc]
    public void Rpc_SendRotations(Quaternion rot)
    {
        if (!isServer)
        {
            rotations = rot;
            Debug.Log("getting rotations");
        }
    }
    [ClientRpc]
    public void Rpc_Input(KeyCode key)
    {
        if (!isServer)
        {
            if (sceneController1) { sceneController1.inputSelector(key); }
            else if (sceneController2) { sceneController2.inputSelector(key); }
            else if (sceneController3) { }//sceneController3.inputSelector(key); }
            Debug.Log("getting keycodes");
        }
    }
    public void SendInput(KeyCode key)
    {
        if (isServer)
        {
            Rpc_Input(key);
        }
    }
    public void reset()
    {
        sceneController1 = null;
        sceneController2 = null;
        sceneController3 = null;
    }
}
