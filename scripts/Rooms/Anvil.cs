using Godot;
using Player;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class Anvil : CharacterBody2D {

    [Export]
    private Room room;

    [Export]
    private Area2D area;

    [Export]
    private Area2D trigger;

    private Vector2 areaSize;

    [Export]
    private Sprite2D sprite;

    [Export]
    private bool showOnStart = true;

    private float speed = 200;

    private Vector2 direction = new Vector2();

    private bool stop = false;

    private bool shown = false;

    public override void _Ready () {
        base._Ready();

        sprite.Hide();
        if (showOnStart) ShowAnvil();

    }

    public void ShowAnvil () {
        if (shown) return;
        shown = true;

        SetCollisionLayerValue(room.RoomNumber, true);
        SetCollisionMaskValue(room.RoomNumber, true);

        SetCollisionLayerValue(32 - room.RoomNumber, true);
        SetCollisionMaskValue(32 - room.RoomNumber, true);

        area.BodyEntered += PushAnvil;

        areaSize = ((RectangleShape2D)area.GetNode<CollisionShape2D>("CollisionShape2D").Shape).Size;

        trigger.BodyEntered += TriggerEntered;

        trigger.SetCollisionLayerValue(room.RoomNumber, true);
        trigger.SetCollisionMaskValue(room.RoomNumber, true);
        sprite.Show();
    }

    public override void _PhysicsProcess (double delta) {
        if (stop) {
            Velocity *= 0.1f;
        } else {
            Velocity = direction * speed;
        }
        MoveAndSlide();
        if (stop) {
            Velocity = new Vector2();
            stop = false;
        }
    }

    public void PushAnvil (Node2D other) {
        if (other.Name == "PlayerState 0") {
            CharacterBody2D player = (CharacterBody2D)other;

            CollisionShape2D playerShape = player.GetNode<CollisionShape2D>("CollisionShape2D");
            Vector2 playerSize = ((RectangleShape2D)playerShape.Shape).Size;

            if (player.GlobalPosition.X + (playerSize.X / 2) < GlobalPosition.X - (areaSize.X / 2)) {
                //if (Mathf.Abs(player.GlobalPosition.Y - GlobalPosition.Y) < playerSize.X / 2) { // No corners
                // GD.Print("Left"); // Side player on
                direction = Vector2.Right; // Move in opposite direction of side
                                           //}
            }
            if (player.GlobalPosition.X - (playerSize.X / 2) > GlobalPosition.X + (areaSize.X / 2)) {
                //if (Mathf.Abs(player.GlobalPosition.Y - GlobalPosition.Y) < playerSize.X / 2) { // No corners
                // GD.Print("Right"); // Side player on
                direction = Vector2.Left; // Move in opposite direction of side
                                          //}
            }
            if (player.GlobalPosition.Y + (playerSize.Y / 2) < GlobalPosition.Y - (areaSize.Y / 2)) {
                //if (Mathf.Abs(player.GlobalPosition.X - GlobalPosition.X) < playerSize.Y / 2) { // No corners
                // GD.Print("Above"); // Side player on
                direction = Vector2.Down; // Move in opposite direction of side
                                          //}
            }
            if (player.GlobalPosition.Y - (playerSize.Y / 2) > GlobalPosition.Y + (areaSize.Y / 2)) {
                //if (Mathf.Abs(player.GlobalPosition.X - GlobalPosition.X) < playerSize.Y / 2) { // No corners
                // GD.Print("Below"); // Side player on
                direction = Vector2.Up; // Move in opposite direction of side
                                        //}
            }

            // direction += Vector2.Left;
            // GD.Print("push " + other.Name);
        } else if (other.Name == "SlidingCollision") {
            stop = true;
        }
    }

    public void TriggerEntered (Node2D other) {
        if (other.Name != "PlayerState 0") return;

        if (other.GetNode<PlayerVisual>("PlayerVisual").PlayerSprite.Big) {
            area.SetCollisionLayerValue(room.RoomNumber, true);
            area.SetCollisionMaskValue(room.RoomNumber, true);
        } else {
            area.SetCollisionLayerValue(room.RoomNumber, false);
            area.SetCollisionMaskValue(room.RoomNumber, false);
        }

    }


}
