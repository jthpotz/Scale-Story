using System;
using Godot;

public partial class SetCollision : StaticBody2D {

    [Export]
    private Room room;

    public override void _Ready () {
        base._Ready();

        SetCollisionLayerValue(room.RoomNumber, true);
        SetCollisionMaskValue(room.RoomNumber, true);
    }

}
