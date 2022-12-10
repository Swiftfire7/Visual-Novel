using Godot;
using System;
using System.Collections.Generic;

public class CharacterManager : Control
{
    private List<DialogueBox> dialogueBox;
    private string characterName;
    public override void _Ready()
    {
        //UI buttons
        InterfaceSelectionObject obj = new InterfaceSelectionObject(1, "hmmm who is this?");
        InterfaceSelectionObject obj2 = new InterfaceSelectionObject(2, "seems to be in charge...");
        InterfaceSelectionObject obj3 = new InterfaceSelectionObject(-1, "she looks busy, I'll just sit down");
        dialogueBox = new List<DialogueBox>
        //character response to button press
        {
            new DialogueBox(new List<InterfaceSelectionObject>(){obj, obj2}, "test dialogue! UwU", 0),
            new DialogueBox(new List<InterfaceSelectionObject>(){obj3}, "hey what's up?", 1),
            new DialogueBox(new List<InterfaceSelectionObject>(){obj3}, "cool! ^.^", 2),
        };
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
