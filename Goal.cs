using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Goal : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Timer timer;
    [SerializeField] private TMP_Text timerText;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            timer.stopTimer();
            timer.saveTime();
            timerText.text = "Time: " + timer.getCurrentTime().ToString("F1");
            animator.SetTrigger("Win");
            other.gameObject.SetActive(false);
        }
    }
}
