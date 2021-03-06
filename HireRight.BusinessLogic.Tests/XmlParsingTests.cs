﻿using NUnit.Framework;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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

            List<ScaleCategory> categories = new List<ScaleCategory>();

            for (int i = 0; i < nodeList.Count; i++)
            {
                try
                {
                    if (!nodeList[i].HasChildNodes) continue;

                    if (i > 90)
                        Debugger.Break();

                    string[] nodes = new string[2];

                    for (int j = 0; j < 2; j++)
                        nodes[j] = nodeList[i].ChildNodes[j].FirstChild.InnerText;

                    string title = nodes[0];
                    string description = nodes[1];

                    categories.Add(new ScaleCategory(title, description));
                }
                catch (System.Exception ex)
                {
                    throw ex;
                }
            }

            return categories.Count;
        }
    }
}