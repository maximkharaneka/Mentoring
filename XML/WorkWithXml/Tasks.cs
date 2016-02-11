using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace WorkWithXml
{
    [TestClass]
    public class Tasks
    {
        private XmlReaderSettings settings;

        [TestInitialize]
        public void Init()
        {
            settings = new XmlReaderSettings();

            settings.Schemas.Add("http://library.by/catalog", "../../books.xsd");
            settings.ValidationEventHandler +=
                delegate (object sender, ValidationEventArgs e)
                {
                    Console.WriteLine(e.Message);
                };

            settings.ValidationFlags = settings.ValidationFlags | XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationType = ValidationType.Schema;
        }

        [TestMethod]
        public void CheckValidXML()
        {
            XmlReader reader = XmlReader.Create("../../books.xml", settings);

            while (reader.Read()) {}
        }

        [TestMethod]
        public void CheckInvalidXML()
        {
            XmlReader reader = XmlReader.Create("../../booksWithErrors.xml", settings);

            while (reader.Read()) { }
        }

        [TestMethod]
        public void GenerateRss()
        {
            var xsl = new XslCompiledTransform();
            var settings = new XsltSettings { EnableScript = true };
            xsl.Load("../../bookrss.xslt", settings, null);

            xsl.Transform("../../books.xml", "../../resultrss.xml");
        }

        [TestMethod]
        public void XMLToHtml()
        {
            var xsl = new XslCompiledTransform();
            var settings = new XsltSettings { EnableScript = true };
            xsl.Load("../../bookhtml.xslt", settings, null);

            xsl.Transform("../../books.xml", "../../result.html");
        }
    }
}
