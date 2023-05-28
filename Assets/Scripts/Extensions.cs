using UnityEngine;

public static class Extensions {

  private static LayerMask defaultLayerMask = LayerMask.GetMask("Default");
  private static LayerMask enemyLayerMask = LayerMask.GetMask("Default", "Enemy Barrier");
  public static bool Raycast(this Rigidbody2D rigidbody, Vector2 direction, float distance) {
    if (rigidbody.isKinematic) {
      return false;
    }

    float radius = 0.25f;

    RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, defaultLayerMask);
    return hit.collider != null && hit.rigidbody != rigidbody;
  }

  public static bool EnemyRaycast(this Rigidbody2D rigidbody, Vector2 direction, float distance) {
    if (rigidbody.isKinematic) {
      return false;
    }

    float radius = 0.25f;

    RaycastHit2D hit = Physics2D.CircleCast(rigidbody.position, radius, direction.normalized, distance, enemyLayerMask);
    return hit.collider != null && hit.rigidbody != rigidbody;
  }

  public static bool DotTest(this Transform transform, Transform other, Vector2 testDirection) {
    Vector2 direction = other.position - transform.position;
    return Vector2.Dot(direction.normalized, testDirection) > 0.25f;
  }
}