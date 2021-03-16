using System;
using UnityEngine;

public class Oeuf : MonoBehaviour
{
    public event EventHandler OnEggDestroy;

    private void OnDestroy()
    {
        if (OnEggDestroy != null)
        {
            OnEggDestroy.Invoke(this, EventArgs.Empty);
        }
    }
}
