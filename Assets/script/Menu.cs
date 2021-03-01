using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    public GameObject acceuil;
    public GameObject instruction;

    public Button fermerInstruction;
    public Button ouvrirInstruction;
    public Button jouer;

    public Texture2D cursorTexture;
    public Vector2 hotSpot = Vector2.zero;

    // Use this for initialization
    void Start()
    {
        Button btnJouer = jouer.GetComponent<Button>();
        btnJouer.onClick.AddListener(fJouer);

        Button btnCloseInstruction = fermerInstruction.GetComponent<Button>();
        btnCloseInstruction.onClick.AddListener(closeInstruction);

        Button btnOpenInstruction = ouvrirInstruction.GetComponent<Button>();
        btnOpenInstruction.onClick.AddListener(openInstruction);

        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.ForceSoftware);

        Cursor.visible = true;
    }


    /*-------------------------------------------------------------------------------------------------------------
*                                            instruction
-------------------------------------------------------------------------------------------------------------*/

    private void openInstruction()
    {
        instruction.GetComponent<Canvas>().enabled = true;
        acceuil.GetComponent<Canvas>().enabled = false;
        Debug.Log("ouvert");
    }

    private void closeInstruction()
    {
        instruction.GetComponent<Canvas>().enabled = false;
        acceuil.GetComponent<Canvas>().enabled = true;
        Debug.Log("fermer");
        return;
    }

    /*-------------------------------------------------------------------------------------------------------------
*                                            Accueil
-------------------------------------------------------------------------------------------------------------*/

    private void fJouer()
    {

        acceuil.GetComponent<Canvas>().enabled = false;
        instruction.GetComponent<Canvas>().enabled = false;

        SceneManager.LoadScene("test");
    }
}
