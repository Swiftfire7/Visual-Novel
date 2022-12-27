using Godot;
using System;

public class BiographyManager : ColorRect
{
    DialogueReader dialogueReader;
    SceneManager sceneManager;
    MenuManager menuManager;
    CharacterManager characterManager;
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
        characterManager = GetNode<CharacterManager>("../../../CharacterSpawner");
        characterSprite = GetNode<TextureRect>("../../Biography/TextureRect");
        menuManager = GetNode<MenuManager>("../../../Background/MenuManager");
        colorRect = GetNode<ColorRect>("../../Biography/ColorRect");
        sceneManager = GetNode<SceneManager>("../../../");
        popup = GetNode<Popup>("../");
    }
    public void CharacterBio()
    {
        sceneManager.MenuIsUp = true;
        //load the speaker's bio to populate the bio menu with
        //if not finished printing
        if (dialogueReader.finished == false)
        {
            //if the narrator is speaking, call the background bio instead
            if (dialogueReader.characterManagers[dialogueReader.phraseNum].Speaker == " ")
            {
                AreaBio();
            }
            //otherwise, populate the char bio screen
            else if (dialogueReader.characterManagers[dialogueReader.phraseNum].Speaker != " ")
            {
                PopulateCharBio(dialogueReader.phraseNum);
            }
        }
        //if finished printing line
        else
        {
            //if the narrator is speaking, call the background bio instead
            if (dialogueReader.characterManagers[dialogueReader.previousIndex].Speaker == " ")
            {
                AreaBio();
            }
            //otherwise, populate the char bio screen
            else if (dialogueReader.characterManagers[dialogueReader.previousIndex].Speaker != " ")
            {
                PopulateCharBio(dialogueReader.previousIndex);
            }
        }
    }
    public void PopulateCharBio(int index)
    {
        //reference to the speaker
        charBio = dialogueReader.characterManagers[index].Speaker + dialogueReader.characterManagers[index].Mod;
        //find the speaker's bio
        temp = menuManager.SelectedScenePath + "/biographies/" + charBio + ".txt";
        //display the sprite next to the bio
        characterSprite.Texture = (Texture)GD.Load(menuManager.SelectedScenePath + "/characters/" + charBio + dialogueReader.characterManagers[index].Emotion + ".png");
        //write to the charBio colorRect/RichText Doc
        File file = new File();
        file.Open(temp, File.ModeFlags.Read);
        temp = file.GetAsText();
        file.Close();
        biography = GetNode<Popup>("../../Biography");
        richTextLabel = GetNode<RichTextLabel>("../../Biography/RichTextLabel");
        richTextLabel.Text = temp;
        //hide other UI and show Bio
        biography.Show();
        popup.Hide();
    }
    public void HideChar()
    {
        biography.Hide();
        popup.Show();
        sceneManager.MenuIsUp = false;
    }
    public void HideArea()
    {
        biography.Hide();
        popup.Show();
        characterManager.Show();
        sceneManager.MenuIsUp = false;
    }
    public void AreaBio()
    {
        //hide UI to show background
        popup.Hide();
        characterManager.Hide();
        //current background path
        areaBio = menuManager.background.Texture.ResourcePath;
        //remove absolute path
        areaBio = areaBio.Remove(0, menuManager.SelectedScenePath.Length());
        //remove scenery directory
        areaBio = areaBio.Remove(0, 9);
        //remove file type
        areaBio = areaBio.Remove(areaBio.Length() - 4, 4);
        //current scene path, menu to biographies look for area
        temp = menuManager.SelectedScenePath + "/biographies/" + areaBio + ".txt";
        //load area bio
        File file = new File();
        file.Open(temp, File.ModeFlags.Read);
        temp = file.GetAsText();
        file.Close();
        //populate bio
        biography = GetNode<Popup>("../../AreaInfo");
        richTextLabel = GetNode<RichTextLabel>("../../AreaInfo/TextureRect/RichTextLabel");
        richTextLabel.Text = temp;
        biography.Show();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
