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

        public event EventHandler onVeranderd;

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
                    case "VolOvaalObject":
                        so = VolOvaalObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "VolRechthoekObject":
                        so = VolRechthoekObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "TekstObject":
                        so = TekstObject.VanSerialisatie(typeAndValue[1]);
                        break;
                    case "OvaalObject":
                        so = OvaalObject.VanSerialisatie(typeAndValue[1]);
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

        public void Push(ISchetsObject schetsObject, bool clearToekomst = true)
        {
            historie.Push(schetsObject);
            if (clearToekomst)
            {
                toekomst.Clear();
            }
            onVeranderd?.Invoke(schetsObject, new EventArgs());
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
                onVeranderd?.Invoke(schetsObject, new EventArgs());
                return schetsObject;
            }
        }

        public ISchetsObject BACK_TO_THE_FUTURE()
        {
            if (toekomst.Peek() == null)
            {
                return null;
            }
            else
            {
                ISchetsObject schetsObject = toekomst.Pop();
                this.Push(schetsObject, false);
                return schetsObject;
            }
        }

        public ISchetsObject Peek()
        {
            if (historie.Count == 0)
            {
                return null;
            }
            else
            {
                return historie.Peek();
            }
        }

        public ISchetsObject PeekToekomst()
        {
            if (toekomst.Count == 0)
            {
                return null;
            }
            else
            {
                return toekomst.Peek();
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
