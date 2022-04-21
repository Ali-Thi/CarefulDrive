using Godot;
using System;

public class CloudsTitleScene : Sprite
{
	private bool animated = false;
	private const float coordYMiddleOfScreen = 360;
	private float translationSpeed = 500F;
	
	public override void _Process(float delta)
	{
		if (animated)
		{
			if (Position.y > coordYMiddleOfScreen)
			{
				Position = new Vector2(Position.x, Position.y-translationSpeed * delta);
				if (Position.y <= coordYMiddleOfScreen)
				{
					GetTree().ChangeScene("res://scenes/main.tscn");
				}
			}
		}
	}
	
	private void OnStartButtonPressed()
	{
		animated = true;
	}
}
