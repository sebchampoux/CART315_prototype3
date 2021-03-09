using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [HideInInspector]
    public int nbrEnnemis = 2;
    public int nombreEnnemisActuel;

    public int curOeufs;
    public int maxOeufs = 4;

    public GameObject[] spawnPointEnnemis;
    public GameObject[] spawnPointOeufs;

    public GameObject oeuf;
    public GameObject ennemi;

    private GameObject player1;

    private GameObject[] video;

    public GameObject paneauVictoire;
    public GameObject paneauJoueur1;

    private void Awake()
    {
        player1 = GameObject.FindGameObjectWithTag("player1");
    }


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
            GameObject oeufInstance = Instantiate(oeuf, spawnPt.transform);
            oeufInstance.transform.parent = gameObject.transform;
        }
    }

    private void spawnEnnemies()
    {
        foreach (GameObject spawnPt in spawnPointEnnemis)
        {
            GameObject ennemiGO = Instantiate(ennemi, spawnPt.transform);
            ennemiGO.transform.parent = gameObject.transform;
        }
    }
}
