using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour {

    #region Variables

    public enum PlatformType
    {
        Horizontal,
        Vertical
    }

    public PlatformType Type;
    public bool ReverseDirection;
    public float MoveDistance; // 1/2 Of Total distance moved by the platform
    private float MoveSpeed; // Speed of platform movement

    private bool isMovePositive;
    public float MoveNegativeMax { get; private set; }
    public float MovePositiveMax { get; private set; }

    public float SpeedDeviation { get; private set; }

    #endregion

    #region Init and Movement Functions

    // Use this for initialization
    void Start()
    {
        float CheckAxisVar = GetAxisVar(Type);
        MoveSpeed = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (MoveDistance == 0)
        {
            Debug.Log("Error. Distance not set for: " + GetInstanceID());
            Debug.Log("MaxDistance" + MoveNegativeMax);
        }
        Vector3 VectorChange = new Vector3(0,0,0); //Hold the Change in Position
        float CheckAxisVar = GetAxisVar(Type);//Variable Ensures movement doesn't exceed max

        //Check what axis to move the platform
        if (MoveSpeed == 0)
            return;
        if (Type == PlatformType.Horizontal)
            VectorChange = new Vector3(MoveDistance / MoveSpeed, 0, 0);
        else if (Type == PlatformType.Vertical)
            VectorChange = new Vector3(0, MoveDistance / MoveSpeed, 0);

        //Check the direction of which to mvoe the platform, revert if platform is set to move negative first then positive
        //Compare it to the proper axis specified by the type of platform
        if (CheckReverse(isMovePositive))
        {
            if (CheckAxisVar >= MovePositiveMax)
            {
                isMovePositive = CheckReverse(false);
                return;
            }
            gameObject.transform.position += VectorChange;
        }
        else
        {
            if (CheckAxisVar <= MoveNegativeMax)
            {
                isMovePositive = CheckReverse(true);
                return;
            }
            gameObject.transform.position -= VectorChange;
        }
    }

    #endregion

    #region Outside Access Methods

    /// <summary>
    /// Set the distance it will move to and fro on the axis for the platform type. 
    /// [Negative]CurrentAxis -= setDistance, [Positive] CurrentAxis += setDistance
    /// </summary>
    /// <param name="setDistance">Float it will move forward and backward</param>
    public void SetNewDistance(float setDistance)
    {
       // Debug.Log("Distance set to: " + setDistance + " for " + GetInstanceID());

        //Get corresponding position based on the set axis for the platform type
        float CheckAxisVar = GetAxisVar(Type);

        //Set the distance to move alongside the axis
        this.MoveDistance = setDistance;
        MoveNegativeMax = CheckAxisVar - MoveDistance;
        MovePositiveMax = CheckAxisVar + MoveDistance;
        Update();
    }
    /// <summary>
    /// Modify the speed's result with a decrease or increase. Want to avoid setting the actual variable
    /// </summary>
    /// <param name="IsIncrease">Increase or decrease speed</param>
    /// <param name="AmountOfChange">Scale of which to increase or decraese the speed. Reset to Original by setting this to 0</param>
    public void ChangeSpeed(bool IsIncrease, float AmountOfChange)
    {
        //Allow for reset of speed to original
        if (AmountOfChange == 0)
        {
            if (SpeedDeviation < 0)
            {
                MoveSpeed /= SpeedDeviation;
            }
            else if (SpeedDeviation > 0)
            {
                MoveSpeed *= SpeedDeviation;
            }
            SpeedDeviation = 0;
        }
        else if (IsIncrease)
        {
            if (AmountOfChange == 0)
                return;
            MoveSpeed /= AmountOfChange;
            SpeedDeviation += AmountOfChange;
        }
        else
        {
            if (AmountOfChange == 0)
                return;
            MoveSpeed *= AmountOfChange;
            SpeedDeviation -= AmountOfChange;
        }

    }

    #endregion

    #region Misc Helper Methods

    /// <summary>
    /// Get the Position Variable on the corresponding axis based on platform type
    /// </summary>
    /// <param name="TypeCheck">Platform type to find axis position</param>
    /// <returns></returns>
    private float GetAxisVar(PlatformType TypeCheck)
    {
        switch (TypeCheck)
        {
            case PlatformType.Horizontal:
                return gameObject.transform.position.x;
            case PlatformType.Vertical:
                return gameObject.transform.position.y;
            default:
                return 0;
        }
    }

    /// <summary>
    /// Checks if the looped direction order needs to be reversed
    /// </summary>
    /// <param name="CheckDirection">Original direction bool</param>
    /// <returns></returns>
    private bool CheckReverse(bool CheckDirection)
    {
        if (ReverseDirection)
            return !CheckDirection;
        else
            return CheckDirection;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Player"))
            collision.gameObject.gameObject.transform.parent = this.gameObject.transform;
      }

    private void OnCollisionExit2D(Collision2D collision)
    {

        if (collision.collider.tag.Contains("Player"))
            collision.collider.gameObject.transform.parent = null;
     }

    #endregion

}
