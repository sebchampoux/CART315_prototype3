using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventoryDisplay : MonoBehaviour
{
    [SerializeField] private PlayerStatus _playerInventory;

    private void Awake()
    {
        _playerInventory.StatusUpdate += UpdateDisplay;
    }

    private void UpdateDisplay(object sender, System.EventArgs e)
    {

    }
}
