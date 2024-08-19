using System;
using System.Collections.Immutable;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class LightFountain : Node2D {

    [Export]
    private Area2D area;

    private Action onLight;

    [Export]
    private FountainNote note;

    public enum FountainNote {
            A,
            B,
            C,
            D,
            E,
            F,
            G,
            HighA
        }

    public override void _Ready () {
        base._Ready();

        Hide();
        area.AreaEntered += Light;
    }

    public void SetLightAction (Action lightAction) {
        onLight = lightAction;
    }

    public void SetNote (FountainNote note) {
        this.note = note;
    }

    public void Light (Node2D other) {
        Show();
        AudioStreamPlayer2D music = GetNode<AudioStreamPlayer2D>("/root/GameWorld/AudioStreamPlayer2D");
        music.Stream = GD.Load<AudioStreamMP3>("res://resources/music/fire/" + note.ToString() + ".mp3");
        music.Play();
        onLight?.Invoke();
    }

    public void Quench () {
        Hide();
    }

}
