using UnityEngine;

public class ObjectPickUp : MonoBehaviour
{
    public KeyCode destroyKey = KeyCode.K;
    public float proximityDistance = 2f;
    private GameObject player;
    public AudioClip soundClip;
    public AudioSource audioSource;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= proximityDistance && Input.GetKey(destroyKey))
        {

            audioSource.clip = soundClip;
            audioSource.Play();
            
            Debug.Log("Destroying the object");
            Destroy(this.gameObject);
        }
    }
}