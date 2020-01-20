using AplicacionNetCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AplicacionNetCore.Data
{
    public class DbInitializer
    {
        public static void Initialize(AplicacionNetCoreContext aplicacionNetCoreContext)
        {
            aplicacionNetCoreContext.Database.EnsureCreated();

            //Registrar datos de prueba
            if (aplicacionNetCoreContext.Contacto.Any())
            {
                return;
            }

            var Contactos = new Contacto[]{
                new Contacto{ Nombre = "Aldair", Direccion = "Santa Anita", Email = "aldair.rc70@gmail.com", Telefono = "987079031", Verificado = true },
                new Contacto{ Nombre = "Sendy", Direccion = "Callao", Email = "nose@nose.com", Telefono = "941285021", Verificado = true }
            };

            foreach (Contacto oContacto in Contactos)
            {
                aplicacionNetCoreContext.Contacto.Add(oContacto);
            }

            aplicacionNetCoreContext.SaveChanges();
        }
    }
}