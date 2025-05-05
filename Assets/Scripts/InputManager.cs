using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    [ShowInInspector] private Stack<IInputHandler> _inputHandlers;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            _inputHandlers = new Stack<IInputHandler>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RegisterHandler(IInputHandler handler)
    {
        if (_inputHandlers.Contains(handler)) return;
        
        _inputHandlers.Push(handler);
    }

    public void UnregisterHandler(IInputHandler handler)
    {
        if (_inputHandlers.Count > 0 && _inputHandlers.Peek() == handler)
        {
            _inputHandlers.Pop();
        }
    }

    private void ClearHandlers()
    {
        if (_inputHandlers.Count > 10)
        {
            _inputHandlers.Clear();
        }
    }
    
    private void Update()
    {
        if (_inputHandlers.Count == 0) return;
        
        _inputHandlers?.Peek().HandleInput();
        ClearHandlers();
    }
}
