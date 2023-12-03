using UnityEngine;

public class ReverseGravityItem : MonoBehaviour
{
    private Vector3 rotation = new Vector3(1, 0, 0);
    [SerializeField] private float timer;
    [SerializeField] private PlayerMovement playerMovement; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement.startReverseGravity(timer);
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * 200);
    }
}
