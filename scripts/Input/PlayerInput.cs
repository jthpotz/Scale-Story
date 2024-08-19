using System;
using Godot;

public partial class PlayerInput : Node {

    [Export]
    private PlayerMovement playerMovement;

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Up")) {
            playerMovement.SetDirection(Vector2.Up, false);
        } else if (@event.IsActionReleased("Up")) {
            playerMovement.SetDirection(Vector2.Down, true);
        }

        if (@event.IsActionPressed("Down")) {
            playerMovement.SetDirection(Vector2.Down, false);
        } else if (@event.IsActionReleased("Down")) {
            playerMovement.SetDirection(Vector2.Up, true);
        }

        if (@event.IsActionPressed("Left")) {
            playerMovement.SetDirection(Vector2.Left, false);
        } else if (@event.IsActionReleased("Left")) {
            playerMovement.SetDirection(Vector2.Right, true);
        }

        if (@event.IsActionPressed("Right")) {
            playerMovement.SetDirection(Vector2.Right, false);
        } else if (@event.IsActionReleased("Right")) {
            playerMovement.SetDirection(Vector2.Left, true);
        }
    }

}
