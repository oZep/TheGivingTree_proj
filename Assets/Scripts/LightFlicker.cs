using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light lightComponent;
    private float flickerTime = 3f;
    private float flickerDuration = 0.1f;
    private float flickerIntensity = 1f;

    private bool isFlickering = false;
    private float flickerTimer = 0f;
    private float originalIntensity;

    private void Start()
    {
        lightComponent = GetComponent<Light>();
        originalIntensity = lightComponent.intensity;
    }

    private void Update()
    {
        if (!isFlickering)
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= flickerTime)
            {
                StartFlicker();
            }
        }
        else
        {
            flickerTimer += Time.deltaTime;
            if (flickerTimer >= flickerDuration)
            {
                StopFlicker();
            }
            else
            {
                lightComponent.intensity = Random.Range(originalIntensity - flickerIntensity, originalIntensity + flickerIntensity);
            }
        }
    }

    private void StartFlicker()
    {
        isFlickering = true;
        flickerTimer = 0f;
    }

    private void StopFlicker()
    {
        isFlickering = false;
        flickerTimer = 0f;
        lightComponent.intensity = originalIntensity;
    }
}
