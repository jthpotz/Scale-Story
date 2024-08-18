using System;
using Godot;

public partial class Door : Area2D {

    [Export]
    private Vector2 targetPosition;

    public Vector2 TargetPosition {
        get {
            return targetPosition;
        }
    }

    [Export]
    private int targetRoom;

    public int TargetRoom {
        get {
            return targetRoom;
        }
    }

    [Export]
    private Room room;

    public override void _Ready () {
        base._Ready();
        BodyEntered += ChangeRoom;
    }

    public void ChangeRoom (Node2D other) {
        if (other is not CharacterBody2D) return;
        room.ChangeRoom(targetRoom, targetPosition);
    }

}
