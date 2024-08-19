using System;
using Godot;

public partial class SetSlidingCollision : StaticBody2D {

    [Export]
    private Room room;

    public override void _Ready () {
        base._Ready();

        CollisionLayer = 0;
        CollisionMask = 0;

        SetCollisionLayerValue(32 - room.RoomNumber, true);
        SetCollisionMaskValue(32 - room.RoomNumber, true);
    }

}
