using System;
using System.Collections.Generic;
using Godot;
using ListExtensions;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class PlayerSprite : AnimatedSprite2D
{
    
    [Export]
	private PlayerMovement playerMovement;
    
    [Export]
	private PlayerVisual playerVisual;

    [Export]
	private AnimatedSprite2D fireSprite;

    private bool big = false;
    private bool smol = false;

	public override void _Ready()
	{
		Play("walk");
        AnimationFinished += MyOnAnimationFinished;
	}

    private void MyOnAnimationFinished() {
        if (playerMovement.IsMoving()) {
            Play("walk");
        }
        else {
            var list = new List<string> { "blink", "look-around", "flap-twice" };
            Play(list.GetRandomItem());
        }
    }

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Roar")) {
            Roar();
        } 
        else if (@event.IsActionPressed("Grow")) {
            Grow();
        } 
        else if (@event.IsActionPressed("Shrink")) {
            Shrink();
        } 
    }

    private void Roar() {
        if (playerVisual.HasScale(ScaleColor.Pink.ToString())) {
            Play("roar");
            fireSprite.Play("flame");
        }
    }

    private void Grow() {
        if (big) {
            playerVisual.Parent.Scale /= 2;
            big = false;
        }
        else if (smol) {
            playerVisual.Parent.Scale *= 4;
            big = true;
            smol = false;
        }
        else {
            playerVisual.Parent.Scale *= 2;
            big = true;
        }
    }

    private void Shrink() {
        if (smol) {
            playerVisual.Parent.Scale *= 2;
            smol = false;
        }
        else if (big) {
            playerVisual.Parent.Scale /= 4;
            smol = true;
            big = false;
        }
        else {
            playerVisual.Parent.Scale /= 2;
            smol = true;
        }
    }
}