using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SpaPlum.Web.Service;

namespace SpaPlum.Web.Controllers
{
    public class BackendController : SpaPlumBaseController
    {
        //
        // GET: /Scheduler/

        public ActionResult Day()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Week()
        {
            return new Dpc().CallBack(this);
        }

        public ActionResult Month()
        {
            return new Dpc().CallBack(this);
        }

    }
}