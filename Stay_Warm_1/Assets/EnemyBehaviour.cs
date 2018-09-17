using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private float walkingRange = 5;
    public Vector3 StartingPoint;
    private bool isReturn = false;
    private bool isActive = false;
    private readonly int speed = 1000;

	// Use this for initialization
	void Start () {
        StartingPoint = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            ActiveMovement();//Potentially set delay???
        }
 	}

    public void SetWalkingRange(float range)
    {
        walkingRange = range;
    }

    public void SetActiveBehaviour(bool activeStatus)
    {
        isActive = activeStatus;
    }

    //Move back and forth between given range. 
    public void ActiveMovement()
    {
        float currentPos = gameObject.transform.position.x;
        float startPoint = StartingPoint.x;
        float endRange = gameObject.transform.position.x - walkingRange;
        float movementIncrement = walkingRange / 1000;

        if (isReturn && currentPos < startPoint)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + movementIncrement, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (currentPos == startPoint)
        {
            isReturn = !isReturn;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - movementIncrement, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (!isReturn && currentPos > endRange)
        {
            gameObject.transform.position = new Vector3(gameObject.transform.position.x - movementIncrement, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if (currentPos == endRange)
        {
            isReturn = !isReturn;
            gameObject.transform.position = new Vector3(gameObject.transform.position.x + movementIncrement, gameObject.transform.position.y, gameObject.transform.position.z);
        }

    }


}
