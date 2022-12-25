using Godot;
using System;

public class MenuManager : Control
{
    private string backgroundPath = "res://Assets/Books/Book1/";
    private string volumePath = "";
    private string chapterPath = "";
    string selectedScenePath = "";
    string currentPath = "";
    ColorRect colorRect;
    TextureRect background;
    SceneManager sceneManager;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        sceneManager = GetNode<SceneManager>("../../");
        background = GetNode<TextureRect>("../");
    }
    public void OnVolume1()
    {
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Show();
        volumePath = backgroundPath + "Scenes/";
    }
    public void OnChapter1()
    {
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Show();
        chapterPath = "Introduction/";
    }
    public void OnScene1()
    {
        sceneManager.MenuIsUp = false;
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Hide();
        selectedScenePath = volumePath + chapterPath + ("intro1/");
        var img = (Texture)GD.Load(selectedScenePath + "scenery/Sandtown.png");
        background.Texture = img;
        Button instructions = GetNode<Button>("instructions");
        instructions.Show();
    }
    public void OnVolume(string PressedButton)
    {
        colorRect = GetNode<ColorRect>("Volume Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Show();
        volumePath = backgroundPath + PressedButton + "/";
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
        selectedScenePath = chapterPath + PressedButton;
        var img = (Texture)GD.Load(selectedScenePath + "/scenery/Sandtown.png");
        background.Texture = img;
        Button instructions = GetNode<Button>("instructions");
        instructions.Show();
    }
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

    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
