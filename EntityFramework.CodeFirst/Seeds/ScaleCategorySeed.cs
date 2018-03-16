using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web.Configuration;
using System.Xml;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using Newtonsoft.Json;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    public static class ScaleCategorySeed
    {
        private static readonly string JsonFilePath;
        private static readonly string XmlFilePath;

        static ScaleCategorySeed()
        {
            JsonFilePath = WebConfigurationManager.AppSettings["ScaleCategoriesJsonPath"].ToLower();
            XmlFilePath = WebConfigurationManager.AppSettings["ScaleCategoriesXmlDocPath"].ToLower();
        }
        public static List<ScaleCategory> Seed()
        {
            var json = GetCategoriesJson();
            var categories = JsonConvert.DeserializeObject<List<ScaleCategory>>(json);
            categories = categories.OrderBy(x => x.Title).ToList();
            bool jsonFileOutOfDate = false;
            foreach (ScaleCategory scaleCategory in categories)
                if (scaleCategory.StaticId != (categories.IndexOf(scaleCategory) + 1))
                {
                    scaleCategory.StaticId = categories.IndexOf(scaleCategory) + 1;
                    jsonFileOutOfDate = true;
                }

            if (jsonFileOutOfDate) UpdateJsonFile(categories);

            return categories;
        }

        private static string GetCategoriesJson()
        {
            if (File.Exists(JsonFilePath)) return File.ReadAllText(JsonFilePath);

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(XmlFilePath);

            XmlNodeList nodeList = xDoc.GetElementsByTagName("tr");
            var categories = new List<ScaleCategory>();

            AggregateException exceptions = null;
            foreach (XmlNode node in nodeList)
            {
                try
                {
                    if (!node.HasChildNodes) continue;

                    string[] nodes = new string[2];
                    for (int i = 0; i < 2; i++)
                        nodes[i] = node.ChildNodes[i].FirstChild.InnerText;

                    string title = nodes[0];
                    string description = nodes[1];

                    categories.Add(new ScaleCategory(title, description));
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    if (exceptions == null)
                        exceptions = new AggregateException(ex);

                    exceptions = new AggregateException(ex, exceptions);
                }
            }

            if (exceptions != null)
                throw exceptions;

            categories.Add(new ScaleCategory("Go-Getter Attitude", "The degree to which the individual is dedicated, shows initiative, has a positive demeanor and exhibits independence.  This characteristic is important for jobs requiring independent work and a self-starter attitude. Also applicable for the ever growing home based jobs."));
            categories.Add(new ScaleCategory("Goal Focus", "The degree to which the individual is able to focus on long-term goals regardless of distractions or obstacles that may be encountered."));

            return UpdateJsonFile(categories);
        }

        public static string UpdateJsonFile(List<ScaleCategory> categories)
        {
            string json = JsonConvert.SerializeObject(categories.Select(x => new { x.StaticId, x.Title, x.Description }));
            File.WriteAllText(JsonFilePath, json);
            return json;
        }
    }
}