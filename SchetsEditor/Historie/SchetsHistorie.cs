using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SchetsEditor.Historie
{
    public class SchetsHistorie : IEnumerable<ISchetsObject>
    {
        private Stack<ISchetsObject> historie;
        private Stack<ISchetsObject> toekomst;

        public SchetsHistorie()
        {
            historie = new Stack<ISchetsObject>();
            toekomst = new Stack<ISchetsObject>();
        }

        public SchetsHistorie(string geserialiseerdeHistorie)
        {
            historie = new Stack<ISchetsObject>();
            toekomst = new Stack<ISchetsObject>();
        }

        public string Serialiseer()
        {
            StringBuilder s = new StringBuilder();
            foreach (ISchetsObject schetsObject in this)
            {
                s.AppendLine(schetsObject.GetType().Name + "=" + schetsObject.Serialiseer());
            }
            return s.ToString();
        }

        public void Push(ISchetsObject schetsObject)
        {
            historie.Push(schetsObject);
        }

        public ISchetsObject Undo()
        {
            ISchetsObject schetsObject = historie.Pop();
            toekomst.Push(schetsObject);
            return schetsObject;
        }

        public ISchetsObject Redo()
        {
            ISchetsObject schetsObject = toekomst.Pop();
            this.Push(schetsObject);
            return schetsObject;
        }

        public IEnumerator<ISchetsObject> GetEnumerator()
        {
            return new Stack<ISchetsObject>(historie.ToArray()).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
