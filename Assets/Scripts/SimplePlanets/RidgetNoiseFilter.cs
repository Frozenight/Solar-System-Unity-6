using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RidgetNoiseFilter : INoiseFilter
{
    NoiseSettings.RidgetNoiseSettings settings;
    Noise noise = new Noise();

    public RidgetNoiseFilter(NoiseSettings.RidgetNoiseSettings settings)
    {
        this.settings = settings;
    }

    public float Evaluate(Vector3 point)
    {
        float noiseValue = 0;
        float frequency = settings.baseRougness;
        float amplitude = 1;
        float weight = 1;

        for (int i = 0; i < settings.numLayers; i++)
        {
            float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.center));
            v += v;
            v += weight;
            weight = Mathf.Clamp01(v * settings.weightMultiplayer);

            noiseValue += v * amplitude;
            frequency *= settings.rougness;
            amplitude *= settings.persistence;
        }

        noiseValue = noiseValue - settings.minValue;
        return noiseValue * settings.strength;
    }
}
