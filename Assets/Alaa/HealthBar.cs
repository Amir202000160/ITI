using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthBar: MonoBehaviour
{
    private float LerbTime;
    public float MaxHealth = 100;
    public float ChipsSpeed = 2f;
    public Image FrontHealthBar;
    public Image Frame;
    public Image BackHealthBar;
    public TMP_Text HealthText;

    public float ShakeDuration = 0.2f;
    public float ShakeMagnitude = 10f;

    private RectTransform HealthBarRectTransform;
    private Vector3 OriginalPosition;


    float CurrentHealth;
    float HealthBeforeUpdate;


    public PlayerMovement player;

    void Start()
    {
        CurrentHealth = MaxHealth;

        HealthBarRectTransform = Frame.GetComponent<RectTransform>();
        OriginalPosition = HealthBarRectTransform.localPosition;
    }


    void Update()
    {

        CurrentHealth = player.Health;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
        UpdateHealthUi();
        if (CurrentHealth > HealthBeforeUpdate)
        {
            RestoreHealth(CurrentHealth);
        }
        else if (CurrentHealth < HealthBeforeUpdate)
        {
            TakeDamage(CurrentHealth);
        }

        if (CurrentHealth <= 100 && CurrentHealth > 80)
        {
            FrontHealthBar.color = new Color(1, 1, 1, 1);
        }
        else if (CurrentHealth <= 80 && CurrentHealth > 60)
        {
            FrontHealthBar.color = new Color(0f, 0.6f, 1f);
        }

        else if (CurrentHealth <= 60 && CurrentHealth > 40)
        {
            FrontHealthBar.color = new Color(255 / 255f, 141 / 255f, 1);

        }

        else if (CurrentHealth <= 40 && CurrentHealth > 20)
        {
            FrontHealthBar.color = new Color(255 / 255f, 94 / 255f, 0);
        }

        else if (CurrentHealth <= 20)
        {
            FrontHealthBar.color = new Color(255 / 255, 27 / 255, 0 / 255, 1);
        }
        HealthBeforeUpdate = CurrentHealth;
    }

    public void UpdateHealthUi()
    {
        float fillf = FrontHealthBar.fillAmount;
        float fillB = BackHealthBar.fillAmount;
        float hFraction = CurrentHealth / MaxHealth;

        if (fillB > hFraction)
        {
            FrontHealthBar.fillAmount = hFraction;
            BackHealthBar.color = Color.red;
            LerbTime += Time.deltaTime;
            float precentComplete = LerbTime / ChipsSpeed;
            precentComplete = precentComplete * precentComplete;
            BackHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, precentComplete);
        }

        if (fillf < hFraction)
        {
            BackHealthBar.color = Color.green;
            BackHealthBar.fillAmount = hFraction;
            LerbTime += Time.deltaTime;
            float precentComplete = LerbTime / ChipsSpeed;
            precentComplete = precentComplete * precentComplete;
            FrontHealthBar.fillAmount = Mathf.Lerp(fillf, BackHealthBar.fillAmount, precentComplete);
        }

        if (HealthText != null)
        {
            float healthPercentage = (CurrentHealth / MaxHealth) * 100;

            HealthText.text = $"{healthPercentage:0}%";

        }

    }

    public void TakeDamage(float damage)
    {
        LerbTime = 0f;
        
        StartCoroutine(ChangeTextColor(Color.red, 0.4f));

        StartCoroutine(ShakeHealthBar());
    }

    public void RestoreHealth(float healAmount)
    {
        CurrentHealth += healAmount;
        LerbTime = 0f;
        StartCoroutine(ChangeTextColor(Color.green, 0.5f));


    }

    private IEnumerator ChangeTextColor(Color newColor, float duration)
    {
        HealthText.color = newColor;
        yield return new WaitForSeconds(duration);
        HealthText.color = Color.white;
    }

    private IEnumerator ShakeHealthBar()
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = HealthBarRectTransform.localPosition;

        while (elapsedTime < ShakeDuration)
        {
            float xOffset = Random.Range(-ShakeMagnitude, ShakeMagnitude);
            float yOffset = Random.Range(-ShakeMagnitude, ShakeMagnitude);
            HealthBarRectTransform.localPosition = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        HealthBarRectTransform.localPosition = originalPosition;
    }
}
