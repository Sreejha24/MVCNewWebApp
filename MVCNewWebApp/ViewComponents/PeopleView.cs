using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public PeopleView(MVCNewWebAppContext context)
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
