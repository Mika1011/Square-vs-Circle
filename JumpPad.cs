using UnityEngine;
public class JumpPad : MonoBehaviour
{
    [SerializeField] private float jumpBoost = 50;
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(UnityEngine.Vector2.up * jumpBoost, ForceMode2D.Impulse);
        }
    }
}
