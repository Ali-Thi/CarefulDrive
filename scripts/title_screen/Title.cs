using Godot;
using System;

public class title : Sprite
{
	private bool animated = false;
	
	public  override void _Ready(){
		Scale = new Vector2(2, 2);
		Modulate = new Color(1, 1, 1, 0);
	}

	public override void _Process(float delta)
	{
		if (animated && Modulate.a < 1)
		{
			Modulate = new Color(1, 1, 1, Modulate.a+1F*delta);
		}
		
		if (animated && Scale[0] > 1 && Scale[1] > 1)
		{
			Scale= new Vector2(Scale[0]-1*delta, Scale[1]-1*delta);
		}
	}
	
	private void OnTitleTimerTimeout()
	{
		animated = true;
	}
}
