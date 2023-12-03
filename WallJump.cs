using UnityEngine;

public class WallJump : MonoBehaviour
{
    [Header("Forces")]
    [SerializeField] private float wallJumpForce = 8f;
    [SerializeField] private float wallSlideSpeed = 1.5f;
    private Rigidbody2D rb;
    
    [Header("Detection")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallJumpLayer;
    private Vector3 offset = new Vector3(0, -.5F, 0);
    private bool isWallOnRight, isWallOnLeft;
    private bool isGrounded;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(transform.position + offset, .5f, groundLayer);
        
        isWallOnRight  = Physics2D.Raycast(transform.position, Vector2.right, 0.6f, wallJumpLayer);
        isWallOnLeft = Physics2D.Raycast(transform.position, Vector2.left, 0.6f, wallJumpLayer);

        if (!isGrounded && (isWallOnRight || isWallOnLeft))
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlideSpeed, float.MaxValue));
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (isWallOnRight)
                {
                    Debug.Log("Jump");
                    wallJumpAction(Vector2.left);
                }
                else if (isWallOnLeft)
                {
                    Debug.Log("Jump");
                    wallJumpAction(Vector2.right);
                }
            }
        }
    }

    private void wallJumpAction(Vector2 direction)
    {
        rb.velocity = Vector2.zero;
        rb.AddForce((direction + Vector2.up) * wallJumpForce, ForceMode2D.Impulse);
    }
}
