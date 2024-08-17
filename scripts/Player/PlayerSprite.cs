using System;
using System.Collections.Generic;
using Godot;
using ListExtensions;

public partial class PlayerSprite : AnimatedSprite2D
{
    
    [Export]
	private PlayerMovement playerMovement;

    [Export]
	private AnimatedSprite2D fireSprite;

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
}