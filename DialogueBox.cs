using System;
using System.Collections.Generic;
public class DialogueBox
{
    public int Index;
    public List<InterfaceSelectionObject> InterfaceSelectionObjects;
    public List<DialogueBox> DialogueBoxes;
    public string DisplayText;
    //constructor to continue the dialogue chain
    public DialogueBox(List<InterfaceSelectionObject> interfaceSelectionObjects, string displayText, int index, List<DialogueBox> dialogues = null)
    {
        InterfaceSelectionObjects = interfaceSelectionObjects;
        DisplayText = displayText;
        Index = index;
        //is this the last dialogue?
        if (dialogues != null)
        {
            DialogueBoxes = dialogues;
        }
    }
}