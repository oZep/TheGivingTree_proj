using UnityEngine;

public class DoorController : MonoBehaviour
{
    public KeyCode openKey = KeyCode.X;
    public float rotationAngle = 90f;
    public float rotationSpeed = 5f;
    public float closeDelay = 10f;

    private bool isOpen = false;
    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private Collider doorCollider;

    public AudioClip doorOpenClip;
    public AudioSource doorOpenSource;

    private void Start()
    {
        doorCollider = GetComponent<Collider>();
        doorCollider.isTrigger = false; // Initially turned off
        doorOpenSource.clip = doorOpenClip;
    }

    private void Update()
    {
        if (Input.GetKeyDown(openKey))
        {
            doorOpenSource.Play();
            ToggleDoor();
        }

        if (isOpen)
        {
            RotateDoor();
        }
    }

    private void ToggleDoor()
    {
        isOpen = !isOpen;

        if (isOpen)
        {
            doorCollider.isTrigger = true; // Turn on trigger
            targetRotation = Quaternion.Euler(0f, rotationAngle, 0f);
            initialRotation = transform.rotation;
            StartCoroutine(CloseDoorAfterDelay());
        }
        else
        {
            doorCollider.isTrigger = false; // Turn off trigger
            targetRotation = Quaternion.Euler(0f, 0f, 0f);
            initialRotation = transform.rotation;
            doorOpenSource.Stop();
        }
    }

    private void RotateDoor()
    {
        float step = rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, step);
    }

    private System.Collections.IEnumerator CloseDoorAfterDelay()
    {
        yield return new WaitForSeconds(closeDelay);
        ToggleDoor();
    }
}
