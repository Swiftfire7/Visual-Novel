using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class DialogueManager : Control
{
    public List<DialogueBox> dialogueBox;
    [Export]
    private bool dialogueIsUp = false;

    public bool FinishedPrinting = false;

    private int currentSelectionIndex = 0;
    public string DialogueHeader;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        FinishedPrinting = true;
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if (SceneManager.GlobalSceneManager.Paused && dialogueIsUp)
        {
            //Next button's logic
            if (Input.IsActionJustPressed("ui_accept") && FinishedPrinting == true)
            {
                //continues the dialogue chain after pressing next


            }
        }
    }
    public void ShowDialogueElement()
    {
        GetNode<Popup>("Popup").Popup_();
        //GetNode<Label>("Popup/Label").Text = DialogueHeader;
        FinishedPrinting = false;
    }
    private void shutDownDialogue()
    {
        GetNode<Popup>("Popup").Hide();
        SceneManager.GlobalSceneManager.Paused = false;
        dialogueIsUp = false;
    }
    private void displayNextDialogueElement(int index)
    {
        if (dialogueBox.ElementAtOrDefault(index) == null || index == -1)
        {
            shutDownDialogue();
        }
        else
        {
            //call the write dialogue method here
        }
    }
}
