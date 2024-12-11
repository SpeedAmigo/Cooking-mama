using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LanternScript : MonoBehaviour
{
    private Light2D _light;
    private SpriteRenderer _sr;
    
    [SerializeField] private Sprite _unlitSprite;
    [SerializeField] private Sprite _litSprite;
    
    private bool _isFlickering = false;
    private float _noiseOffset;
    
    private void Light(bool state)
    {
        _light.enabled = state;

        _sr.sprite = state ? _litSprite : _unlitSprite;
        
        _isFlickering = state;
    }

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        _light = GetComponent<Light2D>();
        _light.enabled = false;
        _noiseOffset = UnityEngine.Random.Range(0f, 100f);
        _sr.sprite = _unlitSprite;
        StartCoroutine(LightWave());
    }
    
    private IEnumerator LightWave()
    {
        float time = 0;
        while (true)
        {
            if (_isFlickering)
            {
                time += Time.deltaTime;
                float noise = Mathf.PerlinNoise(time + _noiseOffset, 0f);
                _light.falloffIntensity = Mathf.Lerp(0.7f, 0.8f, noise);
            }
            yield return null;
        }
    }
    
    private void OnEnable()
    {
        EventsManager.LightToggleEvent += Light;
    }

    private void OnDisable()
    {
        EventsManager.LightToggleEvent -= Light;
    }
}
