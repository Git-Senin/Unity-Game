using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField]
    private UIInventoryPage inventoryUI;

    public int inventorySize = 10;  // Temp

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryUI.isActiveAndEnabled == false)
            {
                inventoryUI.Show();
            }
            else
            {
                inventoryUI.Hide();
            }
        }
    }
}
