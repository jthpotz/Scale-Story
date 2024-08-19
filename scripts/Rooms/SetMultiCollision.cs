using Godot;

public partial class SetMultiCollision : Node2D {

    [Export]
    private Room room;

    [Export]
    private StaticBody2D[] bodies;

    public override void _Ready () {
        base._Ready();

        foreach (StaticBody2D body in bodies) {
            body.CollisionLayer = 0;
            body.CollisionMask = 0;

            body.SetCollisionLayerValue(room.RoomNumber, true);
            body.SetCollisionMaskValue(room.RoomNumber, true);
        }


    }
}
