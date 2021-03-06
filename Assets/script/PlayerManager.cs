using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    // Le reste
    private float baseSpeed = 4.0f;
    private float sprintSpeed = 8.0f;
    public float rotSpeed = 5.0f;
    private float gazLeft = 100.0f;
    private float gazMax = 100.0f;
    public GameObject gaz;
    public float curDanger = 0;
    public float maxDanger = 100.0f;
    public GameObject danger;
    public GameObject[] possibleDanger;
    public int vie = 3;
    private int maxVie = 3;
    public bool touchable = true;
    [HideInInspector] public Vector3 startPos;
    public GameObject vies;
    [HideInInspector] public Vector3[] posVies;
    public GameObject camera1;
    private Keyboard keyboard;

    // Use this for initialization
    void Start()
    {
        keyboard = Keyboard.current;
        if (keyboard == null)
        {
            throw new UnityException("Keyboard not detected, not cool");
        }
        PositionLifeIndicators();
        StartCoroutine(DistanceComparison());
    }

    private void PositionLifeIndicators()
    {
        startPos = transform.position;
        for (int x = 0; vies.transform.childCount < vie; x++)
        {
            posVies[x] = vies.transform.GetChild(x).transform.localPosition;
        }
    }

    private void ListenForInputs()
    {
        if (keyboard == null)
        {
            throw new UnityException("No keyboard detected");
        }
        if (keyboard.dKey.isPressed)
        {
            transform.Rotate(Vector3.up * rotSpeed, Space.World);
        }
        if (keyboard.aKey.isPressed)
        {
            transform.Rotate(Vector3.up * -1 * rotSpeed, Space.World);
        }
        if (keyboard.wKey.isPressed)
        {
            MoveTractorForward();
        }
    }

    private void MoveTractorForward()
    {
        RaycastHit hit;
        if (TractorHitsObject(out hit))
        {
            string tTag = hit.transform.tag;
            if (tTag == "Danger" || tTag == "obstacle")
            {
                return;
            }
        }
        if (keyboard.leftShiftKey.isPressed && HasGazLeft())
        {
            MoveForwardWithBoost();
        }
        else
        {
            transform.Translate(Vector3.forward * baseSpeed * Time.deltaTime);
        }
    }

    private bool TractorHitsObject(out RaycastHit hit)
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1f);
    }

    private bool HasGazLeft()
    {
        return gazLeft > 0;
    }

    private void MoveForwardWithBoost()
    {
        transform.Translate(Vector3.forward * sprintSpeed * Time.deltaTime);
        gazLeft--;
        if (!HasGazLeft())
        {
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        ListenForInputs();

        if (vie == 0)
            mort();

        if (gazLeft < gazMax && !Keyboard.current.leftShiftKey.isPressed)
            gazLeft += 0.5f;

        if (curDanger > 95.0f && touchable == true)
            perdVie();

        if (curDanger > maxDanger)
            curDanger = maxDanger;

        if (curDanger > 50.0f)
        {
            //camera1.GetComponent<Grayscale>().enabled = true;
        }
        else
        {
            //camera1.GetComponent<Grayscale>().enabled = false;
        }
        gaz.GetComponent<RectTransform>().localScale = new Vector3((gazLeft / 100.0f), 1, 1);
        danger.GetComponent<RectTransform>().localScale = new Vector3((curDanger / 100), 1, 1);
    }

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
        touchable = false;
        yield return new WaitForSeconds(2);
        touchable = true;
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
            v = Instantiate(Resources.Load("prefabs/vie", typeof(GameObject))) as GameObject;

            v.transform.SetParent(vies.transform);

            Vector3 pos = v.transform.GetComponent<RectTransform>().localPosition;
            pos.y = 0;
            pos.x += 155 + (x * 35);

            v.transform.GetComponent<RectTransform>().localPosition = pos;
        }
    }

}
