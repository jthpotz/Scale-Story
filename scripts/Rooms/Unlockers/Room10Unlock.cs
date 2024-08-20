using System;
using Godot;
using Timers;
using Timer = Timers.Timer;

public partial class Room10Unlock : Node {

    [Export]
    private LightFountain fountain1;

    [Export]
    private LightFountain fountain2;

    [Export]
    private LightFountain fountain3;

    [Export]
    private LightFountain fountain4;

    [Export]
    private LightFountain fountain5;

    [Export]
    private LightFountain fountain6;

    [Export]
    private ButtonPress button1;

    [Export]
    private ButtonPress button2;

    [Export]
    private ButtonPress button3;

    [Export]
    private ButtonPress button4;

    [Export]
    private ButtonPress button5;

    [Export]
    private PedestalBlock pedestalBlock1;

    [Export]
    private PedestalBlock pedestalBlock2;

    [Export]
    private PedestalBlock pedestalBlock3;

    [Export]
    private Anvil anvil1;

    [Export]
    private Anvil anvil2;

    [Export]
    private DoorBlock doorBlock1;

    private bool fountain1On = false;

    private bool fountain2On = false;

    private bool fountain3On = false;

    private bool fountain4On = false;

    private bool fountain5On = false;

    private bool fountain6On = false;

    private Timer quenchGroup1Timer;

    private Timer quenchGroup2Timer;

    private int button2Pressed = 0;

    private int button4Pressed = 0;

    public override void _Ready () {
        base._Ready();

        button5.SetPressAction((Node2D other) => doorBlock1.DisableDoorBlock(true));

        button1.SetPressAction((Node2D other) => anvil1.ShowAnvil());
        button3.SetPressAction((Node2D other) => anvil2.ShowAnvil());

        fountain1.SetLightAction(() => LightFountainGroup1(1));
        fountain2.SetLightAction(() => LightFountainGroup1(2));
        fountain3.SetLightAction(() => LightFountainGroup1(3));
        quenchGroup1Timer = Timekeeper.AddTimer(1, QuenchAllGroup1, false, false);

        fountain4.SetLightAction(() => LightFountainGroup2(4));
        fountain5.SetLightAction(() => LightFountainGroup2(5));
        fountain6.SetLightAction(() => LightFountainGroup2(6));
        quenchGroup2Timer = Timekeeper.AddTimer(1, QuenchAllGroup2, false, false);

        button2.SetPressAction((Node2D other) => PressButton(2));
        button2.SetReleaseAction(() => ReleaseButton(2));

        button4.SetPressAction((Node2D other) => PressButton(4));
        button4.SetReleaseAction(() => ReleaseButton(4));
    }

    private void LightFountainGroup1 (int num) {
        if (num == 1) {
            fountain1On = true;
            if (!fountain2On) {
                Timekeeper.StartTimer(quenchGroup1Timer);
            }
        }
        if (num == 2) {
            fountain2On = true;
        }
        if (num == 3) {
            fountain3On = true;
            if (!fountain1On) {
                Timekeeper.StartTimer(quenchGroup1Timer);
            }
        }

        if (fountain1On && fountain2On && fountain3On) {
            pedestalBlock1.ShowStairs(true);
        }
    }

    private void QuenchAllGroup1 () {
        if (fountain1On) {
            fountain1.Quench();
            fountain1On = false;
        }
        if (fountain2On) {
            fountain2.Quench();
            fountain2On = false;
        }
        if (fountain3On) {
            fountain3.Quench();
            fountain3On = false;
        }
    }

    private void LightFountainGroup2 (int num) {
        if (num == 4) {
            fountain4On = true;
            if (!fountain6On) {
                Timekeeper.StartTimer(quenchGroup2Timer);
            }
        }
        if (num == 5) {
            fountain5On = true;
            if (!fountain4On) {
                Timekeeper.StartTimer(quenchGroup2Timer);
            }
        }
        if (num == 6) {
            fountain6On = true;
        }

        if (fountain4On && fountain5On && fountain6On) {
            pedestalBlock2.ShowStairs(true);
        }
    }

    private void QuenchAllGroup2 () {
        if (fountain4On) {
            fountain4.Quench();
            fountain4On = false;
        }
        if (fountain5On) {
            fountain5.Quench();
            fountain5On = false;
        }
        if (fountain6On) {
            fountain6.Quench();
            fountain6On = false;
        }
    }

    public void PressButton (int num) {
        if (num == 2) button2Pressed++;
        if (num == 4) button4Pressed++;
        Check();
    }

    public void ReleaseButton (int num) {
        if (num == 2) button2Pressed--;
        if (num == 4) button4Pressed--;
        Check();
    }

    private void Check () {
        if (button2Pressed > 0 && button4Pressed > 0) pedestalBlock3.ShowStairs(true);
        else pedestalBlock3.HideStairs(true);
    }
}
