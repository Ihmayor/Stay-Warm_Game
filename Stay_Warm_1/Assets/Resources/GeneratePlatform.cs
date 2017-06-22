using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("test??");
        if (collision.CompareTag("Player"))
        {
            int coinFlip = Random.Range(0, 2);
            Vector3 verticalChange;
            if (coinFlip > 0)
                verticalChange = Vector3.up / 2;
            else
                verticalChange = Vector3.down / 2;


            Vector3 position = transform.position + Vector3.right + verticalChange;
            Instantiate(gameObject, position, transform.rotation);
            Invoke("SelfDestruct",1.5f);
        }
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}