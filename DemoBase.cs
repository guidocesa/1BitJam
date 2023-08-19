using Godot;
using System;

public partial class DemoBase : Node2D
{
	private const int SHOOTING_SPEED = 400;


	private Marker2D cannonPivot;
	private float cannonCorrection = MathF.PI/2;
	private PackedScene drillScene;
	private PackedScene radarScene;
	private int maxCollisions = 3;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.cannonPivot = this.GetChild<Marker2D>(0);
		this.drillScene = ResourceLoader.Load<PackedScene>("res://drill_pod.tscn");
		this.radarScene = ResourceLoader.Load<PackedScene>("res://radar_pod.tscn");

	}

	public void _Fire(){
		
		RigidBody2D newRadar = radarScene.Instantiate<RigidBody2D>();
		newRadar.Rotation = cannonPivot.Rotation;
		newRadar.LinearVelocity = new Vector2(GetCannonDirection().X, GetCannonDirection().Y) * SHOOTING_SPEED;
		this.AddChild(newRadar);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 cannonDirection = GetCannonDirection();
		cannonPivot.Rotation = cannonDirection.Angle() + cannonCorrection;
	}

	private Vector2 GetCannonDirection(){
		return (this.GetGlobalMousePosition() - this.GlobalPosition).Normalized();
	}
}