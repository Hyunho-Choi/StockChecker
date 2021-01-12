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
using StockChecker.PageDir;
using System.Collections.Concurrent;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Runtime.InteropServices;


namespace StockChecker
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ContentFrame.Background = Brushes.Red;

            ContentFrame.Navigate(new MyStockList());
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void SetData()
        {
            Excel.Application excelApp = null; 
            Excel.Workbook workBook = null; 
            Excel.Worksheet workSheet = null;

            try
            {
                string path = "Excel.xlsx"; // 엑셀 파일 저장 경로 

                excelApp = new Excel.Application(); // 엑셀 어플리케이션 생성 
                workBook = excelApp.Workbooks.Open(path); // 워크북 열기 
                workSheet = workBook.Worksheets.get_Item(1) as Excel.Worksheet; // 엑셀 첫번째 워크시트 가져오기 
                Excel.Range range = workSheet.UsedRange; // 사용중인 셀 범위를 가져오기 

                for (int row = 1; row <= range.Rows.Count; row++) // 가져온 행 만큼 반복 
                { 
                    for (int column = 1; column <= range.Columns.Count; column++) // 가져온 열 만큼 반복 
                    { 
                        string str = (string)(range.Cells[row, column] as Excel.Range).Value2; // 셀 데이터 가져옴
                       
                        Console.Write(str + " "); 
                    } 
                    Console.WriteLine(); 
                } 
                workBook.Close(true); // 워크북 닫기 
                excelApp.Quit(); // 엑셀 어플리케이션 종료 
            } 
            finally 
            {
                ReleaseObject(workSheet); 
                ReleaseObject(workBook); 
                ReleaseObject(excelApp); 
            }




            ConcurrentDictionary<string, string> Kospis = new ConcurrentDictionary<string, string>();
            
            //Kospis.TryAdd()
        }
        static void ReleaseObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj); // 액셀 객체 해제 
                    obj = null; 
                } 
            } 
            catch (Exception ex) 
            { 
                obj = null; throw ex;
            } 
            finally 
            { 
                GC.Collect(); // 가비지 수집 
            } 
        }
    }
}
