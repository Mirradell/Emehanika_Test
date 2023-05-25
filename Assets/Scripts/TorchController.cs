using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchController : MonoBehaviour
{
    public UIController ui;
    public ScoreController scores;

    [Header("Интенсивность освещения")]
    public Light torchLight;
    public float speed = .1f;

    public float maxIntensity = 1f;
    public float minIntensity = 0f;

    [Header("Интенсивность горения(через particleSystem)")]
    public ParticleSystem fire1;
    public ParticleSystem fire2;

    public int step = 1;
    public int minParticles = 0;
    public int maxParticles = 10;


    // Start is called before the first frame update
    void Start()
    {
        if (torchLight == null)
            Debug.LogError("Пустой материал факела!");
        else
            torchLight.intensity = maxIntensity;

        if (fire1 == null || fire2 == null)
            Debug.LogError("Пустые Particle System!");
        else
        {
            ChangeEmission(fire1.emission, maxParticles);
            ChangeEmission(fire2.emission, maxParticles);
        }
    }

    public void DecreaseTorch()
    {
        if (torchLight.intensity - speed > minIntensity)
        {
            torchLight.intensity -= speed;

            var newEmission = fire1.emission.rateOverTime.constant - step;
            if (newEmission > minParticles)
                ChangeEmission(fire1.emission, newEmission);

            newEmission = fire2.emission.rateOverTime.constant - step;
            if (newEmission > minParticles)
                ChangeEmission(fire2.emission, newEmission);
        }
        else
            ui.GameEnd();
    }

    public void IncreaseTorch()
    {
        if (torchLight.intensity + speed < maxIntensity)
        {
            torchLight.intensity += speed;

            var newEmission = fire1.emission.rateOverTime.constant + step;
            if (newEmission < maxParticles)
                ChangeEmission(fire1.emission, newEmission);

            newEmission = fire2.emission.rateOverTime.constant + step;
            if (newEmission < maxParticles)
                ChangeEmission(fire2.emission, newEmission);
        }
        scores.ScoresIncrease();
    }

    private void ChangeEmission(ParticleSystem.EmissionModule emission, float newRate)
    {
        emission.rateOverTime = newRate;
    }
}
