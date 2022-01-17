using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Net6.Api.DataContext;
using Net6.Api.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Net6.Test
{
    public class InitData
    {
        private Net6Context context;

        public InitData(Net6Context _context)
        {
            context = _context;
        }
        public async Task CreateData()
        {
       
            if (context.Property.ToList().Count == 0)
            {

                var property = context.Property.Add(new Property
                {
                    Id = 1,
                    Title = "Propiedad uno",
                    Address = "Direccion de propiedad uno",
                    Description = "Descripcion propiedad uno",
                    CreatedAt = DateTime.Now,
                    UpdateAt = DateTime.Now,
                    DisabledAt = DateTime.Now,
                    Status = EstatusEnum.ACTIVE
                });

                var activity = context.Activity.Add(new Activity
                {
                    Id = 1,
                    PropertyId = 1,
                    Schedule = DateTime.Now,
                    Title = "Actividad titulo",
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    Status = EstatusEnum.ACTIVE

                });

                await context.SaveChangesAsync();

            }
        }
    }
}
