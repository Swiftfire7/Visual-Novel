using Godot;
using System;
using System.Collections.Generic;

public class SoundManager : Control
{
    public List<AudioStreamPlayer> Music = new List<AudioStreamPlayer>();
    public List<AudioStreamPlayer> Sfx = new List<AudioStreamPlayer>();
    public List<AudioStreamPlayer> Voice = new List<AudioStreamPlayer>();
    AudioStreamPlayer audio;
    HSlider slider;



    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        audio = GetNode<AudioStreamPlayer>("MusicPlayer");
        Music.Add(audio);
        Music[0].Play();
        audio = GetNode<AudioStreamPlayer>("SfxPlayer");
        Sfx.Add(audio);
        audio = GetNode<AudioStreamPlayer>("VoicePlayer");
        Voice.Add(audio);
    }
    public void UpdateMusic(string song)
    {
        AudioStream sound = GD.Load<AudioStream>("res://Music/" + song + ".ogg");
        Music[0].Stream = sound;
        Music[0].Play();
    }
    public void MusicVolume(double value)
    {
        slider = GetNode<HSlider>("../Options/Menu/Music");
        foreach (AudioStreamPlayer a in Music)
        {
            UpdateVolume(a, slider, value);
        }

    }
    public void SfxVolume(double value)
    {
        slider = GetNode<HSlider>("../Options/Menu/Sfx");
        foreach (AudioStreamPlayer a in Sfx)
        {
            UpdateVolume(a, slider, value);
        }
    }
    public void VoiceVolume(double value)
    {
        slider = GetNode<HSlider>("../Options/Menu/Voice");
        foreach (AudioStreamPlayer a in Voice)
        {
            UpdateVolume(a, slider, value);
        }
    }
    public void UpdateVolume(AudioStreamPlayer sound, HSlider hSlider, double value)
    {
        if (value < -25)
        {
            value = -100;
        }
        sound.VolumeDb = (float)value;
    }
    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    //  public override void _Process(float delta)
    //  {
    //      
    //  }
}
