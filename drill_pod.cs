using Godot;
using System;

public partial class drill_pod : RigidBody2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnBodyEntered(Node2D body)
	{
		GD.Print("Choco");

		if(body.GetGroups().Contains("Minable"))
		{
			GD.Print("Con un minable");
			this.Sleeping = true;
			this.GetChild<CpuParticles2D>(0).Emitting = true;
		}

	}
}
