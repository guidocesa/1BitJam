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
		Vector2 cannonDirection = (this.GetGlobalMousePosition() - this.GlobalPosition).Normalized();
		cannonPivot.Rotation = cannonDirection.Angle() + cannonCorrection;
		
		//Quizas los taladros no deberian ser hijos de la base, checkear;
		if(Input.IsActionJustPressed("shoot_drill"))
		{
			RigidBody2D newDrill = drillScene.Instantiate<RigidBody2D>();
			//newDrill.GlobalPosition = this.GlobalPosition;
			newDrill.Rotation = cannonPivot.Rotation;
			newDrill.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			this.AddChild(newDrill);
		}
		if(Input.IsActionJustPressed("shoot_radar"))
		{
			RigidBody2D newRadar = radarScene.Instantiate<RigidBody2D>();
			//newRadar.GlobalPosition = this.GlobalPosition;
			newRadar.Rotation = cannonPivot.Rotation;
			newRadar.LinearVelocity = new Vector2(cannonDirection.X, cannonDirection.Y) * SHOOTING_SPEED;
			this.AddChild(newRadar);
			newRadar.Connect("body_entered", Callable.From(OnRadarCollided));
		}
	}

	public void OnRadarCollided()
	{
		GD.Print("Aun funca");
	}
}
