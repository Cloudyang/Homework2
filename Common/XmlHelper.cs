using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Common
{
    /// <summary>
    /// XML文件系列化与反系列化
    /// </summary>
    public class XmlHelper
    {
        #region Xml与实体对象之间转换
        /// <summary>   
        /// 实体转化为XML   
        /// </summary>   
        public static string ParseToXml<T>(T model, string fatherNodeName)
        {
            var xmldoc = new XmlDocument();
            var modelNode = xmldoc.CreateElement(fatherNodeName);
            xmldoc.AppendChild(modelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    var attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    //else
                    //    attribute.InnerText = "[Null]";
                    modelNode.AppendChild(attribute);
                }
            }
            return xmldoc.OuterXml;
        }

        /// <summary>   
        /// XML转换为实体,默认 fatherNodeName="body"
        /// </summary> 
        public static T ParseToModel<T>(string xml, string fatherNodeName = "body") where T : class, new()
        {
            T model = new T();
            if (string.IsNullOrEmpty(xml))
                return default(T);
            var xmldoc = new XmlDocument();
            xmldoc.LoadXml(xml);

            var attributes = xmldoc.SelectSingleNode(fatherNodeName).ChildNodes;
            foreach (XmlNode node in attributes)
            {
                foreach (var property in model.GetType().GetProperties().Where(property => node.Name == property.Name))
                {
                    if (!string.IsNullOrEmpty(node.InnerText))
                    {
                        property.SetValue(model,
                                          property.PropertyType == typeof(Guid)
                                              ? new Guid(node.InnerText)
                                              : Convert.ChangeType(node.InnerText, property.PropertyType), null);
                    }
                    else
                        property.SetValue(model, null, null);
                }
            }
            return model;
        }

        #endregion


        #region XML系列化与反系列化
        /// <summary>
        /// 系列化，将数据转换并保存为XML文件
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="path">确定文件夹已经存在</param>
        /// <param name="sourceObj">要系列化的数据源</param>
        public static void ToXml<T>(string path, T sourceObj) where T : class, new()
        {
            var filePath = Path.Combine(path, $"{typeof(T).Name}.xml");
            if (!string.IsNullOrWhiteSpace(filePath) && sourceObj != null)
            {
                //  Type type = sourceObj.GetType();
                Type type = typeof(T);
                using (Stream sm = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    using (StreamWriter writer = new StreamWriter(sm))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(type);
                        xmlSerializer.Serialize(writer, sourceObj);
                    }
                }
            }
        }

        /// <summary>
        /// 反系列化，读取XML文件并返回指定类型的数据，为空或错误时返回null
        /// </summary>
        /// <typeparam name="T">返回数据类型</typeparam>
        /// <param name="path">xml文件地址，需要转换成物理路径</param>
        /// <returns></returns>
        public static T FromXml<T>(string path) where T : class, new()
        {
            object result = null;
            try
            {
                var filePath = Path.Combine(path, $"{typeof(T).Name}.xml");
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                        result = xmlSerializer.Deserialize(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return result == null ? null : (T)result;
        }
        #endregion
    }
}
