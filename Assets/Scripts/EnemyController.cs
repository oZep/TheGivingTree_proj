using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Transform player;
    public float speed = 5f;
    public float delay = 10f;

    public AudioClip screamClip;
    public AudioSource screamer;

    private bool canMove = false;

    private void Start()
    {
        Invoke("StartMoving", delay);
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            MoveTowardsPlayer();
            RotateTowardsPlayer();
        }
    }

    private void StartMoving()
    {
        canMove = true;
        screamer.clip = screamClip;
        screamer.Play();

    }

    private void MoveTowardsPlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();

        transform.position += direction * speed * Time.fixedDeltaTime;
    }

    private void RotateTowardsPlayer()
    {
        Vector3 targetDirection = player.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime);
    }
}
