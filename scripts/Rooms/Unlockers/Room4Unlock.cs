using Godot;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class Room4Unlock : Node {

    [Export]
    private DoorBlock doorBlock1;

    [Export]
    private ButtonPress button1;

    public override void _Ready () {
        base._Ready();

        button1.SetPressAction(CheckDoor1);
    }

    public void CheckDoor1 (Node2D other) {
        if (other is not CharacterBody2D) return;
        if (other.GetNode<PlayerVisual>("PlayerVisual").HasScale(ScaleColor.Blue.ToString())) {
            UnlockDoor1();
        }
    }

    public void UnlockDoor1 () {
        doorBlock1.DisableDoorBlock(true);
    }

}
