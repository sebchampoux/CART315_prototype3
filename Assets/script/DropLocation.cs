using System;
using UnityEngine;

public class DropLocation : MonoBehaviour
{
    private int _numberOfDroppedEggs = 0;
    public event EventHandler DropEggsEvent;

    public int DroppedEggs
    {
        get { return _numberOfDroppedEggs; }
        private set { _numberOfDroppedEggs = Mathf.Max(0, value); }
    }

    public void DropEggs(int numberOfEggs)
    {
        DroppedEggs += numberOfEggs;
        DropEggsEvent.Invoke(this, EventArgs.Empty);
    }
}
