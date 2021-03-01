using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public int player1points;
    public int player2points;

    public Text pointsPlayer1;
    public Text pointsPlayer2;

    public int curPlayer1HoldEggs;
    public int curPlayer2HoldEggs;

    [HideInInspector]
    public int nbreEnnemie = 2;
    public int curNbreEnnemie;

    public int curOeufs;
    public int maxOeufs = 4;

    public int curLvl;
    public int maxLvl;
    
    public GameObject[] spawnPointEnnemis;
    public GameObject[] spawnPointOeuf;
    
    public GameObject oeuf;
    public GameObject ennemi;

    private GameObject player1;
    private GameObject player2;

    private GameObject[] video;

    public GameObject paneauVictoire;
    public GameObject paneauJoueur1;
    public GameObject paneauJoueur2;

    private string[] cheatCode;
    private int indexDuCheatCode;

    private void Awake()
    {
        // Le cheat code
        cheatCode = new string[] { "b", "a", "m", "b", "o", "o", "z", "l", "e" };
        indexDuCheatCode = 0;

        player1points = 0;
        player2points = 0;

        curLvl = 0;
        maxLvl = 4;

        player1 = GameObject.FindGameObjectWithTag("player1");
        player2 = GameObject.FindGameObjectWithTag("player2");
    }


    void Start()
    {
        mapGen();

        video = GameObject.FindGameObjectsWithTag("cancer");

    }


    private void Update()
    {

        if(Int32.Parse(pointsPlayer1.text) != player1points)
        {
            pointsPlayer1.text = player1points.ToString();
        }

        if (Int32.Parse(pointsPlayer2.text) != player2points)
        {
            pointsPlayer2.text = player2points.ToString();
        }

        //nouveau niveau
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


        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(cheatCode[indexDuCheatCode]))
            {
                indexDuCheatCode++;
            }

            else
            {
                indexDuCheatCode = 0;
            }
        }

        if (indexDuCheatCode == cheatCode.Length)
        {
            for(int x = 0; x < video.Length; x++)
            {
                video[x].GetComponent<MeshRenderer>().enabled = true;
                video[x].GetComponent<Video>().commencerVideo();
            }
        }
    }

    private void mapGen()
    {
        curLvl += 1;

        curPlayer1HoldEggs = 0;
        curPlayer2HoldEggs = 0;

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
        //spawnPointOeuf = GameObject.FindGameObjectsWithTag("spawnPointOeuf");
        for (int x = 0; x < maxOeufs; x++)
        {
            int rand = UnityEngine.Random.Range(0, spawnPointOeuf.Length);
            GameObject e;
            e = Instantiate(oeuf, spawnPointOeuf[rand].transform);
            e.transform.parent = gameObject.transform;

            curOeufs++;
        }
    }

    private void spawnEnnemies()
    {
        //spawnPointEnnemis = GameObject.FindGameObjectsWithTag("spawnPointEnnemie");

        for(curNbreEnnemie = 0; curNbreEnnemie < nbreEnnemie; curNbreEnnemie++)
        {
            int rand = UnityEngine.Random.Range(0, spawnPointEnnemis.Length);

            GameObject e;
            e = Instantiate(ennemi, spawnPointEnnemis[rand].transform);
            e.transform.parent = gameObject.transform;
        }
    }
}
