using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace net_il_mio_fotoalbum.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "inserisci un email.")]
        [EmailAddress(ErrorMessage = "email non corretta.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "inserisci un messaggio.")]
        [Column(TypeName = "text")]
        public string MessageText { get; set; } = string.Empty;
    }
}
