using Godot;
using System;

public class WinnerTitle : Node2D
{
	private bool animated = false;
	private float resizingCoef = 0.5F;
	
	private float sizeMax = 1.2F;
	private float sizeMin = 0.7F;

	public override void _Ready()
	{
		GetNode<Sprite>("Player2Won").Hide();
		
		Sprite winnerTitle = GetNode<Sprite>("Player1Won");
		winnerTitle.Scale = new Vector2(sizeMax, sizeMax);
		winnerTitle.Modulate = new Color(1, 1, 1, 0);
	}

	public override void _Process(float delta)
	{
		Sprite winnerTitle = GetNode<Sprite>("Player1Won");
		
		
		if (animated)
		{
			if (winnerTitle.Modulate.a < 1)
			{
				winnerTitle.Modulate = new Color(1, 1, 1, winnerTitle.Modulate.a+1F*delta);
			}
			
			if (winnerTitle.Scale[0] >= sizeMax && winnerTitle.Scale[1] >= sizeMax  && resizingCoef > 0)
			{
				resizingCoef = -resizingCoef;
			}
			else if (winnerTitle.Scale[0] <= sizeMin && winnerTitle.Scale[1] <= sizeMin  && resizingCoef < 0)
			{
				resizingCoef = -resizingCoef;
			}
			winnerTitle.Scale = new Vector2(winnerTitle.Scale[0]+resizingCoef*delta, winnerTitle.Scale[1]+resizingCoef*delta);
		}
	}
	
	private void OnTitleTimerTimeout()
	{
		animated = true;
	}
}
