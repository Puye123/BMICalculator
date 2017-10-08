using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMICalculator.Models
{
    /// <summary>
    /// BMIの計算を行うクラス
    /// </summary>
    class CalculateBMI : BindableBase
    {
        private double _bmi;
        /// <summary>
        /// BMI
        /// </summary>
        public double BMI
        { 
            get { return _bmi; }
            set { SetProperty(ref _bmi, value); }
        }

        private double _standardWeight;
        /// <summary>
        /// 適正体重
        /// </summary>
        public double StandardWeight
        {
            get { return _standardWeight; }
            set { SetProperty(ref _standardWeight, value); }
        }

        /// <summary>
        /// BMI及び適正体重を求める
        /// </summary>
        /// <param name="height">身長(cm)</param>
        /// <param name="weight">体重(kg)</param>
        public void calculateBMI(double height, double weight)
        {
            double heightMeter = height / 100; 
            BMI = weight / (heightMeter * heightMeter);
            StandardWeight = heightMeter * heightMeter * 22.0;
        }
    }
}
