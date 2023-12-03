using UnityEngine;
public class JumpBoostItem : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    private Vector3 rotation = new Vector3(1, 0, 0);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement.startJumpBoost();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.Rotate(rotation * Time.deltaTime * 200);
    }
}
