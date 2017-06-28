using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Web.Configuration;
using System.Xml;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.EntityFramework.CodeFirst.Seeds
{
    internal static class ScaleCategorySeed
    {
        internal static List<ScaleCategory> Seed()
        {
            XmlDocument xDoc = new XmlDocument();
            var pathFromConfig = WebConfigurationManager.AppSettings["ScaleCategoriesXmlDocPath"];
            xDoc.Load(pathFromConfig);

            XmlNodeList nodeList = xDoc.GetElementsByTagName("tr");

            List<ScaleCategory> categories = new List<ScaleCategory>();

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

            return categories;
        }
    }
}