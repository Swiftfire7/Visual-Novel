using System;

public class InterfaceSelectionObject
{
    //UI cursor that goes on the Dialogue Buttons
    public string SelectionText;
    public int SelectionIndex;
    public InterfaceSelectionObject(int index, string selectionText)
    {
        SelectionText = selectionText;
        SelectionIndex = index;
    }
}