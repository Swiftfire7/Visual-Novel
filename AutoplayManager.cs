using Godot;
using System;

public class AutoplayManager : Button
{
    private DialogueReader dialogueReader;
    public bool Auto = false;
    public float AutoSpeed = 0f;
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
    }
    public void OnAutoButton()
    {
        Auto = !Auto;
    }
    public void SetAutoSpeed()
    {
        AutoSpeed = dialogueReader.DialogueBox.Text.Length * dialogueReader.textSpeed;
        AutoSpeed = AutoSpeed + 2f;
        if (AutoSpeed < 1.2f)
        {
            AutoSpeed = 1.2f;
        }
    }
    public void UpdateAutoSpeed()
    {
        if (Auto)
        {
            AutoSpeed = AutoSpeed - dialogueReader.textSpeed;
            if (AutoSpeed < 0)
            {
                AutoSpeed = 0;
            }
        }
        else
        {
            AutoSpeed = 0;
        }
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
