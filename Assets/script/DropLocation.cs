using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropLocation : MonoBehaviour {

    private GameObject player1;
    private GameObject gameController;

    void Start()
    {

        player1 = GameObject.FindGameObjectWithTag("player1");

        gameController = GameObject.Find("gameController");

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform == player1.transform && gameController.GetComponent<GameController>().curPlayer1HoldEggs > 0)
        {
            gameController.GetComponent<GameController>().player1points += 1 * gameController.GetComponent<GameController>().curPlayer1HoldEggs;
            gameController.GetComponent<GameController>().curPlayer1HoldEggs = 0;
        }
    }
}
