using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopControlApp
{
    class MsgBoxCustom
    {

        public string Article { get; set; }

        public string Description { get; set; }

        public MsgBoxCustom(string description, string article)
        {
            this.Article = article;
            this.Description = description;
        }
    }
}
