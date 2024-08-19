using Godot;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class DoorBlock : Node2D {

    [Export]
    private bool useScaleColor = false;

    [Export]
    private ScaleColor scaleNeeded;

    [Export]
    private Room room;

    private AudioQueue music;

    [Export]
    private StaticBody2D blocker;

    [Export]
    private Area2D trigger;

    public override void _Ready () {
        base._Ready();

        Hide();

        if (useScaleColor) trigger.BodyEntered += CheckColorTrigger;
        EnableDoorBlock(false);

        music = GetNode<AudioQueue>("/root/GameWorld/SFXPlayer");
    }

    public void DisableDoorBlock (bool sound) {
        if (Visible) {
            Hide();
            blocker.SetCollisionLayerValue(room.RoomNumber, false);
            blocker.SetCollisionMaskValue(room.RoomNumber, false);
            trigger.SetCollisionLayerValue(room.RoomNumber, false);
            trigger.SetCollisionMaskValue(room.RoomNumber, false);
            if (sound) {
                music.QueueSound("res://resources/music/door-open.mp3", AudioQueue.AudioType.mp3);
            }
            
        }
    }

    public void EnableDoorBlock (bool sound) {
        if (!Visible) {
            Show();
            blocker.SetCollisionLayerValue(room.RoomNumber, true);
            blocker.SetCollisionMaskValue(room.RoomNumber, true);
            trigger.SetCollisionLayerValue(room.RoomNumber, true);
            trigger.SetCollisionMaskValue(room.RoomNumber, true);
            if (sound) {
                music.Play("res://resources/music/door-close.mp3", AudioQueue.AudioType.mp3);
            }
        }
    }

    public void CheckColorTrigger (Node2D other) {
        if (other is not CharacterBody2D) return;
        if (useScaleColor) {
            if (other.GetNode<PlayerVisual>("PlayerVisual").HasScale(scaleNeeded.ToString())) 
                DisableDoorBlock(true);
            else {
                music.Play("res://resources/music/door-knock.mp3", AudioQueue.AudioType.mp3);
            }
        }
        
    }

}
