using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampPost : MonoBehaviour {

    private GameObject player1;
    private GameObject player2;

    private GameObject gameController;

    // Use this for initialization
    void Start () {

        player1 = GameObject.FindGameObjectWithTag("player1");
        player2 = GameObject.FindGameObjectWithTag("player2");

        gameController = GameObject.Find("gameController");

    }
	
    void OnTriggerEnter(Collider col)
    {
        if (col.transform == player1.transform && player1.GetComponent<PlayerManager>().touchable == true)
        {
            player1.GetComponent<PlayerManager>().curDanger += 10;
        }

        if (col.transform == player2.transform && player2.GetComponent<Player2Manager>().touchable == true)
        {
             player2.GetComponent<Player2Manager>().curDanger += 10;
        }


    }
}
