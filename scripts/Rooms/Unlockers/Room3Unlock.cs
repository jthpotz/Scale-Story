using System;
using Godot;

public partial class Room3Unlock : Node {

    [Export]
    private LightFountain fountain1;

    [Export]
    private LightFountain fountain2;

    [Export]
    private LightFountain fountain3;

    [Export]
    private DoorBlock doorBlock1;

    [Export]
    private DoorBlock doorBlock2;

    private bool fountain2Lit = false;

    private bool fountain3Lit = false;

    public override void _Ready () {
        base._Ready();

        fountain1.SetLightAction(UnlockDoor1);
        fountain2.SetLightAction(() => LightFountain(2));
        fountain3.SetLightAction(() => LightFountain(3));
    }

    public void LightFountain (int num) {
        if (num == 2) fountain2Lit = true;
        if (num == 3) fountain3Lit = true;

        if (fountain2Lit && fountain3Lit) UnlockDoor2();
    }

    public void UnlockDoor2 () {
        doorBlock2.DisableDoorBlock(true);
    }

    public void UnlockDoor1 () {
        doorBlock1.DisableDoorBlock(true);
    }

}
