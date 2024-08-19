using System;
using Godot;

public partial class Room7Unlock : Node {

    [Export]
    private PedestalBlock pedestalBlock;

    [Export]
    private ButtonPress button1;

    [Export]
    private ButtonPress button2;

    private int button1Pressed = 0;

    private int button2Pressed = 0;

    public override void _Ready () {
        base._Ready();

        button1.SetPressAction((Node2D other) => PressButton(1));
        button2.SetPressAction((Node2D other) => PressButton(2));

        button1.SetReleaseAction(() => ReleaseButton(1));
        button2.SetReleaseAction(() => ReleaseButton(2));
    }

    public void PressButton (int num) {
        if (num == 1) button1Pressed++;
        if (num == 2) button2Pressed++;

        if (button1Pressed > 0 && button2Pressed > 0) pedestalBlock.ShowStairs(true);

    }

    public void ReleaseButton (int num) {
        if (num == 1) button1Pressed--;
        if (num == 2) button2Pressed--;

        if (button1Pressed < 1 || button2Pressed < 1) pedestalBlock.HideStairs(true);
    }

}
