using UnityEngine;

public abstract class BedroomCleanAbstract : MonoBehaviour, IMinigameInteractable
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
