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
            int adjustSecond = 0;
            int successTimeAVG = 0;
            int adjustSense = 0;

            adjustSense = GetSenseOfOperation(RoundTime,llst);
            successTimeAVG = CalculateAverage(llst);
            adjustSecond = AdjustSecond(llst);

            if (adjustSense > 0)
            { roundTimeDGB = RoundTime + adjustSecond; }
            else
            { roundTimeDGB = RoundTime - adjustSecond; }

            return roundTimeDGB;
        }
        public int CalculateAverageRound(int value, int div)
        {
            return value / div;
        }

        private int AdjustSecond(List<LevelSuccessTime> llst)
        {
            ///return CalculateTypicalDesv(llst) >= 1 ? 1 : 0;
            return (int) CalculateTypicalDesv(llst);
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
                if (lst.SuccessTime > (RoundTime/2))
                { AdjustSense += 1; }
                else
                { AdjustSense -= 1; }
            }
            return AdjustSense;
        }
        
    }
}
