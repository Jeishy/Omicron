using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaParticle : MonoBehaviour
{
    [HideInInspector] public Vector3 ParticleDirection;                         // The normalized direction of the particle
    [HideInInspector] public bool IsHot;                                        // flag if the temperature is hot or cold
    [HideInInspector] public Vector3 LastVelocity;                              // Last frame's velocity
    [HideInInspector] public bool IsParticleStateChanged;                       // Bool that states if the particle's state has changed
    [HideInInspector] public bool IsParticleInCorrectChamber;                   // Bool that checks if the particle is in the correct chamber, depending on its state
    [HideInInspector] public float InitialSpeed;
    [HideInInspector] public float HotSpeedModifier = 8.0f;                     // Hot particle speed multiplier, which is used in the calculation of the a hot particle's speed
    [HideInInspector] public float ColdSpeedModifier = 9.0f;                    // Cold particle speed multiplier, which is used in the calculation of the a cold particle's speed
    [HideInInspector] public Color OriginalColour;
    [HideInInspector] public float OriginalTemperature;
    [Range(minTemp, maxTemp)] public float Temperature;                         // Initial temperature of the particle, which can be changed over time
    [HideInInspector] public float TemperatureTime;                             // The temperature time, which ensures the particles temperature changes during regular intervals

    [SerializeField] private float timeTillTemperatureChange;                   // Time till the temperature of the particle changes
    [SerializeField] private bool canTemperatureChange;                         // bool for if the temperature of the particle can be changed during the puzzle
    public bool CanTemperatureIncrease;                                         // bool for if the temperature will increase or decrease

    [SerializeField][Range(minTemp, maxTemp)] private float maxTemperatureIncrease;
    [SerializeField][Range(minTemp, maxTemp)] private float minTemperatureDecrease;
    [SerializeField] private Transform _positionDirectionTrans;
    [SerializeField] private float temperatureChangeRate;                       // Rate at which the temperature of the particle will change
    [SerializeField] private float temperatureDelta;                            // The change in the temperature of a particle over time
    [SerializeField] private Color blueColour;                                  // Blue particle colour (When its at its coldest)
    [SerializeField] private Color lighBlueColour;                              // Light blue particle colour (When its lukewarm, but isHot is false)
    [SerializeField] private Color lightRedColour;                              // light red particle colour (When its lukewarm, but isHot is true)
    [SerializeField] private Color redColour;                                   // Red particle colour (When its at its hottest)
    [SerializeField] private GameObject _tempChangeIndicator;
    [SerializeField] private Animator _tempChangeAnim;

    private const float temperatureThreshold = 1f;                              // Temperature threshold of a particle
    private const float minSpeed = 1.0f;                                        // Minimum speed that a particle can travel at
    private const float maxTemp = 2.0f;                                         // Maximum particle temperature
    private const float minTemp = 0.1f;                                         // Minimum particle temperature
    private Rigidbody _particleRb;                                           
    private MeshRenderer _particleMeshRenderer;                              
    private float _speed;                                                       // Speed of the particle, which can be changed over time
    private GammaLevelManager _gammaManager;
    private GammaParticle _gammaParticle;

    private void Awake()
    {
        _tempChangeIndicator.SetActive(false);
        _particleRb = GetComponent<Rigidbody>();
        _particleMeshRenderer = GetComponent<MeshRenderer>();
        _gammaParticle = GetComponent<GammaParticle>();
    }

    private void Start()
    {
        _gammaManager = GameObject.Find("GammaLevelManager").GetComponent<GammaLevelManager>();
        Setup();
    }

    private void FixedUpdate()
    {
        LastVelocity = _particleRb.velocity;
        if (canTemperatureChange)
        {
            if (timeTillTemperatureChange < Time.time && TemperatureTime < Time.time)
            {
                // Set TemperatureTime so that the particle's temperature changes according to regular intervals
                TemperatureTime = Time.time + temperatureChangeRate;       
                // Change temperature based on if it is set to increase or not
                ChangeTemperature();
                // Check and change state if below or above threshold
                CheckTemperatureState(Temperature);
                // Change speed based on temperature
                ChangeSpeed(Temperature);
                // Change colour based on temperature
                ChangeColour(Temperature);
            }
        }
    }

    public void Setup()
    {
        TemperatureTime = 0;
        IsParticleStateChanged = false;
        IsParticleInCorrectChamber = false;
        // Debug.Log(IsParticleStateChanged ? (gameObject.name + " is in the correct chamber") : (gameObject.name + " is in the wrong chamber"));
        // Change the temperature state from the beginning
        SetupTemperatureState(Temperature);
        // Change the colour of the particle, according to its temperature
        OriginalColour = ChangeColour(Temperature);
        OriginalTemperature = Temperature;
        // Set particle's velocity
        SetVelocity();
    }

    private void SetVelocity()
    {
        Vector3 positionToMoveTowards = _positionDirectionTrans.position;
        ParticleDirection = Vector3.Normalize(positionToMoveTowards - transform.position);

        // Set speed multipliers depending on a particle's state
        if (IsHot)
            _speed = Temperature * HotSpeedModifier;
        else
        {
            if (Temperature >= 0.85 && Temperature < temperatureThreshold)
                _speed = Temperature * ColdSpeedModifier * 0.85f;
            else
                _speed = Temperature * ColdSpeedModifier;
        }

        // Set particles velocity 
        _particleRb.velocity = _speed * ParticleDirection;
        InitialSpeed = _speed;
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
        // New reflected direction vector, reflected around the normal of the hit GameObject
        Vector3 direction = Vector3.Reflect(LastVelocity.normalized, hitNormal);
        // Get the bigger of the values, between the speed and minSpeed variable
        _particleRb.velocity = direction * Mathf.Max(speed, minSpeed);
    }

    private void SetupTemperatureState(float temperature)
    {
        if (temperature < temperatureThreshold)
        {
            // If temperature goes below the threshold, the particle is cold
            IsHot = false;
        }
        else if (temperature >= temperatureThreshold)
        {
            // If temperature goes above the threshold, the particle it is hot
            IsHot = true;
        }
    }


    public void CheckTemperatureState(float temperature)
    {
        if (canTemperatureChange && !IsParticleStateChanged)
        {
            if (!CanTemperatureIncrease && temperature < temperatureThreshold)
            {
                IsParticleStateChanged = true;
                // If temperature goes below the threshold, the particle is cold
                IsHot = false;
                // Inform the gamma level manager that the particle has changed state
                _gammaManager.ParticleStateChange(_gammaParticle);
                HideTemperatureChangeIndicator();
            }
            else if (CanTemperatureIncrease && temperature > temperatureThreshold)
            {
                IsParticleStateChanged = true;
                // If temperature goes above the threshold, the particle it is hot
                IsHot = true;
                // Inform the gamma level manager that the particle has changed state
                _gammaManager.ParticleStateChange(_gammaParticle);
                HideTemperatureChangeIndicator();
            }
        }
    }

    public void ChangeTemperature()
    {
        if (Temperature < maxTemperatureIncrease && Temperature > minTemperatureDecrease && !IsParticleStateChanged)
        {
            // Show temperature change indicator when temperature starts to change
            ShowTemperatureChangeIndicator(CanTemperatureIncrease);

            if (CanTemperatureIncrease)
            {

                // Increase temperature by the delta
                Temperature += temperatureDelta;
                Temperature = Mathf.Clamp(Temperature, minTemp, maxTemperatureIncrease);
                if (Temperature >= 0.8f)
                    ShowImminentStateChangeAnimation(Temperature);
            }
            else
            {
                // Decrease temperature by the delta
                Temperature -= temperatureDelta;
                Temperature = Mathf.Clamp(Temperature, minTemperatureDecrease, maxTemp);
                if (Temperature <= 1.2f)
                    ShowImminentStateChangeAnimation(Temperature);
            }
            // Clamp temperature between min and max temperature
            Temperature = Mathf.Clamp(Temperature, minTemp, maxTemp);
        }
    }

    private void ShowTemperatureChangeIndicator(bool canTemperatureIncrease)
    {
        _tempChangeIndicator.SetActive(true);
        _tempChangeAnim.SetTrigger("TempChange");
        MeshRenderer meshRenderer = _tempChangeIndicator.GetComponent<MeshRenderer>();
        meshRenderer.material.color = canTemperatureIncrease ? new Color(1f, 0.38f, 0.35f, 1f) : new Color(0.27f, 0.88f, 1f, 1f);
    }

    private void HideTemperatureChangeIndicator()
    {
        _tempChangeIndicator.SetActive(false);
        MeshRenderer meshRenderer = _tempChangeIndicator.GetComponent<MeshRenderer>();
        _tempChangeAnim.SetTrigger("AnimationOff");
        _tempChangeAnim.ResetTrigger("TempChange");
        _tempChangeAnim.ResetTrigger("TempStateChange");
        _tempChangeAnim.ResetTrigger("AnimationOff");

        meshRenderer.material.color = Color.white;
    }

    private void ShowImminentStateChangeAnimation(float temperature)
    {
        _tempChangeAnim.SetTrigger("TempStateChange");
    }

    private void ChangeSpeed(float temperature)
    {
        if (temperature < maxTemp && temperature > minTemp)
        {
            if (IsHot)
            {
                _speed = temperature * HotSpeedModifier;
            }
            else
            {
                _speed = temperature * ColdSpeedModifier;
            }

            Vector3 direction = _particleRb.velocity.normalized;
            _particleRb.velocity = _speed * direction;
        }
    }

    private Color ChangeColour(float temperature)
    {
        if (temperature < temperatureThreshold)
        {
            _particleMeshRenderer.material.color = Color.Lerp(blueColour, lighBlueColour, temperature);
        }
        else if (temperature > temperatureThreshold)
        {
            _particleMeshRenderer.material.color = Color.Lerp(lightRedColour, redColour, temperature - 1);
        }

        Color colour = _particleMeshRenderer.material.color;
        return colour;
    }
}
