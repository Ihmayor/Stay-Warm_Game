using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{

    #region Heart Cooling Variables

    private bool isHeartCooling; //Bool to hold if Heart Subject to Cooling
    private float HeartCoolingFactor;//Amount Heart should cool by. Centralized as multiple cooling elements can increase its effect
    private float CoolStrength;//Level of cooling strength. Amount to increase or decrease by. 
    private float OriginalHeartCoolingFactor; //Store original cooling factor to reset when out of cooling elements
    private float HeartWait;
    public bool isFirstCooling { get; private set; }
    public bool hasHeart;
    #endregion

    #region Health Sacrfice Variables

    private float SacrificeFactor;

    #endregion

    #region Character Fighting Variables

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

    private int matchCount;

    #endregion

    #region UI and Other Scene Interaction Variables

    private AudioSource audioSource;
    public GameObject HeartBar; //Assigned from the menu in the scene
    public GameObject HealthBar; //Assigned from the menu in the scene
    public string CharacterName;
    public bool isWarming { private set; get; }
    public bool isFirstWarming { get; private set; }
    public float PushPower;
    #endregion

    #region Init/Repeated Functions
    // Use this for initialization
    void Start()
    {
        //Init First Pass Variables
        isFirstWarming = true;
        isFirstCooling = true;
        hasHeart = false;

        //Init Heart Cooling Factors
        HeartCoolingFactor = 0.002f;
        OriginalHeartCoolingFactor = HeartCoolingFactor;
        CoolStrength = 0.0005f;
        isHeartCooling = false;
        HeartWait = 0.001f;
        InvokeRepeating("CheckHeart", 0, 0.2f);

        //Init sacrifice factor
        SacrificeFactor = 0.05f;

        //Init Character name
        CharacterName = "????";

        //Init character will against player 
        isFightingPlayer = false;
        CharacterWill = 5;
        Debate = 2;
        FightCount = 0;

        //Amount of Matches to restart life
        matchCount = 3;

        //Amount character can push objects
        PushPower = 0.3f;

        //Get Audio Source for later effect usage
        audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Repeatedly cool the heart. if heart is near cooling factors, then cool faster
    /// </summary>
    void CheckHeart()
    {
        if (isHeartCooling && hasHeart)
        {
            CoolHeart(HeartCoolingFactor);
            //Lower Heart Health
        }
        else if (!isWarming && hasHeart)
        {
            CoolHeart(HeartWait);
        }
    }

    public void ToggleOffFirstWarming()
    {
        MenuManager.Instance.SetThought(CharacterName, "Huh, it's really nice here. I should stay a little while to warm up.");
        isFirstWarming = false;
    }

    public void ToggleOffFirstCooling()
    {
        MenuManager.Instance.SetThought(CharacterName, "It's cold. I shouldn't stay in this for too long.");
        isFirstCooling = false;
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
        if (HealthBar.GetComponent<Image>().fillAmount - HealthDecrease <= 0 && matchCount != 0)
        {
            HealthBar.GetComponent<Image>().fillAmount = 0.00000000000000001f;
            isWarming = true;
            SwitchCharacterStates();
            Invoke("TriggerMatchHeal", 5f);
            return;
        }
        else
        {
            HealthBar.GetComponent<Image>().fillAmount -= HealthDecrease;
            CheckCharacterHealth(true);
        }
    }

    public void CheckCharacterHealth(bool setThought)
    {
        float HealthAmount = HealthBar.GetComponent<Image>().fillAmount;
        if (HealthAmount <= 0)
            MenuManager.Instance.ShowGameOver();
        else if (HealthAmount <= 0.2f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "There is no point in carrying on. Why bother? I should have never tried in the first place. This was stupid. I am stupid. I cannot believe that I thought that I could ever do this. The pain is far too great.");

            //Increase chance of character backtalk
            isFightingPlayer = true;
            CharacterWill *= 2;
            Debate++;
        }
        else if (HealthAmount <= 0.333f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "I don't want to move on.");
            //Character will start fighting player when they try to leave a warming element
            isFightingPlayer = true;
            ResetFightLevels(); //If we have healed from a previous encounter, we don't want to still have the same fight levels
        }
        else if (HealthAmount <= 0.5f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "I'm so exhausted. I don't want to move anymore.");
            if (!audioSource.isPlaying)
            {
                AudioClip audio = Resources.Load<AudioClip>("Audio/heartbeat");
                audioSource.clip = audio;
                audioSource.loop = true;
                audioSource.Play();
            }

            //Slow player down
            //Lessen how much healing helps warm the heart
        }
        else if (HealthAmount <= 0.7f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "It's getting hard to breathe. Why am I even doing this?");
            AudioClip audio = Resources.Load<AudioClip>("Audio/double_cough");
            this.GetComponent<AudioSource>().loop = false;
            this.GetComponent<AudioSource>().PlayOneShot(audio);
            //Trigger Coughing sounds
            //Lessen how much healing helps warm the heart
        }
        else
        {
            this.GetComponent<AudioSource>().Stop();
        }
    }

    /// <summary>
    /// Sacrifice Health
    /// </summary>
    public void SacrificeHealth()
    {
        float HealthAmount = HealthBar.GetComponent<Image>().fillAmount;
        int SacrificeEfficiency = 1; //It should be less efficient to warm up the heart the lower the health
        if (HealthAmount <= 0.2f)
        {
            SacrificeEfficiency *= 10;
        }
        else if (HealthAmount <= 0.333f)
        {
            SacrificeEfficiency *= 5;
        }
        else if (HealthAmount <= 0.5f)
        {
            SacrificeEfficiency *= 3;
        }
        else if (HealthAmount <= 0.7f)
        {
            SacrificeEfficiency *= 2;
        }

        WarmHeart(SacrificeFactor / SacrificeEfficiency);
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
    private void CoolHeart(float CoolFactor)
    {
        HeartBar.GetComponent<Image>().fillAmount -= CoolFactor;
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

    #region Character Motivation Methods

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

        int Response = Random.Range(0, CharacterWill);
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

    #region Match System Methods

    private void TriggerMatchHeal()
    {
        Heal(0.75F);
        MenuManager.Instance.RemoveMatchAt(matchCount);
        matchCount--;
        isFightingPlayer = false;
        isWarming = false;
        SwitchCharacterStates();
    }

    private void SwitchCharacterStates()
    {
        GameObject mainState = GameObject.Find("MainChar");
        GameObject warmState = GameObject.Find("WarmChar");
        if (isWarming)
        {
            Debug.Log(warmState);
            warmState.transform.position = new Vector3(mainState.transform.position.x, warmState.transform.position.y, warmState.transform.position.z);
            mainState.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
            warmState.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
        }
        else
        {
            mainState.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 255);
            warmState.GetComponent<SpriteRenderer>().color -= new Color(0, 0, 0, 255);
        }
    }

    #endregion
}
