using UnityEngine;

public class RangedAttack : RangeBasedAttack {
  [SerializeField]
  private GameObject projectilePrefab;
  [SerializeField]
  private GameObject projectileSpawnPoint;

  protected override string AnimationName {
    get {
      return AnimationConstants.SKILL;
    }
  }

  protected override float CastTime {
    get {
      return AnimationConstants.CASTING_DURATION;
    }
  }

  protected override void PerformImplementation(GameObject target) {
    GameObject projectile = Instantiate(projectilePrefab);
    projectile.transform.position = projectileSpawnPoint.transform.position;

    MovementController projectileController =
      projectile.GetComponent<MovementController>();
    projectile.GetComponent<DamageProjectile>().Damage = Damage;
    projectile.GetComponent<DamageProjectile>().Caster = gameObject;

    if (target == null) {
      projectileController.MoveInDirection(transform.forward);
    } else {
      projectileController.MoveInDirection(
        target.transform.position - transform.position
      );
    }
  }
}
