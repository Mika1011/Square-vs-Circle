using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InGameUI gameUI;
    [SerializeField] private Animator animator;
    [SerializeField] private CinemachineVirtualCamera cmVC;
    [SerializeField] private CheckpointManager cpM;
    [SerializeField] private AudioSource hitSound;
    private Rigidbody2D rb;
    [SerializeField] private Slider invincSlider;
    [SerializeField] private PostProcessVolume ppv;
    [SerializeField] private List<GameObject> collectables;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private Timer timerObject;
    
    [Header("Values")]
    [SerializeField] private int health = 3;
    [SerializeField] private Vector3 startPos;
    private float timer = 0;
    private float invinceTime = 0;
    
    [Header("Chcks")]
    private bool isDead = false;
    private bool isInvincible = false;

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        if (Physics2D.gravity.y > 0)
        {
            Physics2D.gravity *= -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7 && !isDead)
        {
            damage();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Lava"))
        {
            health = 0;
            dead();
            gameUI.UpdateHealth(health);
        }
    }

    private void Update()
    {
        if (invincSlider && invincSlider.value > 0)
        {
            if (Time.time > invinceTime + .04F)
            {
                invinceTime = Time.time + .04F;
                invincSlider.value -= 0.08F;
            }
        }

        if (Input.GetKeyDown(KeyCode.Delete) && !isDead)
        {
            setBackCollectables();
            damage();
        }
    }

    void dead()
    {
        timerText.text = "Time: " + timerObject.getCurrentTime().ToString("F1");
        animator.SetTrigger("Dead");
        gameObject.SetActive(false);
        hitSound.Play();
    }

    void damage()
    {
        if (!isInvincible)
        {
            hitSound.Play();
            health -= 1;
            cmVC.GetComponent<CameraShake>().Shake(10, .2F);
            gameUI.UpdateHealth(health);
            setBackCollectables();

            if (health == 0) 
            {                
                dead();
            }
            
            if (cpM.getLastCheck() != null)
            {
                transform.position = cpM.getLastCheck().transform.position;
            }
            else
            {
                transform.position = startPos;
                rb.velocity = new Vector2(0, 0);
            }
        }
    }
    
    void setBackCollectables()
    {
        if (collectables.Count > 0)
        {
            for (int i = 0; i < collectables.Count; i++)
            {
                collectables[i].SetActive(true);
            }
        }
    }
    public void addHealth(int number)
    {
        health += number;
        gameUI.UpdateHealth(health);
    }

    public void startInvincibility(float timer)
    {
        this.timer = timer;
        invincSlider.maxValue = timer;
        invincSlider.value = invincSlider.maxValue;
        invinceTime = Time.time;
        StartCoroutine(invincibility());
    }

    IEnumerator invincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(timer);
        isInvincible = false; 
    }
}
