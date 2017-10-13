using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace PlanetBuilder.Abstracts
{
    public abstract class AXMLSerializable<T>
    {
        protected string _name;
        /// <summary>
        /// Name of the xml serializable object
        /// </summary>
        public string Name
        {
            get { return this._name; }
            set { _name= value; }
        }
        protected Guid _uuid;
        /// <summary>
        /// GUID of the xml serializable object
        /// </summary>
        public Guid UUID
        {
            get { return this._uuid; }
            set { _uuid = value; }
        }

        /// <summary>
        /// Generates a T object from a T xml file
        /// </summary>
        /// <param name="path">File containing the T</param>
        /// <returns></returns>
        public static T FromFile(string path)
        {
            T body;
            XmlSerializer x = new XmlSerializer(typeof(T));

            using (StreamReader reader = new StreamReader(path))
                body = (T)x.Deserialize(reader);

            return body;
        }

        /// <summary>
        /// Saves the serializable object under its name in xml
        /// </summary>
        /// <param name="path">Saves the serializable object under its name in xml</param>
        public void Save(string path)
        {
            XmlDocument xmlDocument = new XmlDocument();
            // M$ oddity, System.IO.FileNotFoundException is apparently a normal behaviour for runtime Assembly generation
            System.Xml.Serialization.XmlSerializer x = new System.Xml.Serialization.XmlSerializer(this.GetType());
            using (StreamWriter stream = new StreamWriter(path + this.Name + ".xml"))
                x.Serialize(stream, this);
        }

        /// <summary>
        /// Deletes the file associated to this serializable object
        /// </summary>
        /// <returns></returns>
        public bool DeleteFile(string path)
        {
            string filepath = path + this.Name + ".xml";

            if (!File.Exists(filepath))
            {
                FileInfo[] files = null;
                DirectoryInfo dir = null;

                dir = new DirectoryInfo(path);
                files = dir.GetFiles("*.*");
                XmlSerializer x = new XmlSerializer(this.GetType());
                foreach (FileInfo fi in files)
                {
                    using (StreamReader reader = new StreamReader(fi.FullName))
                    {
                        AXMLSerializable<T> temp = (AXMLSerializable<T>)x.Deserialize(reader);
                        if (temp.Name == this.Name)
                        {
                            reader.Close();
                            File.Delete(fi.FullName);
                            return true;
                        }
                    }
                }
                return false;
            }
            File.Delete(filepath);
            return true;
        }
        protected AXMLSerializable(string name)
        {
            _name = name;
            _uuid = Guid.NewGuid();
        }
    }
}
