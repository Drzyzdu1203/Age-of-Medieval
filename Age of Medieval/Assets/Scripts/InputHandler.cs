using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public bool inventory_Input;

    public bool inventoryFlag;

    UIManager uiManager;

    private void Awake ()
    {
        uiManager = FindObjectOfType<UIManager>();
    }

    private void LateUpdate()
    {
        InputHandler.inventory_Input = false;

    }

    public void TickInput(float delta)
    {
        HandleInventoryInput();
    }

    private void HandleInventoryInput()
    {
        inputActions.PlayerActions.Inventory.performed += i => inventory_Input = true;

        if(inventory_Input)
        {
            inventoryFlag = !inventoryFlag;

            if (inventoryFlag)
            {
                uiManager.OpenSelectWindow();
            }
            else
            {
                uiManager.CloseSelectWindow();
            }
        }
    }
}
