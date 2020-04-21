using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

public abstract class Extensions
{
    protected abstract string Path { get; }
    protected abstract XDocument XmlLoad { get; set; }

    private string FileName
    {
        get
        {
            return Path.Split('\\').Last();
        }
    }

    public abstract void CreateXml();

    public string GetExtensionType(string extension)
    {
        try
        {

            var type = from name in XmlLoad.Descendants("name")
                       where name.Value.ToUpper() == extension.ToUpper()
                       select name.Parent.Attribute("type").Value;
            return type.FirstOrDefault().ToUpper();
        }
        catch (Exception ex)
        {
            throw new ExtensionsException($"Something went wrong with {FileName}", ex);
        }
    }

    public List<XAttribute> GetExtensionTypes()
    {
        try
        {
            var types = from name in XmlLoad.Descendants("extension")
                        select name.Attribute("type");
            return types.ToList();
        }
        catch (Exception ex)
        {
            throw new ExtensionsException($"Something went wrong with {FileName}", ex);
        }
    }

    public List<XElement> GetAllExtensions()
    {
        try
        {

            var extensions = from name in XmlLoad.Descendants("name")
                             select name;

            return extensions.ToList();
        }
        catch (Exception ex)
        {
            throw new ExtensionsException($"Something went wrong with {FileName}", ex);
        }
    }
    public List<XElement> GetAllExtensions(string type)
    {
        try
        {
            var extensions = from extension in XmlLoad.Descendants("extension")
                             where extension.Attribute("type").Value == type
                             from name in extension.Elements("name")
                             select name;

            return extensions.ToList();
        }
        catch (Exception ex)
        {
            throw new ExtensionsException($"Something went wrong with {FileName}", ex);
        }
    }

    public string GetDescription(string type)
    {
        try
        {
            var extensions = from extension in XmlLoad.Descendants("extension")
                             where extension.Attribute("type").Value == type
                             select extension.Element("description");

            return extensions.FirstOrDefault().Value;
        }
        catch (Exception ex)
        {
            throw new ExtensionsException($"Something went wrong with {FileName}", ex);
        }
    }
}