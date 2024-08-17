using System;
using Godot;
using Player;

public partial class PlayerMovement : Node {

    [Export]
    private CharacterBody2D characterBody2D;

    private PlayerVisual playerVisual;

    private Vector2 direction = new Vector2();

    [Export]
    private float speed = 1;

    public void SetDirection (Vector2 direction) {
        this.direction += direction;
    }

    public override void _PhysicsProcess (double delta) {
        characterBody2D.Velocity = direction * speed;
        characterBody2D.MoveAndSlide();
    }

}
