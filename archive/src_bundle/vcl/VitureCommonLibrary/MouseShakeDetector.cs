using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace VitureCommonLibrary;

public static class MouseShakeDetector
{
	private struct MousePoint
	{
		public Point position;

		public DateTime timestamp;

		public MousePoint(Point pos, DateTime time)
		{
			position = pos;
			timestamp = time;
		}
	}

	private static DateTime lastShakeTime = DateTime.MinValue;

	private static bool isShaking = false;

	private const int ShakeGap = 3000;

	private static readonly List<MousePoint> mouseTrail = new List<MousePoint>();

	private const int TrailDurationMs = 1000;

	private const double MinShakePathLength = 200.0;

	public static bool IsShaking
	{
		get
		{
			return isShaking;
		}
		private set
		{
			isShaking = value;
		}
	}

	public static bool EnableMouseShake { get; set; } = true;


	public static event Action? OnMouseShake;

	internal static void MouseMove(Point nowPos)
	{
		if (!EnableMouseShake)
		{
			return;
		}
		DateTime now = DateTime.Now;
		mouseTrail.Add(new MousePoint(nowPos, now));
		mouseTrail.RemoveAll((MousePoint p) => (now - p.timestamp).TotalMilliseconds > 1000.0);
		if (mouseTrail.Count < 10 || !AnalyzeMouseTrail())
		{
			return;
		}
		mouseTrail.Clear();
		if ((now - lastShakeTime).TotalMilliseconds > 3000.0 && !isShaking)
		{
			Task.Run(delegate
			{
				isShaking = true;
				Thread.Sleep(500);
				MouseShakeDetector.OnMouseShake?.Invoke();
				lastShakeTime = now;
				isShaking = false;
			});
		}
	}

	private static bool AnalyzeMouseTrail()
	{
		if (mouseTrail.Count < 10)
		{
			return false;
		}
		if (CalculateMaxDistance() < 200.0)
		{
			return false;
		}
		if (DetectBackAndForthMotion())
		{
			return true;
		}
		return false;
	}

	private static bool DetectBackAndForthMotion()
	{
		if (mouseTrail.Count < 10)
		{
			return false;
		}
		List<MousePoint> list = SimplifyTrail(mouseTrail, 5);
		if (list.Count < 5)
		{
			return false;
		}
		List<Point> list2 = DouglasPeucker(list.Select((MousePoint p) => p.position).ToList(), 2.0);
		if (list2.Count < 3)
		{
			return false;
		}
		int num = 0;
		for (int i = 2; i < list2.Count; i++)
		{
			Point point = list2[i - 2];
			Point point2 = list2[i - 1];
			Point point3 = list2[i];
			double num2 = point2.X - point.X;
			double num3 = point2.Y - point.Y;
			double num4 = point3.X - point2.X;
			double num5 = point3.Y - point2.Y;
			double num6 = num2 * num4 + num3 * num5;
			double num7 = Math.Sqrt(num2 * num2 + num3 * num3);
			double num8 = Math.Sqrt(num4 * num4 + num5 * num5);
			if (!(num7 < 1E-06) && !(num8 < 1E-06))
			{
				double val = num6 / (num7 * num8);
				val = Math.Max(-1.0, Math.Min(1.0, val));
				if (Math.Acos(val) * (180.0 / Math.PI) > 135.0)
				{
					num++;
				}
			}
		}
		return num >= 4;
	}

	private static List<MousePoint> SimplifyTrail(List<MousePoint> trail, int interval)
	{
		List<MousePoint> list = new List<MousePoint>();
		for (int i = 0; i < trail.Count; i += interval)
		{
			list.Add(trail[i]);
		}
		if (trail.Count > 0 && !list.Contains(trail[trail.Count - 1]))
		{
			list.Add(trail[trail.Count - 1]);
		}
		return list;
	}

	private static double Distance(Point lhs, Point rhs)
	{
		Point point = new Point(rhs.X - lhs.X, rhs.Y - lhs.Y);
		return Math.Sqrt(point.X * point.X + point.Y * point.Y);
	}

	private static double CalculateMaxDistance()
	{
		if (mouseTrail.Count < 2)
		{
			return 0.0;
		}
		Point position = mouseTrail[0].position;
		Point position2 = mouseTrail[0].position;
		foreach (MousePoint item in mouseTrail)
		{
			Point position3 = item.position;
			if (position3.X >= position.X)
			{
				position3 = item.position;
				if (position3.Y >= position.Y)
				{
					goto IL_0085;
				}
			}
			position = item.position;
			goto IL_0085;
			IL_0085:
			position3 = item.position;
			if (position3.X <= position2.X)
			{
				position3 = item.position;
				if (position3.Y <= position2.Y)
				{
					continue;
				}
			}
			position2 = item.position;
		}
		return position2.X - position.X;
	}

	private static List<Point> DouglasPeucker(List<Point> points, double epsilon)
	{
		if (points.Count <= 2)
		{
			return points;
		}
		double num = 0.0;
		int num2 = 0;
		Point point = points[0];
		Point point2 = points[points.Count - 1];
		for (int i = 1; i < points.Count - 1; i++)
		{
			double num3 = PerpendicularDistance(point, point2, points[i]);
			if (num3 > num)
			{
				num = num3;
				num2 = i;
			}
		}
		List<Point> list = new List<Point>();
		if (num > epsilon)
		{
			List<Point> points2 = points.Take(num2 + 1).ToList();
			List<Point> points3 = points.Skip(num2).ToList();
			List<Point> list2 = DouglasPeucker(points2, epsilon);
			List<Point> collection = DouglasPeucker(points3, epsilon);
			list.AddRange(list2.Take(list2.Count - 1));
			list.AddRange(collection);
		}
		else
		{
			list.Add(point);
			list.Add(point2);
		}
		return list;
	}

	private static double PerpendicularDistance(Point start, Point end, Point point)
	{
		double num = Math.Abs(0.5 * (double)(start.X * (end.Y - point.Y) + end.X * (point.Y - start.Y) + point.X * (start.Y - end.Y)));
		double num2 = Math.Sqrt(Math.Pow(start.X - end.X, 2.0) + Math.Pow(start.Y - end.Y, 2.0));
		return num / num2;
	}
}
