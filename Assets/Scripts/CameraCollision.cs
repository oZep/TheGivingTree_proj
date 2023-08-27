using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float movementSpeed = 5f; 
    private Vector3 lastValidPosition;

    private void Start()
    {
        lastValidPosition = transform.position;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);
        moveDirection.Normalize();

        Vector3 newPosition = transform.position + moveDirection * movementSpeed * Time.deltaTime;

        RaycastHit hit;
        if (Physics.Linecast(transform.position, newPosition, out hit))
        {
            transform.position = lastValidPosition;
        }
        else
        {
            transform.position = newPosition;
            lastValidPosition = transform.position;
        }
    }
}
