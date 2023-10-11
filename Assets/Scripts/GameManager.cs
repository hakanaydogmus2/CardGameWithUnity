using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using Random = UnityEngine.Random;


public class GameManager : MonoBehaviour
{

    public GameObject[] cards;

    public GameObject deck;
    public GameObject playerCard;
    public GameObject[] playerHand;
    public GameObject[] opponentHand;

    
    public int playerCardValue;
    [SerializeField] private int playerScore;
    public GameObject opponentCard;
    public int opponentCardValue;
    [SerializeField] private int opponentScore;
    public TextMeshProUGUI playerText;
    public TextMeshProUGUI opponentText;

    private GameObject[] container;
    int totalCards;
    private GameObject[] holder;
    
    CardAttributes cardAttributes;
    CardController cardController;



    // Start is called before the first frame update
    void Start()
    {

        createCards();
        CreateHands();
        Debug.Log("alektamovik");
        opponentHand[1].transform.position = new Vector3(1, 1, 1);
        Debug.Log("movik");
    }

    void Awake()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(opponentCard.transform.position, Vector3.forward, out hit, 10f))
        {
            Debug.Log("Hit: " + hit.collider.gameObject.name);
        }
    }
    public void CardSelecter()
    {
        Debug.Log("card selecter called");
        // maximum card value for opponent
        opponentCardValue = 0;
        foreach(GameObject card in opponentHand)
        {
            if(card.GetComponent<CardAttributes>().value > opponentCardValue)
            {
                Debug.Log("Card Position Before: " + card.transform.position);
                opponentCardValue = card.GetComponent<CardAttributes>().value;
                opponentCard = card;
                Debug.Log("Card Position After: " + card.transform.position);
            }
            
        }
        int index = Array.IndexOf(opponentHand, opponentCard);

        //RemoveElement(ref opponentHand, 0 );
        opponentHand[index].transform.position =  new Vector3 (0, -0.100000001f, 0.5f);
        Debug.Log("card moved");
        
    }

    public void scoreCalculator()
    {
        if(playerCardValue > opponentCardValue)
        {
            playerScore++;
            playerText.text = "Puan: " + playerScore;
        }
        else if(playerCardValue < opponentCardValue)
        {
            opponentScore++;
            opponentText.text = "Puan: " + opponentScore;
        }              
    }

    private void CreateHands()
    {
        int playerZ = 5;
        float playerX = 10;

        for(int i = 0; i < 4; i++)
        {
            
            playerHand[i] = cards[0];           
            playerHand[i].transform.position = new Vector3(playerX, -7.5f, 0.5f);
            playerHand[i].transform.localScale = new Vector3(50, 50, playerZ);
            playerHand[i].transform.localRotation = new Quaternion(0,0,0,0);
            RemoveElement(ref cards, 0);
            Instantiate(playerHand[i]);
            playerX -= 2.5f;
            playerZ++;
            

        }
        int opponentZ = 5;
        float opponentX = 5;
        for (int i = 0; i < 4; i++)
        {
            opponentHand[i] = cards[0];           
            opponentHand[i].transform.position = new Vector3(opponentX, 7.5f, 0.5f);
            opponentHand[i].transform.localScale = new Vector3(50, 50, opponentZ);
            opponentHand[i].transform.localRotation = new Quaternion(0, 1, 0, 0);
            RemoveElement(ref cards, 0);
            Instantiate(opponentHand[i]);
            opponentX -= 2.5f;
            opponentZ++;
            

        }
        Debug.Log("hands created");
        
    }
    private void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int i = index ; i < arr.Length -1; i++) 
        {
            arr[i] = arr[i + 1];

        }

        Array.Resize(ref arr, arr.Length -1);
    }
    
    //private void Shuffle(GameObject[] deck)
    //{
    //    for (int i = 0; i < deck.Length; i++)
    //    {
    //        container[0] = deck[i];
    //        int randomIndex = Random.Range(i,deck.Length);
    //        deck[i] = deck[randomIndex];
    //        deck[randomIndex] = container[0];
    //    }
    //    Debug.Log("cards shuffled");
    //}
    void createCards()
    {
        totalCards = deck.transform.childCount;
        cards = new GameObject[totalCards];

        for (int i = 0; i < totalCards; i++)
        {
            cards[i] = deck.transform.GetChild(i).gameObject;
        }
        System.Random rng = new System.Random();
        int n = cards.Length;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            GameObject value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }
    
}
