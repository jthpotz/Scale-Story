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

    private bool roar = false;

    public void SetDirection (Vector2 direction, bool release) {
        if (!release || this.direction.X != 0 || this.direction.Y != 0) this.direction += direction;

        if (this.direction.X * facing < 0) {
            playerVisual.Parent.Scale = new Vector2(playerVisual.Parent.Scale.X * -1, playerVisual.Parent.Scale.Y);
            facing *= -1;
        }
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

    public void StopMoving () {
        direction = new Vector2();
    }

    public override void _PhysicsProcess (double delta) {
        ((CharacterBody2D)playerVisual.Parent).Velocity = direction * speed;
        ((CharacterBody2D)playerVisual.Parent).MoveAndSlide();
        if (roar) {
            ((CharacterBody2D)playerVisual.Parent).Velocity = new Vector2(10, 10);
            ((CharacterBody2D)playerVisual.Parent).MoveAndSlide();
            roar = false;
        }
    }

    public void Roar () {
        roar = true;
    }

}
