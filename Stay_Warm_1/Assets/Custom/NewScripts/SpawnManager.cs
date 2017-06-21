using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public int maxPlatforms = 20;
    public GameObject platform;
    public float horizontalMin = 6.5f;
    public float horizontalMax = 14f;
    public float verticalMin = -4;
    public float verticalMax = 4;

    private Vector2 originPosition;
    // Use this for initialization
    void Start () {
        originPosition = transform.position;
        Spawn();
	}
	
	// Update is called once per frame
	void Spawn () {
		for(int i = 0; i< maxPlatforms; i++)
        {
            Vector2 randomPosition = originPosition + new Vector2(Random.Range(horizontalMin, horizontalMax), Random.Range(verticalMin, verticalMax));
            Instantiate(platform, randomPosition, Quaternion.identity);
            originPosition = randomPosition;
        }
	}
}
