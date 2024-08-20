using System;
using Godot;

public partial class EvilWizardEnd : Control {

    [Export]
    private PlayerMovement playerMovement;

    private bool visible = false;

    public override void _Ready () {
        base._Ready();

        Hide();
    }

    public void ShowEnd () {
        Show();
        GetTree().Paused = true;
        playerMovement.StopMoving();
        visible = true;
    }

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Continue")) {
            if (visible) {
                visible = false;
                Hide();
                GetTree().Paused = false;
            }
        }
    }

}
