using Netick.Unity;
using UnityEngine;

public class PlayerControl : NetworkBehaviour
{
	[SerializeField] private float speed = 5;

	private float _currentTimer = 1;
	private readonly float _timeToChangeDirection = .25f;
	private bool _isSet = false;
	private bool _isRandom = false;
	private float accelX, accelY;

	public void Set(bool isRandom)
	{
		_isSet = true;
		_isRandom = isRandom;
	}

	private void Update()
	{
		if (!_isSet)
		{
			return;
		}

		if (_isRandom)
		{
			_currentTimer += Time.deltaTime;
			if (_currentTimer >= _timeToChangeDirection)
			{
				_currentTimer = 0;
				accelX = Random.Range(-speed, speed);
				accelY = Random.Range(-speed, speed);
			}
		}
		else
		{
			accelX = Input.GetAxis("Horizontal");
			accelY = Input.GetAxis("Vertical");
		}

		UpdateMovement(accelX, accelY);
	}

	private void UpdateMovement(float accelX, float accelY)
	{
		transform.position += speed * Time.deltaTime * new Vector3(accelX, accelY);
	}
}
