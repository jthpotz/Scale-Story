using System;
using Godot;

public partial class Room8Unlock : Node {

    [Export]
    private PedestalBlock pedestalBlock;

    [Export]
    private LightFountain fountain1;

    [Export]
    private ButtonPress button1;

    private bool fountainLit = false;

    private int buttonPressed = 0;

    public override void _Ready () {
        base._Ready();

        fountain1.SetLightAction(LightFountain);

        button1.SetPressAction((Node2D other) => PressButton());
        button1.SetReleaseAction(ReleaseButton);
    }

    public void LightFountain () {
        fountainLit = true;
        Check();
    }

    public void PressButton () {
        buttonPressed++;
        Check();
    }

    public void ReleaseButton () {
        buttonPressed--;
        Check();
    }

    private void Check () {
        if (fountainLit && buttonPressed > 0) pedestalBlock.ShowStairs(true);
        else pedestalBlock.HideStairs(true);
    }

}
