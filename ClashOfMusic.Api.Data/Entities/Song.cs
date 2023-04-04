using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClashOfMusic.Api.Data.Entities
{
    public class Song
    {
        public string Title { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string YouTube_Link { get; set; }

        public ICollection<PlayListsSongs> PlayListsSongs { get; set; }
    }
}
