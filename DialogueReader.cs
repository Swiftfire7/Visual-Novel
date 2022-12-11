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
    public bool finished = false;
    public AnimatedSprite Indicator;
    public Sprite characterSprite;
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
        characterSprite = GetNode<Sprite>("/root/SceneManager/CharacterManager/Sprite");
        GetDialogue();
    }

    public void _Process()
    {

    }

    public void GetDialogue()
    {
        File dialogueFile = new File();
        dialogueFile.Open(dialoguePath, File.ModeFlags.Read);
        string jsonFile = "";
        jsonFile = dialogueFile.GetAsText();
        characterManagers = JsonConvert.DeserializeObject<List<CharacterManager>>(jsonFile);
        dialogueFile.Close();
    }
    public async void NextPhrase()
    {
        if (phraseNum >= characterManagers.Count())
        {
            QueueFree();
            return;
        }
        finished = false;

        Speaker.BbcodeText = characterManagers[phraseNum].Speaker;
        DialogueBox.BbcodeText = characterManagers[phraseNum].Text;

        DialogueBox.VisibleCharacters = 0;

        //character sprite logic
        File f = new File();
        var speakerEmotion = characterManagers[phraseNum].Speaker + characterManagers[phraseNum].Emotion;

        var img = (Texture)GD.Load("res://Assets/Scenes/Introduction/intro1/characters/" + speakerEmotion + ".png");
        characterSprite.Texture = img;
        while (DialogueBox.VisibleCharacters < DialogueBox.Text.Length)
        {
            DialogueBox.VisibleCharacters += 1;
            timer.Start();
            await ToSignal(timer, "timeout");
        }
        finished = true;
        phraseNum += 1;
        return;

    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
