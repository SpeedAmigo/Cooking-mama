using UnityEngine;

public class KitchenBookUI : MonoBehaviour
{
    [SerializeField] private Animator bookAnimator;

    public void NexPageAnimation()
    {
        bookAnimator.SetTrigger("NextPage");
    }

    public void PreviousPageAnimation()
    {
        bookAnimator.SetTrigger("PreviousPage");
    }
}
