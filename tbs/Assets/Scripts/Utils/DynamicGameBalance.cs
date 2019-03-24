using Assets.Scripts.DataPersistence.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
    public class DynamicGameBalance
    {
        public int CalculateRoundTime(int RoundTime, List<LevelSuccessTime> llst)
        {
            int roundTimeDGB = 0;
            int adjustSense = 0;
            float typDevForRound = 0;
            float percentDeviation = 0;

            adjustSense = GetSenseOfOperation(RoundTime, llst);
            typDevForRound = CalculateTypicalDevForRound(RoundTime, llst);
            percentDeviation = Math.Abs(CalculatePercentOfDevation(RoundTime, typDevForRound));

            if (adjustSense > 0)
            { roundTimeDGB = (int)Math.Round(RoundTime * (percentDeviation /100) + (RoundTime *0.1)); }
            else
            { roundTimeDGB = (int)Math.Round(RoundTime * (percentDeviation /100) - (RoundTime * 0.1)); }

            return roundTimeDGB;
        }

        private float CalculatePercentOfDevation(int roundTime, float typicalDeviation)
        {
            return ((typicalDeviation * 100) / (roundTime / 2));
        }

        private float CalculateTypicalDevForRound(int roundTime, List<LevelSuccessTime> llst)
        {
            double roundAVG = roundTime / 2;
            double successTime = 0f;

            foreach (LevelSuccessTime lst in llst)
            {
                successTime += Math.Pow(Math.Abs(lst.SuccessTime), 2);
            }
            return (float)(Math.Sqrt((successTime / llst.Count)) - roundAVG);
        }

        public int CalculateAverageRound(int value, int div)
        {
            return value / div;
        }

        private double CalculateTypicalDesv(List<LevelSuccessTime> llst)
        {
            double successTimeTypDesv = 0;
            int successTimeAVG = 0;

            successTimeAVG = CalculateAverage(llst);

            foreach (LevelSuccessTime lst in llst)
            {
                int indCalc = lst.SuccessTime - successTimeAVG;
                successTimeTypDesv += Math.Pow(Math.Abs(indCalc), 2);
            }
            successTimeTypDesv /= llst.Count;

            return successTimeTypDesv;
        }
        private int CalculateAverage(List<LevelSuccessTime> llst)
        {
            int successTimeAVG = 0;
            successTimeAVG = ((llst.Sum(t => t.SuccessTime)) / llst.Count);
            return successTimeAVG;
        }
        private int GetSenseOfOperation(int RoundTime, List<LevelSuccessTime> llst)
        {
            int AdjustSense = 0;

            foreach (LevelSuccessTime lst in llst)
            {
                if (lst.SuccessTime > (RoundTime / 2))
                { AdjustSense += 1; }
                else
                { AdjustSense -= 1; }
            }
            return AdjustSense;
        }
    }
}
