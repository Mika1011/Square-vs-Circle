using UnityEngine;

public class Bullet : MonoBehaviour
{
    float spawnTime = 0;
    float killTime = 10;
    void Start()
    {
        spawnTime = Time.time;
    }

    void Update()
    {
        if(Time.time > spawnTime + killTime) { Destroy(gameObject); }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Shooter"))
        {
            Destroy(gameObject);
        }
    }
}
