using Godot;
using System;

public class StartButton : Button
{	
	private bool show = false;
	private bool animated = true;
	private float resizingCoef = 0.3F;
	
	
	public  override void _Ready(){
		Modulate = new Color(1, 1, 1, 0);
	}

	public override void _Process(float delta)
	{
		if (show && Modulate.a < 1)
		{
			Modulate = new Color(1, 1, 1, Modulate.a+1F*delta);
		}
		
		if (animated)
		{
			if (RectScale[0] >= 1 && RectScale[1] >= 1  && resizingCoef > 0)
			{
			resizingCoef = -resizingCoef;
			}
			else if (RectScale[0] <= 1+resizingCoef && RectScale[1] <= 1+resizingCoef && resizingCoef < 0)
			{
				resizingCoef = -resizingCoef;
			}
			RectScale = new Vector2(RectScale[0]+resizingCoef*delta, RectScale[1]+resizingCoef*delta);
		}
	}
	
	private void OnStartButtonPressed()
	{
		Hide();
	}
	
	private void OnButtonTimerTimeout()
	{
		show = true;
	}
	
	private void OnStartButtonMouseEntered()
	{
		animated = false;
		RectScale = new Vector2(1, 1);
	}
	
	private void OnStartButtonMouseExited()
	{
		animated = true;
	}
}
