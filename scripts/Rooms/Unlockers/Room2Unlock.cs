using System;
using Godot;

public partial class Room2Unlock : Node {

    [Export]
    private LightFountain fountain1;

    [Export]
    private DoorBlock doorBlock;

    public override void _Ready () {
        base._Ready();

        fountain1.SetLightAction(UnlockDoor);

    }

    public void UnlockDoor () {
        doorBlock.DisableDoorBlock();
    }

}
