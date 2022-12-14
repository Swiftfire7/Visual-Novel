using Godot;
using System;

public class MenuManager : Control
{
    private string backgroundPath = "res://Assets/Books/";
    private string bookPath = "";
    private string volumePath = "";
    private string chapterPath = "";
    public string SelectedScenePath = "";
    string currentPath = "";
    ColorRect colorRect;
    public TextureRect background;
    SceneManager sceneManager;
    CharacterManager characterManager;
    Popup dialogue;
    SoundManager soundManager;
    public Popup Options;
    public bool MenuWasUp = false;
    bool thisWasUp = false;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("../../");
        background = GetNode<TextureRect>("../");
        Options = GetNode<Popup>("../../Options");
        soundManager = GetNode<SoundManager>("../../SoundManager");
    }
    public void OnOptions()
    {
        if (!thisWasUp)
        {
            if (sceneManager.MenuIsUp)
            {
                MenuWasUp = true;
            }
            else
            {
                MenuWasUp = false;
            }
            if (MenuWasUp)
            {
                Options.Show();
            }
            else
            {
                sceneManager.MenuIsUp = true;
                Options.Show();
            }
        }
        else
        {
            if (MenuWasUp)
            {
                MenuWasUp = false;
                Options.Hide();
            }
            else
            {
                sceneManager.MenuIsUp = false;
                Options.Hide();
            }
        }
        thisWasUp = !thisWasUp;
    }
    //when an item is selected, show the next menu
    public void OnBook(string PressedButton)
    {
        colorRect = GetNode<ColorRect>("Book Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Show();
        bookPath = backgroundPath + PressedButton + "/";
    }
    public void OnVolume(string PressedButton)
    {
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Show();
        volumePath = bookPath + PressedButton + "/";
    }
    public void OnChapter(string PressedButton)
    {
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Show();
        chapterPath = volumePath + PressedButton + "/";
    }
    public void OnScene(string PressedButton)
    {
        sceneManager.MenuIsUp = false;
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Hide();
        SelectedScenePath = chapterPath + PressedButton;
        var img = (Texture)GD.Load(SelectedScenePath + "/scenery/Sandtown.png");
        background.Texture = img;
        Button instructions = GetNode<Button>("instructions");
        instructions.Show();
    }
    //when the menu button is clicked, step back through the menu
    public void OnVolumeMenu()
    {
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Show();
    }
    public void OnChapterMenu()
    {
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Show();
    }
    public void OnBookMenu()
    {
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Book Select");
        colorRect.Show();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
