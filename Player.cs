using Godot;
using System;

public partial class Player : Node2D
{
	private Marker2D cannonPivot;
	private float cannonCorrection = MathF.PI/2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.cannonPivot = this.GetChild<Marker2D>(0);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(this.GetGlobalMousePosition());
		Vector2 cannonDirection = (this.GetGlobalMousePosition() - this.Position).Normalized();
		cannonPivot.Rotation = GetAngleTo(cannonDirection) + cannonCorrection;
		
	}
}
