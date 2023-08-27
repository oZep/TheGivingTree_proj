using UnityEngine;
using UnityEngine.UI;

public class PressKeyOpenDoor : MonoBehaviour
{
    public Text collisionText;
    private bool isColliding = false;
    public KeyCode destroyKey = KeyCode.K;
    public float proximityDistance = 3f;
    private GameObject player;
    private bool clicked = true;

    private void Start()
    {
        collisionText.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && clicked)
        {
            isColliding = true;
            collisionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && clicked)
        {
            isColliding = false;
            collisionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isColliding && Vector3.Distance(transform.position, player.transform.position) <= proximityDistance && Input.GetKey(destroyKey))
        {
            collisionText.gameObject.SetActive(false);
            clicked = false;
        }
    }
}
