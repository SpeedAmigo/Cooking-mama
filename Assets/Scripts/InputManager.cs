using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    public bool isEmpty;
    
    private IInputHandler _currentHandler;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHandler(IInputHandler handler)
    {
        _currentHandler = handler;
        isEmpty = false;
    }

    public void UnregisterHandler(IInputHandler handler)
    {
        if (_currentHandler == handler)
        {
            _currentHandler = null;
            isEmpty = true;
        }
    }

    private void Update()
    {
        _currentHandler?.HandleInput();
    }
}
