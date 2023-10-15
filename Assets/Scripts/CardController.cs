using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class CardController : MonoBehaviour
{
    
    GameManager gameManager;
    CardAttributes cardAttributes;
    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotspot = Vector2.zero;
    private Vector2 hotspot2 = Vector2.zero;

    public static float zCounter = 5.0f;
    

    // Start is called before the first frame update
    void Start()
    {
       
        gameManager = FindObjectOfType<GameManager>();
        cardAttributes = GetComponent<CardAttributes>();

        Cursor.SetCursor(cursorTexture2, hotspot2, CursorMode.ForceSoftware);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit) )
        {
            if(hit.collider.transform.position.y < -0.2f)
            {
                Cursor.SetCursor(cursorTexture, hotspot, cursorMode);
            }
        }
        
        
    }
     void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexture2, hotspot2, CursorMode.Auto);
        
    }
    
    private void OnMouseDown()
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray,out hit) )
        {
            if (hit.collider.transform.position.y < -0.2f && gameManager.isPlayerFirst == true)
            {
                
                gameManager.playerCard = gameObject;
                gameManager.playerCardValue = GetCardValue(gameObject);
                gameManager.playerCard.transform.position = new Vector3(0, -0.100000001f, 0.5f);
                gameManager.playerCard.transform.localScale = new Vector3(50, 50, zCounter);

                gameManager.CardSelector(gameManager.isPlayerFirst);
                gameManager.scoreCalculator();
                
                gameManager.opponentCard.transform.position = new Vector3(6.5f, -0.1f, 0.5f);
                gameManager.opponentCard.transform.localRotation = new Quaternion(0, 0, 0, 0);
                gameManager.opponentCard.transform.localScale = new Vector3(50f, 50f, zCounter);
                zCounter++;
            }

            if(hit.collider.transform.position.y < -0.2f && gameManager.isPlayerFirst == false && gameManager.isAiPlayed == true)
            {
                gameManager.playerCard = gameObject;
                gameManager.playerCardValue = GetCardValue(gameObject);
                gameManager.playerCard.transform.position = new Vector3(0, -0.100000001f, 0.5f);
                gameManager.playerCard.transform.localScale = new Vector3(50, 50, zCounter);
                zCounter++;
                gameManager.scoreCalculator();
                gameManager.isAiPlayed = false;
                gameManager.CardSelector(gameManager.isPlayerFirst);
            }
        }

        
    }
    public int GetCardValue(GameObject Card)
    {
        CardAttributes cardAttributes = GetComponent<CardAttributes>();

        return cardAttributes.value;
        
    }

}
