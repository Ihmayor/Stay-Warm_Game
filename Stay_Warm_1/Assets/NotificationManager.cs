using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationManager : MonoBehaviour {

    #region Singleton instance
    public static NotificationManager Instance { private set; get; }
    #endregion

    // Use this for initialization
    void Start () {
        NotificationManager.Instance = this;
        //Create all props here with unique ID's 

        //For now deal with the hardcoded bush
        GameObject.Find("Background").transform.Find("Bush").GetComponent<NotifyPropScript>().SetNotification("You picked up a 'Glass Heart'");

    }

    // Update is called once per frame
    void Update () {
		
	}
}
