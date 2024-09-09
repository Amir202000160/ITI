using System.Collections;
using UnityEngine;

public class DisappearingGround : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;
    [SerializeField] private Material _disappearMaterial; // Material used for disappearance

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _disappearMaterials;

    private int _dissolveAmount = Shader.PropertyToID("_Dissolve_Amount");

    private void Start()
    {
        _spriteRenderers = GetComponents<SpriteRenderer>();
        _disappearMaterials = new Material[_spriteRenderers.Length];
        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            // Create new material instance for each SpriteRenderer to prevent shared material issue
            _disappearMaterials[i] = new Material(_disappearMaterial);
            _spriteRenderers[i].material = _disappearMaterials[i];
        }
    }

    public IEnumerator Disappear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(0, 1f, elapsedTime / _dissolveTime);

            for (int i = 0; i < _disappearMaterials.Length; i++)
            {
                _disappearMaterials[i].SetFloat(_dissolveAmount, lerpedDissolve);
            }

            yield return null; // Smooth frame-by-frame dissolve
        }

        // Destroy the object after dissolve completes
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            StartCoroutine(Disappear());
        }
    }
}
