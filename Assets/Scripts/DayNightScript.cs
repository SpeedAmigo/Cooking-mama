using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using DG.Tweening;

public class DayNightScript : MonoBehaviour
{
    [TabGroup("References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Light2D _globalLight;
    
    [TabGroup("Properties")]
    [ShowInInspector] private bool changeBackgroundColor = false;
    
    [SerializeField] private SO_DayCycle soDayCycle;
    private Color currentTargetColor;
    public DayCycles CurrentDayCycle {get => soDayCycle.dayCycle;}
    
    [TabGroup("AmbientColors")]
    [LabelWidth(100f)]
    [SerializeField] private Color _sunrise;
    [TabGroup("AmbientColors")]
    [LabelWidth(100f)]
    [SerializeField] private Color _day;
    [TabGroup("AmbientColors")]
    [LabelWidth(100f)]
    [SerializeField] private Color _sunset;
    [TabGroup("AmbientColors")]
    [LabelWidth(100f)]
    [SerializeField] private Color _midnight;
    [TabGroup("AmbientColors")]
    [LabelWidth(100f)]
    [SerializeField] private Color _night;
    
    [Range(0f, 10f)]
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

    public void IncreaseDayCount()
    {
        soDayCycle.dayCount++;
    }
    
    public void SetDayCycle(DayCycles dayCycle)
    {
        soDayCycle.dayCycle = dayCycle;
    }
    
    private DayCycles GetNextCycle(DayCycles currentCycle)
    {
        int next = (int)currentCycle + 1 % System.Enum.GetNames(typeof(DayCycles)).Length;
        
        if (DayCycles.Night == currentCycle)
        {
            if (soDayCycle.dayCount >= 7)
            {
                soDayCycle.dayCount++;
            } 
            
            return DayCycles.Sunrise;
        }
        
        return (DayCycles)next;
    }
    
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
            //case DayCycles.Midnight:
                //targetColor = _midnight;
                //break;
            case DayCycles.Night:
                targetColor = _night;
                break;
            default:
                print("DayCycles not implemented");
                break;
        }
        
        return targetColor;
    }

    public int GetDayCount()
    {
        return soDayCycle.dayCount;
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
            //case DayCycles.Midnight:
                //targetColor = _midnight;
                //break;
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

