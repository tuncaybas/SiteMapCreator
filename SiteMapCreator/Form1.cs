using HtmlAgilityPack;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace SiteMapCreator
{
    public partial class Form1 : Form
    {
        public Regex regUrl = new Regex(@"(((ht)tp(s?))\://)?(www.|[a-zA-Z].)[a-zA-Z0-9\-\.]+\.([a-z]{2,5})(\:[0-9]+)*(/($|[a-zA-Z0-9\.\,\;\?\'\\\+&amp;%\$#\=~_\-]+))");
        public string AppDir = Directory.GetParent(Application.ExecutablePath).ToString() + "\\";
        public string url { get; set; }
        public static Queue<Uri> queue;
        public static ArrayList allSiteUrls;
        public Form1()
        {
            InitializeComponent();
            Form1.CheckForIllegalCrossThreadCalls = false;
        }

        private void btnBasla_Click(object sender, EventArgs e)
        {
            url = txtUrl.Text;
            if (String.IsNullOrEmpty(url))
            {
                MessageBox.Show("Link girmelisiniz.");
                txtUrl.Focus();
            }else
            {
                var match = regUrl.Match(url);
                if (match.Success)
                {

                    Task.Factory.StartNew(delegate
                    {
                        linkTopla();
                    });
                }
                else
                {
                    MessageBox.Show("Geçersiz URL adresi");
                    txtUrl.Focus();
                }
            }
        }

        private void linkTopla()
        {
            listUrl.Items.Clear();
            btnBasla.Enabled = false;
            btnBasla.Text = "Bekleyiniz.";
            LinkleriToplaBakalim(url);
        }

        private void LinkleriToplaBakalim(string p)
        {
            string result = string.Empty;
            Uri u = new Uri(p);
            queue = new Queue<Uri>();
            allSiteUrls = new ArrayList();

            queue.Enqueue(u);
            allSiteUrls.Add(u);
            string anaDomain = anaDomaingetir(u);
            lblLog.Text = u.ToString();
            while (queue.Count > 0)
            {

                try
                {
                    Uri url = queue.Dequeue();
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; rv:31.0) Gecko/20100101 Firefox/31.0";
                    request.Method = "GET";

                    using (var stream = request.GetResponse().GetResponseStream())
                    using (var reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        result = reader.ReadToEnd();
                    }

                    HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                    doc.Load(new StringReader(result));

                    foreach (HtmlNode link in doc.DocumentNode.SelectNodes(@"//a[@href]"))
                    {
                        if (link != null)
                        {
                            HtmlAttribute att = link.Attributes["href"];
                            if (att == null) continue;
                            string href = att.Value;
                            if (href.StartsWith("javascript", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.StartsWith("document", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.StartsWith("mailto", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.StartsWith("ftp", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.EndsWith(".png", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.EndsWith(".jpg", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.EndsWith(".rar", StringComparison.InvariantCultureIgnoreCase)) continue;
                            if (href.EndsWith(".jpeg", StringComparison.InvariantCultureIgnoreCase)) continue;
                            //if (!href.StartsWith(p, StringComparison.InvariantCultureIgnoreCase)) continue;
                            Uri urlNext = new Uri(href, UriKind.RelativeOrAbsolute);

                            // Make it absolute if it's relative
                            if (!urlNext.IsAbsoluteUri)
                            {
                                urlNext = new Uri(u, urlNext);
                            }

                            if (!urlNext.ToString().Contains(txtUrl.Text)) continue;


                            if (!allSiteUrls.Contains(urlNext))
                            {
                                allSiteUrls.Add(urlNext);               // keep track of every page we've handed off

                                if (u.IsBaseOf(urlNext))
                                {
                                    queue.Enqueue(urlNext);
                                }

                                ListViewItem item = new ListViewItem();
                                item.Text = (listUrl.Items.Count + 1).ToString();
                                item.SubItems.Add(urlNext.ToString());
                                listUrl.Items.Add(item);
                                listUrl.EnsureVisible(listUrl.Items.Count - 1);
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show(ex.Message);
                    StreamWriter sw = new StreamWriter("error.log", true);
                    sw.WriteLine(ex.Message);
                    sw.Close();
                    continue;
                }
            }

            lblLog.Text = "SiteMap Oluşturuluyor";
            if (GenerateXML())
            {
                btnBasla.Enabled = true;
                btnBasla.Text = "Başla";
            }
        }

        private bool GenerateXML()
        {
            bool sonuc = false;
            try
            {
                string anaDomain = anaDomaingetir(new Uri(txtUrl.Text));
                string fileName = anaDomain+"_sitemap.xml";

                string LAST_MODIFY = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                string CHANGE_FREQ = "daily";
                string TOP_PRIORITY = "0.5";


                using (XmlTextWriter writer = new XmlTextWriter(AppDir+fileName, Encoding.UTF8))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("urlset");
                    writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
                    for(int i = 0; i < listUrl.Items.Count; i++)
                    {
                        string u = listUrl.Items[i].SubItems[1].Text;
                        writer.WriteStartElement("url");
                        writer.WriteElementString("loc", u);
                        writer.WriteElementString("lastmod", LAST_MODIFY);
                        writer.WriteElementString("changefreq", CHANGE_FREQ);
                        writer.WriteElementString("priority", TOP_PRIORITY);
                        writer.WriteEndElement();
                    }
                }

                lblLog.Text=fileName+" oluşturuldu.";
                sonuc = true;

            }
            catch (Exception ex)
            {
                StreamWriter sw = new StreamWriter("error.log", true);
                sw.WriteLine(ex.Message);
                sw.Close();
            }
            return sonuc;
        }

        private string anaDomaingetir(Uri u)
        {
            // string pathQuery = u.PathAndQuery;
            // string hostName = u.ToString().Replace(pathQuery, "");
            string uriStr = u.ToString();
            if (!uriStr.Contains(Uri.SchemeDelimiter))
            {
                uriStr = string.Concat(Uri.UriSchemeHttp, Uri.SchemeDelimiter, uriStr);
            }
            Uri uri = new Uri(uriStr);
            string hostName = uri.Host;
            //string tld = uri.GetLeftPart(UriPartial.Authority);
            return hostName;
        }
    }
}
