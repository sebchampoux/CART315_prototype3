using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiturePolice : MonoBehaviour
{
    public AudioSource sourceAudioSirene;

    private int destPoint = 0;
    public Transform[] path;
    private int target = 0;
    private float speed = 10.0f;


    // Pour la sirène
    //private void Start()
    //{
    //    StartCoroutine("SonPolice");
    //}

    private IEnumerator SonPolice()
    {
        yield return new WaitForSeconds(7f);

        sourceAudioSirene.Play();

        yield return new WaitForSeconds(15f);

        StartCoroutine("SonPolice");
    }


    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, path[target].position, step);

        Vector3 targetDir = path[target].position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0F);
        transform.rotation = Quaternion.LookRotation(newDir);

        float dist = Vector3.Distance(path[target].position, transform.position);

        if (dist < 0.5f)
        {
            if (target < path.Length - 1)
                target++;
            else
                target = 0;
        }
    }

}
