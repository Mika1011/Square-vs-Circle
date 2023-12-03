using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    [SerializeField] private Button enterNameButtonPrefab;
  
    public void UpdateHealth(int health)
    {
        healthText.text = "Health: " + health.ToString();
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
        if (Physics2D.gravity.y > 0)
        {
            Physics2D.gravity *= -1;
        }
    }
    public void Menu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void EnterScoreButton()
    {
        SceneManager.LoadScene(6);
        enterNameButtonPrefab.interactable = true;
    }

    public void nextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
