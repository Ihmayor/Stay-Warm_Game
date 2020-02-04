using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndDemoTrigger : MonoBehaviour
{
    public GameObject EndDemoMenu;
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            GameObject player = c.gameObject;
            player.GetComponent<CharacterMovement>().enabled = false;
            player.GetComponent<CharacterMovement>().setAnimation("Idle");
            EndDemoMenu.SetActive(true);
        }

    }
}
