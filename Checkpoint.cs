using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] CheckpointManager cpM;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if (cpM.getLastCheck() != null)
            {
                if (cpM.getLastCheck() != this)
                {
                    cpM.add(this);
                }
            } else { cpM.add(this); }
            Debug.Log("Checkpoint added!");
        }
    }
}
