using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [HideInInspector]
    public GameObject player1;
    [HideInInspector]
    public GameObject player2;

    public AudioClip hitSound;
    public AudioSource hitAudioSource;


    void Start () {
        player1 = GameObject.FindGameObjectWithTag("player1");
        player2 = GameObject.FindGameObjectWithTag("player2");
    }
	

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.forward, out hit, 15f))
        {
            if(hit.transform.tag == "player1")
            {
                if(player1.GetComponent<PlayerManager>().touchable == true)
                {
                    float distanceFromPlayer;
                    distanceFromPlayer = Vector3.Distance(this.transform.position, player1.transform.position);

                    player1.GetComponent<PlayerManager>().curDanger += 15/distanceFromPlayer;

                    HitSound();
                }
            }
        }
    }


    void HitSound()
    {
        if(hitSound != null && hitAudioSource != null)
        {
            hitAudioSource.clip = hitSound;
            hitAudioSource.Play();
        }
    }
}
