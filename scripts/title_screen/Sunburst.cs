using Godot;
using System;

public class sunburst : Sprite
{
	private bool animated = false;
	
	public  override void _Ready(){
		Modulate = new Color(1, 1, 1, 0);
		Scale = new Vector2(0, 0);
	}

	public override void _Process(float delta)
	{
		if (animated)
		{
			if (Modulate.a < 1)
			{
				Modulate = new Color(1, 1, 1, Modulate.a+1F*delta);
			}
			if (Scale[0] <= 1F && Scale[1] <= 1F)
			{
				Scale = new Vector2(Scale[0]+1F*delta, Scale[1]+1F*delta);
			}
			Rotate((float)Math.PI/10 * delta);
		}
	}
	
	private void OnSunburstTimerTimeout()
	{
		animated = true;
	}
}
