using UnityEngine;

public class ExtraLifeItem : MonoBehaviour
{
    [SerializeField] private Health health;
    private Vector3 rotation = new Vector3(1, 0, 0);
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            health.addHealth(1);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * 200);
    }
}
