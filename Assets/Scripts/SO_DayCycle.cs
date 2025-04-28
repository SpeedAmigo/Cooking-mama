using UnityEngine;
using Sirenix.OdinInspector;

[CreateAssetMenu(fileName = "DayCycle_SO", menuName = "Scriptable Objects/DayCycle_SO")]
[InlineEditor]
public class SO_DayCycle : ScriptableObject
{
    [EnumToggleButtons]
    public DayCycles dayCycle;
}
