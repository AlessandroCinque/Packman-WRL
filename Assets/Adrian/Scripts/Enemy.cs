using UnityEngine;

public enum EnemyType { Blinky, Pinky, Inky, Clyde };

[RequireComponent(requiredComponent: typeof(Rigidbody2D), requiredComponent2: typeof(CircleCollider2D), requiredComponent3: typeof(Unit))]
public class Enemy : MonoBehaviour
{
    //Alessandro messing up ============================
    private Score score;
    //==================================================
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private EnemyBehaviour enemyBehaviour;
    [SerializeField] private EnemyType enemyType;
    private bool isChasing = true;
    private bool isFleeing = false;
    private Vector2 lastPos;
    [SerializeField] private Rigidbody2D rigidbody;
    [SerializeField] private float speed = 5;
    private float timeElapsed = 0;
    [SerializeField] private Unit unit;
    [SerializeField] private float updateInterval = 1;
    public EnemyType GetEnemyType => enemyType;
    public Vector2 LasPos => lastPos;
    [SerializeField] private float clydeMinDistance = 4;

    public float ClydeMinDistance => clydeMinDistance;
    private void Reset()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.gravityScale = 0;

        circleCollider = GetComponent<CircleCollider2D>();
        circleCollider.radius = 0.5f;

        unit = GetComponent<Unit>();
        unit.speed = speed;
    }

    // Start is called before the first frame update
    private void Start()
    {
        lastPos = transform.position;

        unit.target = enemyBehaviour.Target;
        unit.speed = speed;
        //Alessandro messing up
        score = GameObject.FindObjectOfType<Score>();
    }

    // Update is called once per frame
    private void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Vector2 delta = player.transform.position - transform.position;

        if (enemyType == EnemyType.Clyde && !isFleeing && (delta.magnitude <= clydeMinDistance))
        {
            unit.target = enemyBehaviour.Target;

            isFleeing = true;
            timeElapsed = 0;
        }
        else if (isFleeing)
        {
            if (unit.HasReachedTarget)
            {
                unit.target = enemyBehaviour.Target;

                lastPos = transform.position;

                isFleeing = true;
            }
        }
        else
        {
            if (timeElapsed >= updateInterval)
            {
                unit.target = enemyBehaviour.Target;
                timeElapsed = 0;
            }

            timeElapsed += Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            score.AdjustScore(-20);
        }
    }
}