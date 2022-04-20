using Godot;

public class Player : Area2D
{
	private float speed = 0f;
	private const float SPEED_MIN = 0f;
	private const float SPEED_MAX = 5000f;
	private const float ACCELERATION = 1000f;
	private const float DECELERATION = 2.5f;
	private float counterOfFramePressed = 0f;
	private float counterOfFrameUnpressed = 0f;
	public bool speeding = false;
	public bool slowing = false;

	private Path2D path2D;
	private Curve2D curve2D;

	public override void _Ready()
	{
		path2D = GetParent<Path2D>();
		curve2D = path2D.Curve;

		Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		float computedOffset = (curve2D.GetClosestOffset(Position) + speed * delta) % curve2D.GetBakedLength();
		Position = curve2D.InterpolateBaked(computedOffset);
		//Debug2D.Instance.DrawVector(Vector2.Zero, ToGlobal(Vector2.Zero), new Color(0, .8f, .8f));
//		GD.Print(speed + "speed");
		//GD.Print(counterOfFramePressed + "pressed");
		//GD.Print(counterOfFrameUnpressed + "unpressed");
	}

	public void SpeedUp()
	{ 
		//speeding = true;
		//slowing = false;

		if (speed < SPEED_MAX)
		{
			//++counterOfFramePressed;
			speed += ACCELERATION;
		}

		//if(counterOfFrameUnpressed != 0)
		//{
		//	counterOfFrameUnpressed = 0f;
		//}

	}

	public void SpeedDown()
	{
		//slowing = true;
		//speeding = false;

		if(speed > SPEED_MIN)
		{
			//++counterOfFrameUnpressed;
			speed -= DECELERATION;
		}

		//if(counterOfFramePressed != 0)
		//{
		//	counterOfFramePressed = 0f;
		//}

		//GD.Print(counterOfFrameUnpressed);
		
	}
}
