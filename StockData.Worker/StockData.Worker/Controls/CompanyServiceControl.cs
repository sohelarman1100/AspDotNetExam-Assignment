using HtmlAgilityPack;
using StockExchange.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExchange.Controls
{
    public class CompanyServiceControl : ICompanyServiceControl
    {
        private ICreateCompanyModel _company;

        public CompanyServiceControl(ICreateCompanyModel company)
        {
            _company = company;
        }
        public List<string> snglLst = new List<string>();
        public void SetVal()
        {
            var TotCompany = _company.NumOfCompany();

            //Console.WriteLine("total = {0}", TotCompany);

            if (TotCompany == 0)
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument document = web.Load("https://www.dse.com.bd/latest_share_price_scroll_l.php");
                var nodes = document.DocumentNode.SelectNodes("//div[contains(@class, 'table-responsive inner-scroll')]" +
                    "//table[contains(@class, 'table table-bordered background-white shares-table fixedHeader')]//td").ToList();

                var Status = nodes.Last().SelectSingleNode("//span/span").InnerText;
                
                if (Status == "Open")
                {
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
            }
        }
        public void createObj()
        {
            if (snglLst.Count > 0)
            {
                _company.TradeCode = snglLst[0];
            }

            //Console.WriteLine("hello from serviceControl");

            _company.CreateCompany();

            snglLst.Clear();
        }
    }
}
