using Godot;
using System;

public class SceneManager : Control
{
    public bool SceneStarted = false;
    public bool MenuIsUp = true;
    private bool enabled = false;
    FastManager fastManager;
    AnimationManager animationManager;
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
        fastManager = GetNode<FastManager>("DialogueManager/Popup/FastForward");
        dialogueReader = GetNode<DialogueReader>("DialogueManager/Popup");
        animationManager = GetNode<AnimationManager>("Background");
        button = GetNode<Button>("Background/MenuManager/instructions");
    }
    public override void _Process(float delta)
    {
        dialogueReader.Indicator.Visible = dialogueReader.finished;
        if (Input.IsActionJustPressed("ui_right") && SceneStarted == true)
        {
            if (dialogueReader.finished)
            {
                //allow continuation once printing ends
                dialogueReader.NextPhrase();
            }
            else
            {
                //print full dialogue line
                dialogueReader.DialogueBox.VisibleCharacters = dialogueReader.DialogueBox.Text.Length;
            }
        }
        else if (Input.IsActionJustPressed("ui_accept") && SceneStarted == false && MenuIsUp == false)
        {
            //find the scene's character dialogue and UI, then display the dialogue
            Node obj = GetNode<Node>("CharacterSpawner");
            showDialogueBox(obj);
            button.Hide();
            SceneStarted = true;
        }
        else if (dialogueReader.dialogueEnded)
        {
            SceneStarted = false;
            fastManager.FastPressed = false;
            button.Show();
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
    public void OnNextButton()
    {
        if (dialogueReader.finished)
        {
            //allow continuation once printing ends
            dialogueReader.NextPhrase();
        }
        else
        {
            //print full dialogue line
            dialogueReader.DialogueBox.VisibleCharacters = dialogueReader.DialogueBox.Text.Length;
        }
        if (dialogueReader.dialogueEnded)
        {
            SceneStarted = false;
            button.Show();
        }
    }
    public void ShowDialogueElement()
    {
        dialogueReader.Popup_();
    }
}
