using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotifyPropScript : MonoBehaviour {
    public string notificationMessage = "";
    public string optionalThoughtMessage = "";

    // Use this for initialization
    void Start () {
	}

    public void AddExclaim(Transform parent)
    {
        GameObject ExclaimPrefab = Resources.Load<GameObject>("Prefabs/PropsAndNots/Exclaim");
        GameObject ExclaimInstance = Instantiate(ExclaimPrefab);
        ExclaimInstance.transform.parent = parent;
        ExclaimInstance.transform.localPosition = new Vector3(0,1.49f);
        ExclaimInstance.SetActive(false);
        Debug.Log(ExclaimInstance.name);
    }
	
	// Update is called once per frame
	void Update () {
		if (transform.Find("Exclaim(Clone)").gameObject.activeSelf && Input.GetKeyDown(KeyCode.J))
        {
            MenuManager.Instance.OpenInteractionMenu(notificationMessage);
            if (optionalThoughtMessage != "")
            {
                MenuManager.Instance.SetThought("",optionalThoughtMessage);
            }
            Destroy(transform.Find("Exclaim(Clone)").gameObject);
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
            transform.Find("Exclaim(Clone)").gameObject.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            transform.Find("Exclaim(Clone)").gameObject.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTriggerEnter2D(collision.collider);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        OnTriggerExit2D(collision.collider);

    }

    #endregion


}
