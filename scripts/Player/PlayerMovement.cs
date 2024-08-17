using System;
using Godot;
using Player;

public partial class PlayerMovement : Node {

    [Export]
    private PlayerVisual playerVisual;

    private Vector2 direction = new Vector2();

    private int facing = 1;

    [Export]
    private AnimatedSprite2D animatedSprite;

    [Export]
    private float speed = 1;
    private bool moving = false;


    public void SetDirection (Vector2 direction) {
        this.direction += direction;
        if (this.direction.X * facing < 0) {
            playerVisual.Parent.Scale = new Vector2(playerVisual.Parent.Scale.X * -1, playerVisual.Parent.Scale.Y);
            facing *= -1;
        }
        GD.Print(this.direction);
        if (this.direction.Length() > 0 && !this.moving) {
            this.moving = true;
            animatedSprite.Stop();
            animatedSprite.Play("walk");
        } else if (this.direction.Length() == 0 && this.moving) {
            this.moving = false;
            animatedSprite.Stop();
            animatedSprite.Play("blink");
        }
    }

    public bool IsMoving () {
        return this.direction.Length() > 0;
    }

    public override void _PhysicsProcess (double delta) {
        ((CharacterBody2D)playerVisual.Parent).Velocity = direction * speed;
        ((CharacterBody2D)playerVisual.Parent).MoveAndSlide();

    }

}
