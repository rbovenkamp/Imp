using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class SchetsHistorie : IEnumerable<ISchetsObject>
    {
        private Stack<ISchetsObject> historie;
        private Stack<ISchetsObject> toekomst;

        public event EventHandler onObjectToegevoegd;

        public int Count
        {
            get
            {
                return historie.Count;
            }
        }

        public SchetsHistorie()
        {
            historie = new Stack<ISchetsObject>();
            toekomst = new Stack<ISchetsObject>();
        }

        public SchetsHistorie(string geserialiseerdeHistorie)
        {
            historie = new Stack<ISchetsObject>();
            toekomst = new Stack<ISchetsObject>();

            StringReader reader = new StringReader(geserialiseerdeHistorie);
            string line;
            while (!String.IsNullOrEmpty(line = reader.ReadLine()))
            {
                string[] typeAndValue = line.Split('=');
                ISchetsObject so = null;
                switch (typeAndValue[0])
                {
                    case "PenObject":
                        so = PenObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "PlaatjeObject":
                        so = PlaatjeObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "LijnObject":
                        so = PenObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "RechthoekObject":
                        so = PenObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "GumObject":
                        so = GumObject.VanSerialisatie(typeAndValue[1]);
                        break;
                }
                this.Push(so);
            }
        }

        public string Serialiseer()
        {
            StringBuilder s = new StringBuilder();
            foreach (ISchetsObject schetsObject in this)
            {
                s.Insert(0, schetsObject.GetType().Name + "=" + schetsObject.Serialiseer() + Environment.NewLine);
            }
            return s.ToString();
        }

        public void Push(ISchetsObject schetsObject)
        {
            historie.Push(schetsObject);
            onObjectToegevoegd?.Invoke(schetsObject, new EventArgs());
        }

        public ISchetsObject Undo()
        {
            if (historie.Peek() == null)
            {
                return null;
            }
            else
            {
                ISchetsObject schetsObject = historie.Pop();
                toekomst.Push(schetsObject);
                return schetsObject;
            }
        }

        public ISchetsObject Redo()
        {
            if (toekomst.Peek() == null)
            {
                return null;
            }
            else
            {
                ISchetsObject schetsObject = toekomst.Pop();
                this.Push(schetsObject);
                return schetsObject;
            }
        }

        public ISchetsObject this[int i]
        {
            get
            {
                return historie.ToArray()[i];
            }
        }

        public IEnumerator<ISchetsObject> GetEnumerator()
        {
            return historie.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
