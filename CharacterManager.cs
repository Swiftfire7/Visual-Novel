using Godot;
using System;
using System.Collections.Generic;

public class CharacterManager : Control
{
    private List<DialogueBox> dialogueBox;
    private string characterName;
    public override void _Ready()
    {
        characterName = "Sasheen";

    }
    public void setDialogueBox()
    {
        InterfaceManager.dialogueManager.dialogueBox = dialogueBox;
        InterfaceManager.dialogueManager.DialogueHeader = characterName;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
