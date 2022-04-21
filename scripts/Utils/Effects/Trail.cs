using Com.IsartDigital.OneButtonGame;
using Godot;
using System;

namespace Com.IsartDigital.Utils.Effects {

	public class Trail : Line2D
	{
        [Export]
        private NodePath targetPath;
        private Node2D target;

        public float speed;

        public override void _Ready()
        {
            target = (Node2D)GetNode(targetPath);
            Position = Vector2.Zero;
            //speed = target.speed;
        }  

        public override void _Process(float pDelta)
        {
            base._Process(pDelta);

            if (!Visible) return;

            Position+=new Vector2(speed * pDelta,0);
            AddPoint(ToLocal(target.GlobalPosition));

            if (GetPointCount() > 30) RemovePoint(0);

        }

        public void Clear ()
        {
            Points = null;
        }

    }

}