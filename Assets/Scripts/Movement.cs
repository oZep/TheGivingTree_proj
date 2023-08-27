using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 5f;
    public float mouseSensitivity = 2f;
    public float slowMouseSensitivity = 1f;
    public float verticalLoopSpeed = 1f;
    public float verticalLoopAmount = 0.1f;

    [Header("Bobbing Effect")]
    public float bobbingSpeed = 0.2f;
    public float bobbingAmount = 0.1f;

    public AudioClip heartbeatClip; // Add the heartbeat audio clip here

    private CharacterController characterController;
    private float defaultPosY;
    private float timer = 0f;
    private float verticalTimer = 0f;
    private bool isLoopingUp = false;

    private Transform cameraTransform;
    private Transform playerBody;
    public AudioSource audioSource; // Add an AudioSource component to the same game object and assign it here

    // Limit the rotation angle so the player cannot look upside down
    public float maxVerticalAngle = 80f;
    public float minVerticalAngle = -80f;
    private float verticalRotation = 0f;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        defaultPosY = Camera.main.transform.localPosition.y;

        // Get the main camera's transform
        cameraTransform = Camera.main.transform;
        // Reset the camera's position and rotation
        cameraTransform.localPosition = Vector3.forward;

        // Get the player's body transform
        playerBody = transform;

        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        // Set up the heartbeat audio
        audioSource.loop = true;
        audioSource.clip = heartbeatClip;
        audioSource.Play();
    }

    private void Update()
    {
        // Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Calculate the movement direction based on the camera's forward direction
        Vector3 moveDirection = cameraTransform.forward * moveVertical + cameraTransform.right * moveHorizontal;
        moveDirection.y = 0f; // Ensure the player stays grounded
        moveDirection.Normalize(); // Normalize the direction to avoid faster movement diagonally

        characterController.SimpleMove(moveDirection * movementSpeed);

        // Mouse Look
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Rotate the player's body based on the mouse input
        playerBody.Rotate(Vector3.up, mouseX);

        // Rotate the camera vertically based on the mouse input
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVerticalAngle, maxVerticalAngle);
        cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // looking left or right depending on mouse direction
        if (Input.GetMouseButton(0))
        {
            cameraTransform.Rotate(0f, -slowMouseSensitivity * mouseX, 0f);
        }
        else if (Input.GetMouseButton(1))
        {
            cameraTransform.Rotate(0f, slowMouseSensitivity * mouseX, 0f);
        }

        // Loop Up and Down
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isLoopingUp = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            isLoopingUp = false;
        }

        float verticalLoopOffset = Mathf.Sin(verticalTimer) * verticalLoopAmount;
        if (isLoopingUp)
        {
            verticalTimer += verticalLoopSpeed * Time.deltaTime;
        }
        else
        {
            verticalTimer -= verticalLoopSpeed * Time.deltaTime;
        }

        cameraTransform.localPosition = new Vector3(0f, defaultPosY + verticalLoopOffset, 0.5f);

        // Bobbing Effect
        if (moveHorizontal != 0 || moveVertical != 0)
        {
            timer += bobbingSpeed * Time.deltaTime;
            float bobbingOffset = Mathf.Sin(timer) * bobbingAmount;
            cameraTransform.localPosition = new Vector3(0f, defaultPosY + verticalLoopOffset + bobbingOffset, 0.5f);
        }
        else
        {
            cameraTransform.localPosition = new Vector3(0f, defaultPosY + verticalLoopOffset, 0.5f);
        }

        // Close the application when the enemy is 1f away from the player
        if (IsEnemyCloseToPlayer(1f))
        {
            Debug.Log("Enemy is 1f away from the player. Closing the application...");
            QuitApplication();
        }

        // Quit the application when pressing the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitApplication();
        }
    }

    // Check if the enemy is close to the player within the given distance
    private bool IsEnemyCloseToPlayer(float distance)
    {
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        if (enemy != null)
        {
            float enemyDistance = Vector3.Distance(transform.position, enemy.transform.position);
            return enemyDistance <= distance;
        }
        return false;
    }

    // Quit the application
    private void QuitApplication()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    // When enemy collides, the player dies
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Close the application
            Debug.Log("Death");
            QuitApplication();
        }
    }
}
