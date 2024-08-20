using System;
using Godot;
using Timers;
using Timer = Timers.Timer;

public partial class EvilWizardOpen : Control {

    [Export]
    private Sprite2D evilWizard;

    [Export]
    private PlayerMovement playerMovement;

    private bool visible = true;

    private Timer hideWizard;

    public override void _Ready () {
        base._Ready();

        hideWizard = Timekeeper.AddTimer(1.5f, evilWizard.Hide, false, false);

        GetTree().Paused = true;
        playerMovement.StopMoving();
    }

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Continue")) {
            if (visible) {
                visible = false;
                Hide();
                GetTree().Paused = false;
                Timekeeper.StartTimer(hideWizard);
            }
        }
    }
}
