using System.ComponentModel.DataAnnotations;

namespace Projeto_Integrador_III.InputModel
{
    public class ContaInputModel
    {
        [Required]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome de usuario deve conter de 3 e 100 caracteres")]
        public string Usuario { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "A senha deve conter de 6 e 20 caracteres")]
        public string Senha { get; set; }

        [Required(ErrorMessage = "Campo não pode ser vázio")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail invalido")]
        public string Email { get; set; }

    }
}
