using System;
using System.Collections.Generic;
public class DialogueBox
{
    public int Index;
    public List<DialogueBox> DialogueBoxes;
    public string DisplayText;
    //constructor to continue the dialogue chain
    public DialogueBox(string displayText, int index, List<DialogueBox> dialogues = null)
    {
        DisplayText = displayText;
        Index = index;
        //is this the last dialogue?
        if (dialogues != null)
        {
            DialogueBoxes = dialogues;
        }
    }
}