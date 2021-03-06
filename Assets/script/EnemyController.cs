﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    [HideInInspector]
    public GameObject player1;

    public AudioClip hitSound;
    public AudioSource hitAudioSource;


    void Start () {
        player1 = GameObject.FindGameObjectWithTag("player1");
    }
	

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, -Vector3.forward, out hit, 15f))
        {
            if(hit.transform.tag == "player1")
            {
                if(player1.GetComponent<PlayerMovement>().Touchable == true)
                {
                    float distanceFromPlayer;
                    distanceFromPlayer = Vector3.Distance(this.transform.position, player1.transform.position);

                    player1.GetComponent<PlayerMovement>()._currentDanger += 15/distanceFromPlayer;

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
