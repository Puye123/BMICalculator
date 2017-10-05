using BMICalculator.Models;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.ComponentModel;

namespace BMICalculator.ViewModels
{
    /// <summary>
    /// MainPageのViewModel
    /// </summary>
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        #region プロパティ
        private string _title;
        /// <summary>
        /// タイトル名
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private double _userHeight;
        /// <summary>
        /// 身長
        /// </summary>
        public double UserHeight
        {
            get { return _userHeight; }
            set {
                SetProperty(ref _userHeight, value);
                this.CalcBMICommand.RaiseCanExecuteChanged();
            }
        }

        private double _userWeight;
        /// <summary>
        /// 体重
        /// </summary>
        public double UserWeight
        {
            get { return _userWeight; }
            set {
                SetProperty(ref _userWeight, value);
                this.CalcBMICommand.RaiseCanExecuteChanged();
            }
        }

        private double _userBMI;
        /// <summary>
        /// BMI
        /// </summary>
        public double UserBMI
        {
            get { return _userBMI; }
            set { SetProperty(ref _userBMI, value); }
        }

        private double _userStandardWeight;

        /// <summary>
        /// 適正体重
        /// </summary>
        public double UserStandardWeight
        {
            get { return _userStandardWeight; }
            set { SetProperty(ref _userStandardWeight, value); }
        }
        #endregion

        public DelegateCommand CalcBMICommand { get; }
        private CalculateBMI calculateBMI;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainPageViewModel()
        {
            CalcBMICommand = new DelegateCommand(CalcBMIExecute, CanCalcBMIExecute);
            calculateBMI = new CalculateBMI();
            calculateBMI.PropertyChanged += this.CalcPropertyChanged;
        }

        /// <summary>
        /// CalculateBMIクラスのプロパティの変化があった場合のコールバック関数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalcPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == "BMI")
            {
                this.UserBMI = Math.Round(calculateBMI.BMI, 2, MidpointRounding.AwayFromZero);
            }
            if(e.PropertyName == "StandardWeight")
            {
                this.UserStandardWeight = Math.Round(calculateBMI.StandardWeight, 2, MidpointRounding.AwayFromZero);
            }
        }

        #region BMI計算コマンド
        /// <summary>
        /// 入力された身長、体重が妥当かどうか判定する
        /// </summary>
        /// <returns>計算可能値であればtrue, そうでなければfalse</returns>
        private bool CanCalcBMIExecute()
        {
			if (this.UserHeight < 0.0) return false;
            if (this.UserWeight < 1.0) return false;

            return true;
        }

        /// <summary>
        /// BMIと適正体重を計算する
        /// </summary>
        private void CalcBMIExecute()
        {
            this.calculateBMI.calculateBMI(UserHeight, UserWeight);
        }
#endregion

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("title"))
                Title = (string)parameters["title"] + " and Prism";
        }
    }
}
