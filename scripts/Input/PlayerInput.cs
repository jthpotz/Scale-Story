using System;
using Godot;

public partial class PlayerInput : Node {

    [Export]
    private PlayerMovement playerMovement;

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Up")) {
            playerMovement.SetDirection(Vector2.Up);
        } else if (@event.IsActionReleased("Up")) {
            playerMovement.SetDirection(Vector2.Down);
        }

        if (@event.IsActionPressed("Down")) {
            playerMovement.SetDirection(Vector2.Down);
        } else if (@event.IsActionReleased("Down")) {
            playerMovement.SetDirection(Vector2.Up);
        }

        if (@event.IsActionPressed("Left")) {
            playerMovement.SetDirection(Vector2.Left);
        } else if (@event.IsActionReleased("Left")) {
            playerMovement.SetDirection(Vector2.Right);
        }

        if (@event.IsActionPressed("Right")) {
            playerMovement.SetDirection(Vector2.Right);
        } else if (@event.IsActionReleased("Right")) {
            playerMovement.SetDirection(Vector2.Left);
        }
    }

}
