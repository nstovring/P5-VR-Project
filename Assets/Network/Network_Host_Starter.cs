using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Network_Host_Starter : MonoBehaviour {
    NetworkManager manager;
	// Use this for initialization
	void Start () {
        manager = GetComponent<NetworkManager>();
        manager.StartHost();
        Debug.Log(manager.serverBindAddress);
	}
}
