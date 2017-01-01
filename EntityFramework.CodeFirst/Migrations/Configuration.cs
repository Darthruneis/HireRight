using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;
using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace HireRight.EntityFramework.CodeFirst.Migrations
{
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<HireRightDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            CodeGenerator = new BaseMigrationCodeGenerator();
        }

        protected override void Seed(HireRightDbContext context)
        {
            List<Discount> discounts = new List<Discount>
            {
                new Discount(true, 0.20m, 100),
                new Discount(false, 8.00m, 500),
                new Discount(true, 0.30m, 1000),
                new Discount(false, 10.00m, 10000)
            };

            Product product = new Product("Assessment Test", 25.00m, discounts);

            context.Products.AddOrUpdate(product);
            context.SaveChanges();

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"C:\Users\Chris\Documents\GitHubVisualStudio\HireRight\EntityFramework.CodeFirst\test.xml");

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
                catch (System.Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    if (exceptions == null)
                        exceptions = new AggregateException(ex);

                    exceptions = new AggregateException(ex, exceptions);
                }
            }

            if (exceptions != null)
                throw exceptions;

            foreach (ScaleCategory scaleCategory in categories)
            {
                context.Categories.AddOrUpdate(scaleCategory);
            }

            context.SaveChanges();
        }
    }
}