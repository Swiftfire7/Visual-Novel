using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
public class DialogueManager : Control
{
    public List<DialogueBox> dialogueBox;
    [Export]

    public PackedScene InterfaceSelectableObject;
    public List<InterfaceSelection> Selections = new List<InterfaceSelection>();
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
    public async override void _Process(float delta)
    {
        if (SceneManager.GlobalSceneManager.Paused && dialogueIsUp)
        {
            //allows user to navigate the ui using left, right, and enter
            if (Input.IsActionJustPressed("ui_left"))
            {
                foreach (var item in Selections)
                {
                    item.SetSelected(false);
                }
                currentSelectionIndex -= 1;
                if (currentSelectionIndex < 0)
                {
                    currentSelectionIndex = 0;
                }
                Selections[currentSelectionIndex].SetSelected(true);
            }
            else if (Input.IsActionJustPressed("ui_right"))
            {
                foreach (var item in Selections)
                {
                    item.SetSelected(false);
                }
                currentSelectionIndex += 1;
                if (currentSelectionIndex > Selections.Count - 1)
                {
                    currentSelectionIndex = Selections.Count - 1;
                }
                Selections[currentSelectionIndex].SetSelected(true);
            }
            else if (Input.IsActionJustPressed("ui_accept") && FinishedPrinting == true)
            {
                //continues the dialogue chain after pressing next
                await ToSignal(GetTree(), "idle_frame");
                displayNextDialogueElement(Selections[currentSelectionIndex].interfaceSelectionObject.SelectionIndex);


            }
        }
    }
    public void ShowDialogueElement()
    {
        GetNode<Popup>("Popup").Popup_();
        GetNode<Label>("Popup/Label").Text = DialogueHeader;
        FinishedPrinting = false;
        WriteDialogue(dialogueBox[0]);
    }
    public void WriteDialogue(DialogueBox dialogue)
    {
        foreach (Node item in GetNode<Node>("Popup/HBoxContainer").GetChildren())
        {
            item.QueueFree();
        }
        Selections = new List<InterfaceSelection>();
        GetNode<RichTextLabel>("Popup/RichTextLabel").Text = dialogue.DisplayText;
        foreach (var item in dialogue.InterfaceSelectionObjects)
        {
            //sets up our buttons and passes back an object after instantiation so we can reference it dynamically through code
            InterfaceSelection interfaceSelection = InterfaceSelectableObject.Instance() as InterfaceSelection;
            interfaceSelection.interfaceSelectionObject = item;
            GetNode<HBoxContainer>("Popup/HBoxContainer").AddChild(interfaceSelection);
            Selections.Add(interfaceSelection);
            interfaceSelection.SetSelected(false);
        }
        Selections[0].SetSelected(true);
        currentSelectionIndex = 0;
        SceneManager.GlobalSceneManager.Paused = true;
        dialogueIsUp = true;
        FinishedPrinting = true;
    }
    //upon selecting dialogue, fetch the scene's dialogue dynamically
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
            WriteDialogue(dialogueBox[index]);
        }
    }
}
