using UnityEngine;
using UnityEngine.UI;

public class PlayRadio : MonoBehaviour
{
    public KeyCode playKey = KeyCode.P;
    public float proximityDistance = 3f;
    private GameObject player;
    public AudioClip soundClip;
    public AudioSource audioSource;
    public Text Subtitle;
    private bool audioPlaying = false;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Subtitle.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= proximityDistance && Input.GetKeyDown(playKey))
        {
            audioPlaying = true;
            Subtitle.gameObject.SetActive(true);
            audioSource.clip = soundClip;
            audioSource.Play();
        }

        if (audioPlaying && !audioSource.isPlaying)
        {
            audioPlaying = false;
            Subtitle.gameObject.SetActive(false);
        }
    }
}
