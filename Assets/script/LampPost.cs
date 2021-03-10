using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        PlayerMovement playerMovement = col.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement)
        {
            playerMovement._currentDanger += 10;
        }
    }
}
