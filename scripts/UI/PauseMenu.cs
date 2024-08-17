using Godot;

public partial class PauseMenu : Control {

    [Export]
    private string targetScene = "";

    private Button ResumeButton { get; set; } = default;

    private Button MainMenuButton { get; set; } = default;

    private bool paused = false;

    public override void _Ready () {
        Hide();

        ResumeButton = GetNode<Button>("%Resume");
        MainMenuButton = GetNode<Button>("%MainMenu");

        ResumeButton.Pressed += PauseAction;
        MainMenuButton.Pressed += ReturnToMainMenu;
    }


    public override void _Input (InputEvent @event) {

        if (@event.IsActionPressed("Pause")) {
            PauseAction();
        }

    }

    private void PauseAction () {
        paused = !paused;
        GetTree().Paused = paused;
        if (paused) Show();
        else Hide();
    }

    private void ReturnToMainMenu () {
        PauseAction();
        GetTree().ChangeSceneToFile(targetScene);
    }

}
