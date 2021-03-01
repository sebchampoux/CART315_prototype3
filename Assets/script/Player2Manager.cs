using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class Player2Manager : MonoBehaviour
{

    // Pour le son
    //public AudioSource sourceAudioDriving;
    //public AudioSource sourceAudioSFX;
    //public AudioClip sonEngineIdle;
    //public AudioClip sonEngineDriving;
    //public float pitchRange = 0.2f;
    //private float originalSoundPitch;

    //public bool enDeplacement = false;

    private float baseSpeed = 4.0f;
    private float sprintSpeed = 8.0f;
    public float rotSpeed = 5.0f;

    private float gazLeft = 100.0f;
    private float gazMax = 100.0f;
    public GameObject gaz;

    public float curDanger = 0;
    public float maxDanger = 100;
    public GameObject danger;

    public GameObject[] possibleDanger;

    public int vie = 3;
    private int maxVie = 3;

    public bool touchable = true;

    [HideInInspector]
    public Vector3 startPos;

    public GameObject vies;

    [HideInInspector]
    public Vector3[] posVies;

    public GameObject camera1;


    // Use this for initialization
    void Start()
    {
        //originalSoundPitch = sourceAudioDriving.pitch;
        //sourceAudioDriving.clip = sonEngineIdle;
        //sourceAudioDriving.Play();

        startPos = transform.position;

        for (int x = 0; vies.transform.childCount < vie; x++)
        {
            posVies[x] = vies.transform.GetChild(x).transform.localPosition;
        }


        StartCoroutine(DistanceComparison());

    }

    // Update is called once per frame
    void Update()
    {

        InputListen();

        if (vie == 0)
            mort();

        if (gazLeft < gazMax && !(Input.GetKey(KeyCode.LeftShift)))
            gazLeft += 0.5f;

        if (curDanger > 95.0f && touchable == true)
            perdVie();

        if (curDanger > maxDanger)
            curDanger = maxDanger; 

        gaz.GetComponent<RectTransform>().localScale = new Vector3((gazLeft / 100.0f * -1), 1, 1);

        danger.GetComponent<RectTransform>().localScale = new Vector3((curDanger / 100 * -1), 1, 1);

        if (curDanger > 50.0f)
        {
            camera1.GetComponent<Grayscale>().enabled = true;
        }
        else
        {
            camera1.GetComponent<Grayscale>().enabled = false;
        }
        // Ajuster les sons selon les mouvements du tracteur
        //EngineAudio();
    }


    // Ajuste le son du tracteur selon s'il est en mouvement ou pas
    //private void EngineAudio()
    //{
    //    if (enDeplacement && sourceAudioDriving.clip != sonEngineDriving)
    //    {
    //        sourceAudioDriving.clip = sonEngineDriving;
    //        sourceAudioDriving.pitch = UnityEngine.Random.Range(originalSoundPitch - pitchRange, originalSoundPitch + pitchRange);
    //        sourceAudioDriving.Play();
    //    }
    //    else if (!enDeplacement && sourceAudioDriving != sonEngineIdle)
    //    {
    //        sourceAudioDriving.clip = sonEngineIdle;
    //        sourceAudioDriving.pitch = UnityEngine.Random.Range(originalSoundPitch - pitchRange, originalSoundPitch + pitchRange);
    //        sourceAudioDriving.Play();
    //    }
    //}


    public IEnumerator DistanceComparison()
    {
        while (true)
        {
            possibleDanger = GameObject.FindGameObjectsWithTag("Danger");
            yield return new WaitForSeconds(0.01f);

            float distanceFromPlayer;

            for (int x = 0; x < possibleDanger.Length; x++)
            {

                distanceFromPlayer = Vector3.Distance(this.transform.position, possibleDanger[x].transform.position);

                if (distanceFromPlayer < 7.5f)
                {
                    if (distanceFromPlayer < 5f)
                    {
                        if (distanceFromPlayer < 3.5f)
                        {
                            if (distanceFromPlayer < 1.5f)
                            {
                                if (curDanger < maxDanger)
                                    curDanger += 1f;
                            }
                            else if (curDanger < maxDanger)
                                curDanger += 0.5f;
                        }
                        else if (curDanger < maxDanger)
                            curDanger += 0.25f;
                    }
                    else if (curDanger < maxDanger)
                        curDanger += 0.15f;
                }
            }

            if (curDanger > 0.0f)
            {
                curDanger -= 1f;
            }

        }
    }

    public void perdVie()
    {
        StartCoroutine(timerImmuniter());
        Destroy(vies.transform.GetChild(vies.transform.childCount - 1).gameObject);
        vie -= 1;
    }

    public IEnumerator timerImmuniter()
    {
        // suspend execution for 5 seconds
        touchable = false;
        yield return new WaitForSeconds(2);
        touchable = true;
    }

    private void InputListen()
    {

        //enDeplacement = false;

        // Rotation

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * rotSpeed, Space.World);
            //enDeplacement = true;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -1 * rotSpeed, Space.World);
            //enDeplacement = true;
        }



        //Avancer

        if (Input.GetKey(KeyCode.UpArrow))
        {
            //enDeplacement = true

            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f))
            {
                string tTag = hit.transform.tag;

                if (tTag == "Danger" || tTag == "obstacle")
                {
                    return;
                }

                if (Input.GetKey(KeyCode.KeypadEnter) && gazLeft > 0)
                {
                    transform.Translate(Vector3.forward * sprintSpeed * Time.deltaTime);
                    gazLeft--;

                    if (gazLeft <= 0)
                        return;
                }
                else
                {
                    transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
                }
            }


            if (Input.GetKey(KeyCode.KeypadEnter) && gazLeft > 0)
            {
                transform.Translate(Vector3.forward * sprintSpeed * Time.deltaTime);
                gazLeft--;

                if (gazLeft <= 0)
                    return;
            }
            else
            {
                transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
            }
        }

    }

    public void mort()
    {
        transform.position = startPos;
        vie = maxVie;
        gazLeft = gazMax;
        regainLife();
    }

    public void regainLife()
    {
        for (int x = 0; vies.transform.childCount < vie; x++)
        {
            GameObject v;
            v = Instantiate(Resources.Load("prefabs/vie2", typeof(GameObject))) as GameObject;

            v.transform.SetParent(vies.transform);

            Vector3 pos = v.transform.GetComponent<RectTransform>().localPosition;
            pos.y = 0;
            pos.x = 0 + (x * 35);

            v.transform.GetComponent<RectTransform>().localPosition = pos;
        }
    }

}
