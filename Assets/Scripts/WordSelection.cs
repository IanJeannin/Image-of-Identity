using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WordSelection : MonoBehaviour {

    //================================================
    // WORDS:

    // Accepting
    // Adaptable
    // Bold
    // Calm
    // Cheerful
    // Clever

    // Confident
    // Dependable
    // Dignified
    // Empathetic
    // Extroverted
    // Idealistic

    // Independent
    // Intelligent
    // Introverted
    // Kind
    // Logical
    // Mature

    // Modest
    // Nervous
    // Organized
    // Quiet
    // Religious
    // Self-Conscious
  
    public Text listOfWords; //Text element of the chosen words textbox
    [SerializeField]
    private Button startButton;

    private static int wordCount=0; //How many words are currently being used
    private int numberOfWords = 24; //How many words are currently in the game
    private bool isUsed = false; //Checks if word clicked on is already in the list.
    private static List<string> wordsUsed=new List<string>(); //ArrayList used to store values referencing words. Will be used in the GridSetup Class to use the correct shapes. 

    private void Start()
    {
        listOfWords.text = "Chosen Words: "; //On startup, will lay out the starting text

    }

    private void Update()
    {
        if(wordCount>=3) //If at least 3 words are being used
        {
            startButton.interactable = true; //Allow use of start button
        }
        else //If there are less than 3 words being used
        {
            startButton.interactable = false; //Do not allow use of start button
        }
    }

  

	public void ChangeWords(string word) //Function that will be on all word buttons, when pressed, will add word to the array
    {
        if (wordCount > 0&&wordCount<9) //If the list has at least one word in it, but not more than nine
        {
            for (int i = 0; i < wordCount; i++) //Iterate through the list
            {
               if(wordsUsed[i]==word) //Checks if list already has word in it
                {
                    isUsed = true; 
                }
            }
            if(isUsed==false) //If the list does not already have the word in it
            {
                wordsUsed.Add(word); //Add the word
                wordCount++; //Increases word count
            }
            else //If the list does have the word in it
            {
                wordsUsed.Remove(word); //Remove the word from the list
                wordCount--; //Decrease word count
                isUsed = false;
                
            }
        }
        else if(wordCount==0)//If list has no words in it
        {
            wordsUsed.Add(word);  //Add word
            wordCount++; //Increase word count
        }
        else //If list is at max capacity (nine)
        {
            for (int i = 0; i < wordCount; i++) //Iterate through the list
            {
                if (wordsUsed[i] == word) //Checks if list already has word in it
                {
                    isUsed = true;
                }
            }
            if(isUsed==true) //If word already used
            {
                wordsUsed.Remove(word); //Remove word
                wordCount--; //Decrease wordCount
                isUsed = false; //To stop isUsed from staying true next time button is clicked
            }
        }
        listOfWords.text = "Chosen Words: "; //Starts text
        for (int c = 0; c < wordCount; c++) //Iterates through wordsUsed
        {
            listOfWords.text = listOfWords.text + wordsUsed[c]+", "; //Put all words currently being used in text box
        }//Adds words to the text every time the button is clicked
    }


    public static int GetCount() //Used to access wordCount
    {
        return wordCount;
    }

    public static List<string> GetList() //Used to access list of words
    {
        return wordsUsed;
    }

}
