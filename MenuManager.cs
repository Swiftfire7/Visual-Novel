using Godot;
using System;

public class MenuManager : Control
{
    private string backgroundPath = "res://Assets/";
    string selectedPath = "";
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
        selectedPath = backgroundPath + "Scenes/";
    }
    public void OnChapter1()
    {
        colorRect = GetNode<ColorRect>("Chapter Select");
        colorRect.Hide();
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Show();
        selectedPath = selectedPath + "Introduction/";
    }
    public void OnScene1()
    {
        sceneManager.MenuIsUp = false;
        colorRect = GetNode<ColorRect>("Scene Select");
        colorRect.Hide();
        selectedPath = selectedPath + ("intro1/");
        var img = (Texture)GD.Load(selectedPath + "scenery/Sandtown.png");
        background.Texture = img;
        Button instructions = GetNode<Button>("instructions");
        instructions.Show();
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
