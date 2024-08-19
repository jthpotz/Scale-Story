using System;
using Godot;

public partial class ButtonPress : Node2D {

    [Export]
    private Room room;

    [Export]
    private Area2D area;

    private Action<Node2D> onPress;

    private Action onRelease;

    public override void _Ready () {
        base._Ready();

        area.SetCollisionLayerValue(room.RoomNumber, true);
        area.SetCollisionMaskValue(room.RoomNumber, true);

        Hide();
        area.BodyEntered += Press;
        area.BodyExited += Release;
    }

    public void SetPressAction (Action<Node2D> pressAction) {
        onPress = pressAction;
    }

    public void SetReleaseAction (Action releaseAction) {
        onRelease = releaseAction;
    }

    public void Press (Node2D other) {
        Show();
        // AudioStreamPlayer2D music = GetNode<AudioStreamPlayer2D>("/root/GameWorld/AudioStreamPlayer2D");
        // music.Stream = GD.Load<AudioStreamMP3>("res://resources/music/fire/" + note.ToString() + ".mp3");
        // music.Play();
        onPress?.Invoke(other);
    }

    public void Release (Node2D other) {
        Hide();
        onRelease?.Invoke();
    }
}
