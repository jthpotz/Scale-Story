using System;
using Godot;

public partial class LightFountain : Node2D {

    [Export]
    private Area2D area;

    private Action onLight;

    public override void _Ready () {
        base._Ready();

        Hide();
        area.AreaEntered += Light;
    }

    public void SetLightAction (Action lightAction) {
        onLight = lightAction;
    }

    public void Light (Node2D other) {
        Show();
        onLight?.Invoke();
    }

}
