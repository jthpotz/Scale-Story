using System;
using Godot;

public partial class Room7Unlock : Node {

    [Export]
    private PedestalBlock pedestalBlock;

    [Export]
    private ButtonPress button1;

    [Export]
    private ButtonPress button2;

    private bool button1Pressed = false;

    private bool button2Pressed = false;

    public override void _Ready () {
        base._Ready();

        button1.SetPressAction((Node2D other) => PressButton(1));
        button2.SetPressAction((Node2D other) => PressButton(2));

        button1.SetReleaseAction(() => ReleaseButton(1));
        button2.SetReleaseAction(() => ReleaseButton(2));
    }

    public void PressButton (int num) {
        if (num == 1) button1Pressed = true;
        if (num == 2) button2Pressed = true;

        if (button1Pressed && button2Pressed) pedestalBlock.ShowStairs(true);

    }

    public void ReleaseButton (int num) {
        if (num == 1) button1Pressed = false;
        if (num == 2) button2Pressed = false;

        if (!button1Pressed || !button2Pressed) pedestalBlock.HideStairs(true);
    }

}
