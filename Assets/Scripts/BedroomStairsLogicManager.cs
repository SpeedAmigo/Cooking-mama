using UnityEngine;

public class BedroomStairsLogicManager : MonoBehaviour
{
    [SerializeField] private BedroomManager bedroomManager;
    [SerializeField] private StairsController stairsController;
    [SerializeField] private SoObjectText soObjectText;

    private void Start()
    {
        bedroomManager = GetComponentInParent<BedroomManager>();
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<PlayerScript>(out PlayerScript playerScript))
        {
            if (bedroomManager.bedCleaned)
            {
                stairsController.CanTransition = true;
            }
            else
            {
                stairsController.CanTransition = false;
                soObjectText.ShowText(soObjectText.popUpText);
            }
        }
    }
}
