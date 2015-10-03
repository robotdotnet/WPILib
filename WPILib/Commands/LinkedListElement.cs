namespace WPILib.Commands
{
    internal class LinkedListElement
    {
        private LinkedListElement next;
        private LinkedListElement previous;
        private Command data;

        public LinkedListElement()
        {
            
        }

        public void SetData(Command newData)
        {
            data = newData;
        }

        public Command GetData()
        {
            return data;
        }

        public LinkedListElement GetNext()
        {
            return next;
        }

        public LinkedListElement GetPrevious()
        {
            return previous;
        }

        public void Add(LinkedListElement l)
        {
            if (next == null)
            {
                next = l;
                next.previous = this;
            }
            else
            {
                next.previous = l;
                l.next = next;
                l.previous = this;
                next = l;
            }
        }

        public LinkedListElement Remove()
        {
            if (previous == null && next == null)
            {
                
            }
            else if (next == null)
            {
                previous.next = null;
            }
            else if (previous == null)
            {
                next.previous = null;
            }
            else
            {
                next.previous = previous;
                previous.next = next;
            }
            LinkedListElement n = next;
            next = null;
            previous = null;
            return n;
        }
    }
}
