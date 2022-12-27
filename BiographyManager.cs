using Godot;
using System;

public class BiographyManager : ColorRect
{
    DialogueReader dialogueReader;
    SceneManager sceneManager;
    MenuManager menuManager;
    TextureRect characterSprite;
    RichTextLabel richTextLabel;
    ColorRect colorRect;
    Control characterSpawner;
    Popup biography;
    Popup popup;
    string charBio = "";
    string areaBio = "";
    string temp = "";
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
        characterSprite = GetNode<TextureRect>("../../Biography/TextureRect");
        menuManager = GetNode<MenuManager>("../../../Background/MenuManager");
        richTextLabel = GetNode<RichTextLabel>("../../Biography/RichTextLabel");
        colorRect = GetNode<ColorRect>("../../Biography/ColorRect");
        biography = GetNode<Popup>("../../Biography");
        sceneManager = GetNode<SceneManager>("../../../");
        popup = GetNode<Popup>("../");
    }
    public void CharacterBio()
    {
        sceneManager.MenuIsUp = true;
        //load the speaker's bio to populate the bio menu with
        charBio = dialogueReader.characterManagers[dialogueReader.phraseNum].Speaker + dialogueReader.characterManagers[dialogueReader.phraseNum].Mod;
        //find the speaker's bio
        temp = menuManager.SelectedScenePath + "/biographies/" + charBio + ".txt";
        //display the sprite next to the bio
        characterSprite.Texture = (Texture)GD.Load(menuManager.SelectedScenePath + "/characters/" + charBio + dialogueReader.characterManagers[dialogueReader.phraseNum].Mod + dialogueReader.characterManagers[dialogueReader.phraseNum].Emotion + ".png");
        //write to the charBio colorRect/RichText Doc
        File file = new File();
        file.Open(temp, File.ModeFlags.Read);
        temp = file.GetAsText();
        file.Close();
        //hide other UI and show Bio
        biography.Show();
        popup.Hide();
        richTextLabel.Text = temp;
    }
    public void HideChar()
    {
        biography.Hide();
        popup.Show();
        sceneManager.MenuIsUp = false;
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
