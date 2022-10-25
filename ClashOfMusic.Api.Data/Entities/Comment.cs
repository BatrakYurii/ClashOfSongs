using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserAuthorId { get; set; }
        public User UserAuthor { get; set; }
        public int PlayListId { get; set; }
        public PlayList? PlayList { get; set; }
        public string? UserRecipientId { get; set; }
        public User? UserRecipient { get; set; }
        public RecipientEnum RecipientType {get;set;}

    }
}
