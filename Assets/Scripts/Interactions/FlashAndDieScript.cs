using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAndDieScript : MonoBehaviour
{
	public float time;
	public float intervalTime;
	private Color[] colors = { Color.yellow, Color.red };
	public void Execute()
	{
		StartCoroutine(FlashAndDie(time, intervalTime));
	}
	IEnumerator FlashAndDie(float time, float intervalTime)
	{
		Material material = GetComponent<MeshRenderer>().material;
		colors[1] = material.color;
		float elapsedTime = 0f;
		int index = 0;
		while (elapsedTime < time)
		{
			material.color = colors[index % 2];

			elapsedTime += Time.deltaTime;
			index++;
			yield return new WaitForSeconds(intervalTime);
		}
		GameObject.Destroy(gameObject);
	}
}
