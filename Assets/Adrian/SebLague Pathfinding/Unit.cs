using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {
	
	public Vector2 target;
	public float speed = 20;

	Vector2[] path;
	int targetIndex;

	Vector2 lastStop;

	void Start() {
		lastStop = transform.position;

		StartCoroutine(RefreshPath ());
	}

	public bool HasReachedTarget
    {
		get
        {
			if (path.Length == 0)
            {
				return true;
            }
			else
			{
				return targetIndex >= path.Length;
			}
        }
    }

	IEnumerator RefreshPath() {
		Vector2 targetPositionOld = target + Vector2.up; // ensure != to target.position initially
			
		while (true) {
			if (targetPositionOld != (Vector2)target) {
				targetPositionOld = (Vector2)target;

                path = Pathfinding.RequestPathAStar(transform.position, target);

                //path = Pathfinding.RequestPathPacMan(transform.position, target, GetComponent<Enemy>().LasPos);

                StopCoroutine ("FollowPath");
				StartCoroutine ("FollowPath");
			}

			yield return new WaitForSeconds (.25f);
		}
	}
		
	IEnumerator FollowPath() {
		if (path.Length > 0) {
			targetIndex = 0;
			Vector2 currentWaypoint = path [0];

			while (true) {
				if ((Vector2)transform.position == currentWaypoint) {
					targetIndex++;
					if (targetIndex >= path.Length)
					{
						yield break;
					}
					else
					{
						currentWaypoint = path[targetIndex];
					}
				}

				transform.position = Vector2.MoveTowards (transform.position, currentWaypoint, speed * Time.deltaTime);
				yield return null;

			}
		}
	}

	public void OnDrawGizmos() {
		if (path != null) {
			for (int i = targetIndex; i < path.Length; i ++) {
				Gizmos.color = Color.black;
				//Gizmos.DrawCube((Vector3)path[i], Vector3.one *.5f);

				if (i == targetIndex) {
					Gizmos.DrawLine(transform.position, path[i]);
				}
				else {
					Gizmos.DrawLine(path[i-1],path[i]);
				}
			}
		}
	}
}
