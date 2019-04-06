using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class DynamicGameBalance
    {
        public int CalculateRoundTime(int roundTime, List<LevelSuccessTime> leveSuccessTimeListModel)
        {
            int roundTimeDGB = 0;
            int adjustSense = 0;
            float typDevForRound = 0;
            float percentDeviation = 0;

            adjustSense = GetSenseOfOperation(roundTime, leveSuccessTimeListModel);
            typDevForRound = CalculateTypicalDevForRound(roundTime, leveSuccessTimeListModel);
            percentDeviation = Math.Abs(CalculatePercentOfDevation(roundTime, typDevForRound));

            if (adjustSense > 0)
            { roundTimeDGB = (int)Math.Round(roundTime * (percentDeviation /100) + (roundTime *0.1)); }
            else
            { roundTimeDGB = (int)Math.Round(roundTime * (percentDeviation /100) - (roundTime * 0.1)); }

            return roundTimeDGB;
        }

        private float CalculatePercentOfDevation(int roundTime, float typicalDeviation)
        {
            return ((typicalDeviation * 100) / (roundTime / 2));
        }

        private float CalculateTypicalDevForRound(int roundTime, List<LevelSuccessTime> levelSuccessTimeListModel)
        {
            double roundAvg = roundTime / 2;
            double successTime = 0f;

            foreach (LevelSuccessTime levelSuccessTimeModel in levelSuccessTimeListModel)
            {
                successTime += Math.Pow(Math.Abs(levelSuccessTimeModel.SuccessTime), 2);
            }
            return (float)(Math.Sqrt((successTime / levelSuccessTimeListModel.Count)) - roundAvg);
        }

        public int CalculateAverageRound(int value, int div)
        {
            return value / div;
        }

        private double CalculateTypicalDesv(List<LevelSuccessTime> levelSuccessTimeListModel)
        {
            double successTimeTypDesv = 0;
            int successTimeAvg = 0;

            successTimeAvg = CalculateAverage(levelSuccessTimeListModel);

            foreach (LevelSuccessTime lst in levelSuccessTimeListModel)
            {
                int indCalc = lst.SuccessTime - successTimeAvg;
                successTimeTypDesv += Math.Pow(Math.Abs(indCalc), 2);
            }
            successTimeTypDesv /= levelSuccessTimeListModel.Count;

            return successTimeTypDesv;
        }
        private int CalculateAverage(List<LevelSuccessTime> levelSuccessTimeListModel)
        {
            int successTimeAvg = 0;
            successTimeAvg = ((levelSuccessTimeListModel.Sum(t => t.SuccessTime)) / levelSuccessTimeListModel.Count);
            return successTimeAvg;
        }
        private int GetSenseOfOperation(int roundTime, List<LevelSuccessTime> levelSuccessTimeListModel)
        {
            int adjustSense = 0;

            foreach (LevelSuccessTime lst in levelSuccessTimeListModel)
            {
                if (lst.SuccessTime > (roundTime / 2))
                { adjustSense += 1; }
                else
                { adjustSense -= 1; }
            }
            return adjustSense;
        }
    }
}
