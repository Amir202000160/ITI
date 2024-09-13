using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class DissolveControl : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;
    [SerializeField] private Material _disappearMaterial; // Material used for disappearance
    [SerializeField] private Material _appearMaterial;    // Material used for appearance
    [SerializeField] private Material AfterMaterial;    // Material used for appearance
    [SerializeField] private SpriteRenderer GangsterSprite;
    [SerializeField] private SpriteRenderer WindSprite;
    [SerializeField] private GameObject GangsterCamera;
    [SerializeField] private GameObject WindCamera;
    [SerializeField] private HealthBar HealthBar;

    private int _dissolveAmount = Shader.PropertyToID("_Dissolve_Amount");

    private void Start() {
        _appearMaterial.SetFloat(_dissolveAmount, 1);
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
        WindSprite.material = AfterMaterial;
        GangsterSprite.gameObject.SetActive(false);
    }

    public IEnumerator DisAppaear()
    {
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1f, (elapsedTime / _dissolveTime));

            _disappearMaterial.SetFloat(_dissolveAmount, lerpedDissolve);

            yield return new WaitForSeconds(1);
        }
    }

    private IEnumerator DisAppaearr()
    {
        GangsterSprite.material = _disappearMaterial;
        WindSprite.material = _appearMaterial;
        float elapsedTime = 0f;
        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedDissolve = Mathf.Lerp(0, 1f, (elapsedTime / _dissolveTime));

           
                _disappearMaterial.SetFloat(_dissolveAmount, lerpedDissolve);
            


            yield return null;
        }
    }

    public void Transformation() {
        StartCoroutine(Transform());
    }

    private IEnumerator Transform()
    {
        StartCoroutine(DisAppaearr());
       
        yield return new WaitForSeconds(0.5f);
        WindSprite.gameObject.SetActive(true);
        WindSprite.transform.position = gameObject.transform.position;
        WindSprite.gameObject.SetActive(true);
        GangsterCamera.SetActive(false);
        WindCamera.SetActive(true);
        WindSprite.gameObject.GetComponent<PlayerMovement>().Health = gameObject.GetComponent<PlayerMovement>().Health;
        HealthBar.player = WindSprite.gameObject.GetComponent<PlayerMovement>();
        StartCoroutine(Appear());
    }
}