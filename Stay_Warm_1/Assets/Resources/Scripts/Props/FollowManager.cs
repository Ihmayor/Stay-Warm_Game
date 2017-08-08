using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowManager : MonoBehaviour {
    public GameObject Target;
    public static FollowManager Instance { get; private set; }
    public float Neighbour;
    public float maxForce;
    public float maxVelocity;

    // Use this for initialization
	void Start () {
        Instance = this;
        Neighbour =5f;
        maxForce = 3f;
        maxVelocity =2f;
    }
	
}
