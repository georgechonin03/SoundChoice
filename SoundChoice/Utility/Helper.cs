using Microsoft.AspNetCore.Mvc.Rendering;
using SoundChoice.Models;

namespace SoundChoice.Utility
{
    public class Helper
    {
        public static List<SelectListItem> GetGenreDropdown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text=""},
                new SelectListItem {Text="Hiphop"},
                new SelectListItem {Text="Pop"},
                new SelectListItem {Text="Rock"},
                new SelectListItem {Text="Indie"},
                new SelectListItem {Text="Jazz"},
                new SelectListItem {Text="Soul"},
                new SelectListItem {Text="Country"},
                new SelectListItem {Text="Other"}
            };
        }
        public static List<SelectListItem> GetTypeDropDown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem {Text=""},
                new SelectListItem {Text="Effect"},
                new SelectListItem{Text="Loop"},
                new SelectListItem {Text="One shot"},
                new SelectListItem{Text="Sample"}
            };
        }
    }
}
