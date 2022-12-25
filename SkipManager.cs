using Godot;
using System;

public class SkipManager : ColorRect
{
    DialogueReader dialogueReader;
    TextureRect background;
    ColorRect bookSelect;
    SceneManager sceneManager;
    string mainMenuPath = "res://Assets/backgrounds/diffused menu scroll.png";
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
        sceneManager = GetNode<SceneManager>("../../../");
        background = GetNode<TextureRect>("../../../Background");
        bookSelect = GetNode<ColorRect>("../../../Background/MenuManager/Book Select");
    }
    public void SkipScene()
    {
        dialogueReader.EndScene();
        var img = (Texture)GD.Load(mainMenuPath);
        background.Texture = img;
        sceneManager.MenuIsUp = true;
        sceneManager.Button.Hide();
        bookSelect.Show();
    }
}
