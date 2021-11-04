using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CharacterDialog
{
    public string passive = "";
    public string attacked = "";
    public string dead = "";
}

public class DialogController : MonoBehaviour
{
    public string dialogDirectory = "C:/Users/Vladis/Desktop/thisgamehasnonameyet/Assets/Scripts/DialogSystem/";
    public string dialogFileName;

    public string dialogContent = "";

    public List<CharacterDialog> characterDialogs;

    public int charCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if (!dialogFileName.EndsWith(".txt"))
        {
            dialogFileName += ".txt";
        }

        characterDialogs = new List<CharacterDialog>();

        StreamReader dialogFile = new StreamReader(dialogDirectory + dialogFileName);

        /**
         * '[' <= storing next characters as the name of the player until you reach this character ']'
         */

        dialogContent = dialogFile.ReadToEnd().ToString();

        for (int i = 0; i < dialogContent.Length; i++)
        {
            charCount++;
        }

        //Debug.Log(reader.ReadToEnd());
        dialogFile.Close();
    }

    // Update is called once per frame
    void Update()
    {

    }
}