using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class TyperRight : MonoBehaviour
{

    public Text wordOutput = null;
    public int trigger = 0;
    public GameObject ForwardTyper;
    public GameObject LeftTyper;
    public string currentWord = "right";

    private string remainingWord = string.Empty;
    private string[] lines = File.ReadAllLines("Assets/Scripts/Dictionary.txt");

    // Start is called before the first frame update
    void Start()
    {
        var random = new System.Random();
        var randomIndex = random.Next(0, lines.Length);
        currentWord = lines[randomIndex];

        while(currentWord == ForwardTyper.GetComponent<TyperForward>().currentWord || currentWord == LeftTyper.GetComponent<TyperLeft>().currentWord)
        {
            randomIndex = random.Next(0, lines.Length);
            currentWord = lines[randomIndex];
        }

        SetCurrentWord();
    }

    public void SetCurrentWord()
    {
        SetRemainingWord(currentWord);
    }

    private void SetRemainingWord(string newString)
    {
        remainingWord = newString;
        if(remainingWord.Length != 0){
            string display = currentWord.Replace(remainingWord,"");
            wordOutput.text = "<color=green>"+ display +"</color>" + remainingWord;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.anyKeyDown)
        {
            string keysPressed = Input.inputString;

            if(keysPressed.Length == 1)
            {
                EnterLetter(keysPressed);
            }
        }
    }

    private void EnterLetter(string typedLetter)
    {
        if(IsCorrectLetter(typedLetter))
        {
            RemoveLetter();

            if (isWordComplete())
            {
                if (ForwardTyper.GetComponent<TyperForward>().trigger == 1 || LeftTyper.GetComponent<TyperLeft>().trigger == 1)
                {
                    ForwardTyper.GetComponent<TyperForward>().trigger = 0;
                    LeftTyper.GetComponent<TyperLeft>().trigger = 0;
                }
                trigger = 1;
                LeftTyper.GetComponent<TyperLeft>().SetCurrentWord();
                ForwardTyper.GetComponent<TyperForward>().SetCurrentWord();

                var random = new System.Random();
                var randomIndex = random.Next(0, lines.Length);
                currentWord = lines[randomIndex];

                while(currentWord == ForwardTyper.GetComponent<TyperForward>().currentWord || currentWord == LeftTyper.GetComponent<TyperLeft>().currentWord)
                {
                    randomIndex = random.Next(0, lines.Length);
                    currentWord = lines[randomIndex];
                }

                SetCurrentWord();
            }
        }
    }

    private bool IsCorrectLetter(string letter)
    {
        return remainingWord.IndexOf(letter) == 0;
    }

    private void RemoveLetter()
    {
        string newString = remainingWord.Remove(0, 1);
        SetRemainingWord(newString);
    }

    private bool isWordComplete() 
    {
        return remainingWord.Length == 0;
    }
}
