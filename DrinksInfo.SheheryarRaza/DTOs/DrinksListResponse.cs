using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DrinksInfo.SheheryarRaza.Models;

namespace DrinksInfo.SheheryarRaza.DTOs
{
    public class DrinksListResponse
    {
        public List<DrinkSimple>? Drinks { get; set; }
    }
}
