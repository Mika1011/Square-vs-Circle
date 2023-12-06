using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject levelSelectMenu;
    [SerializeField] private Toggle effectToggle;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private Button enterNameButtonPrefab;
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider audioSlider;
    [SerializeField] private PostProcessProfile ppp;

    private void Start()
    {
        if (PlayerPrefs.GetInt("PPPOn") == 1)
        {
            effectToggle.isOn = true;
        } else if (PlayerPrefs.GetInt("PPPOn") == 0)
        {
            effectToggle.isOn = false;
        }
    }

    public void play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void options()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(true);
    }
    public void quit()
    {
        Application.Quit();
    }
    public void back() 
    { 
        mainMenu.SetActive(true);
        levelSelectMenu.SetActive(false);
        optionsMenu.SetActive(false);
    }
    public void effectSwitch()
    {
        if(effectToggle.isOn == true)
        {
            PlayerPrefs.SetInt("PPPOn", 1);
            mainCamera.GetComponent<MainCamera>().ChangePostProcessing();
        } else if(effectToggle.isOn == false)
        {
            PlayerPrefs.SetInt("PPPOn", 0);
            mainCamera.GetComponent<MainCamera>().ChangePostProcessing();
        }
    }
    public void scoreBoardButton()
    {
        SceneManager.LoadScene(7);
        enterNameButtonPrefab.interactable = false;
    }

    public void audioSliderChange()
    {
        mixer.SetFloat("MusicVolume",audioSlider.value);
    }

    public void goToLevelSelect()
    {
        mainMenu.SetActive(false);
        optionsMenu.SetActive(false);
        levelSelectMenu.SetActive(true);
    }
}
