using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSelect : MonoBehaviour {

    [SerializeField] private AudioClip click;

    Build build;
    UIManager ui;
    
    void Start()
    {
        build = Build.instance;
        ui = UIManager.instance;
    }
	public void selectToUnit()
    {
        if (build.progressBar[0].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[0]);
            build.progressBar[0].fillAmount = 0;
            ui.costMoney = 50;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit2()
    {
        if (build.progressBar[1].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[1]);
            build.progressBar[1].fillAmount = 0;
            ui.costMoney = 70;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit3()
    {
        if (build.progressBar[2].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[2]);
            build.progressBar[2].fillAmount = 0;
            ui.costMoney = 80;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit4()
    {
        if (build.progressBar[3].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[3]);
            build.progressBar[3].fillAmount = 0;
            ui.costMoney = 90;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit5()
    {
        if (build.progressBar[4].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[4]);
            build.progressBar[4].fillAmount = 0;
            ui.costMoney = 100;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit6()
    {
        if (build.progressBar[5].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[5]);
            build.progressBar[5].fillAmount = 0;
            ui.costMoney = 110;
            SoundManager.instance.playSound(click);
        }
    }

    public void selectToUnit7()
    {
        if (build.progressBar[6].fillAmount == 1)
        {
            build.SetPlayerToBuild(build.selectPlayerPrefab[6]);
            build.progressBar[6].fillAmount = 0;
            ui.costMoney = 120;
            SoundManager.instance.playSound(click);
        }
    }
}
