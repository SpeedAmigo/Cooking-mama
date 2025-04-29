using Sirenix.OdinInspector;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_ObjectText", menuName = "Scriptable Objects/ObjectText")]

[InlineEditor(InlineEditorModes.GUIOnly)]
public class SoObjectText : ScriptableObject
{
    [MultiLineProperty(5)]
    public string[] popUpText;
}
