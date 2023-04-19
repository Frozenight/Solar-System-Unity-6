using UnityEngine;

[CreateAssetMenu(menuName = "Celestial Body/Settings Holder")]
public class Celestial_BodySettings : ScriptableObject
{
	public CelestialBodyShape shape;
	public CelestialBodyShading shading;
}