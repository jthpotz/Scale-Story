using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class RoomSwitcher : Node {

    [Export]
    private Room[] rooms;
    [Export]
    private CharacterBody2D player;

    public override void _Ready () {
        base._Ready();
        foreach (Room room in rooms) {
            room.SetRoomSwitcher(this);
        }
    }

    public void ChangeRoom (int fromRoom, int toRoom, Vector2 targetPosition) {
        player.SetCollisionMaskValue(fromRoom, false);
        rooms.Where(r => r.RoomNumber == fromRoom).First().Hide();
        player.SetCollisionMaskValue(toRoom, true);
        rooms.Where(r => r.RoomNumber == toRoom).First().Show();
        player.Position = targetPosition;
    }

}
