using Godot;
using System;

public partial class animation : Sprite2D
{
	  private Vector2 originalPosition;
    public float amplitude = 20.0f;
    public float speed = 1.0f;
    public float opacityMin = 0.5f;
    public float opacityMax = 1.0f;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
        originalPosition = Position;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
        float offset = amplitude * Mathf.Sin(speed * 2);
        Position = new Vector2(0 + offset, 0);

        float opacityRange = opacityMax - opacityMin;
        float opacityOffset = opacityRange * (Mathf.Sin(speed * 2) + 1.0f) / 2.0f;
        Modulate = new Color(1.0f, 1.0f, 1.0f, opacityMin + opacityOffset);
	}
}
