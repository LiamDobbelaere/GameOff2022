using PixelCrushers.DialogueSystem;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Operation {
    ADD,
    SUBTRACT,
    MULTIPLY,
    DIVIDE
}

public class Calculator : MonoBehaviour {
    private TMP_InputField input;
    private Dictionary<string, Action> buttons;

    // Calculator related stuff
    private string firstEntry = "";
    private string secondEntry = "";
    private Operation? operation = null;
    private float? result = null;

    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start() {
        audioSource = GetComponent<AudioSource>();

        input = transform.Find("Input").GetComponent<TMP_InputField>();

        buttons = new Dictionary<string, Action>() {
            ["Reset"] = () => OnReset(),
            ["Clear"] = OnClear,
            ["="] = OnEquals,
            ["Multiply"] = () => OnOperation(Operation.MULTIPLY),
            ["Divide"] = () => OnOperation(Operation.DIVIDE),
            ["Add"] = () => OnOperation(Operation.ADD),
            ["Subtract"] = () => OnOperation(Operation.SUBTRACT)
        };

        string characters = "0123456789.";
        foreach (char character in characters) {
            buttons[character.ToString()] = () => OnCharacter(character);
        }

        Transform buttonsTransform = transform.Find("Buttons");
        for (int i = 0; i < buttonsTransform.childCount; i++) {
            Transform button = buttonsTransform.GetChild(i);

            if (buttons.ContainsKey(button.name)) {
                button.GetComponent<Button>().onClick.AddListener(() => buttons[button.name]());
            }
        }

        OnReset(false);
    }

    // Update is called once per frame
    void Update() {

    }
    private void PlayClick() {
        audioSource.PlayOneShot(audioSource.clip);
    }
    private void OnReset(bool playSound = true) {
        if (playSound) {
            PlayClick();
        }

        firstEntry = "";
        secondEntry = "";
        operation = null;
        result = null;

        input.text = "";
    }

    private void OnClear() {
        PlayClick();

        if (operation == null) {
            firstEntry = "";
        } else {
            secondEntry = "";
        }

        input.text = "";
    }

    private void OnOperation(Operation op) {
        PlayClick();

        if (op == Operation.SUBTRACT) {
            if (operation == null) {
                if (firstEntry == "") {
                    firstEntry = "-";
                    input.text = firstEntry;
                    return;
                }
            } else {
                if (secondEntry == "") {
                    secondEntry = "-";
                    input.text = secondEntry;
                    return;
                }
            }
        }

        operation = op;

        switch (operation) {
            case Operation.ADD:
                input.text += "+";
                break;
            case Operation.SUBTRACT:
                input.text += "-";
                break;
            case Operation.MULTIPLY:
                input.text += "*";
                break;
            case Operation.DIVIDE:
                input.text += "/";
                break;
        }


    }

    private void OnCharacter(char character) {
        PlayClick();

        if (character == '.') {
            if (operation == null) {
                if (firstEntry.Contains(".")) {
                    return;
                }
            } else {
                if (secondEntry.Contains(".")) {
                    return;
                }
            }
        }

        if (operation == null) {
            firstEntry += character;
            input.text = firstEntry;
        } else {
            secondEntry += character;
            input.text = secondEntry;
        }
    }

    private void OnEquals() {
        PlayClick();

        if (operation == null) {
            return;
        }

        float lhs;
        if (result != null) {
            lhs = (float)result;
        } else {
            lhs = float.Parse(firstEntry);
        }

        switch (operation) {
            case Operation.ADD:
                result = lhs + float.Parse(secondEntry);
                break;
            case Operation.SUBTRACT:
                result = lhs - float.Parse(secondEntry);
                break;
            case Operation.MULTIPLY:
                result = lhs * float.Parse(secondEntry);
                break;
            case Operation.DIVIDE:
                result = lhs / float.Parse(secondEntry);
                break;
        }

        input.text = ((float)result).ToString("#.######");

        // Quartermaster calculation for dialogue system
        if (result == 245 * 1030) {
            DialogueLua.SetVariable("Quartermaster Calculation Done", true);
        }

        // Cheat: show all pictures
        if (result == -159632) {
            GameState.hasTakenPictureOf["flag"] = true;
            GameState.hasTakenPictureOf["hearthorn"] = true;
            GameState.hasTakenPictureOf["seagulls"] = true;

            input.text = "ALLPICS";
        }
    }
}
