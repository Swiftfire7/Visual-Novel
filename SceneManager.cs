using Godot;
using System;

public class SceneManager : Node2D
{
    public bool SceneStarted = false;
    DialogueReader dialogueReader;
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
        dialogueReader = GetNode<DialogueReader>("InterfaceManager/DialogueManager/Popup");
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
        }
        if (Input.IsActionJustPressed("ui_accept") && SceneStarted == false)
        {
            //find the scene's character dialogue and UI, then display the dialogue
            Node obj = GetNode<Node>("CharacterManager");
            showDialogueBox(obj);
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
    public void ShowDialogueElement()
    {
        GetNode<Popup>("/root/SceneManager/InterfaceManager/DialogueManager/Popup").Popup_();
        //GetNode<Label>("Popup/Label").Text = DialogueHeader;
        //FinishedPrinting = false;
    }
}
