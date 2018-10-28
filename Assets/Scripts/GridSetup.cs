using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridSetup : MonoBehaviour {

    [SerializeField]
    private GameObject sampleImage;  //Square image used for testing
    private int numberOfGridUnits = 16; //Set Medium Sized Grid with 16 units

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        GameObject newObject; //Create Instance of Game Object

        for(int i=0; i<numberOfGridUnits;i++)
        {
            newObject = (GameObject)Instantiate(sampleImage, transform); //Creates objects to fill grid
            newObject.GetComponent<Image>().color = Random.ColorHSV();
        }

    }
}
