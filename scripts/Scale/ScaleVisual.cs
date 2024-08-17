using System;
using Framework;
using Godot;
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

        [Export]
        private float bounceFactor = 10f;

        [Export]
        private float bounceDuration = 4f;

        [Export]
        private Area2D area;

        private float y = 0;

        private float initialY = 0;

        public override void _Ready () {
            Initialize(null);
        }

        public override void Initialize (State state) {
            base.Initialize(state);

            initialY = parent.Position.Y;

            area.AreaEntered += AreaHit;

            bounceTimer = Timekeeper.AddTimer(bounceDuration, TimerFunc, true, false);
            Timekeeper.StartTimer(bounceTimer);
        }

        public override void _Process (double delta) {
            y = Mathf.Sin(Timekeeper.GetPercentComplete(bounceTimer) * 2 * Mathf.Pi);
            y = Mathf.Pow(Mathf.Abs(y), 0.8f) * Mathf.Sign(y);
            parent.Position = new Vector2(parent.Position.X, initialY + bounceFactor * y);
        }

        protected override void SetName () {
            parent.Name = "Scale";
        }

        protected override void ExtendedSetTexture () {
            sprite.Texture = GD.Load<Texture2D>(color.SpritePath());
        }

        public void TimerFunc () {
        }

        private static float Easing (float x) {
            return -(Mathf.Cos(Mathf.Pi * x) - 1) / 2;
        }

        public void AreaHit (Area2D other) {
            GD.Print(other.GetParent().Name);
            if (other.GetParent().Name != "Player") return;
            GD.Print("Hit");
        }

    }
}