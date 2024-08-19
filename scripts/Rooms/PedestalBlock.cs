using System;
using Godot;

public partial class PedestalBlock : Node {

    [Export]
    private Room room;

    [Export]
    private StaticBody2D fullBlock;

    [Export]
    private StaticBody2D partialBlock1;

    [Export]
    private StaticBody2D partialBlock2;

    [Export]
    private Sprite2D stairs;

    public override void _Ready () {
        base._Ready();

        HideStairs(false);
    }

    public void ShowStairs (bool sound) {
        stairs.Show();

        partialBlock1.SetCollisionLayerValue(room.RoomNumber, true);
        partialBlock1.SetCollisionMaskValue(room.RoomNumber, true);
        partialBlock2.SetCollisionLayerValue(room.RoomNumber, true);
        partialBlock2.SetCollisionMaskValue(room.RoomNumber, true);

        fullBlock.SetCollisionLayerValue(room.RoomNumber, false);
        fullBlock.SetCollisionMaskValue(room.RoomNumber, false);
    }

    public void HideStairs (bool sound) {
        stairs.Hide();

        partialBlock1.SetCollisionLayerValue(room.RoomNumber, false);
        partialBlock1.SetCollisionMaskValue(room.RoomNumber, false);
        partialBlock2.SetCollisionLayerValue(room.RoomNumber, false);
        partialBlock2.SetCollisionMaskValue(room.RoomNumber, false);

        fullBlock.SetCollisionLayerValue(room.RoomNumber, true);
        fullBlock.SetCollisionMaskValue(room.RoomNumber, true);
    }

}
