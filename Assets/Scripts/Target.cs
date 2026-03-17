using UnityEngine;

public class Target : MonoBehaviour
{
   //public static Action OnTargetHit;

	void Start()
	{
		RandomizePosition();
	}

	public void Hit()
	{

        transform.position = TargetBounds.Instance.GetRandomPosition();
		
	}

	void RandomizePosition()
	{
		transform.position = TargetBounds.Instance.GetRandomPosition();
	}
}
