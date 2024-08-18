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

    [Export]
    private StaticBody2D blocker;

    [Export]
    private Area2D trigger;

    public override void _Ready () {
        base._Ready();

        if (useScaleColor) trigger.BodyEntered += CheckColorTrigger;
    }

    public void DisableDoorBlock () {
        Hide();
        blocker.SetCollisionLayerValue(room.RoomNumber, false);
        blocker.SetCollisionMaskValue(room.RoomNumber, false);
    }

    public void EnableDoorBlock () {
        Show();
        blocker.SetCollisionLayerValue(room.RoomNumber, true);
        blocker.SetCollisionMaskValue(room.RoomNumber, true);
    }

    public void CheckColorTrigger (Node2D other) {
        if (other is not CharacterBody2D) return;
        if (useScaleColor) {
            if (other.GetNode<PlayerVisual>("PlayerVisual").HasScale(scaleNeeded.ToString())) DisableDoorBlock();
        }
    }

}
