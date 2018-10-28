using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour {

    [SerializeField]
    private GameObject sampleImage;  //Square image used for testing
    [SerializeField]
    private GameObject blankImage; //Blank Square used for empty spaces

    private int numberOfGridUnits = 16; //Set Medium Sized Grid with 16 units
    private bool [,] gridArray=new bool [4,4];  //Create a 2D array for keeping track of grid units

    private void Start()
    {
        CreateGrid();
        CheckArray();
    }

    private void CreateGrid()
    {
        GameObject newObject; //Create Instance of Game Object
       int numberOfObjects = 9; //Sets number of Objects player has chosen
        int gridArrayRow = 0; //For selecting row of gridArray
        int gridArrayColumn = 0; //For selecting the column of the gridArray

        for(int i=0; i<numberOfGridUnits;i++)
        {
            if ((i+1)%4 != 0&&numberOfObjects>0) //If square is not in last column and there are still unused chosen images, fill the grid unit with an image
            {
                newObject = (GameObject)Instantiate(sampleImage, transform); //Creates objects to fill grid
                newObject.GetComponent<Image>().color = Random.ColorHSV(); //Gives Square random color
                numberOfObjects--; //Decrements numberOfObjects until all chosen images are used
                gridArray[gridArrayRow, gridArrayColumn] = true; //Sets gridArray index as true
                gridArrayColumn++;
            }
            else //If Square is in last column or all chosen images are used, fill with a blank space
            {
                newObject = (GameObject)Instantiate(blankImage, transform); //Creates blank square
                gridArray[gridArrayRow, gridArrayColumn] = false; //Sets gridArray index as false
                if((i+1)%4==0) //Prevents gridArrayColumn and Row from changing until 'i' reaches last column of grid
                {
                  gridArrayColumn = 0; //Sets gridArrayColumn back to 0
                    gridArrayRow++; //Increases the gridArray row by 1
                }
            }
        }

    }

    private void CheckArray()
    {
        string arrayString = "";

        for (int b = 0; b < 4; b++)
        {
            for (int c = 0; c < 4; c++)
            {
              arrayString += string.Format("{0}", gridArray[b, c]);
            }
            arrayString += System.Environment.NewLine;
        }
        Debug.Log(arrayString);
    }
}
