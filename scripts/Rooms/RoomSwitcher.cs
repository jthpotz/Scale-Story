using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

public partial class RoomSwitcher : Node {

    [Export]
    private Room[] rooms;
    [Export]
    private PlayerCollision player;

    public override void _Ready () {
        base._Ready();
        foreach (Room room in rooms) {
            room.SetRoomSwitcher(this);
        }
    }

    public void ChangeRoom (int fromRoom, int toRoom, Vector2 targetPosition) {
        rooms.Where(r => r.RoomNumber == fromRoom).First().Hide();
        rooms.Where(r => r.RoomNumber == toRoom).First().Show();
        player.ChangeCollisionNumber(toRoom);
        player.Position = targetPosition;
        AudioStreamPlayer2D music = GetNode<AudioStreamPlayer2D>("/root/GameWorld/AudioStreamPlayer2D");
        music.Stream = GD.Load<AudioStreamMP3>("res://resources/music/enter-room.mp3");
        music.Play();
    }

}
