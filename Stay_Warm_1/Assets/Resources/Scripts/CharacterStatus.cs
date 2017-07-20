using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStatus : MonoBehaviour {

    private bool isHeartCooling; //Bool to hold if Heart Subject to Cooling
    private float HeartCoolingFactor;//Amount Heart should cool by.
    private float CoolStrength;//Level of cooling strength. Amount to increase or decrease by. 
    private float OriginalHeartCoolingFactor; //Store original cooling factor to reset when out of cooling elements

    public GameObject HeartBar;
    public GameObject HealthBar;

	// Use this for initialization
	void Start () {
        InvokeRepeating("CheckHeart", 0, 0.2f);
        HeartCoolingFactor = 0.005f;
        OriginalHeartCoolingFactor = HeartCoolingFactor;
        CoolStrength = 0.01f;
        isHeartCooling = false;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// Repeatedly cool the heart if heart is near cooling factors
    /// </summary>
    void CheckHeart()
    {
        if (isHeartCooling)
        {
            CoolHeart();
            //Lower Heart Health
        }
    }

    /// <summary>
    /// Heal the Player's Health
    /// </summary>
    /// <param name="HealthIncrease">Amount to Heal</param>
    public void Heal(float HealthIncrease)
    {

    }

    /// <summary>
    /// Damage the Player's Health
    /// </summary>
    /// <param name="HealthDecrease">Amount to Damage</param>
    public void Hurt(float HealthDecrease)
    {

    }

    /// <summary>
    /// Toggle the cooling on/off. Reset Cooling Factor is toggled off. 
    /// </summary>
    /// <param name="heartCooling"></param>
    public void SetHeartCooling(bool heartCooling)
    {
        isHeartCooling = heartCooling;
        if (!heartCooling)
            ResetCoolingStrength();
    }

    /// <summary>
    /// Increase the cooling strength by set levels
    /// </summary>
    public void IncreaseCoolingStrength()
    {
        HeartCoolingFactor += CoolStrength;
    }

    /// <summary>
    /// Decrease the cooling strength by set levels or reset to back to original
    /// </summary>
    public void DecreaseCoolingStrength()
    {
        if (HeartCoolingFactor > OriginalHeartCoolingFactor - CoolStrength)
            HeartCoolingFactor -= CoolStrength;
        else
            HeartCoolingFactor = OriginalHeartCoolingFactor;
    }

    /// <summary>
    /// Reset the cooling strength to the original set
    /// </summary>
    private void ResetCoolingStrength()
    {
        HeartCoolingFactor = OriginalHeartCoolingFactor;
    }

    /// <summary>
    /// Cool Heart according to heart cooling factor
    /// </summary>
    private void CoolHeart()
    {
        //Fetch Heart Levels
    }

    /// <summary>
    /// Warm up the heart
    /// </summary>
    /// <param name="AmountToWarm">Amount to warm heart by</param>
    public void WarmHeart(float AmountToWarm)
    {
        //Warm Heart
    }



}
