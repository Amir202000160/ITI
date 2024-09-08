using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DissolveControl : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;
    [SerializeField] private Material _disappearMaterial; // Material used for disappearance
    [SerializeField] private Material _appearMaterial;    // Material used for appearance

    private SpriteRenderer[] _spriteRenderers;
    private Material[] _disappearMaterials; 
    private Material[] _appearMaterials;   

    private int _dissolveAmount = Shader.PropertyToID("_Dissolve_Amount");

    private void Start()
    {
        _spriteRenderers = GetComponents<SpriteRenderer>();
        _disappearMaterials = new Material[_spriteRenderers.Length];
        _appearMaterials = new Material[_spriteRenderers.Length];

        for (int i = 0; i < _spriteRenderers.Length; i++)
        {
            // Assume each SpriteRenderer can have different material
            _disappearMaterials[i] = _spriteRenderers[i].material;
            _appearMaterials[i] = _appearMaterial; 
        }
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            StartCoroutine(DisAppaear());
        }

        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartCoroutine(Appear());
        }
    }

    private IEnumerator Appear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(1.1f, 0f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _appearMaterials.Length; i++)
            {
                _appearMaterials[i].SetFloat(_dissolveAmount, lerpedDissolve);
            }

            yield return null;
        }
    }

    private IEnumerator DisAppaear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1f, (elapsedTime / _dissolveTime));

            for (int i = 0; i < _disappearMaterials.Length; i++)
            {
                
                    _disappearMaterials[i].SetFloat(_dissolveAmount, lerpedDissolve);
            }

            yield return null;
        }
    }

}
