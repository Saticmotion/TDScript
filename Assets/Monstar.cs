using UnityEngine;

public class Monstar : MonoBehaviour
{
	public GameObject prevPoint;
	public GameObject nextPoint;	
	public int index;
	public bool dead;

	void Start ()
	{
		prevPoint = Path.instance.points[0];
		nextPoint = Path.instance.points[1];
	}
	
	void Update () 
	{
		if ((transform.position - nextPoint.transform.position).magnitude < 0.05f)
		{
			if (index < Path.instance.points.Count - 1)
			{
				index++;
				prevPoint = nextPoint;
				nextPoint = Path.instance.points[index];
			}
			else
			{
				dead = true;
			}
		}

		var direction = Vector3.Normalize(nextPoint.transform.position - prevPoint.transform.position);
		transform.position += direction * 4f * Time.deltaTime;
	}
}
