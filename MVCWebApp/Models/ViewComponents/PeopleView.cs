using Microsoft.AspNetCore.Mvc;
using MVCNewWebApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCNewWebApp.ViewComponents
{
    public class PeopleView:ViewComponent
    {
        private readonly MVCNewWebAppContext _mvccontext;

        public PeopleViewComponent(MVCNewWebAppContext context)
        {
            _mvccontext = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = await _mvccontext.Person.ToListAsync();
            return View(data);
        }
    }
}
