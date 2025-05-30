using UnityEngine;

public abstract class KitchenMinigameAbstract : MonoBehaviour, IMinigameInteractable
{
    private OutlineFx.OutlineFx outline;
    protected virtual void Awake()
    {
        outline = GetComponent<OutlineFx.OutlineFx>() ?? gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    protected abstract void OnRaycastClick();

    public virtual void OnRaycastOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnRaycastClick();
        }
    }

    public virtual void OnRaycastEnter()
    {
        if (outline != null) outline.enabled = true;
    }

    public virtual void OnRaycastExit()
    {
        if (outline != null) outline.enabled = false;
    }
}