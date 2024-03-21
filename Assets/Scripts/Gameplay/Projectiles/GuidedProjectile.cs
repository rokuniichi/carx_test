using UnityEngine;

public class GuidedProjectile : BaseProjectile {
	private Transform _target;

	public void SetTarget(Transform target)
    {
		_target = target;
		_target.GetComponent<IKillable>().OnKill += SelfRemove;
    }

	private void SelfRemove(Transform target)
    {
		if (_target != target) return;
		_target.GetComponent<IKillable>().OnKill -= SelfRemove;
		SelfRemove();
	}

    void Update () {
		if (_target == null) return;
		Vector3 translation = _target.transform.position - transform.position;
		float frameDistance = _speed * Time.deltaTime;
		if (translation.magnitude > frameDistance) {
			transform.Translate(translation.normalized * frameDistance);
		}
	}
}
