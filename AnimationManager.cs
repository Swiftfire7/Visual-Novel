using Godot;
using System;

public class AnimationManager : TextureRect
{
    public Vector2 leftSpawn = new Vector2(-256, 0);
    public Vector2 rightSpawn = new Vector2(1024, 0);
    public override void _Ready()
    {

    }

    public void SlideCharacterOn(float delay, TextureRect sprite, Tween tween, CharacterManager character)
    {
        if (character.Position.ToInt() == 1)
        {
            //from left
            tween.InterpolateProperty(sprite, "rect_position", leftSpawn, leftSpawn + (Vector2.Right * 256), delay, Tween.TransitionType.Circ, Tween.EaseType.Out);
        }
        else if (character.Position.ToInt() == 2)
        {
            //2nd from left
            tween.InterpolateProperty(sprite, "rect_position", leftSpawn, leftSpawn + (Vector2.Right * 512), delay, Tween.TransitionType.Circ, Tween.EaseType.Out);
        }
        else if (character.Position.ToInt() == 3)
        {
            //2nd from right
            tween.InterpolateProperty(sprite, "rect_position", rightSpawn, rightSpawn + (Vector2.Left * 512), delay, Tween.TransitionType.Circ, Tween.EaseType.Out);
        }
        else if (character.Position.ToInt() == 4)
        {
            //from right
            tween.InterpolateProperty(sprite, "rect_position", rightSpawn, rightSpawn + (Vector2.Left * 256), delay, Tween.TransitionType.Circ, Tween.EaseType.Out);
        }
        tween.Start();
    }
    public void SlideCharacterOff(float delay, TextureRect sprite, Tween tween, CharacterManager character)
    {
        if (character.Position.ToInt() == 1)
        {
            //from left
            tween.InterpolateProperty(sprite, "rect_position", sprite.RectPosition, leftSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
        }
        else if (character.Position.ToInt() == 2)
        {
            //2nd from left
            tween.InterpolateProperty(sprite, "rect_position", sprite.RectPosition, leftSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
        }
        else if (character.Position.ToInt() == 3)
        {
            //2nd from right
            tween.InterpolateProperty(sprite, "rect_position", sprite.RectPosition, rightSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
        }
        else if (character.Position.ToInt() == 4)
        {
            //from right
            tween.InterpolateProperty(sprite, "rect_position", sprite.RectPosition, rightSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
        }
        tween.Start();
    }
    public void SlideAllOff(float delay, Tween tween)
    {
        for (int i = 1; i < 5; i++)
        {
            TextureRect textureRect = GetNode<TextureRect>("/SceneManager/CharacterSpawner/Position" + i);
            if (textureRect.Name == "Position1")
            {
                //far left
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, leftSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position2")
            {
                //2nd from left
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, leftSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position3")
            {
                //2nd from right
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, rightSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position4")
            {
                //far right
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, rightSpawn, delay, Tween.TransitionType.Circ, Tween.EaseType.In);
            }
            tween.Start();
        }
    }
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
