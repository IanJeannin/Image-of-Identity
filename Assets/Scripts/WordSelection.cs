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

    private Text listOfWords; //Text element of the chosen words textbox
    private int wordCount=0; //How many words are currently being used

    private void Start()
    {
        listOfWords.text = "Chosen Words: "; //On startup, will lay out the starting text

    }

    private ArrayList wordsUsed; //ArrayList used to store values referencing words. Will be used in the GridSetup Class to use the correct shapes. 

	public void ChangeWords( string word) //Function that will be on all word buttons, when pressed, will add value based on word to the list
    {
        for (int i = 0; i <= wordCount; i++)
        {
            //TODO: Iterate through listOfWords to see if any are being used

            wordsUsed.Add(word);  //Adds value to the list based on the word pressed. 
            wordCount++; //Increases word count
        }
        listOfWords.text = listOfWords.text + word+", "; //Adds words to the text every time the button is clicked
    }


}
