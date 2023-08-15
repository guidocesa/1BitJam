using Godot;
using System;

public partial class Player : Node2D
{
	private const int SHOOTING_SPEED = 400;


	private Marker2D cannonPivot;
	private float cannonCorrection = MathF.PI/2;
	private PackedScene drillScene;
	private PackedScene radarScene;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.cannonPivot = this.GetChild<Marker2D>(0);
		this.drillScene = ResourceLoader.Load<PackedScene>("res://drill_pod.tscn");
		this.radarScene = ResourceLoader.Load<PackedScene>("res://radar_pod.tscn");

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		GD.Print(this.GetGlobalMousePosition());
		Vector2 cannonDirection = (this.GetGlobalMousePosition() - this.Position).Normalized();
		cannonPivot.Rotation = GetAngleTo(cannonDirection) + cannonCorrection;
		
		if(Input.IsActionJustPressed("shoot_drill"))
		{
			RigidBody2D newDrill = drillScene.Instantiate<RigidBody2D>();
			newDrill.Position = this.Position;
			newDrill.Rotation = cannonPivot.Rotation;
			newDrill.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			this.AddChild(newDrill);
		}
		if(Input.IsActionJustPressed("shoot_radar"))
		{
			RigidBody2D newRadar = radarScene.Instantiate<RigidBody2D>();
			newRadar.Position = this.Position;
			newRadar.Rotation = cannonPivot.Rotation;
			newRadar.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			this.AddChild(newRadar);
		}
	}
}
