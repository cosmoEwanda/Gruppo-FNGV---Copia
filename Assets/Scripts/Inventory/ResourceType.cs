using UnityEngine;

[CreateAssetMenu(fileName = "ResourceType", menuName = "Resource/resourceType")]
public class ResourceType : ScriptableObject
{
	public string resourceName;
	public GameObject prefab;

	public int defaultMaxAmount = 10_000;
}
