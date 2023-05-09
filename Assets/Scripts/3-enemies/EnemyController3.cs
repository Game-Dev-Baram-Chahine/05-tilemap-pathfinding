using UnityEngine;

/**
 * This component patrols between given points, chases a given target object when it sees it, and rotates from time to time.
 */
[RequireComponent(typeof(Patroller))]
[RequireComponent(typeof(Chaser))]
[RequireComponent(typeof(Rotator))]
public class EnemyController3: StateMachine {
    [SerializeField] float radiusToWatch = 5f;
    [SerializeField] float probabilityToRotate = 0.2f;
    [SerializeField] float probabilityToStopRotating = 0.2f;

    private Chaser chaser;
    private Patroller patroller;
    private Hider hider;

    private float DistanceToTarget() {
        return Vector3.Distance(transform.position, chaser.TargetObjectPosition());
    }

    private void Awake() {
        chaser = GetComponent<Chaser>();
        patroller = GetComponent<Patroller>();
        hider = GetComponent<Hider>();
        base
        .AddState(patroller)     // This would be the first active state.
        .AddState(chaser)
        .AddState(hider)
        .AddTransition(patroller, () => DistanceToTarget()<=radiusToWatch,   chaser)
        .AddTransition(hider,   () => DistanceToTarget()<=radiusToWatch,   chaser)
        .AddTransition(chaser,    () => DistanceToTarget() > radiusToWatch,  patroller)
        .AddTransition(hider,   () => Random.Range(0f, 1f) < probabilityToStopRotating * Time.deltaTime, patroller)
        .AddTransition(patroller, () => Random.Range(0f, 1f) < probabilityToRotate       * Time.deltaTime, hider)
        ;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusToWatch);
    }
}
 