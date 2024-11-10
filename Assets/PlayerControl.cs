using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[SerializeField] private float speed = 5;

	public void UpdateMovement(float accelX, float accelY)
	{
		transform.position += speed * Time.deltaTime * new Vector3(accelX, accelY);
	}

	void Update()
	{
		float accelX = Input.GetAxis("Horizontal");
		float accelY = Input.GetAxis("Vertical");

		UpdateMovement(accelX, accelY);
	}
}
