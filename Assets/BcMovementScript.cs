using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BcMovementScript : SerializedMonoBehaviour
{
    [SerializeField] private float offset = 0f;   
    [SerializeField] private float distance = 1f;
    [SerializeField] private Transform target;

    [SerializeField] private Dictionary<DayCycles, GameObject> backgroundDict = new();
    [SerializeField] private DayNightScript dayNightScript;
    
    private DayCycles currentDayCycle;

    private void Start()
    {
        SetBackground(dayNightScript.CurrentDayCycle);
    }
    
    private void Update()
    {
        gameObject.transform.position = MoveBc(gameObject, target);

        if (currentDayCycle != dayNightScript.CurrentDayCycle)
        {
            SetBackground(dayNightScript.CurrentDayCycle);
        }
    }

    private Vector3 MoveBc(GameObject Bc, Transform target)
    {
        float targetY = (target.position.y * distance) + offset;
        return new Vector3(Bc.transform.position.x, targetY, Bc.transform.position.z);
    }

    private void SetBackground(DayCycles dayCycle)
    {
        if (!backgroundDict.ContainsKey(dayCycle)) return;

        foreach (var value in backgroundDict)
        { 
            value.Value.SetActive(value.Key == dayCycle);
        }
        
        currentDayCycle = dayCycle;
    }
}
