using UnityEngine;

namespace Bounce.Framework
{
    public static class Rigidbody2DExtensions
    {
        public static void AddExplosionForce(this Rigidbody2D rb, float explosionForce, Vector2 explosionPosition, float explosionRadius, float upwardsModifier = 0.0F, ForceMode2D mode = ForceMode2D.Force)
        {
            var explosionDir =  explosionPosition - rb.position;
            var explosionDistance = explosionDir.magnitude;

            // Normalize without computing magnitude again
            if (upwardsModifier == 0)
            {
                explosionDir /= explosionDistance;
            }
            else
            {
                // From Rigidbody.AddExplosionForce doc:
                // If you pass a non-zero value for the upwardsModifier parameter, the direction
                // will be modified by subtracting that value from the Y component of the centre point.
                explosionDir.y += upwardsModifier;
                explosionDir.Normalize();
            }

            rb.AddForce(Mathf.Lerp(explosionForce, 0f, explosionDistance / explosionRadius) * explosionDir, mode);
        }
    }
}