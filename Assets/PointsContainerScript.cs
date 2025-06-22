using System.Collections.Generic;
using UnityEngine;

public class PointsContainerScript : MonoBehaviour
{
    [SerializeField] private SophieScript sophieScript;
    private HashSet<SkinPointScript> points;
    
    public void AddToPoints(SkinPointScript pointScript)
    {
        if (points.Contains(pointScript)) return;
           
        points.Add(pointScript);
        sophieScript.cleanedFace = CheckIfComplete(points);
    }

    private bool CheckIfComplete(HashSet<SkinPointScript> points)
    {
        if (points.Count == 3) return true;
        return false;
    }
}
