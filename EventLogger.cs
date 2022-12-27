using Godot;
using System;

public class EventLogger : ColorRect
{
    DialogueReader dialogueReader;
    SceneManager sceneManager;
    public string EventLog = "";
    RichTextLabel richTextLabel;
    Popup popup;
    Popup dialoguePopup;
    CharacterManager characterManager;
    public override void _Ready()
    {
        dialogueReader = GetNode<DialogueReader>("../");
        sceneManager = GetNode<SceneManager>("../../../");
        richTextLabel = GetNode<RichTextLabel>("../../EventLog/TextureRect/RichTextLabel");
        popup = GetNode<Popup>("../../EventLog");
        dialoguePopup = GetNode<Popup>("../");
        characterManager = GetNode<CharacterManager>("../../../CharacterSpawner");
    }
    public void ShowLog()
    {
        dialoguePopup.Hide();
        characterManager.Hide();
        popup.Show();
        sceneManager.MenuIsUp = true;
    }
    public void WriteToLog()
    {
        //record the name of the speaker and what they said
        if (dialogueReader.dialogueEnded == false)
        {
            EventLog = EventLog + dialogueReader.characterManagers[dialogueReader.phraseNum].Speaker;
            EventLog = EventLog + ":" + '\n';
            EventLog = EventLog + dialogueReader.DialogueBox.Text + '\n' + '\n';
            richTextLabel.Text = EventLog;
        }
        //reset log
        else
        {
            EventLog = "";
            richTextLabel.Text = EventLog;
        }
    }
    public void HideLog()
    {
        popup.Hide();
        sceneManager.MenuIsUp = false;
        characterManager.Show();
        dialoguePopup.Show();
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
