using Godot;

public class Player : Area2D
{
	public float speed = 0f;
	private const float SPEED_MIN = 0f;
	private const float SPEED_MAX = 3500f;
	//public const float SPEED_MAX = 2000f;
	public const float PRINTED_SPEED_SLOW = SPEED_MAX/3;
	public const float PRINTED_SPEED_MIDDLE = (SPEED_MAX / 3)*2;
	//private const float PRINTED_SPEED_DANGEROUS = SPEED_MAX / 3;
	private const float ACCELERATION = 250f;
	private const float DECELERATION = 2.5f;
	//private float counterOfFramePressed = 0f;
	//private float counterOfFrameUnpressed = 0f;
	public bool speeding = false;
	public bool slowing = false;
	private Vector2 start;
	private Vector2 scaled = new Vector2(1.5f, 1.5f);
	public Vector2 startScale = new Vector2(1, 1);
	public int round;
	public bool lost = false;
	public bool scaleMore = false;
	private int isLosing = 0;
	private const int COUNTER_TO_LOOSE = 500;
	public Color colo;
	public bool trueColor = true;

	//public float speed  = 0;

	private Path2D path2D;
	private Curve2D curve2D;

	public override void _Ready()
	{
		path2D = GetParent<Path2D>();
		curve2D = path2D.Curve;

		start = new Vector2(Position.x, Position.y);
		colo = Modulate;
	}

	public override void _Process(float delta)
	{
		base._Process(delta);
		float computedOffset = (curve2D.GetClosestOffset(Position) + speed * delta) % curve2D.GetBakedLength();
		Position = curve2D.InterpolateBaked(computedOffset);

		speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX+50);
		//speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);

		//Debug2D.Instance.DrawVector(Vector2.Zero, ToGlobal(Vector2.Zero), new Color(0, .8f, .8f));

		//if(Position == start)
		//{
		//	++round;
		//}
		//GD.Print(speed >= SPEED_MAX);

		if (speed >= SPEED_MAX)
		{
			++isLosing;
			

			if(isLosing == COUNTER_TO_LOOSE)
			{
				lost = true;
			}
		}

		if (isLosing > 0 && speed < SPEED_MAX)
		{
			--isLosing;
		}

		if (scaleMore)
		{
			Scale = scaled;
		}
		else
		{
			Scale = startScale;
		}

		//GD.Print(speed);

		GD.Print(speed);

	}

	public void SpeedUp()
	{ 
		if (speed < SPEED_MAX)
		{
			speed += ACCELERATION;
			scaleMore = true;
			
			//speed += 100;

			//speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);
			//speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);
		}
	}

	public void SpeedDown()
	{
		if(speed > SPEED_MIN)
		{
			speed -= DECELERATION;
			//speed -= 50;

			//speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);
			//speed = Mathf.Clamp(speed, SPEED_MIN, SPEED_MAX);
		}
	}
}
