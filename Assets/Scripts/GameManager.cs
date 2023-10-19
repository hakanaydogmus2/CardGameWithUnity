using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;
using System;
using Random = UnityEngine.Random;
using System.Drawing;
using System.Linq;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] cards;

    public GameObject deck;
    public GameObject playerCard;
    public List<GameObject> playerHand;
    [SerializeField] private List<GameObject> opponentHand;


    public int? playerCardValue;
    [SerializeField] private int playerScore;
    public GameObject opponentCard;
    public int opponentCardValue;
    [SerializeField] private int opponentScore;

    public TextMeshProUGUI playerText;
    public TextMeshProUGUI opponentText;
    public TextMeshProUGUI ManyCardsText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;

    int totalCards;
    private static int NextTurnCounter = 0;
    CardAttributes cardAttributes;
    CardController cardController;
    public bool isPlayerFirst = true;
    private int dif;
    public bool isAiPlayed;
    private static float z = 5.0f;
    [SerializeField] private List<GameObject> pile;
    void Start()
    {

        CreateHands();

    }

    void Awake()
    {
        createCards();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public GameObject CardSelector(bool isPlayerFirst)
    {
        Debug.Log("card selecter called");


        if (isPlayerFirst == false)
        {
            opponentCardValue = 0;
            foreach (GameObject card in opponentHand)
            {
                if (card.GetComponent<CardAttributes>().value > opponentCardValue)
                {

                    opponentCardValue = card.GetComponent<CardAttributes>().value;
                    opponentCard = card;

                }
            }
            StartCoroutine(OpponentCardWaiter());

        }
        else if (playerCardValue != null)
        {
            List<int> validCardValues = new List<int>();
            foreach (GameObject card in opponentHand)
            {
                int cardValue = card.GetComponent<CardAttributes>().value;
                if (cardValue > playerCardValue)
                {
                    validCardValues.Add(cardValue);
                }

            }
            if (validCardValues.Count > 0)
            {
                opponentCardValue = validCardValues.Min();
                opponentCard = opponentHand.First(card => card.GetComponent<CardAttributes>().value == opponentCardValue);
            }
            else
            {
                if (opponentHand.Max(card => card.GetComponent<CardAttributes>().value) == playerCardValue)
                {
                    opponentCardValue = opponentHand.Max(card => card.GetComponent<CardAttributes>().value);
                    opponentCard = opponentHand.First(card => card.GetComponent<CardAttributes>().value == opponentCardValue);
                }
                else
                {
                    opponentCardValue = opponentHand.Min(card => card.GetComponent<CardAttributes>().value);
                    opponentCard = opponentHand.First(card => card.GetComponent<CardAttributes>().value == opponentCardValue);
                }

            }
        }

        if (opponentHand.Count != 0)
        {
            int index = opponentHand.IndexOf(opponentCard);

            opponentCard = opponentHand[index];

            opponentHand.RemoveAt(index);
        }

        return opponentCard;
             


    }


    public void scoreCalculator()
    {
        if (playerCardValue > opponentCardValue)
        {
            playerScore++;
            playerText.text = "Point: " + playerScore;
        }
        else if (playerCardValue < opponentCardValue)
        {
            opponentScore++;
            opponentText.text = "Point: " + opponentScore;
        }
        else
        {
            playerScore++;
            opponentScore++;
            opponentText.text = "Point: " + opponentScore;
            playerText.text = "Point: " + playerScore;
        }


    }

    private void CreateHands()
    {
        int playerZ = 5;
        float playerX = 10;

        for (int i = 0; i < 4; i++)
        {

            playerHand.Add(cards[0]);
            playerHand[i].transform.position = new Vector3(playerX, -7.5f, 0.5f);
            playerHand[i].transform.localScale = new Vector3(50, 50, playerZ);
            playerHand[i].transform.localRotation = new Quaternion(0, 0, 0, 0);
            RemoveElement(ref cards, 0);

            playerHand[i] = Instantiate(playerHand[i]);
            playerX -= 2.5f;
            playerZ++;

            if (cards.Length == 2)
            {
                break;
            }

        }

        int opponentZ = 5;
        float opponentX = 5;
        for (int i = 0; i < 4; i++)
        {
            opponentHand.Add(cards[0]);
            opponentHand[i].transform.position = new Vector3(opponentX, 7.5f, 0.5f);
            opponentHand[i].transform.localScale = new Vector3(50, 50, opponentZ);
            opponentHand[i].transform.localRotation = new Quaternion(0, 1, 0, 0);
            RemoveElement(ref cards, 0);
            opponentHand[i] = Instantiate(opponentHand[i]);
            opponentX -= 2.5f;
            opponentZ++;
            if (cards.Length == 0)
            {
                break;
            }


        }
        Debug.Log("hands created");
        ManyCardsText.text = "Cards: " + cards.Length;

    }
    public static void RemoveElement<T>(ref T[] arr, int index)
    {
        for (int a = index; a < arr.Length - 1; a++)
        {

            arr[a] = arr[a + 1];
        }

        Array.Resize(ref arr, arr.Length - 1);
    }



    void createCards()
    {
        totalCards = deck.transform.childCount;
        cards = new GameObject[totalCards];

        for (int i = 0; i < totalCards; i++)
        {
            cards[i] = deck.transform.GetChild(i).gameObject;
        }
        cards = Shuffle(cards);

    }
    private T[] Shuffle<T>(T[] array)
    {
        System.Random rng = new System.Random();
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = array[k];
            array[k] = array[n];
            array[n] = value;
        }
        return array;
    }
    public void NextTurn()
    {
        playerCardValue = null;

        if (opponentHand.Count == 0)
        {

            DeactiveCards();
            CreateHands();
            isPlayerFirst = !isPlayerFirst;
            if (isPlayerFirst == false)
            {
                CardSelector(isPlayerFirst);
            }

            NextTurnCounter++;
            Debug.Log("next turn counter: " + NextTurnCounter);
        }
        else
        {
            Debug.Log("it cannot");
        }
    }

    private void DeactiveCards()
    {
        CardAttributes[] myItems = FindObjectsOfType<CardAttributes>();
        //Debug.Log("Found " + myItems.Length + " instances with this script attached");
        foreach (CardAttributes item in myItems)
        {
            item.gameObject.SetActive(false);
            pile.Add(item.gameObject);
        }
        //Debug.Log(pile);

    }

    public IEnumerator GameEnder()
    {
        if (playerHand.Count == 0 && opponentHand.Count == 0 && cards.Length == 0)
        {
            Debug.Log("GameOver");
            yield return new WaitForSeconds(0.3f);
            gameOverText.gameObject.SetActive(true);
            restartButton.gameObject.SetActive(true);
            
        }
    }
    private IEnumerator OpponentCardWaiter()
    {
        yield return new WaitForSeconds(0.5f);
        opponentCard.transform.position = new Vector3(6.5f, -0.1f, 0.5f);
        opponentCard.transform.localRotation = new Quaternion(0, 0, 0, 0);
        opponentCard.transform.localScale = new Vector3(50f, 50f, z);
        isAiPlayed = true;
        z++;
        playerCard = null;
        yield return new WaitForSeconds(0.5f);
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }



}
