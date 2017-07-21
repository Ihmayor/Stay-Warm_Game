using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{

    private bool isHeartCooling; //Bool to hold if Heart Subject to Cooling
    private float HeartCoolingFactor;//Amount Heart should cool by. Centralized as multiple cooling elements can increase its effect
    private float CoolStrength;//Level of cooling strength. Amount to increase or decrease by. 
    private float OriginalHeartCoolingFactor; //Store original cooling factor to reset when out of cooling elements

    private float SacrificeFactor;

    ///Character fighting against player variables
    public bool isFightingPlayer { private set; get; }
    private int CharacterWill;
    private int Debate;
    private int FightCount;
    private string[] CannedFightResponses = {
        "...Let's not leave.",
        "Not ready to leave. Wait, please",
        "Let me have a little break.",
        "I want to stay here longer.",
        "I'm not ready to leave yet",
        "I want to stay.",
        "Let me catch my breath",
        "I don't want to leave.",
        "It's warm here. Let's stay.",
        "Please, I don't want to move anymore.",
        "I don't want to go.",
        "No. Please...No.",
        "I cannot move on.",
        "Let me just stay here, where it's warm.",
        "I don't want to move.",
        "There is no point.",
        "What will even happen at the end?",
        "Just a little nap.",
        "Let me sleep here. Just a little bit.",
        "It's...so...warm...here...",
        "...I can't go on.",
        "WHY SHOULD I EVEN TRY!? I AM A POINTLESS SPECK WHO WILL AMOUNT TO NOTHING!",
        "Please, let me stay here a little longer. Please.",
        "I can't do this! I just CAN'T!" };
    private string[] CannedMotivationResponses = {
        "Alright, just a little more",
        "Okay, one more checkpoint.",
        "No. I can do this.",
        "Okay, I can move just a little bit further.",
        "Nope. I can feel that I'm almost there.",
        "No. I can make it.",
        "No...I will not give up! I can move my feet a little bit more" };

    public GameObject HeartBar;
    public GameObject HealthBar;
    public string CharacterName;

    #region Init/Repeated Functions
    // Use this for initialization
    void Start()
    {
        HeartCoolingFactor = 0.003f;
        OriginalHeartCoolingFactor = HeartCoolingFactor;
        CoolStrength = 0.001f;
        isHeartCooling = false;
        InvokeRepeating("CheckHeart", 0, 0.2f);

        SacrificeFactor = 0.05f;
        CharacterName = "????";

        isFightingPlayer = false;
        CharacterWill = 5;
        Debate = 2;
        FightCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

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


   
    #endregion

    #region Health Methods
    /// <summary>
    /// Heal the Player's Health
    /// </summary>
    /// <param name="HealthIncrease">Amount to Heal. 1 = 100% </param>
    public void Heal(float HealthIncrease)
    {
        HealthBar.GetComponent<Image>().fillAmount += HealthIncrease;
    }

    /// <summary>
    /// Damage the Player's Health
    /// </summary>
    /// <param name="HealthDecrease">Amount to Damage Range (1,0)</param>
    public void Hurt(float HealthDecrease)
    {
        HealthBar.GetComponent<Image>().fillAmount -= HealthDecrease;
        CheckCharacterHealth();
    }

    public void CheckCharacterHealth()
    {
        float HealthAmount = HealthBar.GetComponent<Image>().fillAmount;
        if (HealthAmount <= 0)
            MenuManager.Instance.ShowGameOver();
        else if (HealthAmount <= 0.2f)
        {
            MenuManager.Instance.SetThought(CharacterName, "There is no point in carrying on. Why bother? I should have never tried in the first place. This was stupid. I am stupid. I cannot believe that I thought that I could ever do this. The pain is far too great.");

            //Increase chance of character backtalk
            isFightingPlayer = true;
            CharacterWill *= 2;
            Debate++;
        }
        else if (HealthAmount <= 0.333f)
        {
            MenuManager.Instance.SetThought(CharacterName, "I don't want to move on.");

            //Character will start fighting player when they try to leave a warming element
            isFightingPlayer = true;
            ResetFightLevels(); //If we have healed from a previous encounter, we don't want to still have the same fight levels
        }
        else if (HealthAmount <= 0.5f)
        {
            MenuManager.Instance.SetThought(CharacterName, "I'm so exhausted. I don't want to move anymore.");

            //Trigger harsher Coughing sounds
            //Slow player down
            //Lessen how much healing helps warm the heart
        }
        else if (HealthAmount <= 0.8f)
        {
            MenuManager.Instance.SetThought(CharacterName, "This is really starting to sting. Why am I even doing this?");
            //Trigger Coughing sounds
            //Lessen how much healing helps warm the heart
        }
    }

    /// <summary>
    /// Sacrifice Health
    /// </summary>
    public void SacrificeHealth()
    {
        WarmHeart(SacrificeFactor);
        Hurt(SacrificeFactor);
    }
    #endregion

    #region Heart Methods
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
        HeartBar.GetComponent<Image>().fillAmount -= HeartCoolingFactor;
        if (HeartBar.GetComponent<Image>().fillAmount <= 0)
            MenuManager.Instance.ShowGameOver();
    }

    /// <summary>
    /// Warm up the heart
    /// </summary>
    /// <param name="AmountToWarm">Amount to warm heart by. 1 = 100%</param>
    public void WarmHeart(float AmountToWarm)
    {
        HeartBar.GetComponent<Image>().fillAmount += AmountToWarm;
    }
    #endregion

    #region Character Motivation Levels
    public void FightPlayer()
    {
        if (!isFightingPlayer)
            return;
        if (FightCount >= 3)
        {
            MenuManager.Instance.ShowGameOver();
            MenuManager.Instance.SetThought(CharacterName, "It's better to stay here after all");
            return;
        }

        int Response = Random.RandomRange(0, CharacterWill);
        if (Response <= CharacterWill / Debate)
        {
            isFightingPlayer = false;
            FightCount = 0;
            Heal(Random.Range(0.01f, 0.2f));
            MenuManager.Instance.SetThought(CharacterName, CannedMotivationResponses[Random.Range(0, CannedMotivationResponses.Length - 1)]);
            return;
        }
        FightCount++;

        //Character responds to player
        MenuManager.Instance.SetThought(CharacterName, CannedFightResponses[Random.Range(0, CannedFightResponses.Length - 1)]);
    }

    private void ResetFightLevels()
    {
        Debate = 2;
        CharacterWill = 5;
    }
    #endregion
}
