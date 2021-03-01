using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

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

        if (col.transform == player1.transform) {

            gameController.GetComponent<GameController>().curPlayer1HoldEggs += 1;
            gameController.GetComponent<GameController>().curOeufs -= 1;

            Destroy(gameObject);
        }
        else if(col.transform == player2.transform)
        {

            gameController.GetComponent<GameController>().curPlayer2HoldEggs += 1;
            gameController.GetComponent<GameController>().curOeufs -= 1;

            Destroy(gameObject);
        }

        
    }
}
