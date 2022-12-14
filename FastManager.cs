using Godot;
using System;

public class FastManager : ColorRect
{
    DialogueReader dialogueReader;
    public bool FastPressed = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
    }
    public void OnFastButton()
    {
        FastPressed = !FastPressed;
        if (dialogueReader.finished == true)
        {
            dialogueReader.NextPhrase();
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
