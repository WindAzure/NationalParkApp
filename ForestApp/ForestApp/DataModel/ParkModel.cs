using ForestApp.Common;
using MyUserControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;

namespace ForestApp.DataModel
{
    public class ParkModel :BindableBase, ItemMember
    {
        private ObservableCollection<String> _imgPath = null;
        public ObservableCollection<String> ImgPath 
        {
            set
            {
                SetProperty(ref this._imgPath, value, "ImgPath");
            }
            get
            {
                return _imgPath;
            }
        }

        private String _title;
        public String Title 
        {
            set
            {
                SetProperty(ref this._title, value, "Title");
            }
            get
            {
                return _title;
            }
        }

        private String _info;
        public String Info 
        {
            set
            {
                SetProperty(ref this._info, value, "Info");
            }
            get
            {
                return _info;
            }
        }

    }

    public class ParkSource : BindableBase
    {
        private ObservableCollection<Object> _parks = null;
        public ObservableCollection<Object> Parks
        {
            set
            {
                SetProperty(ref this._parks, value, "Parks");
            }
            get
            {
                return _parks;
            }
        }

        public ParkSource()
        {
            Parks = new ObservableCollection<Object>();
        }

        public async Task LoadDataSource()
        {
            XmlDocument xmlDoc = await XmlDocument.LoadFromUriAsync(new Uri("https://mail.kkbox.com.tw/~azureliao/source.xml"));
            XmlNodeList nodelist = xmlDoc.SelectNodes("//Item");
            for (int i = 0; i < nodelist.Count(); i++)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(nodelist[i].GetXml());

                ObservableCollection<String> img = new ObservableCollection<String>();
                img.Add(doc.SelectSingleNode("//Img1").FirstChild.NodeValue.ToString());
                img.Add(doc.SelectSingleNode("//Img2").FirstChild.NodeValue.ToString());
                img.Add(doc.SelectSingleNode("//Img3").FirstChild.NodeValue.ToString());

                ParkModel singleItem = new ParkModel()
                {
                    Title = doc.SelectSingleNode("//Title").FirstChild.NodeValue.ToString(),
                    Info = doc.SelectSingleNode("//Description").FirstChild.NodeValue.ToString(),
                    ImgPath = img
                };

                Parks.Add(singleItem);
            }

        }
    }
}
