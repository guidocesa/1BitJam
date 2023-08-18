using Godot;
using System;

public partial class drill_pod : RigidBody2D
{
	private Timer myTimer;
	private bool active = true;
	private float angleCorrection = MathF.PI/2;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		this.myTimer = this.GetNode<Timer>("Timer");
		this.myTimer.Timeout += this.FinalExplosion;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if(Input.IsActionJustPressed("drill_direction_change") && this.active)
		{
			Vector2 newDirection = (GetGlobalMousePosition() - this.GlobalPosition).Normalized();
			this.LinearVelocity = newDirection * this.LinearVelocity.Length();
			this.Rotation = newDirection.Angle() + angleCorrection;
			
		}
	}

	public void OnBodyEntered(Node2D body)
	{
		this.Sleeping = true;
		this.active = false;

		if(body.GetGroups().Contains("Minable"))
		{
			this.GetNode<CpuParticles2D>("Mining").Emitting = true;
			this.GetNode<PointLight2D>("PointLight2D").Enabled = true;
		}
		else
		{
			this.GetNode<CpuParticles2D>("Explosion").Emitting = true;
			this.myTimer.Start(0.3);
		}
		
		this.CollisionLayer = 0;
		this.CollisionMask = 2;
	}

	private void FinalExplosion()
	{
		this.GetNode<Sprite2D>("Sprite2D").Visible = false;
		this.GetNode<CpuParticles2D>("Explosion").Emitting = false;
		this.myTimer.Timeout -= this.FinalExplosion;
		this.myTimer.Timeout += this.QueueFree;
		this.myTimer.Start(1);
	}
}
