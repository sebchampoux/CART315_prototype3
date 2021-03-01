using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Video : MonoBehaviour
{

    public void commencerVideo()
    {
        StartCoroutine(finVideo());
    }

    public IEnumerator finVideo()
    {
        /*
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Play();
        yield return new WaitForSeconds(17);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        ((MovieTexture)GetComponent<Renderer>().material.mainTexture).Stop();
        StopCoroutine(finVideo());
    */
        yield return null;
    }
}
