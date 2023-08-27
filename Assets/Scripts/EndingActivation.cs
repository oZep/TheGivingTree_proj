using UnityEngine;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingActivation : MonoBehaviour
{
    public Text collisionText;
    private bool isColliding = false;
    public float proximityDistance = 3f;
    private GameObject player;
    private bool clicked = false;
    private bool switchScene = false;

    private void Start()
    {
        collisionText.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = true;
            collisionText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isColliding = false;
            collisionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isColliding && Vector3.Distance(transform.position, player.transform.position) <= proximityDistance)
        {
            if (Input.GetKey(KeyCode.N))
            {
                EndGame();
            }
            else if (Input.GetKey(KeyCode.Y))
            {
                switchScene = true;
                SwitchScene();
            }
        }
    }

    private void EndGame()
    {
        Application.Quit();
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("Ending");
    }
}
