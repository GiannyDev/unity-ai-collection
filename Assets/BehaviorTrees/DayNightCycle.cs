using System;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    public Action OnChanged;
    public float dayDuration = 10f;

    public bool IsNight { get; set; }
    private Light _light;
    private Color dayColor;
    
    private void Start()
    {
        _light = GetComponent<Light>();
        dayColor = _light.color;
    }

    private void Update()
    {
        Color nightColor = Color.white * 0.2f;
        float lightIntensity = 0.5f + Mathf.Sin(Time.time * 2f * Mathf.PI / dayDuration) / 2f;

        if (IsNight != lightIntensity < 0.3f)
        {
            IsNight = !IsNight;
            if (OnChanged != null)
            {
                OnChanged.Invoke();
            }
        }

        _light.color = Color.Lerp(nightColor, dayColor, lightIntensity);
    }
}
