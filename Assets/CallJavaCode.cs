using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System;

public class CallJavaCode : MonoBehaviour {


	private string cacheDir = "Push to get cache dir";
    private AndroidJavaClass empaticaPluginClass;
    void Start()
    {
         empaticaPluginClass = new AndroidJavaClass("com.empatica.sample.MainActivity");
    }
    void OnGUI ()
	{
		if (GUI.Button(new Rect (15, 125, 450, 100), cacheDir))
		{
            empaticaPluginClass = new AndroidJavaClass("com.empatica.sample.MainActivity");

            if (empaticaPluginClass == null)
            {
                Debug.Log("No plugin!!");
                return;
            }
            empaticaPluginClass.CallStatic("OnUnityStart");
            AndroidJavaObject activity = empaticaPluginClass.GetStatic<AndroidJavaObject>("currentActivity");
            AndroidJavaObject context = activity.Call<AndroidJavaObject>("getApplicationContext");
            empaticaPluginClass.CallStatic("initialize", context);
            AndroidJavaObject empaticaDeviceManager = new AndroidJavaObject("EmpaDeviceManager", context);
        }
	}
}
