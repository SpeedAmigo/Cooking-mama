using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class DayNightScript : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Light2D _globalLight;
    
    [ShowInInspector] private bool changeBackgroundColor = false;

    [SerializeField] private SO_DayCycle soDayCycle;
    
    private Color currentTargetColor;
    
    [SerializeField] private Color _sunrise;
    [SerializeField] private Color _day;
    [SerializeField] private Color _sunset;
    [SerializeField] private Color _midnight;
    [SerializeField] private Color _night;
    
    [SerializeField] private float transitionTime;
    
    private void ChangeDayColor(Color newColor, float duration)
    {
        if (currentTargetColor == newColor) return; // avoid switching to the same color
        
        currentTargetColor = newColor;
        
        DOTween.To(() => _globalLight.color, x => _globalLight.color = x, newColor, duration).SetEase(Ease.OutCubic);

        if (changeBackgroundColor)
        {
            DOTween.To(() => mainCamera.backgroundColor, x => mainCamera.backgroundColor = x, newColor, duration).SetEase(Ease.OutCubic);
        }
        
        EventsManager.InvokeLightColorChange(newColor, duration);
    }
    
    private DayCycles GetNextCycle(DayCycles currentCycle)
    {
        int next = (int)currentCycle + 1 % System.Enum.GetNames(typeof(DayCycles)).Length;
        
        if (DayCycles.Night == currentCycle)
        {
            return DayCycles.Sunrise;
        }
        
        return (DayCycles)next;
    }

    [Button]
    private void ChangeTime()
    {
        soDayCycle.dayCycle = GetNextCycle(soDayCycle.dayCycle);
    }
    
    private Color GetTargetColor(DayCycles dayCycle)
    {
        Color targetColor = currentTargetColor;
        
        switch (dayCycle)
        {
            case DayCycles.Sunrise:
                targetColor = _sunrise;
                break;
            case DayCycles.Day:
                targetColor = _day;
                break;
            case DayCycles.Sunset:
                targetColor = _sunset;
                break;
            case DayCycles.Midnight:
                targetColor = _midnight;
                break;
            case DayCycles.Night:
                targetColor = _night;
                break;
            default:
                print("DayCycles not implemented");
                break;
        }
        
        return targetColor;
    }

    #region On Enable/Disable
    private void OnEnable()
    {
        EventsManager.ChangeTimeEvent += ChangeTime;
    }

    private void OnDisable()
    {
        EventsManager.ChangeTimeEvent -= ChangeTime;
    }
    
    #endregion
    
    private void Update()
    {
        Color targetColor = currentTargetColor;
        
        switch (soDayCycle.dayCycle)
        {
            case DayCycles.Sunrise:
                targetColor = _sunrise;
                break;
            case DayCycles.Day:
                targetColor = _day;
                break;
            case DayCycles.Sunset:
                targetColor = _sunset;
                break;
            case DayCycles.Midnight:
                targetColor = _midnight;
                break;
            case DayCycles.Night:
                targetColor = _night;
                break;
            default:
                print("DayCycles not implemented");
                break;
        }
        
        ChangeDayColor(targetColor, transitionTime);
    }
    
    private void Start()
    {
        _globalLight = GetComponent<Light2D>();
        ChangeDayColor(GetTargetColor(soDayCycle.dayCycle), 0.1f);
    }
}

