using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Domain.MovieDomain;

namespace WebApi.Domain.UserDomain
{
    public class TitleBookmark
    {
        public string Username { get; set; }
        
        [ForeignKey("TitleBasics")]
        public string TitleId { get; set; }
        
        public TitleBasics TitleBasics { get; set; }
    }
}