using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DissolveControl : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;
    [SerializeField] private Material _disappearMaterial; // Material used for disappearance
    [SerializeField] private Material _appearMaterial;    // Material used for appearance

    private SpriteRenderer _spriteRenderers;
    //private Material[] _disappearMaterials; 
    //private Material[] _appearMaterials;   

    private int _dissolveAmount = Shader.PropertyToID("_Dissolve_Amount");

    private void Start()
    {
        _spriteRenderers = GetComponent<SpriteRenderer>();
      
     
        
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            StartCoroutine(Test());
          
        }

        //if (Keyboard.current.fKey.wasPressedThisFrame)
        //{
        //    StartCoroutine(Appear());
        //}

    }

    private IEnumerator Appear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(1f, 0f, (elapsedTime / _dissolveTime));

           
                _appearMaterial.SetFloat(_dissolveAmount, lerpedDissolve);
           

            yield return null;
            
        }
    }

    public IEnumerator DisAppaear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1f, (elapsedTime / _dissolveTime));

            _disappearMaterial.SetFloat(_dissolveAmount, lerpedDissolve);

            //for (int i = 0; i < _disappearMaterial.Length; i++)
            //{
                
            //}

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator DisAppaearr()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1f, (elapsedTime / _dissolveTime));

           
                _disappearMaterial.SetFloat(_dissolveAmount, lerpedDissolve);
            


            yield return null;
        }
    }

    private IEnumerator Test()
    {
        StartCoroutine(DisAppaearr());
        Debug.Log("Appaearr");
       
        yield return new WaitForSeconds(0.8f);
       

        StartCoroutine(Appear());
        Debug.Log("DisAppaearr");


    }

}
