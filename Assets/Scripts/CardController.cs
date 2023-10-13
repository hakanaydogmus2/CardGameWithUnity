using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardController : MonoBehaviour
{
    GameManager gameManager;
    CardAttributes cardAttributes;
    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotspot = Vector2.zero;
    public Vector2 hotspot2 = Vector2.zero;

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
            if(hit.collider.transform.position.y < -0.2)
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
        if (gameManager.playerCard == null)
        {
            gameManager.playerCard = gameObject; 
            gameManager.playerCardValue = GetCardValue(gameObject); 
            gameManager.playerCard.transform.position = new Vector3(0, -0.100000001f, 0.5f);
            
            gameManager.CardSelecter();
            gameManager.scoreCalculator();
            Debug.Log("opponentCard pos:" + gameManager.opponentCard.transform.position);
            gameManager.opponentCard.transform.position =  new Vector3(6.5f, -0.1f, 0.5f);
            gameManager.opponentCard.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Debug.Log("opponentCard pos:" + gameManager.opponentCard.transform.position);
        }

    }
    public int GetCardValue(GameObject Card)
    {
        CardAttributes cardAttributes = GetComponent<CardAttributes>();

        return cardAttributes.value;
        
    }

}
