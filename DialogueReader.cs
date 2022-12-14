using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class DialogueReader : Popup
{
    public string dialoguePath = "Assets/Scenes/Introduction/Intro1/dialogue/dialogue.json";
    public float textSpeed = 0.05f;
    int phraseNum = 0;
    private int numOfDialogues = 0;
    public int previousIndex = 0;
    public bool finished = false;
    public bool dialogueEnded = false;
    private Vector2 leftSpawn = new Vector2(-256, 0);
    private Vector2 rightSpawn = new Vector2(1024, 0);
    private Tween tween;
    private float slideSpeed = .33f;
    public AnimatedSprite Indicator;
    private TextureRect previousSprite;
    public TextureRect characterSprite;
    private Control characterSpawner;
    private string spritePath = "";
    private string characterSpawnerPath = "/root/SceneManager/CharacterSpawner";
    public RichTextLabel Speaker;
    public RichTextLabel DialogueBox;
    public File storedDialogue = new File();
    public List<string> Dialogues;
    public Timer timer;
    public string currentSpeaker;
    public List<CharacterManager> characterManagers;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = textSpeed;
        Speaker = GetNode<RichTextLabel>("Name");
        Speaker.BbcodeEnabled = true;
        DialogueBox = GetNode<RichTextLabel>("Dialogue");
        DialogueBox.BbcodeEnabled = true;
        Indicator = GetNode<AnimatedSprite>("AnimatedSprite");
        characterSpawner = GetNode<Control>(characterSpawnerPath);
        tween = GetNode<Tween>("Tween");
        GetDialogue();
    }

    public void _Process()
    {

    }

    public void GetDialogue()
    {
        //read and convert data from the scene's json
        File dialogueFile = new File();
        dialogueFile.Open(dialoguePath, File.ModeFlags.Read);
        string jsonFile = "";
        jsonFile = dialogueFile.GetAsText();
        characterManagers = JsonConvert.DeserializeObject<List<CharacterManager>>(jsonFile);
        dialogueFile.Close();
    }
    public async void NextPhrase()
    {
        //dialogue iterator, reset after completion
        if (phraseNum >= characterManagers.Count())
        {
            SlideAllOff(slideSpeed);
            this.Visible = false;
            phraseNum = 0;
            dialogueEnded = true;
            return;
        }
        //make sure dialogue stays open while there is dialogue to show
        dialogueEnded = false;
        finished = false;
        //text box logic
        Speaker.BbcodeText = characterManagers[phraseNum].Speaker;
        DialogueBox.BbcodeText = characterManagers[phraseNum].Text;

        DialogueBox.VisibleCharacters = 0;

        //character sprite logic
        var speakerEmotion = characterManagers[phraseNum].Speaker + characterManagers[phraseNum].Emotion;

        var img = (Texture)GD.Load("res://Assets/Scenes/Introduction/intro1/characters/" + speakerEmotion + ".png");

        //wire up the sprite according to the speaker's entrance position
        spritePath = characterSpawnerPath + "/Position" + characterManagers[phraseNum].Position;
        characterSprite = GetNode<TextureRect>(spritePath);
        characterSprite.Texture = img;

        //animation logic, only slide on if there is a new speaker or position
        //it would be a good idea to abstract this into its own class later

        //start of dialogue chain, move onscreen
        if (phraseNum == 0)
        {
            SlideCharacterOn(slideSpeed, phraseNum);
        }
        //if the speaker is the same but they move, move them
        else if (characterManagers[phraseNum].Speaker == characterManagers[previousIndex].Speaker
        && characterManagers[phraseNum].Position != characterManagers[previousIndex].Position)
        {
            SlideCharacterOff(slideSpeed, previousIndex);
            SlideCharacterOn(slideSpeed, phraseNum);
        }
        //if the speaker is different but they're in the same spot, move them quickly
        else if (characterManagers[phraseNum].Speaker == characterManagers[previousIndex].Speaker
        && characterManagers[phraseNum].Position != characterManagers[previousIndex].Position)
        {
            SlideCharacterOff(slideSpeed / 2, previousIndex);
            SlideCharacterOn(slideSpeed / 2, phraseNum);
        }
        //if the speaker is the same and they haven't moved, don't animate anything
        else if (characterManagers[phraseNum].Speaker == (characterManagers[phraseNum - 1].Speaker)
           && characterManagers[phraseNum].Position == characterManagers[phraseNum - 1].Position)
        {
        }
        //if the speaker is different and they're in a different spot, bring them onscreen
        else if (characterManagers[phraseNum].Speaker != (characterManagers[phraseNum - 1].Speaker)
           && characterManagers[phraseNum].Position != characterManagers[phraseNum - 1].Position)
        {
            SlideCharacterOn(slideSpeed, phraseNum);
        }
        await ToSignal(GetTree().CreateTimer(slideSpeed + .2f), "timeout");

        //print chars one by one
        while (DialogueBox.VisibleCharacters < DialogueBox.Text.Length)
        {
            DialogueBox.VisibleCharacters += 1;
            timer.Start();
            await ToSignal(timer, "timeout");
        }
        //hold previous values, iterate, and allow progression
        previousSprite = characterSprite;
        previousIndex = phraseNum;
        phraseNum += 1;
        finished = true;
        return;

    }
    public void SlideCharacterOn(float delay, int index)
    {
        if (characterManagers[index].Position.ToInt() == 1)
        {
            //from left
            tween.InterpolateProperty(characterSprite, "rect_position", leftSpawn, leftSpawn + (Vector2.Right * 256), delay, Tween.TransitionType.Linear, Tween.EaseType.Out);
        }
        if (characterManagers[index].Position.ToInt() == 2)
        {
            //2nd from left
            tween.InterpolateProperty(characterSprite, "rect_position", leftSpawn, leftSpawn + (Vector2.Right * 512), delay, Tween.TransitionType.Linear, Tween.EaseType.Out);
        }
        else if (characterManagers[index].Position.ToInt() == 3)
        {
            //2nd from right
            tween.InterpolateProperty(characterSprite, "rect_position", rightSpawn, rightSpawn + (Vector2.Left * 512), delay, Tween.TransitionType.Linear, Tween.EaseType.Out);
        }
        else if (characterManagers[index].Position.ToInt() == 4)
        {
            //from right
            tween.InterpolateProperty(characterSprite, "rect_position", rightSpawn, rightSpawn + (Vector2.Left * 256), delay, Tween.TransitionType.Linear, Tween.EaseType.Out);
        }
        tween.Start();
    }
    public void SlideCharacterOff(float delay, int index)
    {
        if (characterManagers[index].Position.ToInt() == 1)
        {
            //from left
            tween.InterpolateProperty(previousSprite, "rect_position", previousSprite.RectPosition, leftSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
        }
        else if (characterManagers[index].Position.ToInt() == 2)
        {
            //2nd from left
            tween.InterpolateProperty(previousSprite, "rect_position", previousSprite.RectPosition, leftSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
        }
        else if (characterManagers[index].Position.ToInt() == 3)
        {
            //2nd from right
            tween.InterpolateProperty(previousSprite, "rect_position", previousSprite.RectPosition, rightSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
        }
        else if (characterManagers[index].Position.ToInt() == 4)
        {
            //from right
            tween.InterpolateProperty(previousSprite, "rect_position", previousSprite.RectPosition, rightSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
        }
        tween.Start();

    }
    public void SlideAllOff(float delay)
    {
        for (int i = 1; i < 5; i++)
        {
            TextureRect textureRect = GetNode<TextureRect>(characterSpawnerPath + "/Position" + i);
            if (textureRect.Name == "Position1")
            {
                //from left
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, leftSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position2")
            {
                //2nd from left
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, leftSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position3")
            {
                //2nd from right
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, rightSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
            }
            else if (textureRect.Name == "Position4")
            {
                //from right
                tween.InterpolateProperty(textureRect, "rect_position", textureRect.RectPosition, rightSpawn, delay, Tween.TransitionType.Linear, Tween.EaseType.In);
            }
            tween.Start();
        }
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
