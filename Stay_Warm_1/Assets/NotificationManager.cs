using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class handles setting the notification message associated with every object in the project.
/// It's only job is handle these notifications and possibly log them.
/// </summary>
public class NotificationManager : MonoBehaviour {

    #region Singleton instance
    public static NotificationManager Instance { private set; get; }
    #endregion

    // Use this for initialization
    void Start () {
        NotificationManager.Instance = this;
        //Create all props here with unique ID's 
        GameObject.Find("PaperStart").GetComponent<NotifyPropScript>().SetNotification("Picked up 'Note #1'");


    }

    // Update is called once per frame
    void Update () {
		
	}
}
