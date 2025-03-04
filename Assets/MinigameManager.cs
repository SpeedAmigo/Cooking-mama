using System;
using UnityEngine;

public class MinigameManager : Interaction
{
    [SerializeField] private GameObject panel;

    private void Start()
    {
        panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && panel.activeInHierarchy)
        {
            panel.SetActive(false);
        }
    }

    public override void Interact()
    {
        panel.SetActive(true);
    }
}
