using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private int _playerPoints = 0;
    private int _eggs = 0;
    private int _lives = 0;
    [SerializeField] private int _maxLives = 3;

    public event EventHandler StatusUpdate;

    public int PlayerPoints
    {
        get { return _playerPoints; }
        private set
        {
            _playerPoints = Mathf.Max(0, value);
            OnStatusUpdate();
        }
    }

    public int Eggs
    {
        get { return _eggs; }
        private set
        {
            _eggs = Mathf.Max(0, value);
            OnStatusUpdate();
        }
    }

    public int Lives
    {
        get { return _lives; }
        private set
        {
            _lives = Mathf.Max(0, value);
            OnStatusUpdate();
        }
    }

    private void Start()
    {
        Lives = _maxLives;
    }

    protected void OnStatusUpdate()
    {
        StatusUpdate?.Invoke(this, EventArgs.Empty);
    }

    public void PickUpEgg()
    {
        Eggs += 1;
    }

    public void DropEggs(DropLocation dropLocation)
    {
        dropLocation.DropEggs(Eggs);
        Eggs = 0;
    }

    public void LoseLife()
    {
        Lives -= 1;
    }

    public void SetMaximumLives()
    {
        Lives = _maxLives;
    }
}
