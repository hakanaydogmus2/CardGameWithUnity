using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardController : MonoBehaviour
{
    GameManager gameManager;
    CardAttributes cardAttributes;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        cardAttributes = GetComponent<CardAttributes>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnMouseDown()
    {
        if (gameManager.playerCard == null)
        {
            gameManager.playerCard = gameObject; 
            gameManager.playerCardValue = GetCardValue(gameObject); 
            transform.position = new Vector3(0, -0.100000001f, 0.5f);
            
            gameManager.CardSelecter();
            gameManager.scoreCalculator();
        }

    }
    public int GetCardValue(GameObject Card)
    {
        CardAttributes cardAttributes = GetComponent<CardAttributes>();

        return cardAttributes.value;
        
    }

}
