using UnityEngine;

public class InvincibilityItem : MonoBehaviour
{
    private Vector3 rotation = new Vector3(1, 0, 0);
    [SerializeField] private float timer;
    [SerializeField] private Health health; 
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.startInvincibility(timer);
            gameObject.SetActive(false);
        }
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * 200);
    }
}
