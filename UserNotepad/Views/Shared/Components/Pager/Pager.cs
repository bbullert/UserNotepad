using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotepad.Extensions.List;

namespace UserNotepad.Views.Shared.Components.Pager
{
    public class Pager : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedList items)
        {
            return View("Pager", items);
        }
    }
}
