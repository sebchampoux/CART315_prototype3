using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int _droppedEggs = 0;
    private int _numberOfEggsSpanned = 0;

    public GameObject[] spawnPointEnnemis;
    public GameObject[] spawnPointOeufs;

    public GameObject oeufPrefab;
    public GameObject ennemiPrefab;

    private GameObject[] video;

    [SerializeField] private DropLocation[] _dropLocations;

    void Start()
    {
        StartLevel();
        SubscribeToDropLocationsEvents();
        video = GameObject.FindGameObjectsWithTag("cancer");
    }

    private void SubscribeToDropLocationsEvents()
    {
        foreach (DropLocation d in _dropLocations)
        {
            d.DropEggsEvent += OnDropEggAtPickupTruck;
        }
    }

    private void OnDropEggAtPickupTruck(object sender, EventArgs e)
    {
        CountDroppedEggs();
        if (AllEggsPickedUp())
        {
            StartCoroutine(EndGame());
        }
    }

    private void CountDroppedEggs()
    {
        _droppedEggs = 0;
        foreach (DropLocation d in _dropLocations)
        {
            _droppedEggs += d.DroppedEggs;
        }
    }

    private bool AllEggsPickedUp()
    {
        return _droppedEggs >= _numberOfEggsSpanned;
    }

    private void StartLevel()
    {
        spawnOeufs();
        spawnEnnemies();
    }

    public IEnumerator EndGame()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            SceneManager.LoadScene("Menu");
        }
    }

    private void spawnOeufs()
    {
        foreach (GameObject spawnPt in spawnPointOeufs)
        {
            _numberOfEggsSpanned++;
            GameObject oeufInstance = Instantiate(oeufPrefab, spawnPt.transform);
            oeufInstance.transform.parent = gameObject.transform;
        }
    }

    private void spawnEnnemies()
    {
        foreach (GameObject spawnPt in spawnPointEnnemis)
        {
            GameObject ennemiGO = Instantiate(ennemiPrefab, spawnPt.transform);
            ennemiGO.transform.parent = gameObject.transform;
        }
    }
}
