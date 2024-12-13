using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PoyectoPokedexApi.Pages.PanelAdmin
{
    public class vistaAdminModel : PageModel
    {
        // Definir la propiedad Users
        [BindProperty]
        public List<User> Users { get; set; } // Lista de usuarios

        [BindProperty]
        public User SelectedUser { get; set; } // Usuario seleccionado para editar

        // M�todo que se ejecuta al cargar la p�gina
        public void OnGet()
        {
            // Aqu� deber�as cargar los usuarios desde tu base de datos.
            Users = new List<User>
            {
                new User { Id = 1, Name = "Juan", Email = "juan@example.com" },
                new User { Id = 2, Name = "Maria", Email = "maria@example.com" }
            };
        }

        // M�todo para editar un usuario
        public IActionResult OnPostEditUser()
        {
            // Aqu� deber�as actualizar el usuario en tu base de datos
            // Usar SelectedUser para obtener los datos modificados.
            return RedirectToPage(); // Redirigir a la misma p�gina despu�s de editar.
        }
    }

    // Clase User que representa un usuario
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
