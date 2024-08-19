using System;
using Godot;

public partial class Room5Unlock : Node {
    [Export]
    private DoorBlock doorBlock1;

    [Export]
    private ButtonPress button1;

    public override void _Ready () {
        base._Ready();

        button1.SetPressAction((Node2D other) => UnlockDoor1());
        button1.SetReleaseAction(LockDoor1);
    }


    public void UnlockDoor1 () {
        doorBlock1.DisableDoorBlock(true);
    }

    public void LockDoor1 () {
        doorBlock1.EnableDoorBlock(true);
    }
}
