using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using TitanBot.Models;

namespace TitanBot.Services
{
    public class CsvHandler
    {
        private List<Item> items;

        private List<Item> ReadItems(string path)
        {
            items = new List<Item>();
            using (var reader = new StreamReader(path))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Item>();
                items = records.ToList();
            }

            return items;
        }

        public List<Item> GetItems()
        {
            return items != null ? items : new List<Item>();
        }
    }
}
