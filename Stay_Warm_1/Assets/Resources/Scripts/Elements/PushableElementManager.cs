using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableElementManager : MonoBehaviour {

    public static PushableElementManager Instance { get; private set; }

	// Use this for initialization
	void Start () {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
	}
}
