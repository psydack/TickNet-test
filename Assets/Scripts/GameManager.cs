using Netick.Unity;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] NetworkTransform playerPrefab;

    //[SerializeField] bool usePrediction = false;
    //bool _lastPrediction = false;

    NetworkTransform _playerNetTransform;

    public void InstantiatePlayer()
    {
		var obj = Instantiate(playerPrefab);
		obj.GetComponent<PlayerControl>().Set(true);
	}

    void Start()
    {
        _playerNetTransform = Instantiate(playerPrefab);
    }

	private void Update()
	{

	}
}
