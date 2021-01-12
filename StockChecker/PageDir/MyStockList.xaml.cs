using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StockChecker.Utility;

namespace StockChecker.PageDir
{
    /// <summary>
    /// MyStockList.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MyStockList : Page
    {
        public MyStockList()
        {
            InitializeComponent();
            SetStockList(0);
        }

        public void SetStockList(int dataSetNum)
        {
            if(dataSetNum < Properties.Settings.Default.StockListNum)
            {

                string strList = Properties.Settings.Default["StockList" + dataSetNum.ToString()].ToString();
                string[] Stocks = strList.Split(',');

                StockCrawlerKR crawlerKR = new StockCrawlerKR();

                dataSet[] datas = new dataSet[Stocks.Length];
                for(int i = 0; i < Stocks.Length; i++)
                {
                    datas[i] = new dataSet();
                    datas[i].name = Stocks[i];
                    datas[i].price = crawlerKR.GetCurPrice(Stocks[i]);
                    datas[i].differ = crawlerKR.GetLastdatRatio(Stocks[i]);
                }

                StockList.ItemsSource = datas;



            }
            else
            {
                //Todo 예외처리
            }
            
            //Properties.Settings.Default.Save();
            //Properties.Settings.Default.Reload();
        }

        private void MyStockListSettingOpen_Click(object sender, RoutedEventArgs e)
        {
            MyStockListSetting settingWindow = new MyStockListSetting();
            settingWindow.Owner = Window.GetWindow(this);
            settingWindow.Show();
        }
    }
    public class dataSet
    {
        public string name { get; set; }
        public string price { get; set; }
        public string differ { get; set; }
    }
}
