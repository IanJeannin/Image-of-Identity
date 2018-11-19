﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour
{

    /* [SerializeField]
     private GameObject sampleImage;  //Square image used for testing
     [SerializeField]
     private GameObject blankImage; //Blank Square used for empty spaces
     */

    WordSelection script; //WordSelection script
    private int rowColumnSize = 4; //size of the grid
    private int numberOfGridUnits = 16; //Set Medium Sized Grid with 16 units
    private bool[,] gridArray = new bool[4, 4];  //Create a 2D array for keeping track of grid units
    private int numberOfObjects;
    float size = 1.25f; //Size of the objects
    int numberOfBlankImages = 0;
    private GameObject newBlank; //Used to create new blank images with unique name

    private GameObject currentImage; //Used to find the currently selected image

    private List<string> words; //List of words chosen in previous page


    private void Start()
    {
        script = GetComponent<WordSelection>(); //Sets script equal to the word selection script
        words = WordSelection.GetList(); //Makes the list "words" equal to the list "wordsUsed" from the WordSelection screen/script
        numberOfObjects = words.Count;
        CreateGrid();
        CheckArray();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)==true)
        {
            OnMouseDown();
        }
    }


    private void CreateGrid()
    {
        GameObject newObject; //Create Instance of Game Object
        int gridArrayRow = 0; //For selecting row of gridArray
        int gridArrayColumn = 0; //For selecting the column of the gridArray
        string nameOfObject;
        int objectNumber = 0;
        float xPos = 0f; //xPosition of an object
        float yPos = 1f; //yPosition of an object
        int maxObjects = numberOfObjects;
        int currentWord = 0; //Used to track the current word in the array

        for (int i = 0; i < numberOfGridUnits; i++)
        {

            nameOfObject = "Blank"; //Calls blank image
            newObject = GameObject.Find(nameOfObject); //Instantiates object
            newBlank = Instantiate(newObject);
            newBlank.name = "Blank" + numberOfBlankImages;
            numberOfBlankImages++; //Increase the variable marking down how many blank images there are
            newObject.transform.position = new Vector3(xPos, yPos, 5); //Sets object to proper position

            if ((i + 1) % rowColumnSize != 0 && maxObjects > 0) //If square is not in last column and there are still unused chosen images, fill the grid unit with an image
            {
                // OLD CODE===========================================================================:newObject = (GameObject)Instantiate(sampleImage, transform); //Creates objects to fill grid
                //newObject.GetComponent<Image>().color = Random.ColorHSV(); //Gives Square random color
                
                nameOfObject = words[currentWord]; //Sets the name of the object to the string at the given index of the words array
                newObject = GameObject.Find(nameOfObject);
                //POSSIBLY USE  Instantiate(newObject, transform);
                newObject.transform.position = new Vector2(xPos, yPos); //Sets object to proper position
                maxObjects--; //Decrements numberOfObjects until all chosen images are used
                xPos += size;
                gridArray[gridArrayRow, gridArrayColumn] = true; //Sets gridArray index as true
                gridArrayColumn++;
                objectNumber++; //When image is used, move to next image
                currentWord++;
            }
            else if ((i + 1) % rowColumnSize == 0)//If Square is in last column
            {
                xPos = 0;
                gridArray[gridArrayRow, gridArrayColumn] = false; //Sets gridArray index as false
                gridArrayColumn = 0; //Sets gridArrayColumn back to 0
                gridArrayRow++; //Increases the gridArray row by 1
                yPos -= size;

            }
            else
            {
                gridArray[gridArrayRow, gridArrayColumn] = false; //Sets gridArray index as false
                if ((i + 1) % rowColumnSize != 0) //If square is not in last column
                {
                    xPos += size; //Moves next image to adjacent space
                    gridArrayColumn++; //Increases gridArray column by 1
                }
                else if ((i + 1) % rowColumnSize == 0 && numberOfObjects > 0)//If Square is in last column
                {
                    yPos -= size; //Moves next image to the next row
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
        bool areSpaces = checkForRowSpaces(row); //Used to check if there are spaces between images
        int emergencyStop = 0; //Used in case of infinite loop
        string nameOfObject;
        int maxObjects = numberOfObjects; //Maximum number of shapes allowed

        for (int x = 0; x < rowColumnSize; x++) //Iterate through all grid units in row
        {
            if (gridArray[row, x] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            while (gridArray[row, rowColumnSize - 1] == false) //While the rightmost column has no image
            {
                for (int i = rowColumnSize - 1; i >= 0; i--) //For each unit in the row
                {
                    if (gridArray[row, i] == true)
                    {
                        for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects that were chosen
                        {
                            nameOfObject = words[numberOfObject];
                            if (GameObject.Find(nameOfObject).transform.position == new Vector3(i * size, 1 - (row * size)))
                            {
                                GameObject.Find(nameOfObject).transform.position = new Vector3((i * size) + size, 1 - (row * size)); //Moves the game object that matches the current position being looked at right
                            }
                        }
                    }
                    if (i != 0) //Stops the array from going out of bounds
                    {
                        gridArray[row, i] = gridArray[row, i - 1]; //Move all units in the row right
                    }
                }
               
                gridArray[row, 0] = false; //Make the leftmost column have no image
            }
            while (areSpaces == true && emergencyStop < rowColumnSize) //If there is an empty space between images
            {
                for (int z = rowColumnSize - 1; z >= 0; z--) //Iterate through the row
                {
                    if (gridArray[row, z] != true) //If the unit being looked at is empty
                    {
                        for (int i = z; i >= 0; i--) //For each unit left of the empty unit
                        {
                            if (gridArray[row, i] == true)
                            {
                                for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                                {
                                    nameOfObject = words[numberOfObject];
                                    if (GameObject.Find(nameOfObject).transform.position == new Vector3(i * size, 1 - (row * size)))
                                    {
                                        GameObject.Find(nameOfObject).transform.position = new Vector3((i * size) + size, 1 - (row * size)); //Moves the game object that matches the current position being looked at right
                                    }
                                }
                            }
                            if (i != 0) //Stops the array from going out of bounds
                            {
                                gridArray[row, i] = gridArray[row, i - 1]; //Move all units in the row right
                            }
                        }
                        gridArray[row, 0] = false; //Make the leftmost column have no image
                    }
                }
                emergencyStop++;
                areSpaces = checkForRowSpaces(row);
            }
        }
        CheckArray(); //Check to make sure it works
    }

    private void rowLeft(int row)
    {
        int maxObjects = numberOfObjects; //Maximum number of shapes allowed
        bool allEmpty = true; //If all grid units in row are empty
        bool areSpaces = checkForRowSpaces(row); //Used to check if there are spaces between images
        string nameOfObject;
        for (int x = 0; x < rowColumnSize; x++) //Iterate through all grid units in row
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
                for (int i = 0; i <= rowColumnSize - 1; i++) //For each unit in the row
                {
                    if (gridArray[row, i] == true)
                    {
                        for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                        {
                            nameOfObject = words[numberOfObject];
                            if (GameObject.Find(nameOfObject).transform.position == new Vector3(i * size, 1 - (row * size)))
                            {
                                GameObject.Find(nameOfObject).transform.position = new Vector3((i * size) - size, 1 - (row * size)); //Moves the game object that matches the current position being looked at right
                            }
                        }
                    }
                    
                    if (i != 3) //Stops the array from going out of bounds
                    {
                        gridArray[row, i] = gridArray[row, i + 1]; //Move all units in the row left
                    }
                }
              
                gridArray[row, rowColumnSize - 1] = false; //Make the rightmost column have no image

            }
            while (areSpaces == true) //If there is an empty space between images
            {
                for (int z = 0; z <= rowColumnSize - 1; z++) //Iterate through the row
                {
                    if (gridArray[row, z] != true) //If the unit being looked at is empty
                    {
                        for (int i = z; i <= rowColumnSize - 1; i++) //For each unit in the row starting from unit
                        {
                            if (gridArray[row, i] == true)
                            {
                                for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                                {
                                    nameOfObject = words[numberOfObject];
                                    if (GameObject.Find(nameOfObject).transform.position == new Vector3(i * size, 1 - (row * size)))
                                    {
                                        GameObject.Find(nameOfObject).transform.position = new Vector3((i * size) - size, 1 - (row * size)); //Moves the game object that matches the current position being looked at right
                                    }
                                }
                            }
                            
                            if (i != 3) //Stops the array from going out of bounds
                            {
                                gridArray[row, i] = gridArray[row, i + 1]; //Move all units in the row left
                            }
                        }
                        gridArray[row, rowColumnSize - 1] = false; //Make the rightmost column have no image
                    }

                }
                areSpaces = checkForRowSpaces(row);
            }
            CheckArray(); //Check to make sure it works
        }

    }

    private void columnUp(int column)
    {
        bool allEmpty = true; //If all grid units in column are empty
        bool areSpaces = checkForColumnSpaces(column); //Used to check if there are spaces between images
        int emergencyStop = 0; //Used in case of infinite loop
        string nameOfObject; //Used to reference all objects, in order to find the correct one based on position
        int indexOfSpace=0; //Used to find the index where there is a blank image in between two images
        int maxObjects = numberOfObjects; //Maximum number of shapes allowed

        for (int y = 0; y < rowColumnSize; y++) //Iterate through all grid units in column
        {
            if (gridArray[y, column] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the column has an image...
        {
            while (gridArray[0, column] == false) //While the topmost column has no image
            {
                for (int i = 0; i <= rowColumnSize - 1; i++) //For each unit in the column
                {
                    if (gridArray[i, column] == true) //If unit is an image
                    {

                        for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                        {
                            nameOfObject = words[numberOfObject];
                            if (GameObject.Find(nameOfObject).transform.position == new Vector3(column * size, 1 - (i * size))) //Compares position of each image to unit, once correct image is found...
                            {
                                GameObject.Find(nameOfObject).transform.position = new Vector3(column * size, 1 - ((i - 1) * size)); //Moves the game object that matches the current position being looked at up
                            }
                        }

                    }
                  
                    if (i != 3) //Stops the array from going out of bounds
                    {
                        gridArray[i, column] = gridArray[i + 1, column]; //Move all units in the column up
                    }
                }
                gridArray[rowColumnSize - 1, column] = false;
            }
            while (areSpaces == true) //If there is an empty space between images
            {
                for (int z = 0; z <= rowColumnSize - 1; z++) //Iterate through the column
                {
                    if (gridArray[z, column] != true) //If the unit being looked at is empty
                    {
                            indexOfSpace = z;
                            for (int i = z; i <= rowColumnSize - 1; i++) //For each unit in the column starting from unit
                            {
                                if (gridArray[i, column] == true)
                                {

                                    for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                                    {
                                    nameOfObject = words[numberOfObject];
                                    if (GameObject.Find(nameOfObject).transform.position == new Vector3(column * size, 1 - (i * size))) //Find the blank image that matches the position of the unit
                                        {
                                            GameObject.Find(nameOfObject).transform.position = new Vector3(column * size, 1 - ((i - 1) * size)); //Moves the game object that matches the current position being looked at up
                                        }
                                    }

                                }
                               
                                if (i != 3) //Stops the array from going out of bounds
                                {
                                    gridArray[i, column] = gridArray[i + 1, column]; //Move all units in the column up
                                }
                            }
                            gridArray[rowColumnSize - 1, column] = false; //Make the bottom-most column have no image
                        
                    }

                }
                areSpaces = checkForColumnSpaces(column);
                emergencyStop++;
                
            }
            CheckArray(); //Check to make sure it works


        }
    }

    private void columnDown(int column)
    {
        bool allEmpty = true; //If all grid units in column are empty
        bool areSpaces = checkForColumnSpaces(column); //Used to check if there are spaces between images
        int emergencyStop = 0; //Used in case of infinite loop
        string nameOfObject; //Used to reference all objects, in order to find the correct one based on position
        int indexOfSpace = 0; //Used to find the index where there is a blank image in between two images
        int maxObjects = numberOfObjects; //Maximum number of shapes allowed

        for (int y = 0; y < rowColumnSize; y++) //Iterate through all grid units in column
        {
            if (gridArray[y, column] == true) //If one of the units has an image
            {
                allEmpty = false; //All Empty is false
            }
        }

        if (allEmpty == false) //As long as at least one unit in the row has an image...
        {
            while (gridArray[rowColumnSize - 1, column] == false) //While the bottom-most row has no image
            {
                for (int i = rowColumnSize - 1; i >= 0; i--) //For each unit in the column
                {
                    if (gridArray[i, column] == true) //If unit is an image
                    {

                        for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                        {
                            nameOfObject=words[numberOfObject];
                            if (GameObject.Find(nameOfObject).transform.position == new Vector3(column * size, 1 - (i * size))) //Compares position of each image to unit, once correct image is found...
                            {
                                GameObject.Find(nameOfObject).transform.position = new Vector3(column * size, 1 - ((i + 1) * size)); //Moves the game object that matches the current position being looked at down
                            }
                        }

                    }
                    
                    if (i != 0) //Stops the array from going out of bounds
                    {
                        gridArray[i, column] = gridArray[i -1, column]; //Move all units in the column down
                    }
                   
                }
                 
                gridArray[0, column] = false; //Set Top Column unit to no image
            }
            while (areSpaces == true && emergencyStop < rowColumnSize) //If there is an empty space between images
            {
                for (int z = rowColumnSize - 1; z > 0; z--) //Iterate through the column
                {
                    if (gridArray[z, column] != true) //If the unit being looked at is empty
                    {
                            indexOfSpace = z;
                            for (int i = z; i >= 0; i--) //For each unit in the column starting from unit
                            {
                                if (gridArray[i, column] == true)
                                {

                                    for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
                                    {
                                     nameOfObject = words[numberOfObject];
                                    if (GameObject.Find(nameOfObject).transform.position == new Vector3(column * size, 1 - (i * size)))//Find the blank image that matches the position of the unit
                                        {
                                            GameObject.Find(nameOfObject).transform.position = new Vector3(column * size, 1 - ((i + 1) * size)); //Moves the game object that matches the current position being looked at down
                                        }
                                    }

                                }
                                
                                if (i != 0) //Stops the array from going out of bounds
                                {
                                    gridArray[i, column] = gridArray[i - 1, column]; //Move all units in the column down
                                }
                            }
                            gridArray[0, column] = false; //Make the Top-most column have no image
                    }
                }
                areSpaces = checkForColumnSpaces(column);
                emergencyStop++;
            }
            CheckArray(); //Check to make sure it works
        }
    }

    private bool checkForRowSpaces(int row)
    {
        for (int column = 0; column < rowColumnSize / 2; column++) //Iterate up to half the row index
        {
            if (gridArray[row, column] == true) //When one image is true
            {

                if (gridArray[row, column + 1] == false) //If the next one is true
                {
                    for (int start = column + 1; start < rowColumnSize; start++) //Iterate up to the last row index
                    {
                        if (gridArray[row, start] == true) //If the next one isn't true any after is
                        {
                            return true; //Then there is a gap between true's
                        }
                    }
                }
            }
        }
        return false; //Return false if no gaps were found
    }

    private bool checkForColumnSpaces(int column)
    {
        for (int row = 0; row < rowColumnSize / 2; row++) //Iterate up to half the row index
        {
            if (gridArray[row, column] == true) //When one image is true
            {

                if (gridArray[row + 1, column] == false) //If the next one is true do nothing
                {
                    for (int start = row + 1; start < rowColumnSize; start++) //Iterate up to the last row index
                    {
                        if (gridArray[start, column] == true) //If the next one isn't true but the one after is
                        {
                            return true; //Then there is a gap between true's
                        }
                    }
                }
            }
        }
        return false; //Return false if no gaps were found
    }

    private void OnMouseDown()
    {
        int maxObjects = numberOfObjects; //Maximum number of shapes allowed
        string nameOfObject; //Used to reference all objects, in order to find the correct one based on position
            Vector3 mousePos = new Vector3(Input.mousePosition.x-(9.15f/2), Input.mousePosition.y-(5f/2)); //Sets mousePos to the mouses position based on the center of the camera rather than the bottom left
        mousePos = Camera.main.ScreenToWorldPoint(mousePos); //Puts mouse position based on 'world space' rather than 'screen space'
        Collider2D imageCollider;

        /*for (int numberOfObject = 0; numberOfObject < maxObjects; numberOfObject++) //Iterates through all of the objects
            {

            nameOfObject = words[numberOfObject];
             float x= GameObject.Find(nameOfObject).transform.position.x;
             float z= GameObject.Find(nameOfObject).transform.position.y;
             imageCollider = GameObject.Find(nameOfObject).GetComponent<Collider2D>();

             Vector3 abc = new Vector3(x, z);
             Debug.Log(mousePos);
             Debug.Log(abc);
         //GameObject abcd = GameObject.Find(nameOfObject);
         //abcd.transform.position = new Vector3(abcd.transform.position.x - 0.65f, abcd.transform.position.y - 0.6f);

         //Find the image the mouse is within the bounds of when the button was pressed
         // if (mousePos.x>=x-0.65f && mousePos.x<= x-0.65f&& mousePos.y >= z-0.6f && mousePos.y <= z+0.6f) 
         if (imageCollider.bounds.Contains(Input.mousePosition))
         {
               currentImage = GameObject.Find(nameOfObject); //Sets the current object to the one clicked on 
               GameObject y = Instantiate(GameObject.Find("Polygon"));
               y.transform.position = new Vector3(mousePos.x, mousePos.y);
               Debug.Log("Current Object: " + currentImage);
         }*/

        
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y); //Creates vector2 at mouse position
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f); //Raycasts from camera to mouse position, returning object hit

            if (hit) //If something is hit
            {
                currentImage= hit.transform.gameObject; //Make that image the current image
                Debug.Log("Current Image: "+currentImage);

            }

        //}
        
        
       
    }
}
