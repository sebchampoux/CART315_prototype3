using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickable : MonoBehaviour {

    private GameObject player1;
    private GameObject gameController;

    void Start () {
        player1 = GameObject.FindGameObjectWithTag("player1");
        gameController = GameObject.Find("gameController");
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.transform == player1.transform) {

            gameController.GetComponent<GameController>().curPlayer1HoldEggs += 1;
            gameController.GetComponent<GameController>().curOeufs -= 1;

            Destroy(gameObject);
        }
    }
}
