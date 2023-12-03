using System.Collections.Generic;
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
    [SerializeField] private Toggle effects;
    [SerializeField] private GameObject cameraPrefab;
    [SerializeField] private Button enterNameButtonPrefab;
    [SerializeField] private List<Button> levelSelectButton = new List<Button>();
    [SerializeField] private AudioMixer mixer;
    [SerializeField] Slider audioSlider;
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
        if(effects.isOn == true)
        {
            Camera.main.GetComponent<PostProcessLayer>().enabled = true;
            cameraPrefab.GetComponent<PostProcessLayer>().enabled = true;
        } else if(effects.isOn == false) 
        {
            Camera.main.GetComponent<PostProcessLayer>().enabled = false;
            cameraPrefab.GetComponent<PostProcessLayer>().enabled = false;
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
