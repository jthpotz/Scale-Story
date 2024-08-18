using System;
using Framework;
using Godot;
using Player;
using Timers;
using ScaleColor = Scale.ScaleVisual.ScaleColor;
using Timer = Timers.Timer;

namespace Scale {

    public static class ScaleColorExtension {
        public static string SpritePath (this ScaleColor scaleColor) {
            return scaleColor switch
            {
                ScaleColor.Red => "res://resources/scales/scale-red.png",
                ScaleColor.Orange => "res://resources/scales/scale-orange.png",
                ScaleColor.Yellow => "res://resources/scales/scale-yellow.png",
                ScaleColor.Green => "res://resources/scales/scale-green.png",
                ScaleColor.Blue => "res://resources/scales/scale-blue.png",
                ScaleColor.Purple => "res://resources/scales/scale-purple.png",
                ScaleColor.Pink => "res://resources/scales/scale-pink.png",
                ScaleColor.Silver => "res://resources/scales/scale-silver.png",
                _ => "res://resources/scales/scale-green.png",
            };
        }
    }

    public partial class ScaleVisual : Visual2D {

        public enum ScaleColor {
            Red,
            Orange,
            Yellow,
            Green,
            Blue,
            Purple,
            Pink,
            Silver
        }

        public override ScaleState State {
            get {
                return (ScaleState)state;
            }
        }

        [Export]
        private ScaleColor color;

        private Timer bounceTimer;

        private Timer shrinkTimer;

        [Export]
        private float bounceFactor = 10f;

        [Export]
        private float bounceDuration = 4f;

        [Export]
        private Area2D area;

        [Export]
        private float riseAmount = 10;

        [Export]
        private float shrinkDuration = 2;

        [Export]
        private float shrinkTarget = 0.25f;

        private Vector2 shrinkVector;

        private float y = 0;

        private float initialY = 0;

        private Vector2 initialScale;

        private bool bouncing = true;

        private bool shrinking = false;

        public override void _Ready () {
            Initialize(null);
        }

        public override void Initialize (State state) {
            base.Initialize(state);

            initialY = parent.Position.Y;
            initialScale = parent.Scale;
            shrinkVector = new Vector2(shrinkTarget, shrinkTarget);

            area.BodyEntered += AreaHit;

            bounceTimer = Timekeeper.AddTimer(bounceDuration, TimerFunc, true, false);
            shrinkTimer = Timekeeper.AddTimer(shrinkDuration, DoneShrink, false, false);
            Timekeeper.StartTimer(bounceTimer);
        }

        public override void _Process (double delta) {
            if (bouncing) Bounce();
            if (shrinking) Shrink();
        }

        private void Bounce () {
            y = Mathf.Sin(Timekeeper.GetPercentComplete(bounceTimer) * 2 * Mathf.Pi);
            y = Mathf.Pow(Mathf.Abs(y), 0.8f) * Mathf.Sign(y);
            parent.Position = new Vector2(parent.Position.X, initialY + bounceFactor * y);
        }

        private void Shrink () {
            parent.Scale = initialScale - ((initialScale - shrinkVector) * Timekeeper.GetPercentComplete(shrinkTimer));
            parent.Position = new Vector2(parent.Position.X, initialY + y + (y - riseAmount) * Timekeeper.GetPercentComplete(shrinkTimer));
        }

        protected override void SetName () {
            parent.Name = "Scale";
        }

        protected override void ExtendedSetTexture () {
            sprite.Texture = GD.Load<Texture2D>(color.SpritePath());
        }

        public void TimerFunc () {
        }

        public void DoneShrink () {
            parent.QueueFree();
            string path = "/root/Game/ScaleCanvasLayer/ScaleInventory/CenterContainer/PanelContainer/HBoxContainer/" + color + "ScaleBtn";
            GetNode<CanvasItem>(path).Show();
        }

        private static float Easing (float x) {
            return -(Mathf.Cos(Mathf.Pi * x) - 1) / 2;
        }

        public void AreaHit (Node2D other) {
            if (other.Name != "PlayerState 0") return;
            bouncing = false;
            shrinking = true;
            Timekeeper.StartTimer(shrinkTimer);
            other.GetNode<PlayerVisual>("PlayerVisual").AddScale(color.ToString());
        }

    }
}