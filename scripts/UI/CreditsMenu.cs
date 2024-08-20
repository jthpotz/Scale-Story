using Godot;

public partial class CreditsMenu : Control {

    [Export]
    private string targetScene = "";

    private Button MainMenuButton { get; set; } = default;

    public override void _Ready () {

        MainMenuButton = GetNode<Button>("%MainMenu");

        MainMenuButton.Pressed += ReturnToMainMenu;
    }

    private void ReturnToMainMenu () {
        GetTree().ChangeSceneToFile(targetScene);
    }

}
