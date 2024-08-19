using System;
using System.Collections.Immutable;
using System.Linq;
using Godot;
using Godot.Collections;

public partial class LightFountain : Node2D {

    [Export]
    private Room room;

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

        area.SetCollisionLayerValue(room.RoomNumber, true);
        area.SetCollisionMaskValue(room.RoomNumber, true);

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
        AudioQueue music = GetNode<AudioQueue>("/root/GameWorld/SFXPlayer");
        music.QueueSound("res://resources/music/fire/" + note.ToString() + ".mp3", AudioQueue.AudioType.mp3);
        onLight?.Invoke();
    }

    public void Quench () {
        Hide();
    }

}
