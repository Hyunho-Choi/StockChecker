using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;

namespace StockChecker.Utility
{
    class StockCrawlerKR
    {
        public string baseUrl { get; private set; }

        public NewCrawler Crawler;

        public StockCrawlerKR()
        {
            Crawler = new NewCrawler();
            baseUrl = "https://finance.naver.com/item/sise.nhn?code=";
        }

        // 특별한 경우가 아니면 기본 URL사용
        public bool SetUrl(string url)
        {
            baseUrl = url;
            
            if (baseUrl == null)
            {
                MessageBox.Show("BaseUrl이 null입니다.", "에러");
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetCurPrice(string ticker)
        {
            Crawler.ClearUrl();
            if (!Crawler.SetUrl(baseUrl + ticker))
            {
                return "ERROR";
            }
            var result = Crawler.GetValueFromUrl("//strong[@id='_nowVal']");

            return result;
        }

        public string GetLastdatRatio(string ticker)
        {
            Crawler.ClearUrl();
            if (!Crawler.SetUrl(baseUrl + ticker))
            {
                return "ERROR";
            }
            var result = Crawler.GetValueFromUrl("//strong[@id='_diff']");
            result = Regex.Replace(result, @"[^0-9가-힣,]","");
            return result;
        }

        public string GetTodayAll(string ticker)
        {
            Crawler.ClearUrl();
            if (!Crawler.SetUrl(baseUrl + ticker))
            {
                return "ERROR";
            }
            var result = Crawler.GetValueFromUrl("//table[@summary='주요시세 정보에 관한표 입니다.']");
            result = Regex.Replace(result, @"[^0-9A-Za-z가-힣,]", "");
            return result;
        }
    }
}

