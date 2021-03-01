using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Video;

public class accueil : MonoBehaviour {

    public GameObject acceuil;
    public GameObject instruction;

    public Button fermerInstruction;
    public Button ouvrirInstruction;
    public Button jouer;

    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    public GameObject video;

    // Use this for initialization
    void Start () {
        Button btnJouer = jouer.GetComponent<Button>();
        btnJouer.onClick.AddListener(fJouer);

        Button btnCloseInstruction = fermerInstruction.GetComponent<Button>();
        btnCloseInstruction.onClick.AddListener(closeInstruction);

        Button btnOpenInstruction = ouvrirInstruction.GetComponent<Button>();
        btnOpenInstruction.onClick.AddListener(openInstruction);

        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);

        Cursor.visible = true;

    }

    // Update is called once per frame
    void Update()
    {

    }


    /*-------------------------------------------------------------------------------------------------------------
*                                            instruction
-------------------------------------------------------------------------------------------------------------*/

    private void openInstruction()
    {
        instruction.GetComponent<Canvas>().enabled = true;
        acceuil.GetComponent<Canvas>().enabled = false;
    }

    private void closeInstruction()
    {
        instruction.GetComponent<Canvas>().enabled = false;
        acceuil.GetComponent<Canvas>().enabled = true;
    }

    /*-------------------------------------------------------------------------------------------------------------
*                                            Accueil
-------------------------------------------------------------------------------------------------------------*/

    private void fJouer()
    {
        /*
        acceuil.GetComponent<Canvas>().enabled = false;
        instruction.GetComponent<Canvas>().enabled = false;
        */

        StartCoroutine(depart());
    }

    public IEnumerator depart()
    {
        while (true)
        {
            acceuil.GetComponent<Canvas>().enabled = false;
            instruction.GetComponent<Canvas>().enabled = false;

            video.GetComponent<VideoPlayer>().enabled = true;

            yield return new WaitForSeconds(15f);


            SceneManager.LoadScene("AnotherScene");
        }
    }
}
