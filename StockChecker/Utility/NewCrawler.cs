using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using agi = HtmlAgilityPack;
using System.Net;
using System.Web;


namespace StockChecker.Utility
{
	class NewCrawler
	{

		public agi.HtmlDocument htmlDoc { get; private set; }

		public NewCrawler()
        {
			htmlDoc = null;
		}

		// 시간 제한관련 코드 추가

		public bool SetUrl(string url)
        {
			if (htmlDoc != null)
            {
				MessageBox.Show("크롤러에 이미 할당된 URL이 존재합니다. 삭제 후 시도해주세요", "에러");
				return false;
            }				

			WebClient wc = new WebClient();
			agi.HtmlWeb web = new agi.HtmlWeb();
			htmlDoc = web.Load(url);

			if (htmlDoc == null)
            {
				MessageBox.Show("크롤러에 URL할당이 실패하였습니다.", "에러");
				return false;
			}
			else
            {
				return true;
			}
		}

		public bool ClearUrl()
        {
			htmlDoc = null;
			if (htmlDoc != null)
				return false;
			else
				return true;

		}

		public string GetValueFromUrl(string tagID)
        {
			var mainNode = htmlDoc.DocumentNode.SelectSingleNode(tagID);
			
			if(mainNode.InnerText != null)
            {
				return mainNode.InnerText;
			}
            else
            {
				MessageBox.Show("값을 불러오지 못하였습니다.", "에러");
				return "ERROR";
			}
		}


		public void Test3()
		{
			WebClient wc = new WebClient();
			string url = "https://finance.naver.com/item/sise.nhn?code=005930";
			agi.HtmlWeb web = new agi.HtmlWeb();
			agi.HtmlDocument htmlDoc = web.Load(url);

			var mainNode = htmlDoc.DocumentNode.SelectSingleNode("//strong[@id='_nowVal']");
			Console.WriteLine(mainNode.InnerText);
		}
	}
}
