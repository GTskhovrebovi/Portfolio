using UnityEngine;

public class DamageTextSpawner : MonoBehaviour
{
    [SerializeField] DamageText damageTextPrefab = null;

    public void Spawn(float damageAmount)
    {
        DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform.position, Quaternion.identity);
        instance.SetValue(damageAmount);
    }
}

