using UnityEngine;

public abstract class BedroomCleanAbstract : MonoBehaviour
{
    private OutlineFx.OutlineFx outline;
    protected BedroomMinigameManager manager;
    
    protected virtual void Awake()
    {
        outline = GetComponent<OutlineFx.OutlineFx>() ?? gameObject.AddComponent<OutlineFx.OutlineFx>();
        outline.enabled = false;
        
        EventsManager.OnGetBedroomManager += ManagerReference;
    }

    protected virtual void OnDisable()
    {
        EventsManager.OnGetBedroomManager -= ManagerReference;
    }

    private void ManagerReference(BedroomMinigameManager manager)
    {
        this.manager = manager;
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
