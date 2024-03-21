using UnityEngine;

public class CannonProjectile : BaseProjectile {
	void Update () {
		transform.position += transform.forward * _speed * Time.deltaTime;
	}
}
