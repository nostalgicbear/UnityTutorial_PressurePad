using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// This handles assigning colours to pads and boxes, and assigning ids to both boxes and pads.
/// </summary>
public class ColorPadManager : MonoBehaviour
{
    public static ColorPadManager instance;
    [SerializeField]
    int totalCorrectPlacementsNeed; //This is the total number of boxes that needs to be placed correctly before the door will open.
    [SerializeField]
    int currentCorrectPlacements; //Current number of boxes that are placed on the correct pad
    [SerializeField]
    int placements = 0; //This is overall attempted placements. This increments everytime a box is placed on a pad

    public List<GameObject> pads;
    public List<GameObject> boxes;
    public List<Color> possibleColors;


    public Text canvasText;

    public UnityEvent completeEvent; //The event you want to call when all boxes are placed. This can be anything. 

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this) {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        totalCorrectPlacementsNeed = pads.Count; //Set the total number of correct placements needed to be the number of pads in the pads list. 
        currentCorrectPlacements = 0; //Start off with 0 correct placements

        RandomizeColourList(); //Randomize the colors 
        AssignColoursToListObjects(pads); //Assign the colors to the pads
        RandomizeColourList(); //Randomize the colors again
        AssignColoursToListObjects(boxes); //Assign them to the boxes
        ShuffleBoxOrder(); //Shuffle the box order so the same box does not always go on the same box
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Shuffle the colours in the possibleColors list
    /// </summary>
    void RandomizeColourList()
    {
        List<Color> tempList = new List<Color>();

        for (int i = 0; i < possibleColors.Count; i++)
        {
            tempList.Add(possibleColors[i]);
        }


        for (int i = 0; i < possibleColors.Count; i++)
        {
            Color tempColor = possibleColors[i];
            int randomIndex = UnityEngine.Random.Range(i, possibleColors.Count);
            possibleColors[i] = possibleColors[randomIndex];
            possibleColors[randomIndex] = tempColor;
        }
    }

    /// <summary>
    /// Goes through the list of objects passed to the function and applies a color from the possibleColors list to it.
    /// </summary>
    /// <param name="objects"></param>
    void AssignColoursToListObjects(List<GameObject> objects)
    {
        for(int i = 0; i < objects.Count; i++)
        {
            objects[i].GetComponent<Renderer>().material.color = possibleColors[i];
        }
    }

    /// <summary>
    /// Increase the number of attempted placements
    /// </summary>
    public void IncreasePlacement()
    {
        placements++;

        if(placements == totalCorrectPlacementsNeed) //Update the UI board
        {
            canvasText.text = currentCorrectPlacements.ToString();
        }
    }

    /// <summary>
    /// Decrease the number of attempted placements
    /// </summary>
    public void DecreasePlacement()
    {
        placements--;
    }

    /// <summary>
    /// Increase the number of CORRECT placements
    /// </summary>
    public void IncreaseCorrectPlacements()
    {
        currentCorrectPlacements++;

        if(currentCorrectPlacements == totalCorrectPlacementsNeed)
        {
            Debug.Log("ALL BOXES PLACED CORRECTLY");
            completeEvent.Invoke();
        }
    }

    /// <summary>
    /// Decrease the number of incorrect placements
    /// </summary>
    public void DecreaseCorrectPlacements()
    {
        currentCorrectPlacements--;
    }

    /// <summary>
    /// Shuffles the order of the boxes, and applies an ID to each box
    /// </summary>
    void ShuffleBoxOrder()
    {
        int number = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            
            GameObject temp = boxes[i];
            int randomIndex = UnityEngine.Random.Range(i, boxes.Count);
            boxes[i] = boxes[randomIndex];
            boxes[randomIndex] = temp;

            boxes[i].GetComponent<Pickupable>().boxId = number;
            pads[i].GetComponent<Pad>().padId = number;
            number++;
            
        }
    }

}
