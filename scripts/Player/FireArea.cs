using System;
using System.Diagnostics;
using Godot;

public partial class FireArea : Area2D {

    [Export]
    private PlayerCollision playerCollision;

    public void EnableFireArea () {
        SetCollisionLayerValue(playerCollision.CurrentCollisionNumber, true);
        SetCollisionMaskValue(playerCollision.CurrentCollisionNumber, true);
    }

    public void DisableFireArea () {
        CollisionLayer = 0;
        CollisionMask = 0;
    }

}
