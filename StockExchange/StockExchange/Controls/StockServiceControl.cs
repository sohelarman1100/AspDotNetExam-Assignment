using Autofac;
using HtmlAgilityPack;
using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Controls
{
    public class StockServiceControl : IStockServiceControl
    {
        private ICreateStockPriceModel _stockObj; 
        
        public StockServiceControl(ICreateStockPriceModel stockObj)
        {
            _stockObj = stockObj;
        }
        public List<string> snglLst = new List<string>();
        public void SetVal()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load("https://www.dse.com.bd/latest_share_price_scroll_l.php");
            var nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'table-responsive inner-scroll')]" +
                "//table[contains(@class, 'table table-bordered background-white shares-table fixedHeader')]//td").ToList();

            var lst = new List<string>();

            foreach (HtmlNode item in nodes)
                lst.Add(Convert.ToString(item.InnerText));
            //Console.WriteLine("len = {0}", lst.Count);

            for (int i = 0; i < lst.Count; i++)
            {

                if (i % 11 == 1)
                {
                    string data = "";
                    for (int j = 0; j < lst[i].Length; j++)
                    {
                        if ((lst[i][j] >= 'A' && lst[i][j] <= 'Z') || (lst[i][j] >= 48 && lst[i][j] <= 57))
                            data = data + lst[i][j];
                    }
                    snglLst.Add(data);
                }

                else if (i % 11 != 0)
                {
                    snglLst.Add(lst[i]);
                    if (i == 4179)
                        createObj();
                }

                else if (i != 0 && i % 11 == 0)
                    createObj();
            }

            //for testing purpose
            //createObj();
        }
        public void createObj()
        {
            if (snglLst.Count > 0)
            {
                _stockObj.TradeCode = snglLst[0];
                _stockObj.LastTradingPrice = (snglLst[1]);
                _stockObj.High = (snglLst[2]);
                _stockObj.Low = (snglLst[3]);
                _stockObj.ClosePrice = (snglLst[4]);
                _stockObj.YesterdayClosePrice = (snglLst[5]);
                _stockObj.Change = (snglLst[6]);
                _stockObj.Trade = (snglLst[7]);
                _stockObj.Value = (snglLst[8]);
                _stockObj.Volume = (snglLst[9]);
            }

            //Console.WriteLine("hello from serviceControl");

            _stockObj.CreateStockPrice();

            snglLst.Clear();
        }
    }
}
