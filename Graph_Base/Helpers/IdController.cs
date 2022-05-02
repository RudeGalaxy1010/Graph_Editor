using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph_Base
{
    public class IdController
    {
        private List<int> _ids;

        public IdController()
        {
            _ids = new List<int>();
        }

        public int GetId()
        {
            for (int i = 0; i < _ids.Count + 1; i++)
            {
                int id = i + 1;
                if (_ids.Contains(id) == false)
                {
                    _ids.Add(id);
                    return id;
                }
            }

            return -1;
        }

        public bool TryRemoveId(int id)
        {
            if (_ids.Contains(id))
            {
                _ids.Remove(id);
                return true;
            }

            return false;
        }
    }
}
