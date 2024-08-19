using System;
using System.Collections.Generic;
using Godot;
using ListExtensions;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class PlayerSprite : AnimatedSprite2D {

    [Export]
    private PlayerMovement playerMovement;

    [Export]
    private PlayerVisual playerVisual;

    [Export]
    private AnimatedSprite2D fireSprite;

    [Export]
    private FireArea fireArea;

    private AudioStreamPlayer2D music;

    private bool big = false;
    private bool smol = false;

    public override void _Ready () {
        Play("walk");
        AnimationFinished += MyOnAnimationFinished;
        fireSprite.AnimationFinished += FireAnimDone;
        music = GetNode<AudioStreamPlayer2D>("/root/GameWorld/AudioStreamPlayer2D");
    }

    private void FireAnimDone () {
        fireArea.DisableFireArea();
    }

    private void MyOnAnimationFinished () {
        if (playerMovement.IsMoving()) {
            Play("walk");
        } else {
            var list = new List<string> { "blink", "look-around", "flap-twice" };
            Play(list.GetRandomItem());
        }
    }

    public override void _Input (InputEvent @event) {
        if (@event.IsActionPressed("Roar")) {
            Roar();
        } else if (@event.IsActionPressed("Grow")) {
            Grow();
        } else if (@event.IsActionPressed("Shrink")) {
            Shrink();
        }
    }

    private void Roar () {
        if (playerVisual.HasScale(ScaleColor.Orange.ToString())) {
            Play("roar");
            fireSprite.Play("flame");
            fireArea.EnableFireArea();
            playerMovement.Roar();
            music.Stream = GD.Load<AudioStreamMP3>("res://resources/music/fireball.mp3");
            music.Play();
        }
    }

    private void Grow () {
        if (!playerVisual.HasScale(ScaleColor.Red.ToString()))
            return;
        if (big) {
            playerVisual.Parent.Scale /= 2;
            big = false;
        } else if (smol) {
            playerVisual.Parent.Scale *= 4;
            big = true;
            smol = false;
        } else {
            playerVisual.Parent.Scale *= 2;
            big = true;
        }
        if (big) {
            music.Stream = GD.Load<AudioStreamWav>("res://resources/music/grow.wav");
            music.Play();
        }
    }

    private void Shrink () {
        if (!playerVisual.HasScale(ScaleColor.Green.ToString()))
            return;
        if (smol) {
            playerVisual.Parent.Scale *= 2;
            smol = false;
        } else if (big) {
            playerVisual.Parent.Scale /= 4;
            smol = true;
            big = false;
        } else {
            playerVisual.Parent.Scale /= 2;
            smol = true;
        }
        if (smol) {
            music.Stream = GD.Load<AudioStreamWav>("res://resources/music/shrink.wav");
            music.Play();
        }
    }
}