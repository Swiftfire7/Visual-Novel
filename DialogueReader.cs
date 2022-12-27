using Godot;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class DialogueReader : Popup
{
    public string dialoguePath = "Assets/Scenes/Introduction/Intro1/dialogue/dialogue.json";
    public float textSpeed = 0.03f;
    public int phraseNum = 0;
    public int previousIndex = 0;
    public bool finished = false;
    public bool dialogueEnded = false;
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
    public string previousSpeaker;
    public string currentPosition;
    public string previousPosition;
    public AnimationManager animationManager;
    public FastManager fastManager;
    public AutoplayManager autoplayManager;
    public List<CharacterManager> characterManagers;
    public MenuManager menuManager;
    SceneManager scene;
    EventLogger eventLogger;


    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        timer.WaitTime = textSpeed;
        Speaker = GetNode<RichTextLabel>("Name");
        Speaker.BbcodeEnabled = true;
        DialogueBox = GetNode<RichTextLabel>("Dialogue");
        DialogueBox.BbcodeEnabled = true;
        Indicator = GetNode<AnimatedSprite>("Next/AnimatedSprite");
        characterSpawner = GetNode<Control>(characterSpawnerPath);
        animationManager = GetNode<AnimationManager>("../../Background/");
        menuManager = GetNode<MenuManager>("../../Background/MenuManager");
        tween = GetNode<Tween>("Tween");
        fastManager = GetNode<FastManager>("FastForward");
        autoplayManager = GetNode<AutoplayManager>("AutoPlay");
        scene = GetNode<SceneManager>("../../");
        eventLogger = GetNode<EventLogger>("Event");
        GetDialogue();
    }

    //public void _Process()
    //{

    //}

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
        if (dialogueEnded)
        {
            phraseNum = 0;
            previousIndex = 0;
        }
        //dialogue iterator, reset after completion
        if (phraseNum >= characterManagers.Count())
        {
            EndScene();
            return;
        }
        currentSpeaker = characterManagers[phraseNum].Speaker;
        currentPosition = characterManagers[phraseNum].Position;
        //make sure dialogue stays open while there is dialogue to show
        dialogueEnded = false;
        finished = false;
        //text box logic
        Speaker.BbcodeText = characterManagers[phraseNum].Speaker;
        DialogueBox.BbcodeText = characterManagers[phraseNum].Text;
        eventLogger.WriteToLog();
        if (fastManager.FastPressed)
        {
            DialogueBox.VisibleCharacters = DialogueBox.Text.Length;
        }
        else
        {
            DialogueBox.VisibleCharacters = 0;
        }
        //character sprite logic
        string speakerEmotion = characterManagers[phraseNum].Emotion;
        string speakerMod = characterManagers[phraseNum].Mod;

        var img = (Texture)GD.Load(menuManager.SelectedScenePath + "/characters/" + currentSpeaker + speakerMod + speakerEmotion + ".png");

        //wire up the sprite according to the speaker's entrance position
        spritePath = characterSpawnerPath + "/Position" + currentPosition;
        characterSprite = GetNode<TextureRect>(spritePath);

        //animation logic, only slide on if there is a new speaker or position
        //it would be a good idea to abstract this into its own class later

        //start of dialogue chain, move onscreen
        if (phraseNum == 0)
        {
            characterSprite.Texture = img;
            animationManager.SlideCharacterOn(slideSpeed, characterSprite, tween, characterManagers[phraseNum]);
        }
        //if the speaker is the same but they move, move them
        else if (currentSpeaker == previousSpeaker && currentPosition != previousPosition)
        {
            animationManager.SlideCharacterOff(slideSpeed, previousSprite, tween, characterManagers[phraseNum]);
            await ToSignal(GetTree().CreateTimer(slideSpeed), "timeout");
            characterSprite.Texture = img;
            animationManager.SlideCharacterOn(slideSpeed, characterSprite, tween, characterManagers[phraseNum]);
        }
        //if the speaker is different but they're in the same spot, swap them quickly
        else if (currentSpeaker != previousSpeaker && currentPosition == previousPosition)
        {
            animationManager.SlideCharacterOff(slideSpeed / 2, previousSprite, tween, characterManagers[phraseNum]);
            await ToSignal(GetTree().CreateTimer(slideSpeed / 2), "timeout");
            characterSprite.Texture = img;
            animationManager.SlideCharacterOn(slideSpeed / 2, characterSprite, tween, characterManagers[phraseNum]);
        }
        //if the speaker is the same and they haven't moved, don't animate anything
        else if (currentSpeaker == previousSpeaker && currentPosition == previousPosition)
        {
        }
        // if the speaker is different and they're in a different spot
        else if (currentSpeaker != previousSpeaker && currentPosition != previousPosition)
        {
            //if they're onscreen and are different images
            if (characterSprite.RectPosition != animationManager.leftSpawn && characterSprite.RectPosition != animationManager.rightSpawn && characterSprite.Texture != img)
            {
                //slide the old one off
                animationManager.SlideCharacterOff(slideSpeed, characterSprite, tween, characterManagers[phraseNum]);
                await ToSignal(GetTree().CreateTimer(slideSpeed), "timeout");
                //bring the new one on
                characterSprite.Texture = img;
                animationManager.SlideCharacterOn(slideSpeed, characterSprite, tween, characterManagers[phraseNum]);
            }
            //if they're onscreen and are the same image, do nothing
            else if (characterSprite.RectPosition != animationManager.leftSpawn && characterSprite.RectPosition != animationManager.rightSpawn && characterSprite.Texture == img)
            {

            }
            //if they're offscreen
            else if (characterSprite.RectPosition == animationManager.leftSpawn || characterSprite.RectPosition == animationManager.rightSpawn)
            {
                //bring them on
                characterSprite.Texture = img;
                animationManager.SlideCharacterOn(slideSpeed, characterSprite, tween, characterManagers[phraseNum]);
            }
        }
        await ToSignal(GetTree().CreateTimer(slideSpeed), "timeout");
        //check for autoplay value
        if (autoplayManager.Auto)
        {
            autoplayManager.SetAutoSpeed();
        }
        //check for fastforward and progress ASAP if active
        if (fastManager.FastPressed)
        {
            DialogueBox.VisibleCharacters = DialogueBox.Text.Length;
        }
        else
        {
            //print chars one by one
            while (DialogueBox.VisibleCharacters < DialogueBox.Text.Length || autoplayManager.AutoSpeed != 0)
            {
                DialogueBox.VisibleCharacters += 1;
                autoplayManager.UpdateAutoSpeed();
                timer.Start();
                await ToSignal(timer, "timeout");
                //print all if fastforward starts
                if (fastManager.FastPressed)
                {
                    DialogueBox.VisibleCharacters = DialogueBox.Text.Length;
                    autoplayManager.AutoSpeed = 0;
                }
            }
        }
        //disable auto features while in menus
        if (fastManager.FastPressed && scene.MenuIsUp)
        {
            fastManager.FastPressed = false;
        }
        if (autoplayManager.Auto && scene.MenuIsUp)
        {
            autoplayManager.Auto = false;
        }
        //hold previous values, iterate, and allow progression
        previousSprite = characterSprite;
        previousIndex = phraseNum;
        phraseNum += 1;
        previousSpeaker = characterManagers[phraseNum - 1].Speaker;
        previousPosition = characterManagers[phraseNum - 1].Position;
        finished = true;
        //continue if autoplaying
        if (autoplayManager.Auto)
        {
            NextPhrase();
        }
        //or if fast forwarding
        else if (fastManager.FastPressed)
        {
            NextPhrase();
        }
        return;

    }
    public void EndScene()
    {
        animationManager.SlideAllOff(slideSpeed, tween);
        this.Visible = false;
        phraseNum = 0;
        previousIndex = 0;
        dialogueEnded = true;
        eventLogger.WriteToLog();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
