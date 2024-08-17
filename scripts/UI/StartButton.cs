using System;
using Godot;

public partial class StartButton : Button {

    [Export]
    private string TargetScene = "";

    public override void _EnterTree () {
        base._EnterTree();

        Pressed += ChangeScene;
    }

    public override void _ExitTree () {
        base._ExitTree();

        Pressed -= ChangeScene;
    }

    private void ChangeScene () {
        GetTree().ChangeSceneToFile(TargetScene);
    }

}
