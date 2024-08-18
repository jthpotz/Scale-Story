using System;
using Godot;

public partial class Room : Node2D {

    [Export]
    private int roomNumber;

    public int RoomNumber {
        get {
            return roomNumber;
        }
    }

    private RoomSwitcher roomSwitcher;

    public void SetRoomSwitcher (RoomSwitcher rs) {
        roomSwitcher = rs;
    }

    public void ChangeRoom (int toRoom, Vector2 targetPosition) {
        roomSwitcher.ChangeRoom(roomNumber, toRoom, targetPosition);
    }


}
