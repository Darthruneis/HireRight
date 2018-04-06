using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Tests
{
    [TestFixture]
    public class XmlParsingTests
    {
        [Test(ExpectedResult = 96)]
        [Ignore("Out of date")]
        public int TestingXml()
        {
            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\EntityFramework.CodeFirst\test.xml");

            XmlNodeList nodeList = xDoc.GetElementsByTagName("tr");
            var categories = new List<ScaleCategory>();

            for (int i = 0; i < nodeList.Count; i++)
            {
                try
                {
                    if (!nodeList[i].HasChildNodes) continue;
                    if (i > 90) Debugger.Break();

                    var nodes = new string[2];
                    for (int j = 0; j < 2; j++)
                        nodes[j] = nodeList[i].ChildNodes[j].FirstChild.InnerText;

                    string title = nodes[0];
                    string description = nodes[1];

                    categories.Add(new ScaleCategory(title, description));
                }
                // ReSharper disable once RedundantCatchClause
#pragma warning disable 168
                //This catch block is set up this way to assist with debugging.
                catch (System.Exception ex)
                {
                    throw;
                }
#pragma warning restore 168
            }

            return categories.Count;
        }
    }
}