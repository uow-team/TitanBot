using System;
using System.Collections.Generic;
using System.Text;

namespace TitanBot.Models
{
    public class QuoteResponseModel
    {
        public SuccessModel Success { get; set; }
        public ContentModel Contents { get; set; }
    }

    public class SuccessModel
    {
        public int Total { get; set; }
    }

    public class ContentModel
    {
        public List<QuoteModel> Quotes { get; set; }
    }

    public class QuoteModel
    {
        public string Quote { get; set; }
        public string Title { get; set; }
    }
}
