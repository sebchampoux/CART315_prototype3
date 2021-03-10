using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private PlayerStatus _playerStatus;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private RectTransform _gazIndicator;
    [SerializeField] private RectTransform _dangerIndicator;
    [SerializeField] private Text _oeufIndicator;
    [SerializeField] private Image[] _livesIndicators;
    [SerializeField] private Sprite _notCaughtSprite;
    [SerializeField] private Sprite _caughtSprite;

    private void Awake()
    {
        _playerStatus.StatusUpdate += UpdateDisplay;
        _playerMovement.MovementStatsUpdate += MovementStatsUpdate;
    }

    private void OnDestroy()
    {
        _playerStatus.StatusUpdate -= UpdateDisplay;
        _playerMovement.MovementStatsUpdate -= MovementStatsUpdate;
    }

    private void MovementStatsUpdate(object sender, System.EventArgs e)
    {
        UpdateGazIndicator();
        UpdateDangerIndicator();
    }

    private void UpdateDangerIndicator()
    {
        float dangerPercentage = _playerMovement.Danger / _playerMovement.MaxGaz;
        _dangerIndicator.localScale = new Vector3(dangerPercentage, 1, 1);
    }

    private void UpdateGazIndicator()
    {
        float gazPercentage = _playerMovement.Gaz / _playerMovement.MaxGaz;
        _gazIndicator.localScale = new Vector3(gazPercentage, 1, 1);
    }

    private void UpdateDisplay(object sender, System.EventArgs e)
    {
        UpdateEggDisplay();
        UpdateLivesIndicator();
    }

    private void UpdateLivesIndicator()
    {
        for(int i = 0; i < _livesIndicators.Length; i++)
        {
            Image current = _livesIndicators[i];
            if (i < _playerStatus.Lives)
            {
                current.sprite = _notCaughtSprite;
            }
            else
            {
                current.sprite = _caughtSprite;
            }
        }
    }

    private void UpdateEggDisplay()
    {
        _oeufIndicator.text = _playerStatus.Eggs.ToString();
    }
}
