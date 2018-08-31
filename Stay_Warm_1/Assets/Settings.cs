using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    public GameObject MainAudioSource;
    public GameObject SecondaryAudioSource;
    private string isMuted;
	// Use this for initialization
	void Start () {
        Sprite musicMute = Resources.LoadAll<Sprite>("Prefabs/Sprites/MainMenuComponents")[14];
        Sprite musicDefault = Resources.LoadAll<Sprite>("Prefabs/Sprites/MainMenuComponents")[15];
        isMuted = PlayerPrefs.GetInt("IsMute").ToString();
        if (isMuted.Equals("0"))
        {
            gameObject.GetComponent<Image>().sprite = musicDefault;
            MainAudioSource.GetComponent<AudioSource>().mute = false;
            if (SecondaryAudioSource != null)
            {
                SecondaryAudioSource.GetComponent<AudioSource>().mute = false;
            }
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = musicMute;
            MainAudioSource.GetComponent<AudioSource>().mute = true;
            if (SecondaryAudioSource != null)
            {
                SecondaryAudioSource.GetComponent<AudioSource>().mute = true;
            }
        }
    }

    public void onClickMute()
    {
        print(isMuted);
        if (isMuted.Equals("0"))
        {
            isMuted = "1";
            PlayerPrefs.SetInt("IsMute", 1);
            MainAudioSource.GetComponent<AudioSource>().mute = true;
            if (SecondaryAudioSource != null)
            {
                SecondaryAudioSource.GetComponent<AudioSource>().mute = true;
            }
        }
        else
        {
            isMuted = "0";
            PlayerPrefs.SetInt("IsMute", 0);
            MainAudioSource.GetComponent<AudioSource>().mute = false;
            if (SecondaryAudioSource != null)
            {
                SecondaryAudioSource.GetComponent<AudioSource>().mute = false;
            }
        }
    }
	
}
