﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour {

    [SerializeField]
    private GameObject sampleImage;  //Square image used for testing
    [SerializeField]
    private GameObject blankImage; //Blank Square used for empty spaces

    private int rowColumnSize = 4;
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
            if ((i+1)%rowColumnSize != 0&&numberOfObjects>0) //If square is not in last column and there are still unused chosen images, fill the grid unit with an image
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
                if((i+1)%rowColumnSize==0) //Prevents gridArrayColumn and Row from changing until 'i' reaches last column of grid
                {
                  gridArrayColumn = 0; //Sets gridArrayColumn back to 0
                    gridArrayRow++; //Increases the gridArray row by 1
                }
            }
        }

    }

    private void CheckArray() //Method to check array is functioning properly using Debug.Log
    {
        string arrayString = "";

        for (int b = 0; b < rowColumnSize; b++)
        {
            for (int c = 0; c < rowColumnSize; c++)
            {
              arrayString += string.Format("{0}", gridArray[b, c]); //Add each unit in a single row
            }
            arrayString += System.Environment.NewLine; //Start a new line for a new row
        }
        Debug.Log(arrayString);
    }


    //=====================================================================================================
    //Next Methods call for a change in row/column based on which button was clicked
    //======================================================================================================

    public void row0Left()
    {
        rowLeft(0);
    }

    public void row1Left()
    {
        rowLeft(1);
    }

    public void row2Left()
    {
        rowLeft(2);
    }

    public void row3Left()
    {
        rowLeft(3);
    }

    public void row0Right()
    {
        rowRight(0);
    }

    public void row1Right()
    {
        rowRight(1);
    }

    public void row2Right()
    {
        rowRight(2);
    }

    public void row3Right()
    {
        rowRight(3);
    }

    public void column0Up()
    {
        columnUp(0);
    }

    public void column1Up()
    {
        columnUp(1);
    }

    public void column2Up()
    {
        columnUp(2);
    }

    public void column3Up()
    {
        columnUp(3);
    }

    public void column0Down()
    {
        columnDown(0);
    }

    public void column1Down()
    {
        columnDown(1);
    }

    public void column2Down()
    {
        columnDown(2);
    }

    public void column3Down()
    {
        columnDown(3);
    }

    //==================================================================================
    // Functions that move rows/columns left/right/up/down
    //==================================================================================

    private void rowRight(int row) //Moves all units in a row right, until there are no blank spaces on the rightmost column
    {
        bool allEmpty = true; //If all grid units in row are empty
        for(int x=0;x<rowColumnSize-1;x++) //Iterate through all grid units in row
        {
            if(gridArray[row,x]==true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            while (gridArray[row, rowColumnSize - 1] == false) //While the rightmost column has no image
            {
                for (int i = rowColumnSize - 1; i > 0; i--) //For each unit in the row
                {
                    gridArray[row, i] = gridArray[row, i - 1]; //Move all units in the row right
                }
                gridArray[row, 0] = false; //Make the leftmost column have no image
            }
            CheckArray(); //Check to make sure it works
        }

    }

    private void rowLeft(int row)
    {
        bool allEmpty = true; //If all grid units in row are empty
        for (int x = 0; x < rowColumnSize - 1; x++) //Iterate through all grid units in row
        {
            if (gridArray[row, x] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            while (gridArray[row, 0] == false) //While the leftmost column has no image
            {
                for (int i = 0; i < rowColumnSize - 1; i++) //For each unit in the row
                {
                    gridArray[row, i] = gridArray[row, i + 1]; //Move all units in the row left
                }
                gridArray[row, rowColumnSize - 1] = false; //Make the rightmost column have no image
            }
            CheckArray(); //Check to make sure it works
        }

    }

    private void columnUp(int column)
    {
        bool allEmpty = true; //If all grid units in column are empty
        for (int y = 0; y < rowColumnSize - 1; y++) //Iterate through all grid units in column
        {
            if (gridArray[y, column] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            while (gridArray[0, column] == false) //While the topmost row has no image
            {
                for (int i = 0; i < rowColumnSize - 1; i++) //For each unit in the column
                {
                    gridArray[i, column] = gridArray[i+1, column]; //Move all units in the column up
                }
                gridArray[rowColumnSize-1, column] = false; //Set Bottom Column unit to no image
            }
            CheckArray(); //Check to make sure it works
        }
    }

    private void columnDown(int column)
    {

    }
}
