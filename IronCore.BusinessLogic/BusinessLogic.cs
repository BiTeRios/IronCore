using IronCore.BusinessLogic.Interfaces;
using IronCore.BusinessLogic.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IronCore.BusinessLogic
{
    public class BusinessLogic
    {
        public ISession GetSessionBl()
        {
            return new SessionBL();
        }
        public IContact GetContactBL()
        {
            return new ContactBL();
        }
    }
}
