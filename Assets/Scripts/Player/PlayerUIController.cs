using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject gaugeBroken;
    public GameObject gaugePointer;
    public Image cardCooldown;
    public Image[] previewIcons;
    public GameObject healthChipPrefab;
    public GameObject healthChipStart;

    public float chipSpacing = 10f;
    private float oldHealth = 0f;

    private List<GameObject> chips;


    // Start is called before the first frame update
    void Start()
    {
        chips = new List<GameObject>();
        UpdateHealth(GetComponent<Health>().GetMaxHealth());
        UpdateGauge(0);
    }

    public void UpdateHealth(float newHealth)
    {
        if (oldHealth < newHealth)
        {
            // Spawn all new health this time
            for (int i = 0; i < (int)newHealth; i++)
            {
                chips.Add(Instantiate(healthChipPrefab, healthChipStart.transform.position + Vector3.right * chipSpacing * i * (Screen.width / 1920), Quaternion.identity));
                chips[i].transform.SetParent(healthChipStart.transform.parent);
                chips[i].transform.localScale = Vector3.one;
            }
        }
        else
        {
            int difference = (int)(oldHealth - newHealth);
            for (int i = 0; i < difference; i++)
            {
                if (chips.Count != 0)
                {
                    Destroy(chips[chips.Count - 1]);
                    chips.RemoveAt(chips.Count - 1);
                }
            }
        }
        oldHealth = newHealth;
    }

    public void UpdateGauge(int bjNum)
    {
        if (bjNum > 21)
        {
            gaugeBroken.SetActive(true);
            return;
        }
        else
        {
            gaugeBroken.SetActive(false);
        }
        float rotation = Mathf.Lerp(90, -90, bjNum / 21.0f);
        Vector3 newRotation = Vector3.zero;
        newRotation.z = rotation;
        LeanTween.rotate(gaugePointer, newRotation, .2f).setEaseInOutSine();
        UpdatePreview(bjNum);
    }

    public void UpdatePreview(int bjNum)
    {
        int index = 0;
        if (bjNum <= 5)
        {
            index = 0;
        }
        else if (bjNum <= 10)
        {
            index = 1;
        }
        else if (bjNum <= 15)
        {
            index = 2;
        }
        else if (bjNum <= 20)
        {
            index = 3;
        }
        else if (bjNum == 21)
        {
            index = 4;
        }

        for (int i = 0; i < previewIcons.Length; i++)
        {
            if (i == index)
            {
                previewIcons[i].gameObject.SetActive(true);
            }
            else
            {
                previewIcons[i].gameObject.SetActive(false);
            }
        }
    }

    public void SetCoolDown(float coolTime)
    {
        LeanTween.value(cardCooldown.gameObject, 0f, 1f, coolTime).setOnUpdate((float val) =>
        {
            cardCooldown.fillAmount = val;
        });
    }
}
