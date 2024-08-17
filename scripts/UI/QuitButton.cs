using Godot;

public partial class QuitButton : Button {

    public override void _EnterTree () {
        base._EnterTree();

        Pressed += Quit;
    }

    public override void _ExitTree () {
        base._ExitTree();

        Pressed -= Quit;
    }

    private void Quit () {
        GetTree().Root.PropagateNotification((int)NotificationWMCloseRequest);
        GetTree().Quit();
    }

}
