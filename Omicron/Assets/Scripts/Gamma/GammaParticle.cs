using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticle : MonoBehaviour
{
    [HideInInspector] public Vector3 ParticleDirection;                         // The normalized direction of the particle (Set randomly at the beginning of the puzzle)
    [HideInInspector] public bool IsHot;                                        // flag if the temperature is hot or cold
    [HideInInspector] public Vector3 LastVelocity;                              // Last frame's velocity

    [SerializeField][Range(minTemp, maxTemp)] private float temperature;        // Initial temperature of the particle, which can be changed over time
    [SerializeField] private float timeTillTemperatureChange;                   // Time till the temperature of the particle changes

    [SerializeField] private bool canTemperatureChange;                         // bool for if the temperature of the particle can be changed during the puzzle
    [SerializeField] private bool canTemperatureIncrease;                       // bool for if the temperature will increase or decrease
    [SerializeField][Range(0.1f, 1.0f)] private float maxTemperatureIncrease;
    [SerializeField][Range(0.1f, 1.0f)] private float minTemperatureDecrease;
    [SerializeField] private float temperatureChangeRate;                       // Rate at which the temperature of the particle will change
    [SerializeField] private float temperatureDelta;                            // The change in the temperature of a particle over time
    [SerializeField] private Color blueColour;                                  // Blue particle colour (When its at its coldest)
    [SerializeField] private Color lighBlueColour;                              // Light blue particle colour (When its lukewarm, but isHot is false)
    [SerializeField] private Color lightRedColour;                              // light red particle colour (When its lukewarm, but isHot is true)
    [SerializeField] private Color redColour;                                   // Red particle colour (When its at its hottest)

    private const float temperatureThreshold = 1f;                              // Temperature threshold of a particle
    private const float minSpeed = 1.0f;                                        // Minimum speed that a particle can travel at
    private const float maxTemp = 2.0f;                                         // Maximum particle temperature
    private const float minTemp = 0.1f;                                         // Minimum particle temperature
    private const float hotSpeedModifier = 8.0f;                               // Hot particle speed multiplier, which is used in the calculation of the a hot particle's speed
    private const float coldSpeedModifier = 9.0f;                              // Cold particle speed multiplier, which is used in the calculation of the a cold particle's speed

    private Rigidbody particleRB;                                           
    private MeshRenderer particleMeshRenderer;                              
    private float temperatureTime;                                              // The temperature time, which ensures the particles temperature changes during regular intervals
    private float speed;                                                        // Speed of the particle, which can be changed over time

    private void Awake()
    {
        particleRB = GetComponent<Rigidbody>();
        particleMeshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        temperatureTime = 0;
        // Change the temperature state from the beginning
        CheckTemperatureState(temperature);
        // Change the colour of the particle, according to its temperature
        ChangeColour(temperature);
        // Wait for particle's direction to be set before setting the particles velocity
        StartCoroutine(WaitToSetVelocity());
    }

    private void FixedUpdate()
    {
        LastVelocity = particleRB.velocity;
        if (canTemperatureChange)
        {
            if (timeTillTemperatureChange < Time.time && temperatureTime < Time.time)
            {
                // Set temperatureTime so that the particle's temperature changes according to regular intervals
                temperatureTime = Time.time + temperatureChangeRate;       
                // Change temperature based on if it is set to increase or not
                ChangeTemperature(canTemperatureIncrease);
                // Check and change state if below or above threshold
                CheckTemperatureState(temperature);
                // Change speed based on temperature
                ChangeSpeed(temperature);
                // Change colour based on temperature
                ChangeColour(temperature);
            }
        }
    }

    private IEnumerator WaitToSetVelocity()
    {
        yield return new WaitForSeconds(0.1f);
        // Set speed multipliers depending on a particle's state
        if (IsHot)
            speed = temperature * hotSpeedModifier;
        else
        {
            if (temperature >= 0.85 && temperature < temperatureThreshold)
                speed = temperature * coldSpeedModifier * 0.85f;
            else
                speed = temperature * coldSpeedModifier;
        }

        // Set particles velocity 
        particleRB.velocity = speed * ParticleDirection;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Get the normal of the gameobject hit
        Bounce(collision.contacts[0].normal);
    }

    private void Bounce(Vector3 hitNormal)
    {
        // Set the speed variable to be the magnitude of last frame's velocity
        float speed = LastVelocity.magnitude;
        // New reflected direction vector, reflected around the normal of the hit gameobject
        Vector3 direction = Vector3.Reflect(LastVelocity.normalized, hitNormal);
        // Get the bigger of the values, between the speed and minSpeed variable
        particleRB.velocity = direction * Mathf.Max(speed, minSpeed);
    }

    private void CheckTemperatureState(float temperature)
    {
        if (temperature < temperatureThreshold)
        {
            // If temperature goes below the threshold, the particle is cold
            IsHot = false;
        }
        else if (temperature > temperatureThreshold)
        {
            // If temperature goes above the threshold, the particle it is hot
            IsHot = true;
        }
    }

    private void ChangeTemperature(bool canTemperatureIncrease)
    {
        if (temperature < maxTemperatureIncrease && temperature > minTemperatureDecrease)
        {
            if (canTemperatureIncrease)
            {
                // Increase temperature by the delta
                temperature += temperatureDelta;
            }
            else
            {
                // Decrease temperature by the delta
                temperature -= temperatureDelta;
            }
            // Clamp temperature between min and max temperature
            temperature = Mathf.Clamp(temperature, minTemp, maxTemp);
        }
    }

    private void ChangeSpeed(float temperature)
    {
        if (temperature < maxTemp && temperature > minTemp)
        {
            if (IsHot)
            {
                speed = temperature * hotSpeedModifier;
            }
            else
            {
                speed = temperature * coldSpeedModifier;
            }

            Vector3 direction = particleRB.velocity.normalized;
            particleRB.velocity = speed * direction;
        }
    }

    private void ChangeColour(float temperature)
    {
        if (temperature < temperatureThreshold)
        {
            particleMeshRenderer.material.color = Color.Lerp(blueColour, lighBlueColour, temperature);
        }
        else if (temperature > temperatureThreshold)
        {
            particleMeshRenderer.material.color = Color.Lerp(lightRedColour, redColour, temperature - 1);
        }
    }
}
