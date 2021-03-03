using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int player1points;
    public Text pointsPlayer1;
    public int curPlayer1HoldEggs;

    [HideInInspector]
    public int nbreEnnemie = 2;
    public int curNbreEnnemie;

    public int curOeufs;
    public int maxOeufs = 4;
    
    public GameObject[] spawnPointEnnemis;
    public GameObject[] spawnPointOeuf;
    
    public GameObject oeuf;
    public GameObject ennemi;

    private GameObject player1;

    private GameObject[] video;

    public GameObject paneauVictoire;
    public GameObject paneauJoueur1;

    private void Awake()
    {
        player1points = 0;
        player1 = GameObject.FindGameObjectWithTag("player1");
    }


    void Start()
    {
        StartLevel();
        video = GameObject.FindGameObjectsWithTag("cancer");
    }


    private void Update()
    {

        if(Int32.Parse(pointsPlayer1.text) != player1points)
        {
            pointsPlayer1.text = player1points.ToString();
        }

        //nouveau niveau
        /*
        if (curLvl == maxLvl)
        {
            paneauVictoire.GetComponent<Canvas>().enabled = true;
            if (player1points < player2points)
            {
                paneauJoueur2.GetComponent<Image>().enabled = true;
            }
            if (player1points > player2points)
            {
                paneauJoueur1.GetComponent<Image>().enabled = true;
            }

            StartCoroutine(finJeu());
        }
        else if(curOeufs == 0 && curPlayer1HoldEggs == 0 && curPlayer2HoldEggs == 0)
        {
            //destroy tout les monstres
            for (int x = 0; x < gameObject.transform.childCount; x++)
            {
                GameObject obj;
                obj = gameObject.transform.GetChild(x).gameObject;
                Destroy(obj);
            }

            maxOeufs += curLvl;
            nbreEnnemie += curLvl;

            player1.GetComponent<PlayerManager>().mort();
            player2.GetComponent<Player2Manager>().mort();

            mapGen();
        }
        */
    }

    private void StartLevel()
    {
        curPlayer1HoldEggs = 0;
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
        foreach(GameObject spawnPt in spawnPointOeuf)
        {
            GameObject oeufInstance = Instantiate(oeuf, spawnPt.transform);
            oeufInstance.transform.parent = gameObject.transform;
        }
    }

    private void spawnEnnemies()
    {
        foreach(GameObject spawnPt in spawnPointEnnemis)
        {
            GameObject ennemiGO = Instantiate(ennemi, spawnPt.transform);
            ennemiGO.transform.parent = gameObject.transform;
        }
    }
}
