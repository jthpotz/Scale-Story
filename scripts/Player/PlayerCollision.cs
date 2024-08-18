using System;
using Godot;

public partial class PlayerCollision : CharacterBody2D {

    private int currentCollisionNumber = 1;

    public int CurrentCollisionNumber {
        get {
            return currentCollisionNumber;
        }
    }

    public void SetCollision (int number, bool value) {
        SetCollisionLayerValue(number, value);
        SetCollisionMaskValue(number, value);
    }

    public void ChangeCollisionNumber (int newNumber) {
        SetCollision(currentCollisionNumber, false);
        SetCollision(newNumber, true);
        currentCollisionNumber = newNumber;
    }

}
