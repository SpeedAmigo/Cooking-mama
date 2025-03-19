using UnityEngine;

public abstract class BedroomCleanAbstract : MonoBehaviour
{
    private OutlineFx.OutlineFx outline;

    protected virtual void Start()
    {
        outline = GetComponent<OutlineFx.OutlineFx>() ?? gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
    }
    
    protected abstract void OnMouseDown();

    protected virtual void OnMouseEnter()
    {
        if (outline != null) outline.enabled = true;
    }

    protected virtual void OnMouseExit()
    {
        if (outline != null) outline.enabled = false;
    }
}
