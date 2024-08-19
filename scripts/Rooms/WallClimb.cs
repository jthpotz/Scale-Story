using Godot;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class WallClimb : Node2D {

    [Export]
    private Room room;

    [Export]
    private StaticBody2D blocker;

    [Export]
    private Area2D trigger;

    public override void _Ready () {
        base._Ready();

        Hide();

        trigger.BodyEntered += CheckColorTrigger;
        EnableClimbBlock();
    }

    public void DisableClimbBlock () {
        if (Visible) {
            Hide();
            blocker.SetCollisionLayerValue(room.RoomNumber, false);
            blocker.SetCollisionMaskValue(room.RoomNumber, false);
            trigger.SetCollisionLayerValue(room.RoomNumber, false);
            trigger.SetCollisionMaskValue(room.RoomNumber, false);

        }
    }

    public void EnableClimbBlock () {
        if (!Visible) {
            Show();
            blocker.SetCollisionLayerValue(room.RoomNumber, true);
            blocker.SetCollisionMaskValue(room.RoomNumber, true);
            trigger.SetCollisionLayerValue(room.RoomNumber, true);
            trigger.SetCollisionMaskValue(room.RoomNumber, true);
        }
    }

    public void CheckColorTrigger (Node2D other) {
        if (other is not CharacterBody2D) return;
        if (other.GetNode<PlayerVisual>("PlayerVisual").HasScale(ScaleColor.Purple.ToString())) {
            DisableClimbBlock();
        } else {
            EnableClimbBlock();
        }
    }

}