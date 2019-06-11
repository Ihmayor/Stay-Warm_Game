using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBehavior : MonoBehaviour {

    private float walkingRange = 5;
    public Vector3 StartingPoint;
    private bool isReturn = false;
    private bool isActive = false;
    private readonly int speed = 1000;
    private bool isPlayerInView = false;
    private bool isPlayerNearby = false;

    private float currentPos = gameObject.transform.position.x;
    private float startPoint = StartingPoint.x;
    private float endRange = gameObject.transform.position.x - walkingRange;
    private float movementIncrement = walkingRange / 1000;


    // Use this for initialization
    void Start () {
        StartingPoint = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        if (isActive)
        {
            ActiveMovement();
        }
 	}

    public void SetWalkingRange(float range)
    {
        walkingRange = range;
    }

    public void TriggerPlayerInView(bool trigger)
    {

    }

    public void TriggerPlayerNearby(bool trigger)
    {

    }

    public void SetActiveBehaviour(bool activeStatus)
    {
        isActive = activeStatus;
    }

    //Move back and forth between given range. 
    public void ActiveMovement()
    {

        if (isPlayerInView)
        {
            if (isPlayerNearby)
            {
                Attack();
            }
            else
            {
                Chase();
            }
        }
        else
        {
            March();
        }

    }

    private void Attack()
    {
        //setAnimation() for smash
        //Upon smash completion, if player is in view but not nearby, chase, if nearby attack, if out return to start point
        //If out ifReturn = true;
        //March();
    }


    private void Chase()
    {
        //Based on player location continue marching towards them.
        //Plus minus depending on player position vector
    }

    private void March()
    {
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
