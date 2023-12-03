using UnityEngine;

public class TargetEnemy : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject target;
    public GameObject bulletPrefab;

    [Header("Shooting")]
    public float nextTimeToSpawn = 5;
    public float fireRate = 10;
    public float shootingForce = 50;
    bool shootable = false;
    
    [Header("Audio")]
    [SerializeField] AudioSource audioSource;

    void Start()
    {
        nextTimeToSpawn /= fireRate;
    }
    
    void Update()
    {
        if (shootable)
        {
            if (Time.time > nextTimeToSpawn)
            {
                nextTimeToSpawn = Time.time + 5 / fireRate;
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                audioSource.Play();
                bullet.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * shootingForce, ForceMode2D.Impulse);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            shootable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            shootable = false;
        }
    }
}
