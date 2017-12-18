using UnityEngine;

public class GridRenderer : MonoBehaviour
{
	public Material lineMaterial;

	void OnPostRender()
	{
		GL.PushMatrix();
		lineMaterial.SetPass(0);
		GL.LoadPixelMatrix();
		GL.Begin(GL.LINES);
		GL.Color(new Color(0.05f, 0.05f, 0.05f));

		for (int i = 0; i < Screen.width / World.unitSize; i++)
		{
			GL.Vertex(new Vector3(i * World.unitSize, 0));
			GL.Vertex(new Vector3(i * World.unitSize, Screen.height));
		}

		for (int i = 0; i < Screen.height / World.unitSize; i++)
		{
			GL.Vertex(new Vector3(0, i * World.unitSize));
			GL.Vertex(new Vector3(Screen.width, i * World.unitSize));
		}

		GL.End();
		GL.PopMatrix();
	}
}
