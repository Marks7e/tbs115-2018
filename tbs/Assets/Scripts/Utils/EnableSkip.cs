using Assets.Scripts.DataPersistence.DependecyInjector;
using Assets.Scripts.DataPersistence.Models;
using UnityEngine;

public class EnableSkip 
{
    private DependencyInjector _di;
    private LevelData _ld;
         
    public void EnableSkipButton(int level, int timesPlayedBeforeEnableSkip, GameObject skipButton)
    {
        _di = new DependencyInjector();
        _ld = new LevelData();

        _ld = _di.GetLevelData(level);
        
        skipButton.SetActive(_ld.TimesPlayed >= timesPlayedBeforeEnableSkip);
        
    }

}
