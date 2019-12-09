using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DECCA2
{
    public class Scraper
    {
        private ObservableCollection<EntryModel> _entries = new ObservableCollection<EntryModel>();

        public ObservableCollection<EntryModel> Entries
        {
            get { return _entries; }
            set { _entries = value; }
        }

        public void ScrapeData(string page)
        {
            var web = new HtmlWeb();
            var doc = web.Load(page);

            var Articles = doc.DocumentNode.SelectNodes("//*[@class = 'rc']");

            foreach (var article in Articles)
            {
                var header = HttpUtility.HtmlDecode(article.SelectSingleNode(".//h3[@class = 'LC201b']").InnerText);
                Debug.Print(header);
            }
        }

    }
}
