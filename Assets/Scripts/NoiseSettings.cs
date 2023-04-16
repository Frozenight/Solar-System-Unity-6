using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class NoiseSettings
{
    public enum FIlterType { Simple, Rigid};
    public FIlterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;
    [ConditionalHide("filterType", 1)]
    public RidgetNoiseSettings ridgetNoiseSettings;

    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strength = 1;
        [Range(1, 8)]
        public int numLayers = 1;
        public float baseRougness = 1;
        public float rougness = 2;
        public float persistence = 0.5f;
        public Vector3 center;
        public float minValue;
    }

    [System.Serializable]
    public class RidgetNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplayer = .8f;
    }
}
