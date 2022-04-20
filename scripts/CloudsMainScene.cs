using Godot;
using System;

public class CloudsMainScene : Sprite
{
	private bool animated = true;
	private const float coordYMiddleOfScreen = 360;
	private const float coordYOutOfScreenByUp = -480;
	private float coordYAimed = coordYOutOfScreenByUp;
	private float translationSpeed = 500F;
	
	public override void _Process(float delta)
	{
		if (animated)
		{
			Position = new Vector2(Position.x, Position.y-translationSpeed * delta);
			
			if (Position.y <= coordYAimed && coordYAimed < 0)
			{
				animated = false;
			}
			else if (Position.y >= coordYAimed && coordYAimed > 0)
			{
				GetTree().ChangeScene("res://scenes/endScreen.tscn");
			}
		}
	}
	
	private void OnTimerTimeout()
	{
		coordYAimed = coordYMiddleOfScreen;
		translationSpeed = -translationSpeed;
		animated = true;
	}
}
