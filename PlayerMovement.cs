using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float inAirMultiplier = 1f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float speedBoostMultiplier = 1.8F;
    [SerializeField] private float speedBoostDuration = 5F;
    [SerializeField] private float jumpBoostMultiplier = 1.5F;
    [SerializeField] private float jumpBoostDuration = 5F;
    private Rigidbody2D rb;
    
    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    private Vector3 offset = new Vector3(0, -.5F, 0);
    private bool isGrounded;
    private bool ceilingCheck;
    
    [Header("Items")]
    [SerializeField] private Slider rgSlider;
    private bool rg = false;
    private float rgTime = 0;
    private float timer = 0;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position + offset , .5f, groundLayer);
        ceilingCheck = Physics2D.OverlapCircle(transform.position - offset , .5f, groundLayer);

        Move();

        if (isGrounded || ceilingCheck)
        {
            inAirMultiplier = 1F;
        }
        else
        {
            inAirMultiplier = 0.6F;
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded && Physics2D.gravity.y < 0)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            }
            else if(ceilingCheck && Physics2D.gravity.y > 0)
            {
                rb.AddForce(Vector2.down * jumpForce, ForceMode2D.Impulse);
            }
        }

        if (rg && rgSlider.value > 0)
        {
            if (Time.time > rgTime + .04F)
            {
                rgTime = Time.time + .04F;
                rgSlider.value -= 0.08F;
            }
        } else if (rgSlider.value <= 0)
        {
            rg = false;
        }
    }

    void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 movement = new Vector2(horizontalInput * moveSpeed * inAirMultiplier, rb.velocity.y);
        rb.velocity = movement;
    }

    public void startSpeedBoost()
    {
        StartCoroutine(speedBoost());
    }
    IEnumerator speedBoost()
    {
        moveSpeed *= speedBoostMultiplier;
        yield return new WaitForSeconds(speedBoostDuration);
        moveSpeed /= speedBoostMultiplier;
    }
    public void startJumpBoost()
    {
        StartCoroutine(jumpBoost());
    }
    IEnumerator jumpBoost()
    {
        jumpForce *= jumpBoostMultiplier;
        yield return new WaitForSeconds(jumpBoostDuration);
        jumpForce /= jumpBoostMultiplier;
    }

    public void startReverseGravity(float timer)
    {
        this.timer = timer;
        rgSlider.maxValue = timer;
        rgSlider.value = rgSlider.maxValue;
        rgTime = Time.time;
        rg = true;
        StartCoroutine(reverseGravity());
    }
    
    IEnumerator reverseGravity()
    {
        changeGrav();
        yield return new WaitForSeconds(timer);
        changeGrav();
    }
    void changeGrav()
    {
        Physics2D.gravity *= -1; 
    }

}