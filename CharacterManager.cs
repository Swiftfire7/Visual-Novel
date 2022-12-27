using Godot;
using System;
using System.Collections.Generic;

public class CharacterManager : Control
{
    public string Speaker;
    public string Emotion;
    public string Text;
    public string Position;
    public string Mod;
    public override void _Ready()
    {
    }
    public void setDialogueBox(DialogueReader dialogueReader)
    {
        dialogueReader.NextPhrase();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
