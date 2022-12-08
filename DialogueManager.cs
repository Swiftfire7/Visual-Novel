using Godot;
using System;

public class DialogueManager : Control
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        ShowDialogueElement();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
    public void ShowDialogueElement()
    {
        GetNode<Popup>("Popup").Popup_();
        GetNode<RichTextLabel>("Popup/RichTextLabel").Text = "example text";
        GetNode<Label>("Popup/Label").Text = "test name";
    }
}
