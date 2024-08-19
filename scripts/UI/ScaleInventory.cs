using System;
using System.Collections.Generic;
using Godot;
using Scale;
using ScaleColor = Scale.ScaleVisual.ScaleColor;

public partial class ScaleInventory : Control {

	private Dictionary<ScaleColor, TextureButton> btns = new Dictionary<ScaleColor, TextureButton>();

	private Dictionary<ScaleColor, string> defs = new Dictionary<ScaleColor, string>();
	private Dictionary<ScaleColor, string> acts = new Dictionary<ScaleColor, string>();

	private Control ScaleDefinition;

	public override void _Ready () {

		foreach (ScaleColor color in Enum.GetValues(typeof(ScaleColor))) {
			btns.Add(color, GetNode<TextureButton>("%"+color.ToString()+"ScaleBtn"));
			btns[color].Pressed += () => ShowDefinition(color);
		}

		ScaleDefinition = GetParent().GetNode<Control>("ScaleDefinition");

		defs[ScaleColor.Red] = "(verb) - to increase or decrease in size";
		defs[ScaleColor.Pink] = "Origin: First recorded in 1350-1400; Middle English noun scale, skale, “ladder, rung of a ladder,” from Latin scālae “ladder, stairs”; verb derivative of the noun";
		defs[ScaleColor.Orange] = "(noun) - Music. a succession of tones ascending or descending according to fixed intervals, especially such a series beginning on a particular note";
		defs[ScaleColor.Yellow] = "(verb) - Australian Informal. to ride on (public transportation) without paying the fare.";
		defs[ScaleColor.Green] = "(verb) - to adjust in amount according to a fixed scale or proportion (often followed by down or up)";
		defs[ScaleColor.Blue] = "(verb) - to climb; ascend; mount.";
		defs[ScaleColor.Purple] = "(noun) - Botany. Also called bud scale. a rudimentary body, usually a specialized leaf and often covered with hair, wax, or resin, enclosing an immature leaf bud.";
		defs[ScaleColor.Silver] = "(noun) - Zoology. one of the thin, flat, horny plates forming the covering of certain animals, as snakes, lizards, and pangolins.";

		acts[ScaleColor.Red] = "Press 'G' to scale up your character.";
		acts[ScaleColor.Orange] = "Press 'R' to roar a musical scale.";
		acts[ScaleColor.Green] = "Press 'V' to scale down your character.";
		acts[ScaleColor.Purple] = "This dragon scale grants the ability to scale vertical surfaces.";   
	}
	
	public override void _Input (InputEvent @event) {
		if (@event.IsActionPressed("Continue")) { 
			if (ScaleDefinition.Visible)
				ScaleDefinition.Hide();
		}
		// if (@event is InputEventMouseButton mb)
		// {
		//     if (mb.ButtonIndex == MouseButton.Left && mb.Pressed)
		//     {
		//         if (ScaleDefinition.Visible)
		//             ScaleDefinition.Hide();
		//     }
		// }

	}

	public void ShowDefinition (ScaleColor color) {
		ScaleDefinition.GetNode<TextureRect>("%ScaleImg").Texture = 
			GD.Load<Texture2D>(color.SpritePath());
		ScaleDefinition.GetNode<Label>("%ScaleDef").Text = defs[color];
		ScaleDefinition.GetNode<Label>("%ScaleAction").Text = acts.ContainsKey(color)? acts[color] : "";
		ScaleDefinition.Show();
	}

}
