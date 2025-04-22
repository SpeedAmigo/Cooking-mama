using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallWindowScript : MonoBehaviour
{
    [SerializeField] private SpriteRenderer windowOutside;
    
    private ColorTransitionHelper colorHelper = new();

    private void OnEnable()
    {
        EventsManager.LightColorChangeEvent += ChangeLightColor;
    }

    private void OnDisable()
    {
        EventsManager.LightColorChangeEvent -= ChangeLightColor;
    }

    private void ChangeLightColor(Color toColor, float Duration)
    {
        colorHelper.ColorTransition(() => windowOutside.color, x => windowOutside.color = x, toColor, Duration);
    }
}
