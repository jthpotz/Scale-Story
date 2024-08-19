using System;
using Godot;
using Timers;
using Timer = Timers.Timer;

public partial class Room6Unlock : Node {

    [Export]
    private LightFountain fountain1;

    [Export]
    private LightFountain fountain2;

    [Export]
    private LightFountain fountain3;

    [Export]
    private PedestalBlock pedestalBlock;

    private bool fountain1On = false;

    private bool fountain2On = false;

    private bool fountain3On = false;

    private Timer quenchDelayTimer;

    public override void _Ready () {
        base._Ready();

        fountain1.SetLightAction(() => LightFountain(1));
        fountain2.SetLightAction(() => LightFountain(2));
        fountain3.SetLightAction(() => LightFountain(3));

        quenchDelayTimer = Timekeeper.AddTimer(2, QuenchAll, false, false);

    }

    public void LightFountain (int num) {
        if (num == 1) {
            fountain1On = true;
            if (!fountain2On) {
                Timekeeper.StartTimer(quenchDelayTimer);
            }
        }
        if (num == 2) {
            fountain2On = true;
        }
        if (num == 3) {
            fountain3On = true;
            if (!fountain1On) {
                Timekeeper.StartTimer(quenchDelayTimer);
            }
        }
        if (fountain1On && fountain2On && fountain3On) {
            pedestalBlock.ShowStairs(true);
        }
    }

    private void QuenchAll () {
        if (fountain1On) {
            fountain1On = false;
            fountain1.Quench();
        }
        if (fountain2On) {
            fountain2On = false;
            fountain2.Quench();
        }
        if (fountain3On) {
            fountain3On = false;
            fountain3.Quench();
        }
    }

}
