using System;
using System.IO;
using System.Xml.Linq;


public class ImagesExtensions
{
    private string Path { get;} = Environment.CurrentDirectory;

    public void CreateXml()
    {
        try
        {

            XDocument xmlDocument = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Creating images extensions"),
                new XElement("extensions",
                    new XElement("extension",
                    new XAttribute("type", "JPEG"),
                    new XElement("name", "JPEG"),
                    new XElement("name", "JPE"),
                    new XElement("name", "JPG"),
                    new XElement("description", "JPEG Files")
                    ),
                new XElement("extension",
                    new XAttribute("type", "PNG"),
                    new XElement("name", "PNG"),
                    new XElement("name", "PNS"),
                    new XElement("description", "PNG Files")
                    ),
             new XElement("extension",
                    new XAttribute("type", "BMP"),
                    new XElement("name", "BMP"),
                    new XElement("name", "RLE"),
                    new XElement("name", "DIB"),
                    new XElement("description", "BMP Files")
                    ),
             new XElement("extension",
                    new XAttribute("type", "EMF"),
                    new XElement("name", "EMF"),
                    new XElement("description", "EMF Files")
                    ),
             new XElement("extension",
                    new XAttribute("type", "EXIF"),
                    new XElement("name", "EXIF"),
                    new XElement("description", "EXIF Files")
                    ),
             new XElement("extension",
                    new XAttribute("type", "TIFF"),
                    new XElement("name", "TIFF"),
                    new XElement("name", "TIF"),
                    new XElement("description", "TIFF Files")
                    ),
             new XElement("extension",
                    new XAttribute("type", "WMF"),
                    new XElement("name", "WMF"),
                    new XElement("description", "WMF Files")
                    ),
              new XElement("extension",
                    new XAttribute("type", "GIF"),
                    new XElement("name", "GIF"),
                    new XElement("description", "GIF Files")
                    ))
                );
            xmlDocument.Save(Path);
        }
        catch (Exception)
        {
            throw;
        }
    }

}

