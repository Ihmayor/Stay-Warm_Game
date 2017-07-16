using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPropScript : MonoBehaviour {
    private string notificationMessage = "";
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.Find("Exclaim").gameObject.activeSelf && Input.GetKeyDown(KeyCode.J))
        {
            MenuManager.Instance.OpenCuteBox(notificationMessage);
            Destroy(transform.Find("Exclaim").gameObject);
            Destroy(this);
        }
	}

    public void SetNotification(string message)
    {
        notificationMessage = message;
    }

    #region Trigger Scripts

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
            transform.Find("Exclaim").gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            transform.Find("Exclaim").gameObject.SetActive(false);
    }

    #endregion


}
