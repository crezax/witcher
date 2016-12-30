using UnityEngine;
using System.Collections;

public class DamageProjectile : Projectile {
  [SerializeField]
  private GameObject hitParticlesPrefab;

  public float Damage { get; set; }

  protected override void OnHit(Collider collider) {
    Health targetHealth = collider.GetComponent<Health>();
    if (targetHealth != null) {
      targetHealth.CurrentValue -= Damage;
    }
    Character targetCharacter = collider.GetComponent<Character>();
    if (targetCharacter != null) {
      targetCharacter.ReceiveDisablingHit(.5f);
    }
    Instantiate(hitParticlesPrefab).transform.position = transform.position;
    Destroy(gameObject);
  }
}
