using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine.Serialization;

public class DayNightScript : MonoBehaviour {
 
    public Light Sun;
    public float SecondsInFullDay = 120f;
    [Range(0,1)]
    public float CurrentTimeOfDay = 0;
    [HideInInspector]
    public float TimeMultiplier = 1f;

    public float MaxSunAngle = 65;
    
    public Gradient NightDayLightColor;
    
    private float sunInitialIntensity;
    private float sunAngle;

    public List<Light> Lights;
    
    void Start()
    {
        sunInitialIntensity = Sun.intensity;
    }
    
    void Update() {
        UpdateSun();
 
        CurrentTimeOfDay += (Time.deltaTime / SecondsInFullDay) * TimeMultiplier;
 
        if (CurrentTimeOfDay >= 1) {
            CurrentTimeOfDay = 0;
        }
    }
    
    void UpdateSun()
    {

        float yArg = (CurrentTimeOfDay * 360f) - 90;
        float zArg = 0;

        float xArg = 90;
        if (MaxSunAngle < 90)
        {
            xArg = ((-1*Mathf.Pow(yArg, 2) + 180 * yArg)/8100f)*MaxSunAngle;
        }
        
        Sun.transform.rotation = Quaternion.Euler(xArg, yArg, zArg);
        float intensityMultiplier = 1;
        if (CurrentTimeOfDay <= 0.23f || CurrentTimeOfDay >= 0.75f) {
            intensityMultiplier = 0;
            foreach (var light in Lights)
            {
                light.enabled = true;
            }
        }
        else if (CurrentTimeOfDay <= 0.25f) {
            intensityMultiplier = Mathf.Clamp01((CurrentTimeOfDay - 0.23f) * (1 / 0.02f));
            foreach (var light in Lights)
            {
                light.enabled = false;
            }
        }
        else if (CurrentTimeOfDay >= 0.73f) {
            intensityMultiplier = Mathf.Clamp01(1 - ((CurrentTimeOfDay - 0.73f) * (1 / 0.02f)));
        }
        Sun.color = NightDayLightColor.Evaluate(CurrentTimeOfDay); 
        Sun.intensity = sunInitialIntensity * intensityMultiplier;
    }
}