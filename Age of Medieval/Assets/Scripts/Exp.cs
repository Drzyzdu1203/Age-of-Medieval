using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public float XP;
    public int CurrentLevel;
    public float XpNextLevel;
    public float differenceXp;
    public float TotalDifferences;
    public Text XpText;
    public Image XpBar;
    public Text Level;
    private int LevelUp;
    public IncreaseStats Stats;
    public LevelUp Up;
    public AudioSource LvlUp;


    private void Start()
    {
        UpdateXp(0);
        XpText.text = (TotalDifferences - differenceXp).ToString("0") + "/" + TotalDifferences.ToString("0");
    }



    public void UpdateXp(float xp)
    {
        XP += xp;
        int curlvl = (int)(0.1f * Mathf.Sqrt(XP));
        if (curlvl != CurrentLevel)
        {
            LevelUp = curlvl - CurrentLevel;
            CurrentLevel = curlvl;
            LvlUp.Play();
            Stats.AddSkillPoints(LevelUp * 2);
            Up.Up();

        }
        XpNextLevel = 100 * (CurrentLevel + 1) * (CurrentLevel + 1);
        differenceXp = XpNextLevel - XP;

        TotalDifferences = XpNextLevel - (100 * CurrentLevel * CurrentLevel);

        XpText.text = (TotalDifferences - differenceXp).ToString("0") + "/" + TotalDifferences.ToString("0");

        XpBar.fillAmount = 1 / TotalDifferences * (TotalDifferences - differenceXp);
        Level.text = (CurrentLevel + 1).ToString("0");
    }

}
