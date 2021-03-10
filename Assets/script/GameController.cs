using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int _nbrOeufs = 0;

    public GameObject[] spawnPointEnnemis;
    public GameObject[] spawnPointOeufs;

    public GameObject oeufPrefab;
    public GameObject ennemiPrefab;

    private GameObject[] video;

    public GameObject paneauVictoire;
    public GameObject paneauJoueur1;


    void Start()
    {
        StartLevel();
        video = GameObject.FindGameObjectsWithTag("cancer");
    }

    private void StartLevel()
    {
        spawnOeufs();
        spawnEnnemies();
    }

    public IEnumerator finJeu()
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
            _nbrOeufs++;
            GameObject oeufInstance = Instantiate(oeufPrefab, spawnPt.transform);
            oeufInstance.transform.parent = gameObject.transform;
            oeufInstance.GetComponent<Oeuf>().OnEggDestroy += OnEggDestroy;
        }
    }

    private void OnEggDestroy(object sender, EventArgs e)
    {
        _nbrOeufs--;
        if (_nbrOeufs == 0)
        {
            // Y'a pu d'oeufs, fin de la partie
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
