using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class DayNightScript : MonoBehaviour
{
    private Light2D _globalLight;
    private Coroutine _coroutine;

    [SerializeField] private DayCycles _dayCycles;

    [SerializeField] private Color _sunrise;
    [SerializeField] private Color _day;
    [SerializeField] private Color _sunset;
    [SerializeField] private Color _midnight;
    [SerializeField] private Color _night;
    
    [SerializeField] private float transitionTime;

    [SerializeField] private Light2D[] _lightMaps;

    private void ControlLightsMaps(bool state)
    {
        if (_lightMaps.Length > 0)
        {
            foreach (Light2D light in _lightMaps)
            {
                light.enabled = state;
            }
        }
    }

    private void ChangeDayColor(Color newColor)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }
        _coroutine = StartCoroutine(TransitionColor(newColor));
    }

    private IEnumerator TransitionColor(Color targetColor)
    {
        Color currentColor = _globalLight.color;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            _globalLight.color = Color.Lerp(currentColor, targetColor, elapsedTime / transitionTime);
            yield return null;
        }
        _globalLight.color = targetColor;
    }
    
    private void Update()
    {
        switch (_dayCycles)
        {
            case DayCycles.Sunrise:
                ChangeDayColor(_sunrise);
                break;
            case DayCycles.Day:
                ChangeDayColor(_day);
                break;
            case DayCycles.Sunset:
                ChangeDayColor(_sunset);
                break;
            case DayCycles.Midnight:
                ChangeDayColor(_midnight);
                break;
            case DayCycles.Night:
                ChangeDayColor(_night);
                break;
            default:
                print("DayCycles not implemented");
                break;
        }

        if (_dayCycles == DayCycles.Midnight || _dayCycles == DayCycles.Night)
        {
            ControlLightsMaps(true);
        }
        else
        {
            ControlLightsMaps(false);
        }
    }

    private void Start()
    {
        _globalLight = GetComponent<Light2D>();
        _dayCycles = DayCycles.Sunrise;
    }
}

