using Godot;
using System;

public class SceneManager : Node2D
{
    public bool Paused = false;
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
    }
    public override void _Process(float delta)
    {


        if (Input.IsActionJustPressed("ui_accept") && Paused == false)
        {
            //find the scene's character dialogue and UI
            Node obj = GetNode<Node>("CharacterManager");
            showDialogueBox(obj);
        }
    }
    private void showDialogueBox(Node obj)
    {
        //if we found a valid character to begin the dialogue, pass it off to the Dialogue Manager to render the dialogue.
        if (obj is CharacterManager)
        {
            CharacterManager dialogueBox = obj as CharacterManager;
            dialogueBox.setDialogueBox();
            InterfaceManager.dialogueManager.ShowDialogueElement();
        }
    }
}
