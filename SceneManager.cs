using Godot;
using System;

public class SceneManager : Control
{
    public bool SceneStarted = false;
    public bool MenuIsUp = true;
    DialogueReader dialogueReader;
    Button button;
    public static SceneManager GlobalSceneManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        if (GlobalSceneManager == null)
        {
            GlobalSceneManager = this;
        }
        else
        {
            QueueFree();
        }
        dialogueReader = GetNode<DialogueReader>("DialogueManager/Popup");
        button = GetNode<Button>("Background/MenuManager/instructions");
    }
    public override void _Process(float delta)
    {
        dialogueReader.Indicator.Visible = dialogueReader.finished;

        if (Input.IsActionJustPressed("ui_right") && SceneStarted == true)
        {
            if (dialogueReader.finished)
            {
                dialogueReader.NextPhrase();
            }
            else
            {
                dialogueReader.DialogueBox.VisibleCharacters = dialogueReader.DialogueBox.Text.Length;
            }
            if (dialogueReader.dialogueEnded)
            {
                SceneStarted = false;
            }
        }
        if (Input.IsActionJustPressed("ui_accept") && SceneStarted == false && MenuIsUp == false)
        {
            //find the scene's character dialogue and UI, then display the dialogue
            Node obj = GetNode<Node>("CharacterSpawner");
            showDialogueBox(obj);
            button.Hide();
            SceneStarted = true;
        }
    }
    private void showDialogueBox(Node obj)
    {
        //if we found a valid character to begin the dialogue, pass it off to the Dialogue Manager to render the dialogue.
        if (obj is CharacterManager)
        {
            CharacterManager dialogueBox = obj as CharacterManager;
            dialogueBox.setDialogueBox(dialogueReader);
            ShowDialogueElement();
        }
    }
    public void OnStartButtonPressed()
    {
        TextureRect background = GetNode<TextureRect>("Background");
    }
    public void ShowDialogueElement()
    {
        dialogueReader.Popup_();
    }
}
