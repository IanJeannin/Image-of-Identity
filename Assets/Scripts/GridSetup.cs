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

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        GameObject newObject; //Create Instance of Game Object
       int numberOfObjects = 9; //Sets number of Objects player has chosen

        for(int i=0; i<numberOfGridUnits;i++)
        {
            if ((i+1)%4 != 0&&numberOfObjects>0) //If square is not in last column and there are still unused chosen images, fill the grid unit with an image
            {
                newObject = (GameObject)Instantiate(sampleImage, transform); //Creates objects to fill grid
                newObject.GetComponent<Image>().color = Random.ColorHSV(); //Gives Square random color
                numberOfObjects--;
            }
            else //If Square is in last column or all chosen images are used, fill with a blank space
            {
                newObject = (GameObject)Instantiate(blankImage, transform); //Creates blank square
            }
        }

    }
}
