using UnityEngine;

public class KnockbackEntity : MonoBehaviour, IKnockback
{
	private Rigidbody rigidBody;

	void Awake()
    {
       rigidBody = GetComponent<Rigidbody>(); 
    }

	public void ApplyKnockback(Vector3 forceVerse, float knockBack)
	{
		rigidBody.AddForce(-forceVerse * knockBack, ForceMode.Impulse);
	}
}
