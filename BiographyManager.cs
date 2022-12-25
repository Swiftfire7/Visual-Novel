using Godot;
using System;

public class BiographyManager : ColorRect
{
    DialogueReader dialogueReader;
    MenuManager menuManager;
    string charBio = "";
    string areaBio = "";
    string temp = "";
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
    }
    public void CharacterBio()
    {
        //load the speaker's bio to populate the bio menu with
        charBio = dialogueReader.characterManagers[dialogueReader.phraseNum].Name;
        //find the speaker's bio
        temp = menuManager.SelectedScenePath + "biographies/" + charBio;
        //write to the charBio colorRect/RichText Doc
    }
    public void AreaBio()
    {
        //current background path
        areaBio = menuManager.background.Texture.ToString();
        //current scene path, menu to biographies look for area
        temp = menuManager.SelectedScenePath + "biographies/" + areaBio;
        //logic for loading the json/txt file into the AreaBio ColorRect
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
