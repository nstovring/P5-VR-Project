using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class NetworkStreamer : NetworkBehaviour {
    public NetworkManager manager;
    Vector3 rotations;
    public GameObject Camera;
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
            Rpc_GetRotations();
            transform.rotation = Quaternion.Euler(rotations);
        }
	}
    void Update()
    {
        if (isClient && !isServer)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                transform.Rotate(new Vector3(10, 0, 0));
            }
            rotations = Quaternion.ToEulerAngles(transform.rotation);
        }
    }
    //public virtual void OnStartHost()
    //{
    //    Camera = GameObject.Find("Main Camera");
    //}
    [Command]
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
    }
}
