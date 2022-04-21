using Godot;
using System.Collections.Generic;

public class debug2D : Control
{
	public static Debug2D Instance { get; private set; }

	public override void _Ready()
	{
		base._Ready();
		Instance = this; 
	}

	public struct Vector {
		public Vector2 from; 
		public Vector2 to; 
		public Color color;
		public float width;
		public float arrowLength;
		public float arrowAngle;
	}

	[Export] private float defaultArrowLength = 10;
	[Export] private float defaultVectorWidth = 3;
	[Export] private float defaultArrowAngle = 120;

	private Queue<Vector> toDraw = new Queue<Vector>(50);

	public override void _Draw()
	{
		base._Draw();

		while (toDraw.Count > 0)
		{
			Vector vector = toDraw.Dequeue();
			DrawLine(vector.from, vector.to.MoveToward(vector.from, vector.arrowLength), vector.color, vector.width);
			DrawTriangle(vector.to, vector.from.DirectionTo(vector.to), vector.arrowLength, vector.arrowAngle, vector.color);
			Update();
		}
	}

	public void DrawVector(Vector2 from, Vector2 to, Color color, float width = -1, float arrowLength = -1, float arrowAngle = -1)
	{
		toDraw.Enqueue(new Vector()
		{
			from = from,
			to = to,
			color = color,
			width = width == -1 ? defaultVectorWidth : width,
			arrowLength = arrowLength == -1 ? defaultArrowLength : arrowLength,
			arrowAngle = arrowAngle == -1 ? defaultArrowAngle : arrowAngle,
		});
		Update();
	}

	public void DrawPosition(Vector2 position, Color color, float width = -1, float arrowLength = -1, float arrowAngle = -1)
	{
		DrawVector(Vector2.Zero, position, color, width, arrowLength, arrowAngle);
	}

	private void DrawTriangle(Vector2 tip, Vector2 direction, float length, float angle, Color color)
	{
		angle = Mathf.Deg2Rad(angle);
		float sideSize = Mathf.Tan(angle / 2) * length;
		Vector2 bottom = tip - direction.Normalized() * length;
		Vector2 right = bottom + direction.Rotated(Mathf.Pi / 2f) * sideSize;
		Vector2 left = bottom + direction.Rotated(-Mathf.Pi / 2f) * sideSize;
		DrawPolygon(new Vector2[] { tip, right, left }, new Color[]{color});
	}

	protected override void Dispose(bool disposing)
	{
		base.Dispose(disposing);

		if (disposing)
			Instance = null;
	}
}
