using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStatus : MonoBehaviour
{

    #region Heart Cooling Variables

    private bool isHeartCooling; //Bool to hold if Heart Subject to Cooling
    public bool isHeartWarming { set; private get; } //Bool to hold if Heart Subject to warming
    private float HeartCoolingFactor;//Amount Heart should cool by. Centralized as multiple cooling elements can increase its effect
    private float CoolStrength;//Level of cooling strength. Amount to increase or decrease by. 
    private float OriginalHeartCoolingFactor; //Store original cooling factor to reset when out of cooling elements
    private float HeartWait;
    public bool isFirstCooling { get; private set; }
    public bool hasHeart;
    public bool isBehindCoolingBlock;
    public bool canRespawn { get; private set; }
    public bool Respawning { get; private set; }
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
        "They did not want to leave.",
        "They were not ready to leave quite yet.",
        "They decided to take a break.",
        "Just a little longer, they decided.",
        "They were not ready to leave.",
        "They wanted to stay.",
        "They needed to catch their breath",
        "They urged themselves to stay.",
        "It was so warm. They wanted to stay.",
        "Their body could not move another inch forward.",
        "They did not want to go on.",
        "No. They refused to leave.",
        "They could not bear to step out in the cold.",
        "They wanted to stay where it was warm.",
        "Their body did not want to move.",
        "They lost more faith in moving on.",
        "They questioned what would even happen at the end.",
        "They wanted to take a longer nap instead of moving.",
        "They stepped back again to embrace the nice warmth.",
        "Warmth bathed them and they body shook weakly like jelly.",
        "...They did not want to leave the warmth.",
        "They screamed to the void about the pointlessness of it all.",
        "They stayed a little longer still.",
        "They no longer believed that they could do it." };
    private string[] CannedMotivationResponses = {
        "They discovered the hope to move another step forward.",
        "They set their eyes on the next warm light in the distance.",
        "They urged themselves to take the next step.",
        "They convinced their body to move a little more.",
        "They felt like they were close.",
        "They found renewed belief that they could make it.",
        "Though hesistant, they refused to give up!" };

    private int matchCount;

    #endregion

    #region UI and Other Scene Interaction Variables

    private AudioSource audioSource;
    public GameObject HeartBar; //Assigned from the menu in the scene
    public GameObject HealthBar; //Assigned from the menu in the scene
    public string CharacterName;
    public bool isWarmingWithMatches { private set; get; }
    public bool isFirstWarming { get; private set; }

    public bool GameOver { private set; get; }
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
        HeartCoolingFactor = 0.004f;
        OriginalHeartCoolingFactor = HeartCoolingFactor;
        CoolStrength = 0.0005f;
        isHeartCooling = false;
        HeartWait = 0.005f;
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

        //Ability to respawn. Toggled off in roguelike setting
        canRespawn = true;

        //Get Audio Source for later effect usage
        audioSource = this.GetComponent<AudioSource>();
    }

    /// <summary>
    /// Repeatedly cool the heart. if heart is near cooling factors, then cool faster
    /// </summary>
    private void CheckHeart()
    {
        if (isHeartCooling && hasHeart)
        {
            this.GetComponentInChildren<SmokeScript>().ShrinkSmoke(HeartCoolingFactor * 10);
            CoolHeart(HeartCoolingFactor);
        }
        else if (!isWarmingWithMatches && !isHeartWarming && hasHeart)
        {
            this.GetComponentInChildren<SmokeScript>().ShrinkSmoke(HeartWait*1000);
            CoolHeart(HeartWait);
        }
    }

    public void ActivateHeartEffect()
    {
        hasHeart = true;
        this.transform.Find("Smoke").gameObject.SetActive(true);
        this.GetComponentInChildren<SmokeScript>().ActivateSmoke();
    }

    /// <summary>
    /// Triggered Player First Encounters
    /// </summary>
    public void ToggleOffFirstWarming()
    {
        MenuManager.Instance.SetThought(CharacterName, "It felt warm underneath the light. They stayed there for a little while.");
        isFirstWarming = false;
    }

    public void ToggleOffFirstCooling()
    {
        MenuManager.Instance.SetThought(CharacterName, "It was cold. They knew that they shouldn't stay in the wind too long. They had to find cover of some sort.");
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
            isWarmingWithMatches = true;
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
        {
            if (canRespawn)
                Respawn();
            else
                MenuManager.Instance.ShowGameOver();
        }
        else if (HealthAmount <= 0.2f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "They felt stupid. There was no pointing in moving on. They regretted even trying in the first place.");

            //Increase chance of character backtalk
            isFightingPlayer = true;
            CharacterWill *= 2;
            Debate++;
        }
        else if (HealthAmount <= 0.333f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "They did not want to move.");
            //Character will start fighting player when they try to leave a warming element
            isFightingPlayer = true;
            ResetFightLevels(); //If we have healed from a previous encounter, we don't want to still have the same fight levels
        }
        else if (HealthAmount <= 0.5f)
        {
            if (setThought)
                MenuManager.Instance.SetThought(CharacterName, "They were exhausted. They could barely urge the will to move.");
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
                MenuManager.Instance.SetThought(CharacterName, "They found it hard to breathe. They wondered why they were even doing this?");
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
        float HeartAmount = HeartBar.GetComponent<Image>().fillAmount;
        if (!isWarmingWithMatches)
        {
            if (HeartBar.GetComponent<Image>().fillAmount <= 0)
            {
                if (canRespawn)
                    Respawn();
                else
                    MenuManager.Instance.ShowGameOver();
            }
            else if (HeartAmount <= 0.1f && UnityEngine.Random.Range(0, 20) <= 0)
            {
                MenuManager.Instance.SetThought(CharacterName, "The heart was getting cold. They wished that there was something they could do.");
            }
            else if (HeartAmount <= 0.5f && UnityEngine.Random.Range(0, 35) <= 0)
            {
                MenuManager.Instance.SetThought(CharacterName, "The heart was freezing. They really had to find someplace warm very soon or else.");
            }
        }
    }

    /// <summary>
    /// Warm up the heart
    /// </summary>
    /// <param name="AmountToWarm">Amount to warm heart by. 1 = 100%</param>
    public void WarmHeart(float AmountToWarm)
    {
        this.GetComponentInChildren<SmokeScript>().GrowSmoke(AmountToWarm);
        HeartBar.GetComponent<Image>().fillAmount += AmountToWarm;
    }


    #endregion

    #region Character Motivation Methods

    /// <summary>
    /// Handles character to fight against user/player's controls.
    /// </summary>
    public void FightPlayer()
    {
        if (!isFightingPlayer)
            return;
        if (FightCount >= 3)
        {
            MenuManager.Instance.ShowGameOver();
            MenuManager.Instance.SetThought(CharacterName, "They decided to just stay in place.");
            return;
        }

        int Response = UnityEngine.Random.Range(0, CharacterWill);
        if (Response <= CharacterWill / Debate)
        {
            isFightingPlayer = false;
            FightCount = 0;
            Heal(UnityEngine.Random.Range(0.01f, 0.2f));
            MenuManager.Instance.SetThought(CharacterName, CannedMotivationResponses[UnityEngine.Random.Range(0, CannedMotivationResponses.Length - 1)]);
            return;
        }
        FightCount++;

        //Character responds to player
        MenuManager.Instance.SetThought(CharacterName, CannedFightResponses[UnityEngine.Random.Range(0, CannedFightResponses.Length - 1)]);
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
        WarmHeart(1);
        MenuManager.Instance.RemoveMatchAt(matchCount);
        matchCount--;
        isFightingPlayer = false;
        isWarmingWithMatches = false;
        SwitchCharacterStates();
    }

    /// <summary>
    ///Switch Normal Character Animation with Warming Character Animation via Character Object substitution
    /// </summary>

    private void SwitchCharacterStates()
    {
        GameObject mainState = GameObject.Find("MainChar");
        GameObject warmState = GameObject.Find("WarmChar");
        if (isWarmingWithMatches)
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

    #region Respawn Methods

    public void Respawn()
    {
        Respawning = true;
        MenuManager.Instance.SetThought("", "They ached all over. Their eyes faded out into the dark");
        MenuManager.Instance.ShowRespawn();
        
        if (HealthBar != null)
            HealthBar.GetComponent<Image>().fillAmount = 1;
        if (HeartBar != null)
            HeartBar.GetComponent<Image>().fillAmount = 1;

        Vector3 ClosestWarmingElement =  LevelManager.SingletonInstance.GetClosestWarmingElementPosition(gameObject.transform.position);
        if (ClosestWarmingElement != null)
        {
            gameObject.transform.position = ClosestWarmingElement + new Vector3(0, 0.8f);
        }
        Respawning = false;
    }
    #endregion
}
