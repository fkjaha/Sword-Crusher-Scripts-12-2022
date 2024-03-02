using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Blade : AUpgradable
{
    [SerializeField] private int startPixelBreakChance;
    [SerializeField] private int addChancePerLevel;
    
    private int _pixelBreakChance;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Pixel pixel))
        {
            if (Random.Range(0, 101) <= _pixelBreakChance)
            {
                pixel.DetachPixel();
            }
        }
    }

    private protected override void ApplyLevel()
    {
        _pixelBreakChance = startPixelBreakChance + addChancePerLevel * level;
    }
}
