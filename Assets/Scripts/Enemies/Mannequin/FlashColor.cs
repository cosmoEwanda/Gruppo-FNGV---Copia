using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FlashColor : MonoBehaviour
{
	
	public Color flashColorDamage = Color.red;
	public Color flashColorHealing = Color.green;
	public float flashDuration = 0.2f;

	private Color[] allOriginalColor;
	private Renderer[] allRenderers;

	void Awake()
	{
		allRenderers = GetComponentsInChildren<Renderer>();
		allOriginalColor = new Color[allRenderers.Length];

		for (int i = 0; i < allRenderers.Length; i++)
		{
			allOriginalColor[i] = allRenderers[i].material.color;
		}
	}
	private IEnumerator DoFlash(Color flashColor)
	{
		foreach (Renderer rend in allRenderers)
		{
			rend.material.color = flashColor;
		}
			yield return new WaitForSeconds(flashDuration);
		for (int i = 0; i < allRenderers.Length; i++)
		{
			allRenderers[i].material.color = allOriginalColor[i];
		}
	}

	public void Flash(Color flashColor)
	{
		StopAllCoroutines();
		StartCoroutine(DoFlash(flashColor));
	}

	void OnDestroy()
	{
		foreach (Renderer rend in allRenderers)
		{
			if (rend != null)
			{
				Destroy(rend.material);
			}
		}
	}

}
