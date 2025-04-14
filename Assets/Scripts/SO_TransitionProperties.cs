using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SO_TransitionProperties", menuName = "Scriptable Objects/SO_TransitionProperties")]
public class SO_TransitionProperties : ScriptableObject
{
    [Range(0f,5f)] public float transitionDuration = 0.5f;
}
