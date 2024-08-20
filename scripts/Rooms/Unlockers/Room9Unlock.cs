using System;
using Godot;
using Timers;
using Timer = Timers.Timer;

public partial class Room9Unlock : Node {

    [Export]
    private DoorBlock doorBlock1;

    [Export]
    private PedestalBlock pedestalBlock1;

    [Export]
    private LightFountain fountain1;

    [Export]
    private LightFountain fountain2;

    [Export]
    private ButtonPress button1;

    private bool fountain1On = false;

    private bool fountain2On = false;

    private Timer quenchDelayTimer;

    public override void _Ready () {
        base._Ready();

        button1.SetPressAction((Node2D other) => doorBlock1.DisableDoorBlock(true));

        fountain1.SetLightAction(() => LightFountain(1));
        fountain2.SetLightAction(() => LightFountain(2));

        quenchDelayTimer = Timekeeper.AddTimer(1, QuenchAll, false, false);
    }

    private void LightFountain (int num) {
        if (num == 1) {
            fountain1On = true;
            if (!fountain2On) {
                Timekeeper.StartTimer(quenchDelayTimer);
            }
        }
        if (num == 2) {
            fountain2On = true;
        }

        if (fountain1On && fountain2On) {
            pedestalBlock1.ShowStairs(true);
        }
    }

    private void QuenchAll () {
        if (fountain1On) {
            fountain1.Quench();
            fountain1On = false;
        }
        if (fountain2On) {
            fountain2.Quench();
            fountain2On = false;
        }
    }

}
