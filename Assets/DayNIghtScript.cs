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
    
    private Color _currentColor;
    
    private void ChangeDayColor(Color newColor)
    {
        if (_currentColor == newColor) return; // avoid restarting the same coroutine
        
        _currentColor = newColor;
        
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

        if (_dayCycles == DayCycles.Midnight || _dayCycles == DayCycles.Night)
        {
            EventsManager.InvokeLightToggleEvent(true);
        }
        else
        {
            EventsManager.InvokeLightToggleEvent(false);
        }
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
    }

    private void Start()
    {
        _globalLight = GetComponent<Light2D>();
        _dayCycles = DayCycles.Sunrise;
    }
}

