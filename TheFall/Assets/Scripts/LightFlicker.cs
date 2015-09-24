using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Light))]
public class LightFlicker : MonoBehaviour {
    public float minIntensity = 0.25f;
    public float maxIntensity = 0.5f;

    private Light lt;

    float random;

    void Start()
    {
    	lt = GetComponent<Light>();
        random = Random.Range(0.0f, 3f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        lt.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
    }
}